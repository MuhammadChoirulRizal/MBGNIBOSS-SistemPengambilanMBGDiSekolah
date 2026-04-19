using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MBGNIBOSS
{
    public partial class AdminForm : Form
    {
        SqlConnection conn = new SqlConnection(
        @"Data Source=LAPTOP-5LMNPAS3\CHOY;Initial Catalog=DB_MBG;Integrated Security=True");

        public AdminForm()
        {
            InitializeComponent();
        }

        // ================= LOAD DATA =================
        void LoadData()
        {
            try
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
                "SELECT ID,NIS,Nama,Kelas,Alergi,Tanggal,Jam,Status FROM Pengambilan", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                dataGridView1.Columns["ID"].Visible = false;

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        void LoadStatistik()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                txtTotalData.Text = new SqlCommand(
                "SELECT COUNT(*) FROM Pengambilan", conn)
                .ExecuteScalar().ToString();

                txtSudah.Text = new SqlCommand(
                "SELECT COUNT(*) FROM Pengambilan WHERE Status='Sudah Diambil'", conn)
                .ExecuteScalar().ToString();

                txtBelum.Text = new SqlCommand(
                "SELECT COUNT(*) FROM Pengambilan WHERE Status='Belum Diambil'", conn)
                .ExecuteScalar().ToString();

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ================= UPDATE =================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                "UPDATE Pengambilan SET " +
                "Nama=@nama, Kelas=@kelas, Alergi=@alergi, Status=@status " +
                "WHERE NIS=@nis", conn);

                cmd.Parameters.AddWithValue("@nis", txtNIS.Text.Trim());
                cmd.Parameters.AddWithValue("@nama", txtNama.Text.Trim());
                cmd.Parameters.AddWithValue("@kelas", txtKelas.Text.Trim());
                cmd.Parameters.AddWithValue("@alergi", txtAlergi.Text.Trim());
                cmd.Parameters.AddWithValue("@status", txtStatus.Text.Trim());

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Data berhasil diupdate");

                LoadData();
                ClearForm();
                LoadStatistik();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }


        // ================= HAPUS =================
        private void btnHapus_Click(object sender, EventArgs e)
        {
            DialogResult jawab = MessageBox.Show(
                "Yakin ingin menghapus data?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (jawab == DialogResult.No)
                return;
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                "DELETE FROM Pengambilan WHERE NIS=@nis", conn);

                cmd.Parameters.AddWithValue("@nis", txtNIS.Text.Trim());

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Data berhasil dihapus");

                LoadData();
                ClearForm();
                LoadStatistik();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        // ================= CARI =================
        private void btnCari_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
                "SELECT * FROM Pengambilan WHERE NIS LIKE @nis", conn);

                da.SelectCommand.Parameters.AddWithValue(
                "@nis", "%" + txtCariNIS.Text.Trim() + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                conn.Close();

                // kalau data ditemukan
                if (dt.Rows.Count > 0)
                {
                    txtNIS.Text = dt.Rows[0]["NIS"].ToString();
                    txtNama.Text = dt.Rows[0]["Nama"].ToString();
                    txtKelas.Text = dt.Rows[0]["Kelas"].ToString();
                    txtAlergi.Text = dt.Rows[0]["Alergi"].ToString();
                    txtStatus.Text = dt.Rows[0]["Status"].ToString();
                }
                else
                {
                    MessageBox.Show("Data tidak ditemukan");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        // ================= RESET STATUS =================
        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult jawab = MessageBox.Show(
"Yakin ingin Mereset data?",
"Konfirmasi",
MessageBoxButtons.YesNo,
MessageBoxIcon.Question);

            if (jawab == DialogResult.No)
                return;
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                "UPDATE Pengambilan SET Status='Belum Diambil'", conn);

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Reset berhasil");
                LoadData();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        // ================= LOAD BUTTON =================
        private void btnLoad_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand(
            "SELECT * FROM Pengambilan", conn);

            SqlDataReader rd = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(rd);

            dataGridView1.DataSource = dt;

            conn.Close();
        }

     
    }
}
