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
            try
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
                "SELECT NIS,Nama,Kelas,Alergi,Status,Tanggal,Jam " +
                "FROM Pengambilan WHERE NIS=@nis", conn);

                da.SelectCommand.Parameters.AddWithValue("@nis", txtNIS.Text.Trim());

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

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
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT S.NIS, S.Nama, S.Kelas, S.Alergi, P.StatusAmbil " +
                    "FROM Siswa S LEFT JOIN Pengambilan P ON S.NIS = P.NIS " +
                    "WHERE S.NIS = @nis", conn);

                da.SelectCommand.Parameters.AddWithValue("@nis", txtNIS.Text.Trim());

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Data tidak ditemukan");
                    dataGridView1.DataSource = null;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

    }
}