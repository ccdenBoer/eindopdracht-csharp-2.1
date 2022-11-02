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
        private string[] accounts;
        public ChatUsersScreen()
        {
            InitializeComponent();
            this.txtSearchInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnterKeyPress);

            accounts = Client.GetAccounts();
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
            chatScreens.ForEach(screen => screen.Close());
            chatScreens = new List<ChatScreen>();
            Program.LogOut();
        }

        private async void btnChatSelect_Click(object sender, EventArgs e)
        {
            SelectUsers();
            
        }

        private void SelectUsers()
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
                        ChatScreen chatScreen = new ChatScreen(lstChatView.SelectedItems[i].Text);
                        chatScreen.Show();
                        chatScreens.Add(chatScreen);
                    }

                }

            }
        }

        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Return)
            {
                //ListView.ListViewItemCollection items = lstChatView.Items;

                //ColumnHeader header = lstChatView.Columns[0];
                SearchAccountsList(txtSearchInput.Text);

            }
        }

        private void SearchAccountsList(string searchInput)
        {
            lstChatView.Items.Clear();

            for (int i = 0; i < accounts.Count(); i++)
            {
                Console.WriteLine("searching");
                if (accounts[i].ToLower().Contains(searchInput.ToLower()))
                {
                    lstChatView.Items.Add(accounts[i]);
                }
            }
        }

        private void lstChatView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            accounts = Client.GetAccounts();
            SearchAccountsList("");
        }
    }
}
