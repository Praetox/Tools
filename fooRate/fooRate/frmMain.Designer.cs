namespace fooRate {
    partial class frmMain {
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.guLoad = new System.Windows.Forms.Button();
            this.guSave = new System.Windows.Forms.Button();
            this.guSongsAll = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.guSongsDB = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.guRecent = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.guCutSubfix = new System.Windows.Forms.TextBox();
            this.guRate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.guLoad);
            this.groupBox1.Controls.Add(this.guSave);
            this.groupBox1.Controls.Add(this.guSongsAll);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.guSongsDB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database management";
            // 
            // guLoad
            // 
            this.guLoad.Location = new System.Drawing.Point(6, 45);
            this.guLoad.Name = "guLoad";
            this.guLoad.Size = new System.Drawing.Size(125, 23);
            this.guLoad.TabIndex = 5;
            this.guLoad.Text = "Load from database";
            this.guLoad.UseVisualStyleBackColor = true;
            this.guLoad.Click += new System.EventHandler(this.guLoad_Click);
            // 
            // guSave
            // 
            this.guSave.Location = new System.Drawing.Point(137, 45);
            this.guSave.Name = "guSave";
            this.guSave.Size = new System.Drawing.Size(125, 23);
            this.guSave.TabIndex = 4;
            this.guSave.Text = "Save all to database";
            this.guSave.UseVisualStyleBackColor = true;
            this.guSave.Click += new System.EventHandler(this.guSave_Click);
            // 
            // guSongsAll
            // 
            this.guSongsAll.Location = new System.Drawing.Point(212, 29);
            this.guSongsAll.Name = "guSongsAll";
            this.guSongsAll.Size = new System.Drawing.Size(50, 13);
            this.guSongsAll.TabIndex = 3;
            this.guSongsAll.Text = "n";
            this.guSongsAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Amount of songs live and in database";
            // 
            // guSongsDB
            // 
            this.guSongsDB.Location = new System.Drawing.Point(212, 16);
            this.guSongsDB.Name = "guSongsDB";
            this.guSongsDB.Size = new System.Drawing.Size(50, 13);
            this.guSongsDB.TabIndex = 1;
            this.guSongsDB.Text = "n";
            this.guSongsDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Amount of songs in stored database";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.guRecent);
            this.groupBox2.Location = new System.Drawing.Point(12, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 94);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Recently rated songs";
            // 
            // guRecent
            // 
            this.guRecent.FormattingEnabled = true;
            this.guRecent.Location = new System.Drawing.Point(6, 19);
            this.guRecent.Name = "guRecent";
            this.guRecent.Size = new System.Drawing.Size(256, 69);
            this.guRecent.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.guCutSubfix);
            this.groupBox3.Controls.Add(this.guRate);
            this.groupBox3.Location = new System.Drawing.Point(12, 192);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(268, 88);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rate current song";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Cut subfix";
            // 
            // guCutSubfix
            // 
            this.guCutSubfix.Location = new System.Drawing.Point(65, 62);
            this.guCutSubfix.Name = "guCutSubfix";
            this.guCutSubfix.Size = new System.Drawing.Size(197, 20);
            this.guCutSubfix.TabIndex = 1;
            this.guCutSubfix.Text = "C:\\_\\mp3\\TLMC\\";
            // 
            // guRate
            // 
            this.guRate.Location = new System.Drawing.Point(6, 19);
            this.guRate.Name = "guRate";
            this.guRate.Size = new System.Drawing.Size(256, 37);
            this.guRate.TabIndex = 0;
            this.guRate.Text = "Press a key from 1-5 here   (0 removes)";
            this.guRate.UseVisualStyleBackColor = true;
            this.guRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.guRate_KeyDown);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 292);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMain";
            this.Text = "fooRate";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button guLoad;
        private System.Windows.Forms.Button guSave;
        private System.Windows.Forms.Label guSongsAll;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label guSongsDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox guRecent;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button guRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox guCutSubfix;
    }
}

