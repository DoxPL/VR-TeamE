using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.clientInstance.tcp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            packet.Write(Client.clientInstance.myId);
            packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(packet);
        }
    }

    public static void UDPTestReceived()
    {
        using (Packet packet = new Packet((int)ClientPackets.udpTestReceive))
        {
            packet.Write("Received a UDP packet.");

            SendUDPData(packet);
        }
    }
    #endregion

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.clientInstance.udp.SendData(_packet);
    }
}