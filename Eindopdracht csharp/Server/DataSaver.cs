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

        public static void AddNewClient(string client)
        {
            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Clients", client));
        }

        public static bool ClientExists(string username)
        {
            return Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Clients", username));
        }

        public static string[] GetMessageFile(string client, string otherClient)
        {
            string pathClient = Path.Combine(Environment.CurrentDirectory, "Clients", client, otherClient);
            string pathOtherClient = Path.Combine(Environment.CurrentDirectory, "Clients", otherClient, client);

            if (!File.Exists(pathClient))
            {
                File.Create(pathClient).Close();
                File.Create(pathOtherClient).Close();
            }
            return File.ReadAllLines(pathClient);

        }
        public static void WriteMessageFile(string client, string otherClient, string time, string message)
        {
            Console.WriteLine(client + " - " + otherClient+ " - " + message);
            string pathClient = Path.Combine(Environment.CurrentDirectory, "Clients", client, otherClient);
            string pathOtherClient = Path.Combine(Environment.CurrentDirectory, "Clients", otherClient, client);
            if(GetMessageFile(client, otherClient).Length == 0)
            {
                File.AppendAllText(pathClient, client + " " + time + " " + message);
                File.AppendAllText(otherClient, client + " " + time + " " + message);
            }
            File.AppendAllText(pathClient, Environment.NewLine +client + " "+ time + " " + message);
            File.AppendAllText(pathOtherClient, Environment.NewLine + client +" "+ time + " " + message);
        }

        public static string[] GetAccounts(string client)
        {
            var accounts = new List<string>();
            foreach (string clientDirectory in Directory.GetDirectories(Path.Combine(Environment.CurrentDirectory, "Clients")))
                if (clientDirectory != Path.Combine(Environment.CurrentDirectory, "Clients", client))
                accounts.Add(Path.GetFileName(clientDirectory));
            return accounts.ToArray();       
        }
    }
}