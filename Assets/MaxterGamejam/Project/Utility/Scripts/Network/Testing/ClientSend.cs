using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Tools.Networking
{
    public class ClientSend : MonoBehaviour
    {
        public static void WelcomeReceived()
        {
            using (Packet packet = new Packet((int)ClientPackets.WelcomeReceived))
            {
                packet.Write(Client.Instance.LocalId);
                packet.Write("LOK1");

                SendTCPData(packet);
            }
        }

        private static void SendTCPData(Packet packet)
        {
            packet.WriteLength();

            Client.Instance.Tcp.SendData(packet);
        }
    }
}