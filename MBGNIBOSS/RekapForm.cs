using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MBGNIBOSS
{
    public partial class RekapForm : Form
    {
        SqlConnection conn = new SqlConnection(
        @"Data Source=LAPTOP-5LMNPAS3\CHOY;Initial Catalog=DB_MBG;User ID=sa;Password=123;TrustServerCertificate=True;");

        SqlDataAdapter da;
        DataTable dtHasil;

        public RekapForm()
        {
            InitializeComponent();
        }

        private void RekapForm_Load(object sender, EventArgs e)
        {
            cmbKelas.Items.Add("7");
            cmbKelas.Items.Add("8");
            cmbKelas.Items.Add("9");
            cmbKelas.SelectedIndex = 0;

            btnCetak.Enabled = false;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_ReportMBG", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inKelas", cmbKelas.Text);
                cmd.Parameters.AddWithValue("@inTanggal", dtTanggal.Value.Date);

                da = new SqlDataAdapter(cmd);
                dtHasil = new DataTable();
                da.Fill(dtHasil);

                dataGridView1.DataSource = dtHasil;
                dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

                if (dtHasil.Rows.Count > 0)
                {
                    btnCetak.Enabled = true;
                }
                else
                {
                    btnCetak.Enabled = false;
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
        private void btnCetak_Click(object sender, EventArgs e)
        {
            CetakForm frm = new CetakForm(
                cmbKelas.Text,
                dtTanggal.Value);
            frm.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}