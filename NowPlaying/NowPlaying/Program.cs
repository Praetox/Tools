using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NowPlaying {
    static class Program {
        public static System.Text.Encoding enc = System.Text.Encoding.Unicode;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
