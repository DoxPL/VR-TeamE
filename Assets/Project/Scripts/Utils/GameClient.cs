using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class GameClient : MonoBehaviour
{
    public int identifier;
    public TCP tcp;
    public static int bufferSize = 4096;

    public GameClient(int clientID)
    {
        identifier = clientID;
        tcp = new TCP(identifier);
    }

    public class TCP
    {
        public TcpClient socket;
        private readonly int identifier;
        private NetworkStream stream;
        private byte[] receiveBuff;

        public TCP(int id)
        {
            identifier = id;
        }

        public void Connect(TcpClient sckt)
        {
            socket = sckt;
            socket.ReceiveBufferSize = bufferSize;
            socket.SendBufferSize = bufferSize;

            stream = socket.GetStream();

            receiveBuff = new byte[bufferSize];

            stream.BeginRead(receiveBuff, 0, bufferSize, RecvCallback, null);
        }

        private void RecvCallback(IAsyncResult res)
        {
            try
            {
                int byteLen = stream.EndRead(res);

                if(byteLen <= 0)
                {
                    return;
                }

                byte[] data = new byte[byteLen];
                Array.Copy(receiveBuff, data, byteLen);

                stream.BeginRead(receiveBuff, 0, bufferSize, RecvCallback, null);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error receiving TCP data: {exception}");
            }
        }
    }
}
