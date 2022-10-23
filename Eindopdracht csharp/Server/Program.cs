﻿using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program

    {
        private static TcpListener listener;
        private static List<Client> clients = new List<Client>();

        static void Main(string[] args)
        {
            Console.WriteLine("Server started");
            listener = new TcpListener(IPAddress.Any, 15243);
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);

            for (; ; ) ;
        }

        private static void OnConnect(IAsyncResult ar)
        {
            var tcpClient = listener.EndAcceptTcpClient(ar);
            Console.WriteLine($"Client connected from {tcpClient.Client.RemoteEndPoint}");
            Client newClient = new Client(tcpClient);
            clients.Add(newClient);
            //newClient.ClientLogin();
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        internal static void Disconnect(Client client)
        {
            clients.Remove(client);
            Console.WriteLine("Client disconnected");
        }
    }


}

