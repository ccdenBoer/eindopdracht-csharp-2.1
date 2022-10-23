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
    public partial class LoginScreen1 : Form
    {
        public LoginScreen1()
        {
            InitializeComponent();
        }

        private void LoginScreen1_Load(object sender, EventArgs e)
        {

        }

        private void txtxPasswordInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin1_Click(object sender, EventArgs e)
        {
            if (txtNameInput.Text.Length > 0 && txtPassword.Text.Length > 0)
            {
                Program.StartChatUserScreen();
            }
            else
            {
                txtFeedback.Text = "Username or Password not filled in";
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
