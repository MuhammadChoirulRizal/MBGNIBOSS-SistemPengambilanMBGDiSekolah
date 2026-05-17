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
        void LoadStatistik()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                lblTotalData.Text = new SqlCommand(
                "SELECT COUNT(*) FROM vwPengambilan", conn)
                .ExecuteScalar().ToString();

                lblSudah.Text = new SqlCommand(
                "SELECT COUNT(*) FROM vwPengambilan WHERE Status='Sudah Diambil'", conn)
                .ExecuteScalar().ToString();

                lblBelum.Text = new SqlCommand(
                "SELECT COUNT(*) FROM vwPengambilan WHERE Status='Belum Diambil'", conn)
                .ExecuteScalar().ToString();

                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        // ================= FORM LOAD =================
        private void AdminForm_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadJadwal();
            LoadStatistik();
            LoadStokKelas();

            dataGridView1.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView2.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;
            cmbKelasStok.Items.Add("7");
            cmbKelasStok.Items.Add("8");
            cmbKelasStok.Items.Add("9");
            cmbKelas.Items.Add("7");
            cmbKelas.Items.Add("8");
            cmbKelas.Items.Add("9");
            cmbKelasJadwal.Items.Add("7");
            cmbKelasJadwal.Items.Add("8");
            cmbKelasJadwal.Items.Add("9");

            bindingNavigator1.BindingSource = bs;
        }

        // ================= CLEAR FORM =================
        void ClearForm()
        {
            txtNIS.Clear();
            txtNama.Clear();

            cmbKelas.SelectedIndex = -1;

            txtAlergi.Clear();
            txtStatus.Clear();

            txtNIS.Focus();
        }

        // ================= TAMBAH DATA =================
        private void btnTambah_Click(object sender, EventArgs e)
        {
            DialogResult jawab = MessageBox.Show(
            "Apakah ingin menambah data?",
            "Konfirmasi",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (jawab == DialogResult.No)
                return;

            try
            {
                if (txtNIS.Text.Trim() == "" ||
                    txtNama.Text.Trim() == "")
                {
                    MessageBox.Show("Data belum lengkap!");
                    return;
                }

                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                SqlCommand cmd = new SqlCommand(
"spInsertPengambilan", conn);

                cmd.CommandType =
                CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                "@NIS",
                txtNIS.Text);

                cmd.Parameters.AddWithValue(
                "@Nama",
                txtNama.Text);

                cmd.Parameters.AddWithValue(
                "@Kelas",
                cmbKelas.Text);

                cmd.Parameters.AddWithValue(
                "@Alergi",
                txtAlergi.Text);

                cmd.Parameters.AddWithValue(
                "@Status",
                "Belum Diambil");

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Data berhasil ditambah!");

                LoadData();
                LoadStatistik();
                ClearForm();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        // ================= UPDATE =================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStatus.Text.Trim() == "")
                {
                    MessageBox.Show("Status wajib diisi!");
                    return;
                }

                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                SqlCommand cmd = new SqlCommand(
"spUpdatePengambilan",
conn);

                cmd.CommandType =
                CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                "@NIS",
                txtNIS.Text);

                cmd.Parameters.AddWithValue(
                "@Nama",
                txtNama.Text);

                cmd.Parameters.AddWithValue(
                "@Kelas",
                cmbKelas.Text);

                cmd.Parameters.AddWithValue(
                "@Alergi",
                txtAlergi.Text);

                cmd.Parameters.AddWithValue(
                "@Status",
                txtStatus.Text);

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Data berhasil diupdate!");

                LoadData();
                LoadStatistik();
                ClearForm();
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
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                SqlCommand cmd = new SqlCommand(
"spDeletePengambilan",
conn);

                cmd.CommandType =
                CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                "@NIS",
                txtNIS.Text);

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Data berhasil dihapus!");

                LoadData();
                LoadStatistik();
                ClearForm();
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
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();
                SqlDataAdapter da =
