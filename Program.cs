using Game28.Game;
using System;
using System.Windows.Forms;

namespace Game28
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           // Application.Run(new FrmMain());

            Application.Run(new Form1());
        }
    }
}
