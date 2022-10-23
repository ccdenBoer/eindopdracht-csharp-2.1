namespace Eindopdracht_csharp
{
    internal static class Program
    {
        private static LoginScreen1 loginScreen;

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


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            loginScreen = new LoginScreen1();

            Application.Run(loginScreen);

            for (; ; );
        }
    }
}