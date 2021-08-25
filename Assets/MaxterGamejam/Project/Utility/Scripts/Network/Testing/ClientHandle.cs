using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Tools.Networking
{
    public class ClientHandle : MonoBehaviour
    {
        public static void Welcome(Packet packet)
        {
            var message = packet.ReadString();
            var id = packet.ReadInt();

            Client.Instance.LocalId = id;
            ClientSend.WelcomeReceived();

            Debug.Log(message);
        }
    }
}