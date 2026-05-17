using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MBGNIBOSS
{
    public partial class AdminForm : Form
    {
        SqlConnection conn = new SqlConnection(
        @"Data Source=LAPTOP-5LMNPAS3\CHOY;Initial Catalog=DB_MBG;Integrated Security=True");

        BindingSource bs = new BindingSource();
        public AdminForm()
        {
            InitializeComponent();
        }

        // ================= LOAD DATA =================
        void LoadData()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
  "SELECT NIS,Nama,Kelas,Alergi,Status FROM vwPengambilan",
  conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                bs.DataSource = dt;

                dataGridView1.DataSource = bs;

               

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        // ================= LOAD JADWAL =================
        void LoadJadwal()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
                "SELECT * FROM vwJadwal",
                conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView2.DataSource = dt;

                // sembunyikan ID
                dataGridView2.Columns["ID"].Visible = false;

                dataGridView2.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        // ================= LOAD STOK =================
        void LoadStokKelas()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                // ================= KELAS 7 =================
                SqlCommand cmd7 = new SqlCommand(
                "SELECT Jumlah FROM vwStokKelas WHERE Kelas='7'", conn);

                lblKelas7.Text = "Stok 7 : " +
                cmd7.ExecuteScalar().ToString();

                // ================= KELAS 8 =================
                SqlCommand cmd8 = new SqlCommand(
                "SELECT Jumlah FROM vwStokKelas WHERE Kelas='8'", conn);

                lblKelas8.Text = "Stok 8 : " +
                cmd8.ExecuteScalar().ToString();

                // ================= KELAS 9 =================
                SqlCommand cmd9 = new SqlCommand(
                "SELECT Jumlah FROM vwStokKelas WHERE Kelas='9'", conn);

                lblKelas9.Text = "Stok 9 : " +
                cmd9.ExecuteScalar().ToString();

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        // ================= LOAD STATISTIK =================
       
    }
}