using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class GameServer : MonoBehaviour
{
    public static int MaxPlayerCount
    {
        get;
        private set;
    }

    public static int Port
    {
        get;
        private set;
    }

    public static TcpListener tcpListener;
    public static Dictionary<int, GameClient> clients = new Dictionary<int, GameClient>();

    public static void Start(int maxPlayers, int port)
    {
        MaxPlayerCount = maxPlayers;
        Port = port;

        InitServerData();

        tcpListener = new TcpListener(IPAddress.Any, Port);
        tcpListener.Start();
        tcpListener.BeginAcceptTcpClient(new System.AsyncCallback(TCPConnectCallback), null);

        Debug.Log($"Server started on port {Port}");
    }

    private static void TCPConnectCallback(IAsyncResult res)
    {
        TcpClient client = tcpListener.EndAcceptTcpClient(res);
        tcpListener.BeginAcceptTcpClient(new System.AsyncCallback(TCPConnectCallback), null);
        Console.WriteLine($"Incoming connection from {client.Client.RemoteEndPoint}");

        for (int i = 1; i <= MaxPlayerCount; i++)
        {
            if(clients[i].tcp.socket == null)
            {
                clients[i].tcp.Connect(client);
                return;
            }
        }

        Console.WriteLine($"Failed to coonnect, server is full");
    }

    public static void InitServerData()
    {
        for (int i = 1; i <= MaxPlayerCount; i++)
        {
            clients.Add(i, new GameClient(i));
        }
    }
}
