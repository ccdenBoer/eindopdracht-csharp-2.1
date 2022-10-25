using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server.DataSaving
{
    internal class DataSaver
    {

        public static void AddNewClient(ClientHandler client)
        {
            Console.WriteLine("huh");
            Console.WriteLine(Environment.CurrentDirectory);
            string directoryPath = Environment.CurrentDirectory + "\\Clients\\" + client.Username;
            Directory.CreateDirectory(directoryPath);
            string path = Environment.CurrentDirectory + "\\Clients\\" + client.Username + "\\" + client.Username + ".JSON";
            File.Create(path).Close();

            string clientAsJson = JsonConvert.SerializeObject(client);
            File.WriteAllText(path, clientAsJson);
        }

        public static bool ClientExists(string username)
        {
            string[] clientFiles = Directory.GetFiles(Environment.CurrentDirectory + "\\Clients");
            foreach (string clientPath in clientFiles)
            {
                var clientInJson = JObject.Parse(File.ReadAllText(clientPath));
                ClientHandler client = new ClientHandler();
                client.Username = clientInJson["patientId"].ToString();
                if(client.Username == username)
                {
                    return true;
                }
            }

            return false;
        }

        //public static void addpatientfile(tcpclient client, list<jobject> sessiondata)
        //{
        //    jobject jobject = jobject.parse(client.readjsonmessage(client));
        //    string patientid = jobject["data"]["patientid"].tostring();

        //    int amountoffiles = directory.getfiles(environment.currentdirectory + "\\clients\\" + patientid).length;
            
        //    string path = environment.currentdirectory + "\\clients\\" + patientid + "\\" + patientid + " session#" + amountoffiles +
        //                  ".json";
        //    file.create(path).close();

        //    file.writealltext(path, sessiondata.tostring());
        //}
    }
}