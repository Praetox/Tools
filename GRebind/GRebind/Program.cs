using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GRebind
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
        public static bool Reg_DoesExist(string regPath)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser;
                key = key.OpenSubKey(regPath, true);
                long lol = key.SubKeyCount;
                return true;
            }
            catch
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser;
                key.CreateSubKey(regPath);
                return false;
            }
        }
        public static string Reg_Access(string sKey, string sVal)
        {
            Microsoft.Win32.RegistryKey rKey = Microsoft.Win32.Registry.CurrentUser;
            Reg_DoesExist("Software\\Praetox Technologies\\GRebind");
            rKey = rKey.OpenSubKey("Software\\Praetox Technologies\\GRebind", true);
            if (sVal != "") rKey.SetValue(sKey, sVal);
            string sRet = "";
            try
            {
                sRet = rKey.GetValue(sKey).ToString();
            }
            catch { }
            rKey.Close(); return sRet;
        }
    }
}
