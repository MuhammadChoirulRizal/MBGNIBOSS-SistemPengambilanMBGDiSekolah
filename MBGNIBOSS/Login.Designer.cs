namespace MBGNIBOSS
{
    partial class Login
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
            this.label4 = new System.Windows.Forms.Label();
            this.lblJudul = new System.Windows.Forms.Label();
            this.btnAdmin = new System.Windows.Forms.Button();
            this.btnPetugas = new System.Windows.Forms.Button();
            this.btnSiswa = new System.Windows.Forms.Button();
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 16);
            this.label4.TabIndex = 7;
            // 
            // lblJudul
            // 
            this.lblJudul.AutoSize = true;
            this.lblJudul.Location = new System.Drawing.Point(315, 39);
            this.lblJudul.Name = "lblJudul";
            this.lblJudul.Size = new System.Drawing.Size(90, 16);
            this.lblJudul.TabIndex = 8;
            this.lblJudul.Text = "SISTEM MBG";
            // 
            // btnAdmin
            // 
            this.btnAdmin.Location = new System.Drawing.Point(318, 120);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(75, 23);
            this.btnAdmin.TabIndex = 9;
            this.btnAdmin.Text = "ADMIN";
            this.btnAdmin.UseVisualStyleBackColor = true;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // btnPetugas
            // 
            this.btnPetugas.Location = new System.Drawing.Point(286, 149);
            this.btnPetugas.Name = "btnPetugas";
            this.btnPetugas.Size = new System.Drawing.Size(119, 23);
            this.btnPetugas.TabIndex = 10;
            this.btnPetugas.Text = "PETUGAS";
            this.btnPetugas.UseVisualStyleBackColor = true;
            this.btnPetugas.Click += new System.EventHandler(this.btnPetugas_Click);
            // 
            // btnSiswa
            // 
            this.btnSiswa.Location = new System.Drawing.Point(318, 178);
            this.btnSiswa.Name = "btnSiswa";
            this.btnSiswa.Size = new System.Drawing.Size(75, 23);
            this.btnSiswa.TabIndex = 11;
            this.btnSiswa.Text = "SISWA";
            this.btnSiswa.UseVisualStyleBackColor = true;
            this.btnSiswa.Click += new System.EventHandler(this.btnSiswa_Click);
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(187, 351);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(132, 16);
            this.lblLogin.TabIndex = 12;
            this.lblLogin.Text = "Masukkan Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(397, 351);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 22);
            this.txtPassword.TabIndex = 13;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Visible = false;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(318, 407);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 14;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BackgroundImage = global::MBGNIBOSS.Properties.Resources.download;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1111, 560);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.btnSiswa);
            this.Controls.Add(this.btnPetugas);
            this.Controls.Add(this.btnAdmin);
            this.Controls.Add(this.lblJudul);
            this.Controls.Add(this.label4);
            this.Name = "Login";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Login_Load);
            this.Click += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Button btnPetugas;
        private System.Windows.Forms.Button btnSiswa;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
    }
}

