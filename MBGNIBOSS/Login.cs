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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show(
                "Password wajib diisi!");
                return;
            }

            try
            {
                if (conn.State ==
                    System.Data.ConnectionState.Open)
                    conn.Close();

                conn.Open();

                /*SqlCommand cmd =
                new SqlCommand(
                "SELECT * FROM Users " +
                "WHERE RoleUser=@role " +
                "AND Pass=@pass", conn);

                cmd.Parameters.AddWithValue(
                "@role", role);

                cmd.Parameters.AddWithValue(
                "@pass",
                txtPassword.Text);*/
                SqlCommand cmd =
                new SqlCommand(
                "SELECT * FROM Users " +
                "WHERE RoleUser='" + role +
                "' AND Pass='" + txtPassword.Text + "'",
                conn);

                SqlDataReader rd =
                cmd.ExecuteReader();

                if (rd.Read())
                {
                    rd.Close();
                    conn.Close();

                    if (role == "Admin")
                    {
                        AdminForm f =
                        new AdminForm();

                        f.Show();
                        this.Hide();
                    }
                    else if
                    (role == "Petugas")
                    {
                        PetugasForm f =
                        new PetugasForm();

                        f.Show();
                        this.Hide();
                    }
                }
                else
                {
                    rd.Close();
                    conn.Close();

                    MessageBox.Show(
                    "Password salah!");
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

    }
}