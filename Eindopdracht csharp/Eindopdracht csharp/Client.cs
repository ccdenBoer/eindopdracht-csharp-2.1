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
using System.Xml.Xsl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Commands;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Eindopdracht_csharp
{

    public class Client
    {
        private int port;
        private IPAddress address;

        private static TcpClient tcpClient;
        private static bool result;
        private NetworkStream stream;
        private byte[] buffer = new byte[1024];
        private string totalBuffer = "";
        private string talkingTo { get; set; }

        private static bool IsRunning { get; set; } = false;
        public Client(string ip, int port)
        {
            address = IPAddress.Parse(ip);
            this.port = port;
            tcpClient = new TcpClient(ip, port);
            this.talkingTo = null;


            //JObject loginRequest = JObject.Parse(ReadJsonMessage(tcpClient));
            //Command loginRequest = new Command()
            //{
            //    id = "login",
            //    data = "Coen",
            //};

            //Thread.Sleep(500);

            //Console.WriteLine(JsonConvert.SerializeObject(loginRequest));

            //WriteMessage(tcpClient, JsonConvert.SerializeObject(loginCommand));

            //SendData(JsonConvert.SerializeObject(loginRequest), tcpClient);



            try
            {
                new Thread(Listen).Start();
                IsRunning = true;
            }
            catch (Exception ex)
            {

            }

            try
            {
                new Thread(Update).Start();
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
                Console.WriteLine("awaiting message");
                dynamic message = JsonConvert.DeserializeObject(ReadJsonMessage(tcpClient));
                string id = "";
                dynamic data = "";
                Console.WriteLine("received " + message);

                try
                {
                    id = message.id;
                    data = data.id;
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
                            if ((bool)data == true)
                            {
                                //show gui login successful
                                result = true;
                            } else
                            {
                                //show gui login failed 
                                result=false;
                            }
                            Console.WriteLine("message " + data);
                            //LoginScreen1.LoginCheck((bool)message.data);
                            break;
                        }
                    case "register":
                        {
                            if((bool)data == false)
                            {
                                //show gui register successful
                            } else
                            {
                                //show gui register failed
                            }
                            break;
                        }
                    case "update":
                        {
                            //show string[] in gui messages
                            string[] messages = (string[])data;
                            break;
                        }
                    case "accounts":
                        {
                            //show string[] in gui accounts to talk to
                            string[] accounts = (string[])data;
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
        //Refresh messages
        private void Update(object? obj)
        {
            while(true)
            {
                if (talkingTo != null)
                {
                    SendCommand("update", talkingTo);
                }
                Thread.Sleep(1000);
            }
        }
        public static string ReadJsonMessage(TcpClient client)
        {
            var stream = new StreamReader(client.GetStream(), Encoding.ASCII);
            {
                Console.WriteLine("reading message");
                string message = "";
                string line = "";

                while (stream.Peek() != -1)
                {
                    Console.WriteLine("message: " + message);
                    message += stream.ReadLine();
                }
                Console.WriteLine("finished message");

                return message;
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

        public static void SendData(string ob)
        {
            var stream = new StreamWriter(tcpClient.GetStream(), Encoding.ASCII);
            {
                stream.Write(ob + "\n");
                stream.Flush();
                Console.WriteLine("sent!");
            }
        }

        //"login" send username to the server
        //"register" send username to the server [string]
        //"update" send username of person it wants to chat to [string]
        //"send" send username of person it wants to chat to and the message it sent Tuple<[string], [string]>

       
        public static bool SendCommand(string id, dynamic data)
        {
            Command command = new Command
            {
                id = id,
                data = data
            };
            SendData(JsonConvert.SerializeObject(command));
            Thread.Sleep(1000);

            return result;
        }

    }
}
