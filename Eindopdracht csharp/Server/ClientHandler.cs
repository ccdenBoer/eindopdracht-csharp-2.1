using Microsoft.VisualBasic;
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
    internal class ClientHandler
    {
        private TcpClient tcpClient;
        private string username;

        public ClientHandler(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            this.username = "";
            Thread thread = new Thread(HandleClient);
            thread.Start();
        }

        public ClientHandler()
        {
            this.tcpClient = null;
        }

        public async void HandleClient()
        {
            while (true)
            {
                Console.WriteLine("awaiting message");
                dynamic message = JsonConvert.DeserializeObject(ReadJsonMessage(tcpClient));
                Console.WriteLine("message received: " + message);
                string id = "";
                if(message == null)
                {
                    Console.WriteLine("trying to disconnect");
                    Program.Disconnect(this);
                    break;
                }
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

                            if (DataSaver.LoginExists((string)message.data.Item1, (string)message.data.Item2))
                            {
                                status = true;
                                this.username = (string)message.data.Item1;
                                Command accountsCommand = new Command()
                                {
                                    id = "accounts",
                                    data = DataSaver.GetAccounts(username)

                                };
                                SendData(JsonConvert.SerializeObject(accountsCommand), tcpClient);
                            }
                            else
                            {
                                status = false;

                            }
                            Command loginCommand = new Command()
                            {
                                id = "login",
                                data = status
                            };
                            SendData(JsonConvert.SerializeObject(loginCommand), tcpClient);
                            break;
                        }

                    //server checks if register info is available
                    case "register":
                        {
                            bool status;

                            if (DataSaver.ClientExists((string)message.data.Item1))
                            {
                                status = false;
                            }
                            else
                            {
                                status = true;
                                this.username = (string)message.data.Item1;
                                Task task = DataSaver.AddNewClient((string)message.data.Item1, (string)message.data.Item2);

                                Command accountsCommand = new Command()
                                {
                                    id = "accounts",
                                    data = DataSaver.GetAccounts(username)

                                };
                                SendData(JsonConvert.SerializeObject(accountsCommand), tcpClient);

                                await task;
                            }
                            Command registerCommand = new Command()
                            {
                                id = "login",
                                data = status

                            };
                            SendData(JsonConvert.SerializeObject(registerCommand), tcpClient);
                            break;
                        }
                    case "requestMessages":
                        {
                            string[][] data;
                            string[][] a = DataSaver.GetMessageFile(this.username, (string)message.data.Item1);
                            Array.Reverse(a);
                            int messagesLeft = a.Length - (int)message.data.Item2-2;
                            Console.WriteLine(messagesLeft);
                            Console.WriteLine((int)message.data.Item2);
                            Console.WriteLine(a.Length);
                            
                            if(messagesLeft <= 0)
                            {
                                Console.WriteLine("none");
                                data = null;
                            } else if((int)message.data.Item2 == 0)
                            {
                                Console.WriteLine("0 messages");
                                if(messagesLeft >= 20)
                                {
                                    Console.WriteLine("0 messages full 20");
                                    data = a[(int)message.data.Item2..((int)message.data.Item2+20)];
                                } else
                                {
                                    Console.WriteLine("0 messages less than 20");
                                    data = a[(int)message.data.Item2..messagesLeft];
                                }
                            } else if(messagesLeft >= 10)
                            {
                                Console.WriteLine("10 more messages");
                                data = a[(int)message.data.Item2..((int)message.data.Item2 + 10)];
                            } else
                            {
                                Console.WriteLine("last messages");
                                Console.WriteLine((int)message.data.Item2..messagesLeft);
                                data = a[(int)message.data.Item2..(messagesLeft+ (int)message.data.Item2)];
                            }
                            List<string[]> b = new List<string[]>();
                            b.Add(new string[] { (string)message.data.Item1, "", "" });
                            Console.WriteLine(b[0]);
                            if (data != null)
                            {
                                foreach (string[] line in data)
                                {
                                    b.Add(line);
                                    Console.WriteLine(line[2]);
                                }
                            }
                            
                            //Array.Reverse(data);
                            Command updateCommand = new Command()
                            {
                                id = "update",
                                data = b

                            };
                            Console.WriteLine("send "+ b.Count +" messages");
                            SendData(JsonConvert.SerializeObject(updateCommand), tcpClient);
                            break;
                        }
                    case "send":
                        {
                            //Console.WriteLine((string)message.data.Item1 + " - " + (string)message.data.Item2);
                            Task task = DataSaver.WriteMessageFile(this.username, (string)message.data.Item1, (string)message.data.Item2, (string)message.data.Item3);
                            foreach (ClientHandler clientHandler in Program._clients)
                            {
                                if (clientHandler.username == (string)message.data.Item1)
                                    clientHandler.AddMessage(username, (string)message.data.Item2, (string)message.data.Item3, clientHandler.tcpClient);
                            }
                            await task;
                            break;
                        }
                    case "accounts":
                        {
                            Command updateCommand = new Command()
                            {
                                id = "accounts",
                                data = DataSaver.GetAccounts(username)

                            };
                            SendData(JsonConvert.SerializeObject(updateCommand), tcpClient);
                            break;
                        }

                    //error
                    default:
                        Console.WriteLine("received unknown message:\n" + message);
                        break;
                }
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

        private void AddMessage(string name, string time, string message, TcpClient tcpClient)
        {
            Command addMessageCommand = new Command()
            {
                id = "addMessage",
                data = new string[] { name, time, message }
            };
            SendData(JsonConvert.SerializeObject(addMessageCommand), tcpClient);
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