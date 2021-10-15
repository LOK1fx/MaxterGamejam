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
                packet.Write("Player" + Random.Range(0, 15));

                SendTCPData(packet);
            }
        }

        public static void PlayerMovement(bool[] inputs, long tick)
        {
            using (Packet packet = new Packet((int)ClientPackets.PlayerMovement))
            {
                packet.Write(inputs.Length);

                foreach (bool input in inputs)
                {
                    packet.Write(input);
                }

                packet.Write(NetworkGameManager.Players[Client.Instance.LocalId].transform.rotation);
                packet.Write(tick);

                SendTCPData(packet);
            }
        }

        #endregion
    }
}