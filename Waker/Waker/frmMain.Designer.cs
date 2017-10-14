namespace Waker
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.cmdPlay = new System.Windows.Forms.Button();
            this.cmdSNZ = new System.Windows.Forms.Button();
            this.tMain = new System.Windows.Forms.Timer(this.components);
            this.cmdStop = new System.Windows.Forms.Button();
            this.tSnooze = new System.Windows.Forms.Timer(this.components);
            this.cnt = new System.Windows.Forms.GroupBox();
            this.cmdHide = new System.Windows.Forms.Button();
            this.tMover = new System.Windows.Forms.Timer(this.components);
            this.lbt = new System.Windows.Forms.Label();
            this.cnt.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Alert time?";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTime.ForeColor = System.Drawing.Color.LightBlue;
            this.txtTime.Location = new System.Drawing.Point(85, 42);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(74, 20);
            this.txtTime.TabIndex = 1;
            this.txtTime.Text = "07:00";
            this.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtFile
            // 
            this.txtFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtFile.ForeColor = System.Drawing.Color.LightBlue;
            this.txtFile.Location = new System.Drawing.Point(85, 68);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(74, 20);
            this.txtFile.TabIndex = 3;
            this.txtFile.Text = "c:\\waker.mp3";
            this.txtFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alert sound?";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Location = new System.Drawing.Point(6, 19);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(112, 17);
            this.chkEnabled.TabIndex = 4;
            this.chkEnabled.Text = "Enable ToxWaker";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // cmdPlay
            // 
            this.cmdPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdPlay.ForeColor = System.Drawing.Color.LightBlue;
            this.cmdPlay.Location = new System.Drawing.Point(6, 94);
            this.cmdPlay.Name = "cmdPlay";
            this.cmdPlay.Size = new System.Drawing.Size(73, 23);
            this.cmdPlay.TabIndex = 5;
            this.cmdPlay.Text = "Play";
            this.cmdPlay.UseVisualStyleBackColor = true;
            this.cmdPlay.Click += new System.EventHandler(this.cmdPlay_Click);
            // 
            // cmdSNZ
            // 
            this.cmdSNZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSNZ.ForeColor = System.Drawing.Color.LightBlue;
            this.cmdSNZ.Location = new System.Drawing.Point(6, 123);
            this.cmdSNZ.Name = "cmdSNZ";
            this.cmdSNZ.Size = new System.Drawing.Size(153, 23);
            this.cmdSNZ.TabIndex = 7;
            this.cmdSNZ.Text = "Snooze 1 minute";
            this.cmdSNZ.UseVisualStyleBackColor = true;
            this.cmdSNZ.Click += new System.EventHandler(this.cmdSNZ_Click);
            // 
            // tMain
            // 
            this.tMain.Enabled = true;
            this.tMain.Interval = 1000;
            this.tMain.Tick += new System.EventHandler(this.tMain_Tick);
            // 
            // cmdStop
            // 
            this.cmdStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdStop.ForeColor = System.Drawing.Color.LightBlue;
            this.cmdStop.Location = new System.Drawing.Point(85, 94);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(74, 23);
            this.cmdStop.TabIndex = 8;
            this.cmdStop.Text = "Stop";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // tSnooze
            // 
            this.tSnooze.Interval = 60000;
            this.tSnooze.Tick += new System.EventHandler(this.tSnooze_Tick);
            // 
            // cnt
            // 
            this.cnt.Controls.Add(this.cmdHide);
            this.cnt.Controls.Add(this.chkEnabled);
            this.cnt.Controls.Add(this.cmdStop);
            this.cnt.Controls.Add(this.label1);
            this.cnt.Controls.Add(this.cmdSNZ);
            this.cnt.Controls.Add(this.txtTime);
            this.cnt.Controls.Add(this.cmdPlay);
            this.cnt.Controls.Add(this.label2);
            this.cnt.Controls.Add(this.txtFile);
            this.cnt.ForeColor = System.Drawing.Color.LightBlue;
            this.cnt.Location = new System.Drawing.Point(12, 12);
            this.cnt.Name = "cnt";
            this.cnt.Size = new System.Drawing.Size(165, 152);
            this.cnt.TabIndex = 9;
            this.cnt.TabStop = false;
            this.cnt.Text = "ToxWaker Control Panel";
            this.cnt.Visible = false;
            // 
            // cmdHide
            // 
            this.cmdHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdHide.ForeColor = System.Drawing.Color.LightBlue;
            this.cmdHide.Location = new System.Drawing.Point(148, 9);
            this.cmdHide.Name = "cmdHide";
            this.cmdHide.Size = new System.Drawing.Size(13, 10);
            this.cmdHide.TabIndex = 10;
            this.cmdHide.UseVisualStyleBackColor = true;
            this.cmdHide.Click += new System.EventHandler(this.cmdHide_Click);
            // 
            // tMover
            // 
            this.tMover.Interval = 10000;
            this.tMover.Tick += new System.EventHandler(this.tMover_Tick);
            // 
            // lbt
            // 
            this.lbt.AutoSize = true;
            this.lbt.Font = new System.Drawing.Font("Lucida Console", 140.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbt.ForeColor = System.Drawing.Color.Gray;
            this.lbt.Location = new System.Drawing.Point(311, 163);
            this.lbt.Name = "lbt";
            this.lbt.Size = new System.Drawing.Size(991, 187);
            this.lbt.TabIndex = 10;
            this.lbt.Text = "88:88:88";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(537, 328);
            this.Controls.Add(this.lbt);
            this.Controls.Add(this.cnt);
            this.ForeColor = System.Drawing.Color.LightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMain";
            this.Text = "ToxWaker";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            this.cnt.ResumeLayout(false);
            this.cnt.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.Button cmdPlay;
        private System.Windows.Forms.Button cmdSNZ;
        private System.Windows.Forms.Timer tMain;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.Timer tSnooze;
        private System.Windows.Forms.GroupBox cnt;
        private System.Windows.Forms.Button cmdHide;
        private System.Windows.Forms.Timer tMover;
        private System.Windows.Forms.Label lbt;
    }
}

