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


      
    }
    
}