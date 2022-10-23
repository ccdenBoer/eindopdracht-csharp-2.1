﻿using Newtonsoft.Json;
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
    internal class ClientHandler
    {
        private TcpClient tcpClient;
        public string Username { get; set; }


        public ClientHandler(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            Thread thread = new Thread(HandleClient);
            thread.Start();
        }

        public ClientHandler()
        {
            this.tcpClient = null;
        }

        public void HandleClient()
        {
            while (true)
            {
                Console.WriteLine("awaiting message");
                dynamic message = JsonConvert.DeserializeObject(ReadJsonMessage(tcpClient));
                Console.WriteLine("message received: " + message);
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
                Console.WriteLine("yee");
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
    }
}