using Microsoft.VisualBasic.Logging;
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
            txtxPasswordInput.Text = String.Concat(txtxPasswordInput.Text.Where(c => !Char.IsWhiteSpace(c)));
        }

        private void btnLogin1_Click(object sender, EventArgs e)
        {
            if (txtNameInput.Text.Length > 0 && txtxPasswordInput.Text.Length > 0)
            {
                Client.SendCommand("login", new Tuple<string, string>(String.Concat(txtNameInput.Text.Where(c => !Char.IsWhiteSpace(c) || c == '‎')), String.Concat(txtxPasswordInput.Text.Where(c => !Char.IsWhiteSpace(c) || c == '‎'))));
            }
            else
            {
                txtFeedback.Text = "Username or Password not filled in";
            }
            
        }
        public void AuthenticateLogin(bool? authentication)
        {
            Invoke(new Action(() =>
            {
                if (authentication != null)
                {
                    if ((bool)authentication)
                    {
                        Program.StartChatUserScreen();
                    }
                    else
                    {
                        txtFeedback.Text = "Username or Password is incorrect";
                    }
                }   
                else
                {
                    txtFeedback.Text = "Error while connecting to server";
                }
            }));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNameInput_TextChanged(object sender, EventArgs e)
        {
            txtNameInput.Text = String.Concat(txtNameInput.Text.Where(c => !Char.IsWhiteSpace(c)));
        } 



       

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtNameInput.Text.Length > 0 && txtxPasswordInput.Text.Length > 0)
            {
                Client.SendCommand("register", new Tuple<string, string>(String.Concat(txtNameInput.Text.Where(c => !Char.IsWhiteSpace(c))), String.Concat(txtxPasswordInput.Text.Where(c => !Char.IsWhiteSpace(c)))));
            }
            else
            {
                txtFeedback.Text = "Username or Password not filled in";
            }
        }

        public void CreateAccountResponse(bool? authentication)
        {
            Invoke(new Action(() =>
            {

            
                if (authentication != null)
                {
                    if ((bool)authentication)
                    {
                        Program.StartChatUserScreen();
                    }
                    else
                    {
                        txtFeedback.Text = "Account already exists";
                    }
                }
                else
                {
                    txtFeedback.Text = "Error connecting to server";
                }
            }));

        }
    }
}
