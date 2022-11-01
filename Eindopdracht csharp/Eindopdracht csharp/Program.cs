using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Server;
using System.Net.Sockets;

namespace Eindopdracht_csharp
{
    internal static class Program
    {
        public static LoginScreen1 loginScreen;

        private static ChatUsersScreen chatUsersScreen;

        public static void StartChatUserScreen() 
        {
            chatUsersScreen = new ChatUsersScreen();
            chatUsersScreen.Show();
            loginScreen.Hide();

        }

        public static void LogOut()
        {
            loginScreen.Show();
            chatUsersScreen.Close();
        }

        internal static void Disconnect()
        {
            Console.WriteLine("Server disconnected");
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            Client client = new Client("127.0.0.1", 15243);

            loginScreen = new LoginScreen1();

            Application.Run(loginScreen);

            for (; ; );
        }
    }
}