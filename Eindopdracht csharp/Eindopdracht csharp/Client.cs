using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Eindopdracht_csharp
{

    public class Client
    {
        private int port;
        private IPAddress address;
        private static Socket socket;
        public static bool IsRunning { get; private set; } = false;
        public Client(string ip, int port)
        {
            address = IPAddress.Parse(ip);
            this.port = port;
        }

        public void Connect()
        {
            IPEndPoint endPoint = new IPEndPoint(address, port);
            socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Connect(endPoint);
                new Thread(Listen).Start();
                IsRunning = true;
            }
            catch (Exception ex)
            {
                
            }
        }

        private void Listen()
        {
            while (true)
            {
                byte[] message = new byte[1024];
                int receive = socket.Receive(message);
            }
        }

        public static JObject ReadMessage(TcpClient client)
        {
            var stream = new StreamReader(client.GetStream(), Encoding.ASCII);
            {
                string message = "";
                string line = "";
                while (stream.Peek() != -1)
                {
                    message += stream.ReadLine();
                }

                return new JObject.Parse(message);
            }
        }

        public static void WriteMessage(TcpClient client, JObject jObject)
        {
            string jMessage = jObject.ToString();
            var stream = new StreamWriter(client.GetStream(), Encoding.ASCII);
            byte[] RequestLength = BitConverter.GetBytes(jMessage.Length);
            byte[] request = Encoding.ASCII.GetBytes(jMessage);
            {
                stream.BaseStream.Write(request, 0, RequestLength.Length);
            }
        }
    }
}
