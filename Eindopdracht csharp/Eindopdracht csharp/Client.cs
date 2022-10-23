using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Commands;
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
                    //server checks if login info is available
                    case "login":
                        {
                            bool status;

                            if (DataSaver.ClientExists(message.data()))
                            {
                                status = true;
                            }
                            else
                            {
                                status = false;
                                this.Username = message.data();
                            }
                            StatusCommand loginCommand = new StatusCommand()
                            {
                                id = "login",
                                status = status
                            };
                            WriteJsonMessage(tcpClient, JsonConvert.SerializeObject(loginCommand));
                            break;
                        }

                    //server checks if register info is available
                    case "register":
                        {
                            bool status;

                            if (DataSaver.ClientExists(message.data()))
                            {
                                status = false;
                            }
                            else
                            {
                                status = true;
                            }
                            StatusCommand registerCommand = new StatusCommand()
                            {
                                id = "login",
                                status = status

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