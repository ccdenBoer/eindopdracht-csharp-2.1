using Server.DataSaving;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program

    {
        private static TcpListener _listener;
        private static List<ClientHandler> _clients = new List<ClientHandler>();

        static void Main(string[] args)
        {
            DataSaver.AddNewClient(new ClientHandler()
            {
                Username = "Coen"
            });
            Console.WriteLine("Server started");
            _listener = new TcpListener(IPAddress.Any, 15243);
            _listener.Start();
            _listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);

            for (; ; ) ;
        }

        private static void OnConnect(IAsyncResult ar)
        {
            var tcpClient = _listener.EndAcceptTcpClient(ar);
            Console.WriteLine($"Client connected from {tcpClient.Client.RemoteEndPoint}");
            ClientHandler newClient = new ClientHandler(tcpClient);
            _clients.Add(newClient);
            //newClient.ClientLogin();
            _listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        internal static void Disconnect(ClientHandler client)
        {
            _clients.Remove(client);
            Console.WriteLine("Client disconnected");
        }
    }


}

