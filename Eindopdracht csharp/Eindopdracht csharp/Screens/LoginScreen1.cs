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

        }

        private void btnLogin1_Click(object sender, EventArgs e)
        {
            if (txtNameInput.Text.Length > 0 && txtPassword.Text.Length > 0)
            {
                Client.SendCommand("login", txtNameInput.Text);
            }
            else
            {
                txtFeedback.Text = "Username or Password not filled in";
            }
            
        }
        public void Login(bool? authentication)
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

        }



       

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtNameInput.Text.Length > 0 && txtPassword.Text.Length > 0)
            {
                Client.SendCommand("register", txtNameInput.Text);


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
