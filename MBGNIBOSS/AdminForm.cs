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
      
    }
}
