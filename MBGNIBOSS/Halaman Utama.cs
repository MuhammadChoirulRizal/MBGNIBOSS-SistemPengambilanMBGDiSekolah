using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MBGNIBOSS
{
    public partial class HalamanUtama : Form
    {
        public HalamanUtama()
        {
            InitializeComponent();
        }
       

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Buka form pilihan login
            Login formLogin = new Login();
            formLogin.Show();
            this.Hide(); // Sembunyikan halaman depan
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
 }

