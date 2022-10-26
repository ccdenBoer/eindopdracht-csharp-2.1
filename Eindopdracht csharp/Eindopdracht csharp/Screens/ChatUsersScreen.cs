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
        private List<string> availableUsers;
        public ChatUsersScreen()
        {
            InitializeComponent();

            availableUsers = new List<string>();

            lstChatView.Items.Add("1");
            lstChatView.Items.Add("2");
            lstChatView.Items.Add("3");
            lstChatView.Items.Add("4");
            lstChatView.Items.Add("5");
            lstChatView.Items.Add("6");
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
            if (lstChatView.SelectedItems != null && lstChatView.SelectedItems.Count == 1)
            {
                ChatScreen chatScreen = new ChatScreen();
                chatScreen.SetChatName(lstChatView.SelectedItems[0].Text);
                chatScreen.Show();
            }
        }

        private void lstChatView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
