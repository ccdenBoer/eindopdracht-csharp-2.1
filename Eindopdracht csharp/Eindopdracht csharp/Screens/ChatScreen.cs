using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Server.Commands;
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
    public partial class ChatScreen : Form
    {
        public string chatName { get; set; }
        private string[] messages;
        public ChatScreen()
        {
            InitializeComponent();
            this.txtChatInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnterKeyPress);
            Update();
        }

        private void txtChatInput_TextChanged(object sender, EventArgs e)
        {

        }

        public void SetChatName(string name)
        {
            this.chatName = name;
            this.Text = chatName;
            this.lstChatView.Columns[0].Text = "Chatting with: " + chatName;
        }

        private void lstChatView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ChatScreen_Load(object sender, EventArgs e)
        {

        }

        //currently only local, space the text out so that it fits on the chatbox
        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Return && txtChatInput.Text.Length > 0)
            {
               
               
                
                Command sendMessageCommand = new Command() {
                    id = "send",
                    data = new Tuple<string, string>(chatName, txtChatInput.Text)
                };
                Console.WriteLine(txtChatInput.Text);
                Client.SendData(JsonConvert.SerializeObject(sendMessageCommand));
                
                //reset the chat input
                txtChatInput.Text = "";
            }
        }

        public void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Update()
        {
            messages = Client.messages;
            lstChatView.ResetText();
            foreach (string message in messages)
            {
                //put the time above the message, can later also have the sender
                lstChatView.Items.Insert(0, new ListViewItem(DateAndTime.Now.TimeOfDay.ToString().Substring(0, 8) + " - You"));

                //get all the words from the input
                string[] words = txtChatInput.Text.Split(" ");
                string line = "";

                int lineNr = 1;
                for (int i = 0; i < words.Length; i++)
                {
                    //check if the word is bigger than a line
                    if (words[i].Length > 24)
                    {
                        //if the line already has text print it out
                        if (line.Length > 0)
                        {
                            lstChatView.Items.Insert(lineNr, new ListViewItem(line.Substring(1, line.Length)));
                            line = "";
                            lineNr++;
                        }

                        //print out the long word bit by bit
                        string longWord = words[i];

                        while (longWord.Length > 24)
                        {
                            lstChatView.Items.Insert(lineNr, new ListViewItem(longWord.Substring(0, 22) + "-"));
                            lineNr++;
                            longWord = longWord.Substring(22);
                        }
                        lstChatView.Items.Insert(lineNr, new ListViewItem(longWord));
                        lineNr++;
                    }
                    else
                    {
                        //add a word to the line
                        line += " " + words[i];

                        //check if there is a next word
                        if (words.Length > i + 1)
                        {
                            //check if the next word will fit on the line
                            if ((line + " " + words[i + 1]).Length > 23)
                            {
                                //print out the line
                                lstChatView.Items.Insert(lineNr, new ListViewItem(line.Substring(1, line.Length - 1)));
                                line = "";
                                lineNr++;
                            }
                        }
                        else
                        {
                            //print out the last line
                            lstChatView.Items.Insert(lineNr, new ListViewItem(line.Substring(1, line.Length - 1)));
                            line = "";
                            lineNr++;
                        }
                    }
                }
                //add an empty line for clarity
                lstChatView.Items.Insert(lineNr, new ListViewItem());
            }
        }
    }
}
