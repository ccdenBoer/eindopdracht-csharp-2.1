﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Commands;
using static System.Collections.Specialized.BitVector32;

namespace Eindopdracht_csharp
{

    public class Client
    {
        private int port;
        private IPAddress address;

        private TcpClient tcpClient;
        private NetworkStream stream;
        private byte[] buffer = new byte[1024];
        private string totalBuffer = "";

        public static bool IsRunning { get; private set; } = false;
        public Client(string ip, int port)
        {
            address = IPAddress.Parse(ip);
            this.port = port;
            this.tcpClient = new TcpClient(ip, port);

            //JObject loginRequest = JObject.Parse(ReadJsonMessage(tcpClient));
            DataCommand loginCommand = new DataCommand()
            {
                id = "login",
                data = "Coen",
            };

            Thread.Sleep(500);

            Console.WriteLine(JsonConvert.SerializeObject(loginCommand));

            //WriteMessage(tcpClient, JsonConvert.SerializeObject(loginCommand));

            SendData(JsonConvert.SerializeObject(loginCommand), tcpClient);

            

            try
            {
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
                dynamic message = JsonConvert.DeserializeObject(ReadJsonMessage(tcpClient));
                string id = "";
                try
                {
                    id = message.id;
                }
                catch
                {
                    Console.WriteLine("can't find id in message:" + message);
                }
                switch (id)
                {
                    //server checks if login info already exists
                    case "login":
                        {
                            JObject loginRequest = JObject.Parse(ReadJsonMessage(tcpClient));
                            StatusCommand loginCommand = new StatusCommand()
                            {
                                id = "login",
                                status = true
                            };
                            ;
                            WriteJsonMessage(tcpClient, JsonConvert.SerializeObject(loginCommand));
                            break;
                        }

                    //error
                    default:
                        {
                            Console.WriteLine("received unknown message:\n" + message);
                            break;
                        }
                }
            }
        }

        public static string ReadJsonMessage(TcpClient client)
        {
            var stream = new StreamReader(client.GetStream(), Encoding.ASCII);
            {
                string message = "";
                string line = "";

                while (stream.Peek() != -1)
                {
                    message += stream.ReadLine();
                }

                return message;
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

        public static void WriteJsonMessage(TcpClient client, string jsonMessage)
        {
            var stream = new StreamWriter(client.GetStream(), Encoding.ASCII);
            {
                stream.Write(jsonMessage);
                stream.Flush();
            }
        }

        private static void SendData(string ob, TcpClient tcpClient)
        {
            var stream = new StreamWriter(tcpClient.GetStream(), Encoding.ASCII);
            {
                stream.Write(ob + "\n");
                stream.Flush();
                Console.WriteLine("sent!");
            }
        }
    }
}
