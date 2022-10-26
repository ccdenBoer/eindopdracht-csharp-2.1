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
            //DataSaver.AddNewClient(new ClientHandler()
            //{
            //    Username = "Momin"
            //});
            ClientHandler momin = new ClientHandler()
            {
                Username = "momin"
            };
            ClientHandler coen = new ClientHandler()
            {
                Username = "coen"
            };
            //create account
            DataSaver.AddNewClient(momin.Username);
            DataSaver.AddNewClient(coen.Username);

            Console.WriteLine(DataSaver.ClientExists("momin"));
            Console.WriteLine(DataSaver.ClientExists("mo"));

            Console.WriteLine(string.Join("\n", DataSaver.GetAccounts()));
            //open chat
            Console.WriteLine(string.Join("\n", DataSaver.GetMessageFile("momin", "coen")) + "\n");
            //write message
            DataSaver.WriteMessageFile("momin", "coen", "ey");
            //refresh 
            Console.WriteLine(string.Join("\n", DataSaver.GetMessageFile("momin", "coen")) + "\n");
            //write message
            DataSaver.WriteMessageFile("momin", "momin", "yo");
            //refresh 
            Console.WriteLine(string.Join("\n", DataSaver.GetMessageFile("momin", "coen")) + "\n");
            //write message
            DataSaver.WriteMessageFile("momin", "coen", "wow je reageert");
            //refresh 
            Console.WriteLine(string.Join("\n", DataSaver.GetMessageFile("momin", "coen")) + "\n");

            Console.WriteLine(string.Join("\n", DataSaver.GetAccounts()) + "\n");


            Console.WriteLine(DataSaver.ClientExists("test"));


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