new SqlDataAdapter(
"spSearchPengambilan",
conn);

                da.SelectCommand.CommandType =
                CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue(
                "@NIS",
                txtCariNIS.Text);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                conn.Close();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Data tidak ditemukan!");
                }
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
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ================= GRID CLICK =================
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtNIS.Text =
                dataGridView1.Rows[e.RowIndex].Cells["NIS"].Value.ToString();

                txtNama.Text =
                dataGridView1.Rows[e.RowIndex].Cells["Nama"].Value.ToString();

                cmbKelas.Text =
                dataGridView1.Rows[e.RowIndex].Cells["Kelas"].Value.ToString();

                txtAlergi.Text =
                dataGridView1.Rows[e.RowIndex].Cells["Alergi"].Value.ToString();

                txtStatus.Text =
                dataGridView1.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            }
        }

        // ================= RESET =================
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult jawab = MessageBox.Show(
                "Reset seluruh data harian?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (jawab == DialogResult.No)
                    return;

                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                // ================= RESET STATUS SISWA =================
                SqlCommand resetStatus = new SqlCommand(
                "UPDATE Pengambilan " +
                "SET Status='Belum Diambil'", conn);

                resetStatus.ExecuteNonQuery();

                // ================= RESET TOTAL STOK =================
                SqlCommand resetTotal = new SqlCommand(
                "UPDATE StokMBG SET Jumlah=300 WHERE ID=1", conn);

                resetTotal.ExecuteNonQuery();

                // ================= RESET STOK KELAS =================
                SqlCommand resetKelas = new SqlCommand(
                "UPDATE StokKelas SET Jumlah=100", conn);

                resetKelas.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Reset harian berhasil!");

                // ================= REFRESH =================
                LoadData();
                LoadStatistik();

                LoadStokKelas();
                ClearForm();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        // ================= LOGOUT =================
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login f = new Login();
            f.Show();
            this.Hide();
        }

        // ================= VALIDASI NAMA =================
        private void txtNama_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                e.KeyChar != (char)8)
            {
                MessageBox.Show("Nama hanya boleh huruf!");
                e.Handled = true;
            }
        }

        // ================= VALIDASI NIS =================
        private void txtNIS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != (char)8)
            {
                MessageBox.Show("NIS hanya boleh angka!");
                e.Handled = true;
            }
        }
        private void btnTambahJadwal_Click(
 object sender,
 EventArgs e)
        {
            try
            {
                if (cmbKelasJadwal.Text == "")
                {
                    MessageBox.Show(
                    "Pilih kelas terlebih dahulu!");
                    return;
                }

                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                SqlCommand cmd = new SqlCommand(
    "spInsertJadwal",
    conn);

                cmd.CommandType =
                CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                "@Kelas",
                cmbKelasJadwal.Text);

                cmd.Parameters.AddWithValue(
                "@Tanggal",
                dtTanggal.Value.Date);

                cmd.Parameters.AddWithValue(
                "@JamMulai",
                dtJamMulai.Value.ToString("HH:mm"));

                cmd.Parameters.AddWithValue(
                "@JamSelesai",
                dtJamSelesai.Value.ToString("HH:mm"));

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show(
                "Jadwal berhasil ditambah!");

                LoadJadwal();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void btnUpdateJadwal_Click(
object sender,
EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                SqlCommand cmd = new SqlCommand(
 "spUpdateJadwal",
 conn);

                cmd.CommandType =
                CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                "@Kelas",
                cmbKelasJadwal.Text);

                cmd.Parameters.AddWithValue(
                "@Tanggal",
                dtTanggal.Value.Date);

                cmd.Parameters.AddWithValue(
                "@JamMulai",
                dtJamMulai.Value.ToString("HH:mm"));

                cmd.Parameters.AddWithValue(
                "@JamSelesai",
                dtJamSelesai.Value.ToString("HH:mm"));

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show(
                "Jadwal berhasil diupdate!");

                LoadJadwal();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void btnHapusJadwal_Click(
object sender,
EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                SqlCommand cmd = new SqlCommand(
 "spDeleteJadwal",
 conn);

                cmd.CommandType =
                CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                "@Kelas",
                cmbKelasJadwal.Text);

                cmd.Parameters.AddWithValue(
                "@Tanggal",
                dtTanggal.Value.Date);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show(
                "Jadwal berhasil dihapus!");

                LoadJadwal();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView2_CellClick(
object sender,
DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                cmbKelasJadwal.Text =
                dataGridView2.Rows[e.RowIndex]
                .Cells["Kelas"].Value.ToString();

                dtTanggal.Value =
                Convert.ToDateTime(
                dataGridView2.Rows[e.RowIndex]
                .Cells["Tanggal"].Value);

                dtJamMulai.Text =
                dataGridView2.Rows[e.RowIndex]
                .Cells["JamMulai"].Value.ToString();

                dtJamSelesai.Text =
                dataGridView2.Rows[e.RowIndex]
                .Cells["JamSelesai"].Value.ToString();
            }
        }
        private void btnUpdateStok_Click(
 object sender,
 EventArgs e)
        {
            try
            {
                if (cmbKelasStok.Text == "" ||
                    txtTambahStok.Text == "")
                {
                    MessageBox.Show(
                    "Lengkapi data terlebih dahulu!");
                    return;
                }

                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                // update stok kelas
                SqlCommand cmd = new SqlCommand(
                "UPDATE StokKelas " +
                "SET Jumlah=@jumlah " +
                "WHERE Kelas=@kelas",
                conn);

                cmd.Parameters.AddWithValue(
                "@jumlah",
                Convert.ToInt32(txtTambahStok.Text));

                cmd.Parameters.AddWithValue(
                "@kelas",
                cmbKelasStok.Text);

                cmd.ExecuteNonQuery();

                // hitung ulang total stok
                SqlCommand total =
                new SqlCommand(
                "SELECT SUM(Jumlah) FROM StokKelas",
                conn);

                int totalStok =
                Convert.ToInt32(total.ExecuteScalar());

                SqlCommand updateTotal =
                new SqlCommand(
                "UPDATE StokMBG " +
                "SET Jumlah=@jumlah " +
                "WHERE ID=1",
                conn);

                updateTotal.Parameters.AddWithValue(
                "@jumlah",
                totalStok);

                updateTotal.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show(
                "Stok berhasil diupdate!");

                LoadStokKelas();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }

        }

    }
}