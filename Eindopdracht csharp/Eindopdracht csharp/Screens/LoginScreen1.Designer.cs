namespace Eindopdracht_csharp
{
    partial class LoginScreen1
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
            this.txtxPasswordInput = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtNameInput = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnLogin1 = new System.Windows.Forms.Button();
            this.txtFeedback = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtxPasswordInput
            // 
            this.txtxPasswordInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtxPasswordInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtxPasswordInput.Location = new System.Drawing.Point(20, 166);
            this.txtxPasswordInput.Name = "txtxPasswordInput";
            this.txtxPasswordInput.PasswordChar = '*';
            this.txtxPasswordInput.PlaceholderText = "Enter Password";
            this.txtxPasswordInput.Size = new System.Drawing.Size(200, 22);
            this.txtxPasswordInput.TabIndex = 1;
            this.txtxPasswordInput.TextChanged += new System.EventHandler(this.txtxPasswordInput_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPassword.Location = new System.Drawing.Point(20, 144);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.ReadOnly = true;
            this.txtPassword.Size = new System.Drawing.Size(200, 22);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.Text = "Password:";
            // 
            // txtNameInput
            // 
            this.txtNameInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNameInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtNameInput.Location = new System.Drawing.Point(20, 66);
            this.txtNameInput.Name = "txtNameInput";
            this.txtNameInput.PlaceholderText = "Enter Username";
            this.txtNameInput.Size = new System.Drawing.Size(200, 22);
            this.txtNameInput.TabIndex = 0;
            this.txtNameInput.TextChanged += new System.EventHandler(this.txtNameInput_TextChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(88, 312);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(80, 24);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtName.Location = new System.Drawing.Point(20, 44);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(200, 22);
            this.txtName.TabIndex = 4;
            this.txtName.Text = "Name:";
            // 
            // btnLogin1
            // 
            this.btnLogin1.Location = new System.Drawing.Point(88, 282);
            this.btnLogin1.Name = "btnLogin1";
            this.btnLogin1.Size = new System.Drawing.Size(80, 24);
            this.btnLogin1.TabIndex = 2;
            this.btnLogin1.Text = "Login";
            this.btnLogin1.UseVisualStyleBackColor = true;
            this.btnLogin1.Click += new System.EventHandler(this.btnLogin1_Click);
            // 
            // txtFeedback
            // 
            this.txtFeedback.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFeedback.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtFeedback.ForeColor = System.Drawing.Color.Red;
            this.txtFeedback.Location = new System.Drawing.Point(20, 12);
            this.txtFeedback.Name = "txtFeedback";
            this.txtFeedback.ReadOnly = true;
            this.txtFeedback.Size = new System.Drawing.Size(200, 16);
            this.txtFeedback.TabIndex = 18;
            this.txtFeedback.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // LoginScreen1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 381);
            this.Controls.Add(this.txtFeedback);
            this.Controls.Add(this.txtxPasswordInput);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtNameInput);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnLogin1);
            this.Name = "LoginScreen1";
            this.Text = "LoginScreen1";
            this.Load += new System.EventHandler(this.LoginScreen1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtxPasswordInput;
        private TextBox txtPassword;
        private TextBox txtNameInput;
        private Button btnCreate;
        private TextBox txtName;
        private Button btnLogin;
        private Button btnLogin1;
        private TextBox txtFeedback;
    }
}