using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MBGNIBOSS
{
    public partial class SiswaForm : Form
    {
        SqlConnection conn = new SqlConnection(
        @"Data Source=LAPTOP-5LMNPAS3\CHOY;Initial Catalog=DB_MBG;Integrated Security=True");

        public SiswaForm()
        {
            InitializeComponent();
        }
        private void btnCek_Click(object sender, EventArgs e)
        {
            DialogResult jawab = MessageBox.Show(
            "Aoakah Anda Ingin Mencari?",
            "Konfirmasi",
             MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (jawab == DialogResult.No)
                return;
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                SqlDataAdapter da =
                new SqlDataAdapter(
                "SELECT NIS,Nama,Kelas,Alergi,Status " +
                "FROM vwPengambilan " +
                "WHERE NIS=@nis", conn);

                da.SelectCommand.Parameters.AddWithValue(
                "@nis",
                txtNIS.Text.Trim());

                DataTable dt =
                new DataTable();

                da.Fill(dt);

                // tampil ke gridview
                dataGridView1.DataSource = dt;

                dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

                // VALIDASI JIKA NIS TIDAK ADA
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                    "NIS tidak ditemukan!");

                    dataGridView1.DataSource = null;

                    txtNIS.Focus();
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadGrid()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                SqlDataAdapter da =
                new SqlDataAdapter(
                "SELECT * FROM vwPengambilan " +
                "WHERE NIS=@nis", conn);

                da.SelectCommand.Parameters.AddWithValue(
                "@nis",
                txtNIS.Text.Trim());

                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show(
                    "Data tidak ditemukan");

                    dataGridView1.DataSource = null;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();

                MessageBox.Show(
                "ERROR: " + ex.Message);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult jawab = MessageBox.Show(
            "Apakah Anda Ingin Keluar ?",
            "Konfirmasi",
             MessageBoxButtons.YesNo,
             MessageBoxIcon.Question);

            if (jawab == DialogResult.No)
                return;
            try
            {
                Login f = new Login();
                f.Show();

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}