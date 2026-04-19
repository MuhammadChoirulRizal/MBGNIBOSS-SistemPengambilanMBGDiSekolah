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
     
    }
}
