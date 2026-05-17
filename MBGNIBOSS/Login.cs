using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MBGNIBOSS
{
    public partial class Login : Form
    {
        SqlConnection conn = new SqlConnection(
        @"Data Source=LAPTOP-5LMNPAS3\CHOY;
        Initial Catalog=DB_MBG;
        Integrated Security=True;");

        string role = "";

        public Login()
        {
            InitializeComponent();
        }

        // ================= FORM LOAD =================
        private void Login_Load(object sender, EventArgs e)
        {
            lblLogin.Visible = false;
            txtPassword.Visible = false;
            btnLogin.Visible = false;
            
        }

        // ================= ADMIN =================

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            role = "Admin";

            lblLogin.Visible = true;
            txtPassword.Visible = true;
            btnLogin.Visible = true;

            lblLogin.Text =
            "Masukkan Password Admin";

            txtPassword.Clear();
            txtPassword.Focus();
        }

        // ================= PETUGAS =================
        private void btnPetugas_Click(object sender, EventArgs e)
        {
            role = "Petugas";

            lblLogin.Visible = true;
            txtPassword.Visible = true;
            btnLogin.Visible = true;

            lblLogin.Text =
            "Masukkan Password Petugas";

            txtPassword.Clear();
            txtPassword.Focus();
        }

        // ================= SISWA =================
        private void btnSiswa_Click(object sender, EventArgs e)
        {
            SiswaForm f =
            new SiswaForm();

            f.Show();
            this.Hide();
        }

        // ================= LOGIN =================
      


    }
}