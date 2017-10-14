/*  SMSuite -- StepMania MIGS calculator and stats display
 *  Copyright (C) 2009  Praetox (http://praetox.com/)
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SMSuite {
    public partial class frmWaitSM : Form {
        public frmWaitSM() {
            InitializeComponent();
        }
        public Process smProc = null;

        Timer t = new Timer();
        private void frmWaitSM_Load(object sender, EventArgs e) {
            t.Tick += new EventHandler(t_Tick);
            t.Interval = 500; t.Start();
            t_Tick(sender, e);
        }

        void t_Tick(object sender, EventArgs e) {
            Process[] smProcs = Process.
                GetProcessesByName("stepmania");
            if (smProcs.Length > 0) {
                t.Stop();
                smProc = smProcs[0];
                this.Close();
            }
        }
    }
}
