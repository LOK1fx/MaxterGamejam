using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Tools.Networking
{
    public class ClientSend : MonoBehaviour
    {
        private static void SendTCPData(Packet packet)
        {
            packet.WriteLength();

            Client.Instance.Tcp.SendData(packet);
        }

        private static void SendUDPData(Packet packet)
        {
            packet.WriteLength();

            Client.Instance.Udp.SendData(packet);
        }

        #region Packets

        public static void WelcomeReceived()
        {
            using (Packet packet = new Packet((int)ClientPackets.WelcomeReceived))
            {
                packet.Write(Client.Instance.LocalId);
                packet.Write("LOK1");

                SendTCPData(packet);
            }
        }

        public static void UDPTestReceived()
        {
            using(Packet packet = new Packet((int)ClientPackets.UdpTestReceived))
            {
                packet.Write("Received a UDP packet.");

                SendUDPData(packet);
            }
        }

        #endregion
    }
}