using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace LOK1game.Tools.Networking
{
    public class ClientHandle : MonoBehaviour
    {
        public static void Welcome(Packet packet)
        {
            var message = packet.ReadString();
            var id = packet.ReadInt();
            var tick = packet.ReadLong();

            Client.Instance.LocalId = id;
            Client.Instance.Tick = tick;

            ClientSend.WelcomeReceived();

            Debug.Log(message);

            Client.Instance.Udp.Connect(((IPEndPoint)Client.Instance.Tcp.Socket.Client.LocalEndPoint).Port);
        }

        public static void SpawnPlayer(Packet packet)
        {
            var id = packet.ReadInt();
            var username = packet.ReadString();
            var position = packet.ReadVector3();
            var rotation = packet.ReadQuaternion();

            NetworkGameManager.Instance.SpawnPlayer(id, username, position, rotation);
        }

        public static void PlayerPosition(Packet packet)
        {
            var id = packet.ReadInt();
            var position = packet.ReadVector3();

            //NetworkGameManager.Players[id].transform.position = position;

            NetworkGameManager.SetPlayerPosition(id, position);

            Debug.Log(position);
        }

        public static void PlayerRotation(Packet packet)
        {
            var id = packet.ReadInt();
            var rotation = packet.ReadQuaternion();

            NetworkGameManager.Players[id].transform.rotation = rotation;
        }
    }
}