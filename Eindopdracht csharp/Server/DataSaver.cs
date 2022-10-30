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

        public static void AddNewClient(string client, string password)
        {
            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Clients", client));
            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Clients", client, password));
        }

        public static bool ClientExists(string username)
        {
            return Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Clients", username));
        }

        public static bool LoginExists(string username, string password)
        {
            return Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Clients", username, password));
        }

        public static string[][] GetMessageFile(string client, string otherClient)
        {
            Console.WriteLine(Environment.CurrentDirectory);
            string pathClient = Path.Combine(Environment.CurrentDirectory, "Clients", client, otherClient);
            string pathOtherClient = Path.Combine(Environment.CurrentDirectory, "Clients", otherClient, client);

            if (!File.Exists(pathClient))
            {
                File.Create(pathClient).Close();
                File.Create(pathOtherClient).Close();
            }

            List<string[]> a  = new List<string[]>();
            a.Add(new string[] {otherClient, "", "" });
            foreach (string line in File.ReadAllLines(pathClient))
                a.Add(line.Split("‎"));

            return a.ToArray();
        }
        public static void WriteMessageFile(string client, string otherClient, string time, string message)
        {
            Console.WriteLine(client + " - " + otherClient+ " - " + message);
            string pathClient = Path.Combine(Environment.CurrentDirectory, "Clients", client, otherClient);
            string pathOtherClient = Path.Combine(Environment.CurrentDirectory, "Clients", otherClient, client);
            if (File.ReadAllLines(pathClient).Length == 0)
            {
                File.AppendAllText(pathClient, client + "‎" + time + "‎" + message);
                File.AppendAllText(otherClient, client + "‎" + time + "‎" + message);
            }
            File.AppendAllText(pathClient, Environment.NewLine +client + "‎" + time + "‎" + message);
            File.AppendAllText(pathOtherClient, Environment.NewLine + client + "‎" + time + "‎" + message);
        }

        public static string[] GetAccounts(string client)
        {
            Console.WriteLine("client = " + client);
            var accounts = new List<string>();
            foreach (string clientDirectory in Directory.GetDirectories(Path.Combine(Environment.CurrentDirectory, "Clients")))
                if (clientDirectory != Path.Combine(Environment.CurrentDirectory, "Clients", client))
                accounts.Add(Path.GetFileName(clientDirectory));
            return accounts.ToArray();       
        }
    }
}