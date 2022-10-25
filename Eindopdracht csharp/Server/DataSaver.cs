using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Server.DataSaving
{
    internal class DataSaver
    {

        public static void AddNewClient(ClientHandler client)
        {
            Directory.CreateDirectory(Environment.CurrentDirectory + "\\Clients\\" + client.Username);
        }

        public static bool ClientExists(string username)
        {
            foreach (string clientDirectory in Directory.GetDirectories(Environment.CurrentDirectory + "\\Clients"))  
                if (clientDirectory.Equals(Environment.CurrentDirectory + "\\Clients\\" + username)) 
                {
                    return true; 
                }
            return false;
        }

        public static string[] GetMessageFile(ClientHandler client, string otherClient)
        {
            string pathClient = Environment.CurrentDirectory + "\\Clients\\" + client.Username + "\\" + otherClient;
            string pathOtherClient = Environment.CurrentDirectory + "\\Clients\\" + otherClient + "\\" + client.Username;

            if (!File.Exists(pathClient))
            {
                File.Create(pathClient).Close();
                File.Create(pathOtherClient).Close();
            }
            return File.ReadAllLines(pathClient);

        }
        public static void WriteMessageFile(ClientHandler client, string otherClient, string message)
        {
            string clientPath = Environment.CurrentDirectory + "\\clients\\" + client.Username + "\\" + otherClient;
            string otherClientPath = Environment.CurrentDirectory + "\\clients\\" + otherClient + "\\" + client.Username;
            File.AppendAllText(clientPath, client.Username + ": " + message +Environment.NewLine);
            File.AppendAllText(otherClientPath, client.Username + ": " + message + Environment.NewLine);
        }
    }
}