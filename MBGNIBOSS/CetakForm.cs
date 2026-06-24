using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MBGNIBOSS
{
    public partial class CetakForm : Form
    {
        SqlConnection conn = new SqlConnection(
        @"Data Source=LAPTOP-5LMNPAS3\CHOY;Initial Catalog=DB_MBG;User ID=sa;Password=123;TrustServerCertificate=True;");

        SqlDataAdapter da;
        DataTable dtHasil;

        string kelas;
        DateTime tanggal;

        public CetakForm(string Kelas, DateTime Tanggal)
        {
            InitializeComponent();
            kelas = Kelas;
            tanggal = Tanggal;
        }

        private void CetakForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_ReportMBG", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inKelas", kelas);
                cmd.Parameters.AddWithValue("@inTanggal", tanggal.Date);

                da = new SqlDataAdapter(cmd);
                dtHasil = new DataTable();
                da.Fill(dtHasil);

                conn.Close();

                // DEBUG - cek data
                MessageBox.Show("Jumlah data: " + dtHasil.Rows.Count +
                                "\nKelas: " + kelas +
                                "\nTanggal: " + tanggal.Date);

                List<PengambilanData> listData = new List<PengambilanData>();
                foreach (DataRow row in dtHasil.Rows)
                {
                    listData.Add(new PengambilanData
                    {
                        NIS = row["NIS"].ToString(),
                        Nama = row["Nama"].ToString(),
                        Kelas = row["Kelas"].ToString(),
                        Alergi = row["Alergi"].ToString(),
                        Status = row["Status"].ToString(),
                        Tanggal = Convert.ToDateTime(row["Tanggal"]),
                        JamMulai = row["JamMulai"].ToString(),
                        JamSelesai = row["JamSelesai"].ToString()
                    });
                }

                RekapMBG listReport = new RekapMBG();
                listReport.SetDataSource(listData);

                crystalReportViewer1.ReportSource = listReport;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }
    }
}