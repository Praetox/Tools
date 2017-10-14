using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace unDupe {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }

        private void frmMain_DragOver(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }
        private void frmMain_DragDrop(object sender, DragEventArgs e) {
            object fno = e.Data.GetData(DataFormats.FileDrop);
            button1.Text = ((string[])fno)[0];
        }
        private void button1_Click(object sender, EventArgs e) {
            string[] file =
                System.IO.File.ReadAllText(button1.Text).
                Replace("\r", "").Trim('\n').Split('\n');
            button1.Text = "Loading";
            Application.DoEvents();

            Array.Sort(file);
            StringBuilder ret = new StringBuilder();
            for (int a = 1; a < file.Length; a++) {
                if (a % 100 == 0) {
                    button1.Text = a + "";
                    Application.DoEvents();
                }

                string preHash = file[a - 1].Substring(0, 32);
                string curHash = file[a].Substring(0, 32);
                string curPath = file[a].Substring(34);
                if (curHash == preHash) {
                    ret.Append("del " + curPath + "\r\n");
                }
            }
            System.IO.File.WriteAllText(
                "a.bat", ret.ToString());
            button1.Text = "Done!";
        }
    }
}
