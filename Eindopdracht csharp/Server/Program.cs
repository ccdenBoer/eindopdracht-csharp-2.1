using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program

    {
        private static TcpListener _listener;
        private static List<Client> _clients = new List<Client>();

        static void Main(string[] args)
        {
            Console.WriteLine("Server started");
            _listener = new TcpListener(IPAddress.Any, 15243);
            _listener.Start();
            _listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        private static void OnConnect(IAsyncResult ar)
        {
            var tcpClient = _listener.EndAcceptTcpClient(ar);
            Console.WriteLine($"Client connected from {tcpClient.Client.RemoteEndPoint}");
            Client newClient = new Client(tcpClient);
            _clients.Add(newClient);
            //newClient.ClientLogin();
            _listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        internal static void Disconnect(Client client)
        {
            _clients.Remove(client);
            Console.WriteLine("Client disconnected");
        }
    }


}

