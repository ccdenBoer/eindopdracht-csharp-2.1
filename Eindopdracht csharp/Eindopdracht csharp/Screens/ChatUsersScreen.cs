using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eindopdracht_csharp
{
    public partial class ChatUsersScreen : Form
    {
        public static List<ChatScreen> chatScreens = new List<ChatScreen>();
        public ChatUsersScreen()
        {
            InitializeComponent();
            string[] accounts = Client.GetAccounts();
            foreach (string account in accounts)
            {
                Console.WriteLine(account);
                lstChatView.Items.Add(account);
            }
    
        }

        private void ChatUsersScreen_Load(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Program.LogOut();
        }

        private void btnChatSelect_Click(object sender, EventArgs e)
        {
            if (lstChatView.SelectedItems != null && lstChatView.SelectedItems.Count > 0)
            {
                for (int i = 0; i < lstChatView.SelectedItems.Count; i++)
                {
                    bool alreadyOpen = false;
                    for (int j = 0; j < chatScreens.Count; j++)
                    {
                        if (lstChatView.SelectedItems[i].Text == chatScreens[j].chatName)
                        {
                            if (chatScreens[j].IsDisposed)
                            {
                                chatScreens.RemoveAt(j);
                            }
                            else
                            {
                                alreadyOpen = true;
                            }
                            
                            
                        }
                    }

                    if (!alreadyOpen)
                    {
                        ChatScreen chatScreen = new ChatScreen();
                        chatScreen.SetChatName(lstChatView.SelectedItems[i].Text);
                        Client.otherClient = lstChatView.SelectedItems[i].Text;
                        chatScreen.Show();
                        chatScreens.Add(chatScreen);
                    }
                    
                }
                
            }
        }

        private void lstChatView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
