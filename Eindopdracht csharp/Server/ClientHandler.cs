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
                                SendCommand("accounts", DataSaver.GetAccounts(username), tcpClient);
                            }
                            else
                            {
                                status = false;

                            }
                            SendCommand("login", status, tcpClient);
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
                                await DataSaver.AddNewClient((string)message.data.Item1, (string)message.data.Item2);
                                SendCommand("accounts", DataSaver.GetAccounts(username), tcpClient);
                            }
                            SendCommand("login", status, tcpClient);
                            break;
                        }
                    case "requestMessages":
                        {
                            string[][] data;
                            string[][] allData = DataSaver.GetMessageFile(this.username, (string)message.data.Item1);
                            Array.Reverse(allData);
                            int messagesLeft = allData.Length - (int)message.data.Item2-1;
                            Console.WriteLine(messagesLeft);
                            Console.WriteLine((int)message.data.Item2);
                            Console.WriteLine(allData.Length);
                            
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
                                    data = allData[(int)message.data.Item2..((int)message.data.Item2+20)];
                                } else
                                {
                                    Console.WriteLine("0 messages less than 20");
                                    data = allData[(int)message.data.Item2..messagesLeft];
                                }
                            } else if(messagesLeft >= 10)
                            {
                                Console.WriteLine("10 more messages");
                                data = allData[(int)message.data.Item2..((int)message.data.Item2 + 10)];
                            } else
                            {
                                Console.WriteLine("last messages");
                                data = allData[(int)message.data.Item2..(messagesLeft+ (int)message.data.Item2)];
                            }
                            List<string[]> dataList = new List<string[]>();
                            dataList.Add(new string[] { (string)message.data.Item1, "", "" });
                            Console.WriteLine(dataList[0]);
                            if (data != null)
                            {
                                foreach (string[] line in data)
                                {
                                    dataList.Add(line);
                                    Console.WriteLine(line[2]);
                                }
                            }
                            
                            //Array.Reverse(data);
                            Console.WriteLine("send "+ dataList.Count +" messages");
                            SendCommand("update", dataList, tcpClient);
                            break;
                        }
                    case "send":
                        {
                            await DataSaver.WriteMessageFile(this.username, (string)message.data.Item1, (string)message.data.Item2, (string)message.data.Item3);
                            foreach (ClientHandler clientHandler in Program._clients)
                            {
                                if (clientHandler.username == (string)message.data.Item1)
                                    clientHandler.AddMessage(username, (string)message.data.Item2, (string)message.data.Item3, clientHandler.tcpClient);
                            }
                            break;
                        }
                    case "accounts":
                        {
                            SendCommand("accounts", DataSaver.GetAccounts(username), tcpClient);
                            break;
                        }

                    //error
                    default:
                        Console.WriteLine("received unknown message:\n" + message);
                        break;
                }
            }
        }

        private static void SendCommand(string id, dynamic data, TcpClient tcpClient)
        {
            Command command = new Command()
            {
                id = id,
                data = data

            };
            var stream = new StreamWriter(tcpClient.GetStream(), Encoding.ASCII);
            {
                stream.Write(JsonConvert.SerializeObject(command) + "\n");
                stream.Flush();
                Console.WriteLine("sent!");
            }
        }

        private void AddMessage(string name, string time, string message, TcpClient tcpClient)
        {
            SendCommand("addMessage", new string[] { name, time, message }, tcpClient);
        }

        public static string ReadJsonMessage(TcpClient client)
        {
            var stream = new StreamReader(client.GetStream(), Encoding.ASCII);
            {
                Console.WriteLine("reading message");
                string message = "";
            
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