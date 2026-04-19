using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

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
                "SELECT NIS,Nama,Kelas,Alergi,Tanggal,Jam,Status FROM Pengambilan", conn);

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
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Pengambilan WHERE NIS=@nis", conn);

                cmd.Parameters.AddWithValue("@nis", txtCari.Text.Trim());

                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    txtNama.Text = rd["Nama"].ToString();
                    txtKelas.Text = rd["Kelas"].ToString();
                    txtAlergi.Text = rd["Alergi"].ToString();
                    txtStatus.Text = rd["Status"].ToString();
                    txtTanggal.Text = rd["Tanggal"].ToString();
                    txtJam.Text = rd["Jam"].ToString();

                    // VALIDASI SUDAH AMBIL
                    if (txtStatus.Text == "Sudah Diambil")
                    {
                        MessageBox.Show("Jatah Sudah Diambil!");
                        btnProses.Enabled = false;
                    }
                    else
                    {
                        btnProses.Enabled = true;
                    }

                    // VALIDASI ALERGI
                    if (txtAlergi.Text != "")
                    {
                        MessageBox.Show("PERHATIAN! SISWA MEMILIKI ALERGI");
                    }
                }
                else
                {
                    MessageBox.Show("NIS tidak ditemukan");
                }

                rd.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }


        private void btnProses_Click(object sender, EventArgs e)
        {
            DialogResult jawab = MessageBox.Show(
"Yakin ingin Memproses data?",
"Konfirmasi",
MessageBoxButtons.YesNo,
MessageBoxIcon.Question);

            if (jawab == DialogResult.No)
                return;
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                "UPDATE Pengambilan SET " +
                "Status='Sudah Diambil'," +
                "Tanggal=@tgl," +
                "Jam=@jam " +
                "WHERE NIS=@nis", conn);

                cmd.Parameters.AddWithValue("@tgl",
                DateTime.Now.ToString("yyyy-MM-dd"));

                cmd.Parameters.AddWithValue("@jam",
                DateTime.Now.ToString("HH:mm:ss"));

                cmd.Parameters.AddWithValue("@nis",
                txtCari.Text.Trim());

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Pengambilan berhasil!");

                LoadData();
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