using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MBGNIBOSS
{
    public partial class Login : Form
    {
        SqlConnection conn = new SqlConnection(
         @"Data Source=LAPTOP-5LMNPAS3\CHOY;Initial Catalog=DB_MBG;Integrated Security=True;");
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                "SELECT RoleUser FROM Users WHERE Username=@u AND Pass=@p", conn);

                cmd.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@p", txtPassword.Text.Trim());

                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    string role = rd["RoleUser"].ToString().Trim();

                    MessageBox.Show("Login berhasil sebagai: " + role);

                    rd.Close();
                    conn.Close();

                    this.Hide();

                    if (role == "Admin")
                        new AdminForm().Show();
                    else if (role == "Petugas")
                        new PetugasForm().Show();
                    else
                        new SiswaForm().Show();
                }
                else
                {
                    rd.Close();
                    conn.Close();
                    MessageBox.Show("Login gagal! Username / Password salah");
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void btnKembali_Click(object sender, EventArgs e)
        {


            try
            {
                DialogResult hasil = MessageBox.Show(
                    "Kamu mau keluar?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (hasil == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }


        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                lblStatus.Text = "Database Connected";
                conn.Close();
            }
            catch
            {
                lblStatus.Text = "Database Failed";
            }
        }


    }
}


            








