namespace Eindopdracht_csharp
{
    partial class ChatUsersScreen
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
            this.UsersCollomn = new System.Windows.Forms.ColumnHeader();
            this.lstChatView = new System.Windows.Forms.ListView();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.btnChatSelect = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.txtSearchInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // UsersCollomn
            // 
            this.UsersCollomn.Text = "Users";
            this.UsersCollomn.Width = 300;
            // 
            // lstChatView
            // 
            this.lstChatView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstChatView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.UsersCollomn});
            this.lstChatView.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstChatView.FullRowSelect = true;
            this.lstChatView.GridLines = true;
            this.lstChatView.Location = new System.Drawing.Point(12, 75);
            this.lstChatView.Name = "lstChatView";
            this.lstChatView.Size = new System.Drawing.Size(224, 265);
            this.lstChatView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstChatView.TabIndex = 4;
            this.lstChatView.TileSize = new System.Drawing.Size(1, 1);
            this.lstChatView.UseCompatibleStateImageBehavior = false;
            this.lstChatView.View = System.Windows.Forms.View.Details;
            this.lstChatView.SelectedIndexChanged += new System.EventHandler(this.lstChatView_SelectedIndexChanged);
            // 
            // txtHeader
            // 
            this.txtHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtHeader.Location = new System.Drawing.Point(12, 12);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.ReadOnly = true;
            this.txtHeader.Size = new System.Drawing.Size(200, 22);
            this.txtHeader.TabIndex = 3;
            this.txtHeader.Text = "Select someone to chat with!";
            this.txtHeader.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // btnChatSelect
            // 
            this.btnChatSelect.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnChatSelect.Location = new System.Drawing.Point(12, 346);
            this.btnChatSelect.Name = "btnChatSelect";
            this.btnChatSelect.Size = new System.Drawing.Size(224, 50);
            this.btnChatSelect.TabIndex = 1;
            this.btnChatSelect.Text = "Chat!";
            this.btnChatSelect.UseVisualStyleBackColor = true;
            this.btnChatSelect.Click += new System.EventHandler(this.btnChatSelect_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(12, 415);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(75, 23);
            this.btnLogOut.TabIndex = 2;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // txtSearchInput
            // 
            this.txtSearchInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSearchInput.Location = new System.Drawing.Point(12, 40);
            this.txtSearchInput.Name = "txtSearchInput";
            this.txtSearchInput.PlaceholderText = "Search Users";
            this.txtSearchInput.Size = new System.Drawing.Size(224, 29);
            this.txtSearchInput.TabIndex = 0;
            this.txtSearchInput.TextChanged += new System.EventHandler(this.txtSearchInput_TextChanged);
            // 
            // ChatUsersScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 450);
            this.Controls.Add(this.txtSearchInput);
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.btnChatSelect);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.lstChatView);
            this.Name = "ChatUsersScreen";
            this.Text = "ChatUsersScreen";
            this.Load += new System.EventHandler(this.ChatUsersScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ColumnHeader UsersCollomn;
        private ListView lstChatView;
        private TextBox txtHeader;
        private Button btnChatSelect;
        private Button btnLogOut;
        private TextBox txtSearchInput;
    }
}