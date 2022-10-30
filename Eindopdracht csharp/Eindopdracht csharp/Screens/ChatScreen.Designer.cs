namespace Eindopdracht_csharp
{
    partial class ChatScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstChatView = new System.Windows.Forms.ListView();
            this.ReceivedChatCollomn = new System.Windows.Forms.ColumnHeader();
            this.SendCollomn = new System.Windows.Forms.ColumnHeader();
            this.txtChatInput = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnLoadMore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstChatView
            // 
            this.lstChatView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstChatView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ReceivedChatCollomn,
            this.SendCollomn});
            this.lstChatView.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstChatView.FullRowSelect = true;
            this.lstChatView.GridLines = true;
            this.lstChatView.Location = new System.Drawing.Point(12, 12);
            this.lstChatView.Name = "lstChatView";
            this.lstChatView.Size = new System.Drawing.Size(481, 362);
            this.lstChatView.TabIndex = 4;
            this.lstChatView.TileSize = new System.Drawing.Size(1, 1);
            this.lstChatView.UseCompatibleStateImageBehavior = false;
            this.lstChatView.View = System.Windows.Forms.View.Details;
            this.lstChatView.SelectedIndexChanged += new System.EventHandler(this.lstChatView_SelectedIndexChanged);
            // 
            // ReceivedChatCollomn
            // 
            this.ReceivedChatCollomn.Text = "Messages";
            this.ReceivedChatCollomn.Width = 480;
            // 
            // SendCollomn
            // 
            this.SendCollomn.Text = "";
            this.SendCollomn.Width = 240;
            // 
            // txtChatInput
            // 
            this.txtChatInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtChatInput.Location = new System.Drawing.Point(12, 380);
            this.txtChatInput.Name = "txtChatInput";
            this.txtChatInput.PlaceholderText = "Type Message Here";
            this.txtChatInput.Size = new System.Drawing.Size(481, 29);
            this.txtChatInput.TabIndex = 5;
            this.txtChatInput.TextChanged += new System.EventHandler(this.txtChatInput_TextChanged);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 415);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnLoadMore
            // 
            this.btnLoadMore.Location = new System.Drawing.Point(418, 415);
            this.btnLoadMore.Name = "btnLoadMore";
            this.btnLoadMore.Size = new System.Drawing.Size(75, 23);
            this.btnLoadMore.TabIndex = 7;
            this.btnLoadMore.Text = "Load More";
            this.btnLoadMore.UseVisualStyleBackColor = true;
            this.btnLoadMore.Click += new System.EventHandler(this.btnLoadMore_Click);
            // 
            // ChatScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 450);
            this.Controls.Add(this.btnLoadMore);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lstChatView);
            this.Controls.Add(this.txtChatInput);
            this.Name = "ChatScreen";
            this.Text = "ChatScreen";
            this.Load += new System.EventHandler(this.ChatScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView lstChatView;
        private ColumnHeader ReceivedChatCollomn;
        private TextBox txtChatInput;
        private ColumnHeader SendCollomn;
        private Button btnBack;
        private Button btnLoadMore;
    }
}