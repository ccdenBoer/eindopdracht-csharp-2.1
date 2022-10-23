using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.DataSaving;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    internal class Client
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        private byte[] buffer = new byte[1024];
        private string totalBuffer = "";
        public string Username { get; set; }


        public Client(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            Thread thread = new Thread(HandleClient);
            thread.Start();
        }
        
        public Client()
        {
            this.tcpClient = null;
        }

        public void HandleClient()
        {
            while(true)
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
                            string data = "";
                            if (DataSaver.ClientExists(message.data()))
                            {
                                data = "true";
                            }
                            else
                            {
                                data = "false";
                                this.Username = message.data();
                            }

                            Command loginCommand = new Command()
                            {
                                id = "login",
                                data = "true"
                            };
                            ;
                            WriteJsonMessage(tcpClient, JsonConvert.SerializeObject(loginCommand));
                            break;
                        }

                    //server checks if register info already exist
                    case "register":
                        { 
                            string data = "";
                            if (DataSaver.ClientExists(message.data()))
                            {
                                data = "false";
                            }
                            else
                            {
                                data = "true";
                            }
                            Command registerCommand = new Command()
                            {
                                id = "login",
                                data = "true"
                            };
                            WriteJsonMessage(tcpClient, JsonConvert.SerializeObject(registerCommand));
                            break;
                        }

                    //error
                    default:
                        Console.WriteLine("received unknown message:\n" + message);
                        break;
                }
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
    }
}