using System;
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
        private static dynamic result;
        private static bool resultIsValid = false;
        private static bool accountsIsValid;
        private static string[] accounts;
        public static string[] messages = {""};

        private NetworkStream stream;
        private byte[] buffer = new byte[1024];
        private string totalBuffer = "";

        private static bool IsRunning { get; set; } = false;
        public Client(string ip, int port)
        {
            address = IPAddress.Parse(ip);
            this.port = port;
            tcpClient = new TcpClient(ip, port);


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
                //new Thread(Update).Start();
                IsRunning = true;
            }
            catch (Exception ex)
            {

            }

        }

        private async void Listen()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("awaiting message");
                    dynamic message = JsonConvert.DeserializeObject(ReadJsonMessage(tcpClient));
                    if (message == null)
                    {
                        Program.Disconnect();
                        break;
                    }
                    string id = "";
                    dynamic data = "";
                    Console.WriteLine("received " + message);

                    try
                    {
                        id = message.id;
                        data = message.data;
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
                                Console.WriteLine("message " + data);
                                Program.loginScreen.AuthenticateLogin((bool?)data);

                                break;
                            }
                        case "register":
                            {
                                Program.loginScreen.CreateAccountResponse(data);

                                break;
                            }
                        case "update":
                            {
                                //show string[] in gui messages
                                //messages = data.ToObject<string[]>();
                                //await RefreshChat(data.ToObject<string[][]>());

                                new Thread(() =>
                                {
                                    RefreshChat(data.ToObject<string[][]>());
                                }).Start();

                                break;
                            }
                        case "addMessage":
                            {
                                //Task.Run(async () => await AddMessage(data.ToObject < string[]>()));
                                AddMessage(data.ToObject<string[]>());
                                break;
                            }
                        case "accounts":
                            {
                                //show string[] in gui accounts to talk to
                                accounts = data.ToObject<string[]>();
                                accountsIsValid = true;
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
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static void RefreshChat(string[][] messages)
        {
            Console.WriteLine("adding chat history");
            for(int i = 0; i < ChatUsersScreen.chatScreens.Count; i++)
            {
                Console.WriteLine(ChatUsersScreen.chatScreens[i].chatName +" "+ messages[0][0]);
                if (ChatUsersScreen.chatScreens[i].chatName == messages[0][0])
                {
                    if (!ChatUsersScreen.chatScreens[i].IsDisposed)
                    {

                        ChatUsersScreen.chatScreens[i].UpdateChatHistory(messages);

                    }
                    
                }
            }
        }

        public static void AddMessage(string[] message)
        {
            Console.WriteLine("adding message");
            ChatUsersScreen.chatScreens.ForEach(async screen =>
            {
                Console.WriteLine("going through screens " + message[0] + " " + screen.chatName);
                if (!screen.IsDisposed && screen.chatName == message[0])
                {
                    Console.WriteLine("Adding to screen");
                    await Task.Run(() =>
                    {
                        Console.WriteLine("adding message to screen");
                        screen.AddMessage(message[0], message[1], message[2], true);
                    }); 
                }
            });
        }


        public static string ReadJsonMessage(TcpClient client)
        {
            try
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
            } catch
            {
                return "";
            }
        }

        public static void SendCommand(string id, dynamic data)
        {
            Command command = new Command
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

        public static string[] GetAccounts()
        {
            Console.WriteLine("trying to get account");
            SendCommand("accounts", null);

            while (!accountsIsValid)
            {
                Thread.Sleep(25);
            }
            Console.WriteLine("got accounts");
            accountsIsValid = false;
            return accounts;
        }
    }
}
