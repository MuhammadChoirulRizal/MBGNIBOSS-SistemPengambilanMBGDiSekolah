using MBGNIBOSS;
using System;
using System.Windows.Forms;

namespace NamaProjectKamu
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Tambahkan ini
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HalamanUtama());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}