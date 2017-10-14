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

namespace SMSuite {
    public partial class frmSetMode : Form {
        public frmSetMode() {
            InitializeComponent();
        }
        public SM.Mode mode = SM.Mode.None;

        private void frmSetMode_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.D1 ||
                e.KeyCode == Keys.NumPad1)
                mode = SM.Mode.P1;

            if (e.KeyCode == Keys.D2 ||
                e.KeyCode == Keys.NumPad2)
                mode = SM.Mode.P2;

            if (e.KeyCode == Keys.D3 ||
                e.KeyCode == Keys.NumPad3)
                mode = SM.Mode.VS;

            if (mode != SM.Mode.None) {
                this.Close();
            }
        }
    }
}
