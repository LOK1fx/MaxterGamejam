using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace LOK1game.Tools.Networking
{
    public class Client : MonoBehaviour
    {
        public static Client Instance;
        public static int DataBufferSize = 4096;

        public string Ip = "127.0.0.1"; //Local ip
        public int Port = 9600;
        public int LocalId = 0;
        public TCP Tcp;

        private delegate void PacketHandler(Packet packet);
        private static Dictionary<int, PacketHandler> _packetHandlers;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if(Instance != this)
            {
                Debug.LogError("There are more then one instance of the client!");

                Destroy(this);
            }
        }

        private void Start()
        {
            Tcp = new TCP();
        }

        private void Update() //test
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                ConnectToServer();
            }
        }

        public void ConnectToServer()
        {
            InitializeClientData();
            Tcp.Connect();
        }

        public class TCP
        {
            public TcpClient Socket;

            private NetworkStream _stream;
            private Packet _receivedData;
            private byte[] _receiveBuffer;

            public void Connect()
            {
                Socket = new TcpClient
                {
                    ReceiveBufferSize = DataBufferSize,
                    SendBufferSize = DataBufferSize
                };

                _receiveBuffer = new byte[DataBufferSize];
                Socket.BeginConnect(Instance.Ip, Instance.Port, OnConnectCallback, Socket);
            }

            public void SendData(Packet packet)
            {
                try
                {
                    if(Socket != null)
                    {
                        _stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                    }
                }
                catch (Exception exception)
                {
                    Debug.Log($"Error sending data to server: {exception}");
                }
            }

            private void OnConnectCallback(IAsyncResult result)
            {
                Socket.EndConnect(result);

                if(!Socket.Connected)
                {
                    return;
                }

                _stream = Socket.GetStream();
                _receivedData = new Packet();
                _stream.BeginRead(_receiveBuffer, 0, DataBufferSize, ReceiveCallback, null);
            }

            private void ReceiveCallback(IAsyncResult result)
            {
                try
                {
                    var byteLength = _stream.EndRead(result);

                    if (byteLength <= 0)
                    {
                        return;
                    }

                    var data = new byte[byteLength];

                    Array.Copy(_receiveBuffer, data, byteLength);

                    _receivedData.Reset(HandleData(data));
                    _stream.BeginRead(_receiveBuffer, 0, DataBufferSize, ReceiveCallback, null);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Error! Receiving TCP Data Error: {exception}");
                }
            }

            private bool HandleData(byte[] data)
            {
                var packetLength = 0;

                _receivedData.SetBytes(data);

                if(_receivedData.UnreadLength() >= 4)
                {
                    packetLength = _receivedData.ReadInt();

                    if(packetLength <= 0)
                    {
                        return true;
                    }
                }

                while(packetLength > 0 && packetLength <= _receivedData.UnreadLength())
                {
                    var packetBytes = _receivedData.ReadBytes(packetLength);

                    ThreadManager.ExecuteOnMainThread(() =>
                    {
                        using (Packet packet = new Packet(packetBytes))
                        {
                            var packetId = packet.ReadInt();

                            _packetHandlers[packetId](packet);
                        }
                    });

                    packetLength = 0;
                    if (_receivedData.UnreadLength() >= 4)
                    {
                        packetLength = _receivedData.ReadInt();

                        if (packetLength <= 0)
                        {
                            return true;
                        }
                    }
                }

                if(packetLength <= 1)
                {
                    return true;
                }

                return false;
            }
        }

        private void InitializeClientData()
        {
            _packetHandlers = new Dictionary<int, PacketHandler>()
            {
                { (int)ServerPackets.Welcome, ClientHandle.Welcome }
            };

            Debug.Log("Packets are initialezed");
        }
    }
}