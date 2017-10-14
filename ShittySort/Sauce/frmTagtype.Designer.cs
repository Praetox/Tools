namespace ShittySort {
    partial class frmTagtype {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTagtype));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gJnk = new System.Windows.Forms.Button();
            this.gArt = new System.Windows.Forms.Button();
            this.gChr = new System.Windows.Forms.Button();
            this.gSrc = new System.Windows.Forms.Button();
            this.gGen = new System.Windows.Forms.Button();
            this.gtUnk = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gtGen = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gtChr = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.gtArt = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.gtSrc = new System.Windows.Forms.ListBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.gtJnk = new System.Windows.Forms.ListBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.gStatus = new System.Windows.Forms.Label();
            this.gCancel = new System.Windows.Forms.Button();
            this.gLoadAll = new System.Windows.Forms.Button();
            this.gSaveCat = new System.Windows.Forms.Button();
            this.gContinue = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gJnk);
            this.groupBox1.Controls.Add(this.gArt);
            this.groupBox1.Controls.Add(this.gChr);
            this.groupBox1.Controls.Add(this.gSrc);
            this.groupBox1.Controls.Add(this.gGen);
            this.groupBox1.Controls.Add(this.gtUnk);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 350);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "0 // Tags // Unsorted (?)";
            // 
            // gJnk
            // 
            this.gJnk.Location = new System.Drawing.Point(178, 315);
            this.gJnk.Name = "gJnk";
            this.gJnk.Size = new System.Drawing.Size(37, 29);
            this.gJnk.TabIndex = 10;
            this.gJnk.Text = "Jnk";
            this.gJnk.UseVisualStyleBackColor = true;
            this.gJnk.Click += new System.EventHandler(this.gJnk_Click);
            // 
            // gArt
            // 
            this.gArt.Location = new System.Drawing.Point(135, 315);
            this.gArt.Name = "gArt";
            this.gArt.Size = new System.Drawing.Size(37, 29);
            this.gArt.TabIndex = 9;
            this.gArt.Text = "Art";
            this.gArt.UseVisualStyleBackColor = true;
            this.gArt.Click += new System.EventHandler(this.gArt_Click);
            // 
            // gChr
            // 
            this.gChr.Location = new System.Drawing.Point(92, 315);
            this.gChr.Name = "gChr";
            this.gChr.Size = new System.Drawing.Size(37, 29);
            this.gChr.TabIndex = 8;
            this.gChr.Text = "Chr";
            this.gChr.UseVisualStyleBackColor = true;
            this.gChr.Click += new System.EventHandler(this.gChr_Click);
            // 
            // gSrc
            // 
            this.gSrc.Location = new System.Drawing.Point(49, 315);
            this.gSrc.Name = "gSrc";
            this.gSrc.Size = new System.Drawing.Size(37, 29);
            this.gSrc.TabIndex = 7;
            this.gSrc.Text = "Src";
            this.gSrc.UseVisualStyleBackColor = true;
            this.gSrc.Click += new System.EventHandler(this.gSrc_Click);
            // 
            // gGen
            // 
            this.gGen.Location = new System.Drawing.Point(6, 315);
            this.gGen.Name = "gGen";
            this.gGen.Size = new System.Drawing.Size(37, 29);
            this.gGen.TabIndex = 6;
            this.gGen.Text = "Gen";
            this.gGen.UseVisualStyleBackColor = true;
            this.gGen.Click += new System.EventHandler(this.gGen_Click);
            // 
            // gtUnk
            // 
            this.gtUnk.FormattingEnabled = true;
            this.gtUnk.Location = new System.Drawing.Point(6, 19);
            this.gtUnk.Name = "gtUnk";
            this.gtUnk.Size = new System.Drawing.Size(210, 290);
            this.gtUnk.TabIndex = 1;
            this.gtUnk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gtUnk_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gtGen);
            this.groupBox2.Location = new System.Drawing.Point(242, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(224, 172);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "1 // Tags // General (?)";
            // 
            // gtGen
            // 
            this.gtGen.FormattingEnabled = true;
            this.gtGen.Location = new System.Drawing.Point(6, 19);
            this.gtGen.Name = "gtGen";
            this.gtGen.Size = new System.Drawing.Size(212, 147);
            this.gtGen.TabIndex = 1;
            this.gtGen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gtGen_KeyDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gtChr);
            this.groupBox3.Location = new System.Drawing.Point(242, 190);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(224, 172);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "3 // Tags // Characters (?)";
            // 
            // gtChr
            // 
            this.gtChr.FormattingEnabled = true;
            this.gtChr.Location = new System.Drawing.Point(6, 19);
            this.gtChr.Name = "gtChr";
            this.gtChr.Size = new System.Drawing.Size(212, 147);
            this.gtChr.TabIndex = 1;
            this.gtChr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gtChr_KeyDown);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.gtArt);
            this.groupBox4.Location = new System.Drawing.Point(472, 190);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(224, 172);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "4 // Tags // Artists (?)";
            // 
            // gtArt
            // 
            this.gtArt.FormattingEnabled = true;
            this.gtArt.Location = new System.Drawing.Point(6, 19);
            this.gtArt.Name = "gtArt";
            this.gtArt.Size = new System.Drawing.Size(212, 147);
            this.gtArt.TabIndex = 1;
            this.gtArt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gtArt_KeyDown);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.gtSrc);
            this.groupBox5.Location = new System.Drawing.Point(472, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(224, 172);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "2 // Tags // Source (?)";
            // 
            // gtSrc
            // 
            this.gtSrc.FormattingEnabled = true;
            this.gtSrc.Location = new System.Drawing.Point(6, 19);
            this.gtSrc.Name = "gtSrc";
            this.gtSrc.Size = new System.Drawing.Size(212, 147);
            this.gtSrc.TabIndex = 1;
            this.gtSrc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gtSrc_KeyDown);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.gtJnk);
            this.groupBox6.Location = new System.Drawing.Point(702, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(224, 172);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "5 // Junk (?)";
            // 
            // gtJnk
            // 
            this.gtJnk.FormattingEnabled = true;
            this.gtJnk.Location = new System.Drawing.Point(6, 19);
            this.gtJnk.Name = "gtJnk";
            this.gtJnk.Size = new System.Drawing.Size(212, 147);
            this.gtJnk.TabIndex = 1;
            this.gtJnk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gtJnk_KeyDown);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.gStatus);
            this.groupBox7.Controls.Add(this.gCancel);
            this.groupBox7.Controls.Add(this.gLoadAll);
            this.groupBox7.Controls.Add(this.gSaveCat);
            this.groupBox7.Controls.Add(this.gContinue);
            this.groupBox7.Location = new System.Drawing.Point(702, 190);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(224, 172);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Control";
            // 
            // gStatus
            // 
            this.gStatus.BackColor = System.Drawing.SystemColors.Control;
            this.gStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gStatus.Location = new System.Drawing.Point(6, 150);
            this.gStatus.Name = "gStatus";
            this.gStatus.Size = new System.Drawing.Size(212, 16);
            this.gStatus.TabIndex = 6;
            this.gStatus.Text = ":: Ready";
            this.gStatus.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // gCancel
            // 
            this.gCancel.Location = new System.Drawing.Point(6, 118);
            this.gCancel.Name = "gCancel";
            this.gCancel.Size = new System.Drawing.Size(212, 27);
            this.gCancel.TabIndex = 3;
            this.gCancel.Text = "Quit";
            this.gCancel.UseVisualStyleBackColor = true;
            this.gCancel.Click += new System.EventHandler(this.gCancel_Click);
            // 
            // gLoadAll
            // 
            this.gLoadAll.Location = new System.Drawing.Point(6, 85);
            this.gLoadAll.Name = "gLoadAll";
            this.gLoadAll.Size = new System.Drawing.Size(212, 27);
            this.gLoadAll.TabIndex = 2;
            this.gLoadAll.Text = "Reload from disk";
            this.gLoadAll.UseVisualStyleBackColor = true;
            this.gLoadAll.Click += new System.EventHandler(this.gLoad_Click);
            // 
            // gSaveCat
            // 
            this.gSaveCat.Location = new System.Drawing.Point(6, 19);
            this.gSaveCat.Name = "gSaveCat";
            this.gSaveCat.Size = new System.Drawing.Size(212, 27);
            this.gSaveCat.TabIndex = 1;
            this.gSaveCat.Text = "Save to disk";
            this.gSaveCat.UseVisualStyleBackColor = true;
            this.gSaveCat.Click += new System.EventHandler(this.gSave_Click);
            // 
            // gContinue
            // 
            this.gContinue.Location = new System.Drawing.Point(6, 52);
            this.gContinue.Name = "gContinue";
            this.gContinue.Size = new System.Drawing.Size(212, 27);
            this.gContinue.TabIndex = 0;
            this.gContinue.Text = "Close and Continue";
            this.gContinue.UseVisualStyleBackColor = true;
            this.gContinue.Click += new System.EventHandler(this.gContinue_Click);
            // 
            // frmTagtype
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 366);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTagtype";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catergorize tags";
            this.Load += new System.EventHandler(this.frmTagtype_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox gtUnk;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox gtGen;
        private System.Windows.Forms.Button gArt;
        private System.Windows.Forms.Button gChr;
        private System.Windows.Forms.Button gSrc;
        private System.Windows.Forms.Button gGen;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox gtChr;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox gtArt;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox gtSrc;
        private System.Windows.Forms.Button gJnk;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ListBox gtJnk;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button gCancel;
        private System.Windows.Forms.Button gLoadAll;
        private System.Windows.Forms.Button gSaveCat;
        private System.Windows.Forms.Button gContinue;
        private System.Windows.Forms.Label gStatus;
    }
}