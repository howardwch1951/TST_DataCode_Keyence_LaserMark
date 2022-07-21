using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LaserMark
{
    static class Program
    {
        private static System.Threading.Mutex mutex;

        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mutex = new System.Threading.Mutex(true, "Laser");
            if (mutex.WaitOne(0, false))
            {
                Application.Run(new Form1());
            }
            else
            {
                MessageBox.Show("程式已經在執行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
    }
}
