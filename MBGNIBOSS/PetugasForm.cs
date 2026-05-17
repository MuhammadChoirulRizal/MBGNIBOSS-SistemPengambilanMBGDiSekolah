using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MBGNIBOSS
{
    public partial class PetugasForm : Form
    {
        SqlConnection conn = new SqlConnection(
        @"Data Source=LAPTOP-5LMNPAS3\CHOY;Initial Catalog=DB_MBG;Integrated Security=True");

        public PetugasForm()
        {
            InitializeComponent();
        }
     


        void LoadData()
        {
            try
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
 "SELECT NIS,Nama,Kelas,Alergi,Status FROM vwPengambilan",
 conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
                

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
                "SELECT NIS,Nama,Kelas,Alergi,Status FROM vwPengambilan WHERE NIS=@nis", conn);

                da.SelectCommand.Parameters.AddWithValue(
                "@nis", txtCariNIS.Text);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    txtCariNIS.Text = dt.Rows[0]["NIS"].ToString();
                    txtNama.Text = dt.Rows[0]["Nama"].ToString();
                    txtKelas.Text = dt.Rows[0]["Kelas"].ToString();
                    txtStatus.Text = dt.Rows[0]["Status"].ToString();
                }
                else
                {
                    MessageBox.Show("Data tidak ditemukan!");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void btnProses_Click(
object sender,
EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                // ================= CEK DATA SISWA =================
                SqlCommand cek = new SqlCommand(
                "SELECT * FROM vwPengambilan " +
                "WHERE NIS=@nis",
                conn);

                cek.Parameters.AddWithValue(
                "@nis",
                txtCariNIS.Text);

                SqlDataReader rd =
                cek.ExecuteReader();

                if (!rd.Read())
                {
                    rd.Close();
                    conn.Close();

                    MessageBox.Show(
                    "Data siswa tidak ditemukan!");

                    return;
                }

                // ================= AMBIL DATA SISWA =================
                string kelas =
                rd["Kelas"].ToString();

                string status =
                rd["Status"].ToString();

                string alergi =
                rd["Alergi"].ToString();

                rd.Close();

                // ================= NOTIF ALERGI =================
                if (!string.IsNullOrWhiteSpace(alergi))
                {
                    MessageBox.Show(
                    "Perhatian!\n" +
                    "Siswa memiliki alergi: " +
                    alergi,
                    "Peringatan Alergi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                }

                // ================= SUDAH AMBIL =================
                if (status == "Sudah Diambil")
                {
                    conn.Close();

                    MessageBox.Show(
                    "Siswa sudah mengambil MBG!");

                    return;
                }

                // ================= CEK JADWAL =================
                SqlCommand jadwal =
 new SqlCommand(
 "SELECT Tanggal, JamMulai, JamSelesai " +
 "FROM vwJadwal " +
 "WHERE Kelas=@kelas " +
 "AND Tanggal=@tanggal",
 conn);

                jadwal.Parameters.AddWithValue(
                "@kelas",
                kelas);

                jadwal.Parameters.AddWithValue(
                "@tanggal",
                DateTime.Now.Date);

                SqlDataReader jd =
                jadwal.ExecuteReader();

                if (!jd.Read())
                {
                    jd.Close();
                    conn.Close();

                    MessageBox.Show(
                    "Belum ada jadwal pengambilan hari ini!");

                    return;
                }

                TimeSpan mulai =
                TimeSpan.Parse(
                jd["JamMulai"].ToString());

                TimeSpan selesai =
                TimeSpan.Parse(
                jd["JamSelesai"].ToString());

                jd.Close();

                // ================= VALIDASI JAM =================
                TimeSpan sekarang =
                DateTime.Now.TimeOfDay;

                if (sekarang < mulai ||
                    sekarang > selesai)
                {
                    conn.Close();

                    MessageBox.Show(
                    "Belum waktunya pengambilan!");

                    return;
                }

                // ================= CEK STOK KELAS =================
                SqlCommand stokCmd =
                new SqlCommand(
                "SELECT Jumlah " +
                "FROM vwStokKelas " +
                "WHERE Kelas=@kelas",
                conn);

                stokCmd.Parameters.AddWithValue(
                "@kelas",
                kelas);

                int stok =
                Convert.ToInt32(
                stokCmd.ExecuteScalar());

                if (stok <= 0)
                {
                    conn.Close();

                    MessageBox.Show(
                    "Stok kelas habis!");

                    return;
                }

                // ================= UPDATE STATUS =================
                SqlCommand update =
                new SqlCommand(
                "UPDATE Pengambilan " +
                "SET Status='Sudah Diambil' " +
                "WHERE NIS=@nis",
                conn);

                update.Parameters.AddWithValue(
                "@nis",
                txtCariNIS.Text);

                update.ExecuteNonQuery();

                // ================= KURANGI STOK KELAS =================
                SqlCommand kurangKelas =
                new SqlCommand(
                "UPDATE StokKelas " +
                "SET Jumlah = Jumlah - 1 " +
                "WHERE Kelas=@kelas",
                conn);

                kurangKelas.Parameters.AddWithValue(
                "@kelas",
                kelas);

                kurangKelas.ExecuteNonQuery();

                // ================= UPDATE TOTAL STOK =================
                SqlCommand total =
                new SqlCommand(
                "SELECT SUM(Jumlah) " +
                "FROM StokKelas",
                conn);

                int totalStok =
                Convert.ToInt32(
                total.ExecuteScalar());

                SqlCommand updateTotal =
   new SqlCommand(
   "UPDATE StokMBG " +
   "SET Jumlah=@jumlah",
   conn);
                updateTotal.Parameters.AddWithValue(
                "@jumlah",
                totalStok);

                updateTotal.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show(
                "Pengambilan berhasil!");

                LoadData();
                LoadStokKelas();
                ClearForm();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

       


    }
    
}