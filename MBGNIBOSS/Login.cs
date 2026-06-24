using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MBGNIBOSS
{
    public partial class Login : Form
    {
        string role = ""; // global role

        public Login()
        {
            InitializeComponent();
        }

        // =========================
        // BUTTON ROLE
        // =========================

        private void btnPetugas_Click(object sender, EventArgs e)
        {
            role = "Petugas";
            MessageBox.Show("Role dipilih: Petugas");
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            role = "Admin";
            MessageBox.Show("Role dipilih: Admin");
        }

        private void btnSiswa_Click(object sender, EventArgs e)
        {
            role = "Siswa";

            MessageBox.Show("Login Siswa berhasil");

            SiswaForm ds = new SiswaForm();
            ds.Show();
            this.Hide();
        }

        // =========================
        // BUTTON LOGIN
        // =========================

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text.Trim();

            if (role == "")
            {
                MessageBox.Show("Pilih role dulu!");
                return;
            }

            // SISWA sudah auto login
            if (role == "Siswa")
            {
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password tidak boleh kosong!");
                return;
            }

            if (role == "Admin" && password == "123")
            {
                MessageBox.Show("Login Admin berhasil");

                AdminForm da = new AdminForm();
                da.Show();
                this.Hide();
            }
            else if (role == "Petugas" && password == "123")
            {
                MessageBox.Show("Login Petugas berhasil");

                PetugasForm dp = new PetugasForm();
                dp.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Password salah!");
            }
        }
        private void RoundedButton(Button btn)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radius = 20;

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);

            path.CloseFigure();
            btn.Region = new Region(path);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            RoundedButton(btnLogin);
            RoundedButton(btnAdmin);
            RoundedButton(btnPetugas);
            RoundedButton(btnSiswa);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            HalamanUtama f = new HalamanUtama();
            f.Show();
            this.Hide();
        }
    }
}