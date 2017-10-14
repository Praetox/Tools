namespace Batchify
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.cmdSource = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.chkCapsMatters = new System.Windows.Forms.CheckBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.chkCloseConsoleWhenDone = new System.Windows.Forms.CheckBox();
            this.txtScript = new System.Windows.Forms.TextBox();
            this.cmdDestination = new System.Windows.Forms.Button();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.cmdExecute = new System.Windows.Forms.Button();
            this.cmdHelp = new System.Windows.Forms.Button();
            this.cmdSafeToUnsafe = new System.Windows.Forms.Button();
            this.cmdUnsafeToSafe = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.ttip = new System.Windows.Forms.ToolTip(this.components);
            this.panel7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSource
            // 
            this.cmdSource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.cmdSource.ForeColor = System.Drawing.Color.Azure;
            this.cmdSource.Location = new System.Drawing.Point(281, 25);
            this.cmdSource.Name = "cmdSource";
            this.cmdSource.Size = new System.Drawing.Size(28, 24);
            this.cmdSource.TabIndex = 1;
            this.cmdSource.Text = "...";
            this.cmdSource.UseVisualStyleBackColor = false;
            this.cmdSource.Click += new System.EventHandler(this.cmdSource_Click);
            // 
            // txtSource
            // 
            this.txtSource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSource.ForeColor = System.Drawing.Color.Azure;
            this.txtSource.Location = new System.Drawing.Point(7, 25);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(268, 24);
            this.txtSource.TabIndex = 0;
            this.ttip.SetToolTip(this.txtSource, "The folder containing files to work on.");
            // 
            // chkCapsMatters
            // 
            this.chkCapsMatters.AutoSize = true;
            this.chkCapsMatters.Location = new System.Drawing.Point(7, 54);
            this.chkCapsMatters.Name = "chkCapsMatters";
            this.chkCapsMatters.Size = new System.Drawing.Size(215, 19);
            this.chkCapsMatters.TabIndex = 3;
            this.chkCapsMatters.Text = "Differ between big and small letters";
            this.ttip.SetToolTip(this.chkCapsMatters, "Check this for case-sensitivity.");
            this.chkCapsMatters.UseVisualStyleBackColor = true;
            // 
            // txtFilter
            // 
            this.txtFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilter.ForeColor = System.Drawing.Color.Azure;
            this.txtFilter.Location = new System.Drawing.Point(119, 25);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(190, 24);
            this.txtFilter.TabIndex = 2;
            // 
            // cbFilter
            // 
            this.cbFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.cbFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFilter.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilter.ForeColor = System.Drawing.Color.Azure;
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "start with",
            "contain",
            "end with"});
            this.cbFilter.Location = new System.Drawing.Point(7, 25);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(106, 23);
            this.cbFilter.TabIndex = 0;
            this.cbFilter.Text = "end with";
            // 
            // chkCloseConsoleWhenDone
            // 
            this.chkCloseConsoleWhenDone.AutoSize = true;
            this.chkCloseConsoleWhenDone.Checked = true;
            this.chkCloseConsoleWhenDone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCloseConsoleWhenDone.Location = new System.Drawing.Point(7, 262);
            this.chkCloseConsoleWhenDone.Name = "chkCloseConsoleWhenDone";
            this.chkCloseConsoleWhenDone.Size = new System.Drawing.Size(205, 19);
            this.chkCloseConsoleWhenDone.TabIndex = 4;
            this.chkCloseConsoleWhenDone.Text = "Close console window when done";
            this.ttip.SetToolTip(this.chkCloseConsoleWhenDone, "Uncheck this if you wish to skim through the\r\nconsole window once everything is d" +
                    "one.");
            this.chkCloseConsoleWhenDone.UseVisualStyleBackColor = true;
            // 
            // txtScript
            // 
            this.txtScript.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtScript.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScript.Font = new System.Drawing.Font("Lucida Sans Unicode", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScript.ForeColor = System.Drawing.Color.Azure;
            this.txtScript.Location = new System.Drawing.Point(7, 25);
            this.txtScript.Multiline = true;
            this.txtScript.Name = "txtScript";
            this.txtScript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtScript.Size = new System.Drawing.Size(302, 231);
            this.txtScript.TabIndex = 0;
            this.txtScript.Text = "ECHO ==\\\r\nECHO ===) (§n1/§n2) §2b§3a\r\nECHO ==/\r\n\r\n";
            this.ttip.SetToolTip(this.txtScript, "These commands will be executed on all the files in the source\r\nfolder (as long a" +
                    "s they match the \"Files to include\" parameter).");
            // 
            // cmdDestination
            // 
            this.cmdDestination.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.cmdDestination.ForeColor = System.Drawing.Color.Azure;
            this.cmdDestination.Location = new System.Drawing.Point(281, 25);
            this.cmdDestination.Name = "cmdDestination";
            this.cmdDestination.Size = new System.Drawing.Size(28, 24);
            this.cmdDestination.TabIndex = 1;
            this.cmdDestination.Text = "...";
            this.cmdDestination.UseVisualStyleBackColor = false;
            this.cmdDestination.Click += new System.EventHandler(this.cmdDestination_Click);
            // 
            // txtDestination
            // 
            this.txtDestination.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtDestination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDestination.ForeColor = System.Drawing.Color.Azure;
            this.txtDestination.Location = new System.Drawing.Point(7, 25);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(268, 24);
            this.txtDestination.TabIndex = 0;
            this.ttip.SetToolTip(this.txtDestination, "The folder where the batch-file will be created.");
            // 
            // cmdExecute
            // 
            this.cmdExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.cmdExecute.ForeColor = System.Drawing.Color.Azure;
            this.cmdExecute.Location = new System.Drawing.Point(7, 25);
            this.cmdExecute.Name = "cmdExecute";
            this.cmdExecute.Size = new System.Drawing.Size(272, 23);
            this.cmdExecute.TabIndex = 2;
            this.cmdExecute.Text = "Create batch file!";
            this.ttip.SetToolTip(this.cmdExecute, "Create the batch file!");
            this.cmdExecute.UseVisualStyleBackColor = false;
            this.cmdExecute.Click += new System.EventHandler(this.cmdExecute_Click);
            // 
            // cmdHelp
            // 
            this.cmdHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.cmdHelp.ForeColor = System.Drawing.Color.Azure;
            this.cmdHelp.Location = new System.Drawing.Point(285, 25);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(24, 23);
            this.cmdHelp.TabIndex = 8;
            this.cmdHelp.Text = "?";
            this.ttip.SetToolTip(this.cmdHelp, "HALP");
            this.cmdHelp.UseVisualStyleBackColor = false;
            this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
            // 
            // cmdSafeToUnsafe
            // 
            this.cmdSafeToUnsafe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(64)))));
            this.cmdSafeToUnsafe.ForeColor = System.Drawing.Color.Azure;
            this.cmdSafeToUnsafe.Location = new System.Drawing.Point(161, 25);
            this.cmdSafeToUnsafe.Name = "cmdSafeToUnsafe";
            this.cmdSafeToUnsafe.Size = new System.Drawing.Size(148, 23);
            this.cmdSafeToUnsafe.TabIndex = 12;
            this.cmdSafeToUnsafe.Text = "Back to original";
            this.ttip.SetToolTip(this.cmdSafeToUnsafe, "Of course, once you\'re done messing with the files,\r\nyou probably want the origin" +
                    "al filenames back.");
            this.cmdSafeToUnsafe.UseVisualStyleBackColor = false;
            this.cmdSafeToUnsafe.Click += new System.EventHandler(this.cmdSafeToUnsafe_Click);
            // 
            // cmdUnsafeToSafe
            // 
            this.cmdUnsafeToSafe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(64)))));
            this.cmdUnsafeToSafe.ForeColor = System.Drawing.Color.Azure;
            this.cmdUnsafeToSafe.Location = new System.Drawing.Point(7, 25);
            this.cmdUnsafeToSafe.Name = "cmdUnsafeToSafe";
            this.cmdUnsafeToSafe.Size = new System.Drawing.Size(148, 23);
            this.cmdUnsafeToSafe.TabIndex = 11;
            this.cmdUnsafeToSafe.Text = "To safe filenames";
            this.ttip.SetToolTip(this.cmdUnsafeToSafe, resources.GetString("cmdUnsafeToSafe.ToolTip"));
            this.cmdUnsafeToSafe.UseVisualStyleBackColor = false;
            this.cmdUnsafeToSafe.Click += new System.EventHandler(this.cmdUnsafeToSafe_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.label13);
            this.panel7.Controls.Add(this.txtScript);
            this.panel7.Controls.Add(this.chkCloseConsoleWhenDone);
            this.panel7.ForeColor = System.Drawing.Color.White;
            this.panel7.Location = new System.Drawing.Point(15, 85);
            this.panel7.Margin = new System.Windows.Forms.Padding(6);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(4);
            this.panel7.Size = new System.Drawing.Size(318, 290);
            this.panel7.TabIndex = 60;
            this.panel7.Tag = "c2";
            // 
            // label13
            // 
            this.label13.ForeColor = System.Drawing.Color.Silver;
            this.label13.Location = new System.Drawing.Point(7, 4);
            this.label13.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(290, 15);
            this.label13.TabIndex = 35;
            this.label13.Tag = "c4";
            this.label13.Text = "Command(s) to execute on files";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmdSource);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtSource);
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(345, 15);
            this.panel2.Margin = new System.Windows.Forms.Padding(6);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(4);
            this.panel2.Size = new System.Drawing.Size(318, 58);
            this.panel2.TabIndex = 61;
            this.panel2.Tag = "c2";
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.Silver;
            this.label6.Location = new System.Drawing.Point(7, 4);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(302, 15);
            this.label6.TabIndex = 35;
            this.label6.Tag = "c4";
            this.label6.Text = "Source folder";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cmdDestination);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtDestination);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(345, 85);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(4);
            this.panel1.Size = new System.Drawing.Size(318, 58);
            this.panel1.TabIndex = 62;
            this.panel1.Tag = "c2";
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(7, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 15);
            this.label1.TabIndex = 35;
            this.label1.Tag = "c4";
            this.label1.Text = "Destination path";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.chkCapsMatters);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtFilter);
            this.panel3.Controls.Add(this.cbFilter);
            this.panel3.ForeColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(345, 155);
            this.panel3.Margin = new System.Windows.Forms.Padding(6);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(4);
            this.panel3.Size = new System.Drawing.Size(318, 82);
            this.panel3.TabIndex = 62;
            this.panel3.Tag = "c2";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(7, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(302, 15);
            this.label2.TabIndex = 35;
            this.label2.Tag = "c4";
            this.label2.Text = "Files to include...";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.cmdExecute);
            this.panel4.Controls.Add(this.cmdHelp);
            this.panel4.ForeColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(345, 249);
            this.panel4.Margin = new System.Windows.Forms.Padding(6);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(4);
            this.panel4.Size = new System.Drawing.Size(318, 57);
            this.panel4.TabIndex = 63;
            this.panel4.Tag = "c2";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(7, 4);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(302, 15);
            this.label3.TabIndex = 35;
            this.label3.Tag = "c4";
            this.label3.Text = "What is this I don\'t even";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.cmdSafeToUnsafe);
            this.panel5.Controls.Add(this.cmdUnsafeToSafe);
            this.panel5.ForeColor = System.Drawing.Color.White;
            this.panel5.Location = new System.Drawing.Point(345, 318);
            this.panel5.Margin = new System.Windows.Forms.Padding(6);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(4);
            this.panel5.Size = new System.Drawing.Size(318, 57);
            this.panel5.TabIndex = 64;
            this.panel5.Tag = "c2";
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(7, 4);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(302, 15);
            this.label4.TabIndex = 35;
            this.label4.Tag = "c4";
            this.label4.Text = "Safe filename tool";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbLogo
            // 
            this.pbLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(15, 15);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(318, 58);
            this.pbLogo.TabIndex = 65;
            this.pbLogo.TabStop = false;
            this.ttip.SetToolTip(this.pbLogo, "Click here to visit my website.");
            this.pbLogo.Click += new System.EventHandler(this.pbLogo_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(678, 390);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel7);
            this.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.LightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Batchify v";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdSource;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.TextBox txtScript;
        private System.Windows.Forms.Button cmdDestination;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Button cmdExecute;
        private System.Windows.Forms.Button cmdHelp;
        private System.Windows.Forms.CheckBox chkCapsMatters;
        private System.Windows.Forms.Button cmdSafeToUnsafe;
        private System.Windows.Forms.Button cmdUnsafeToSafe;
        private System.Windows.Forms.CheckBox chkCloseConsoleWhenDone;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.ToolTip ttip;
    }
}

