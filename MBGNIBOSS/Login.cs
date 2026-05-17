using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MBGNIBOSS
{
    public partial class Login : Form
    {
        SqlConnection conn = new SqlConnection(
        @"Data Source=LAPTOP-5LMNPAS3\CHOY;
        Initial Catalog=DB_MBG;
        Integrated Security=True;");

        string role = "";

        public Login()
        {
            InitializeComponent();
        }

        // ================= FORM LOAD =================
        private void Login_Load(object sender, EventArgs e)
        {
            lblLogin.Visible = false;
            txtPassword.Visible = false;
            btnLogin.Visible = false;
            
        }

        // ================= ADMIN =================
      

        
      
    }
}