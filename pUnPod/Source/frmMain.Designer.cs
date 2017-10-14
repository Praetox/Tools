namespace pUnPod
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.DG = new System.Windows.Forms.DataGridView();
            this.cNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cArtist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAlbum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbVol1 = new System.Windows.Forms.Label();
            this.lbPrg1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbPrg2 = new System.Windows.Forms.Label();
            this.lbVol2 = new System.Windows.Forms.Label();
            this.cmdUnload = new System.Windows.Forms.Button();
            this.cmdNext = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.cmdPlay = new System.Windows.Forms.Button();
            this.cmdPrev = new System.Windows.Forms.Button();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbLoading = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.cmdPath = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.nIco = new System.Windows.Forms.NotifyIcon(this.components);
            this.tProgress = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DG)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // DG
            // 
            this.DG.AllowUserToResizeRows = false;
            this.DG.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DG.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cNum,
            this.cTitle,
            this.cArtist,
            this.cAlbum,
            this.cPath});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DG.DefaultCellStyle = dataGridViewCellStyle2;
            this.DG.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DG.GridColor = System.Drawing.Color.SlateGray;
            this.DG.Location = new System.Drawing.Point(6, 19);
            this.DG.Name = "DG";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DG.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DG.RowHeadersVisible = false;
            this.DG.RowHeadersWidth = 22;
            this.DG.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DG.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DG.Size = new System.Drawing.Size(748, 373);
            this.DG.TabIndex = 0;
            this.DG.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DG_CellDoubleClick);
            // 
            // cNum
            // 
            this.cNum.HeaderText = "Num";
            this.cNum.Name = "cNum";
            this.cNum.ReadOnly = true;
            this.cNum.Width = 50;
            // 
            // cTitle
            // 
            this.cTitle.HeaderText = "Title";
            this.cTitle.Name = "cTitle";
            this.cTitle.ReadOnly = true;
            this.cTitle.Width = 192;
            // 
            // cArtist
            // 
            this.cArtist.HeaderText = "Artist";
            this.cArtist.Name = "cArtist";
            this.cArtist.ReadOnly = true;
            this.cArtist.Width = 192;
            // 
            // cAlbum
            // 
            this.cAlbum.HeaderText = "Album";
            this.cAlbum.Name = "cAlbum";
            this.cAlbum.ReadOnly = true;
            this.cAlbum.Width = 192;
            // 
            // cPath
            // 
            this.cPath.HeaderText = "Path";
            this.cPath.Name = "cPath";
            this.cPath.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.groupBox1.Controls.Add(this.lbVol1);
            this.groupBox1.Controls.Add(this.lbPrg1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lbPrg2);
            this.groupBox1.Controls.Add(this.lbVol2);
            this.groupBox1.Controls.Add(this.cmdUnload);
            this.groupBox1.Controls.Add(this.cmdNext);
            this.groupBox1.Controls.Add(this.cmdStop);
            this.groupBox1.Controls.Add(this.cmdPlay);
            this.groupBox1.Controls.Add(this.cmdPrev);
            this.groupBox1.Controls.Add(this.cmdLoad);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 81);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Media player";
            // 
            // lbVol1
            // 
            this.lbVol1.BackColor = System.Drawing.Color.Azure;
            this.lbVol1.Location = new System.Drawing.Point(8, 51);
            this.lbVol1.Name = "lbVol1";
            this.lbVol1.Size = new System.Drawing.Size(326, 7);
            this.lbVol1.TabIndex = 9;
            this.lbVol1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbVol1_MouseMove);
            this.lbVol1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbVol1_MouseDown);
            // 
            // lbPrg1
            // 
            this.lbPrg1.BackColor = System.Drawing.Color.Azure;
            this.lbPrg1.Location = new System.Drawing.Point(8, 66);
            this.lbPrg1.Name = "lbPrg1";
            this.lbPrg1.Size = new System.Drawing.Size(326, 7);
            this.lbPrg1.TabIndex = 12;
            this.lbPrg1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbPrg1_MouseMove);
            this.lbPrg1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbPrg1_MouseDown);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(64)))), ((int)(((byte)(96)))));
            this.label7.Location = new System.Drawing.Point(6, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(330, 4);
            this.label7.TabIndex = 13;
            this.label7.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.label3.Location = new System.Drawing.Point(6, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(330, 4);
            this.label3.TabIndex = 11;
            // 
            // lbPrg2
            // 
            this.lbPrg2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(112)))), ((int)(((byte)(136)))));
            this.lbPrg2.Location = new System.Drawing.Point(6, 64);
            this.lbPrg2.Name = "lbPrg2";
            this.lbPrg2.Size = new System.Drawing.Size(330, 11);
            this.lbPrg2.TabIndex = 10;
            this.lbPrg2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbPrg2_MouseMove);
            this.lbPrg2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbPrg2_MouseDown);
            // 
            // lbVol2
            // 
            this.lbVol2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(112)))), ((int)(((byte)(136)))));
            this.lbVol2.Location = new System.Drawing.Point(6, 49);
            this.lbVol2.Name = "lbVol2";
            this.lbVol2.Size = new System.Drawing.Size(330, 11);
            this.lbVol2.TabIndex = 8;
            this.lbVol2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbVol2_MouseMove);
            this.lbVol2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbVol2_MouseDown);
            // 
            // cmdUnload
            // 
            this.cmdUnload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.cmdUnload.Location = new System.Drawing.Point(286, 19);
            this.cmdUnload.Name = "cmdUnload";
            this.cmdUnload.Size = new System.Drawing.Size(50, 23);
            this.cmdUnload.TabIndex = 7;
            this.cmdUnload.Text = "Unload";
            this.cmdUnload.UseVisualStyleBackColor = false;
            this.cmdUnload.Click += new System.EventHandler(this.cmdUnload_Click);
            // 
            // cmdNext
            // 
            this.cmdNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.cmdNext.Location = new System.Drawing.Point(174, 19);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(50, 23);
            this.cmdNext.TabIndex = 6;
            this.cmdNext.Text = "Next";
            this.cmdNext.UseVisualStyleBackColor = false;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.cmdStop.Location = new System.Drawing.Point(230, 19);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(50, 23);
            this.cmdStop.TabIndex = 5;
            this.cmdStop.Text = "Stop";
            this.cmdStop.UseVisualStyleBackColor = false;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // cmdPlay
            // 
            this.cmdPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.cmdPlay.Location = new System.Drawing.Point(118, 19);
            this.cmdPlay.Name = "cmdPlay";
            this.cmdPlay.Size = new System.Drawing.Size(50, 23);
            this.cmdPlay.TabIndex = 4;
            this.cmdPlay.Text = "Play";
            this.cmdPlay.UseVisualStyleBackColor = false;
            this.cmdPlay.Click += new System.EventHandler(this.cmdPlay_Click);
            // 
            // cmdPrev
            // 
            this.cmdPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.cmdPrev.Location = new System.Drawing.Point(62, 19);
            this.cmdPrev.Name = "cmdPrev";
            this.cmdPrev.Size = new System.Drawing.Size(50, 23);
            this.cmdPrev.TabIndex = 3;
            this.cmdPrev.Text = "Prev";
            this.cmdPrev.UseVisualStyleBackColor = false;
            this.cmdPrev.Click += new System.EventHandler(this.cmdPrev_Click);
            // 
            // cmdLoad
            // 
            this.cmdLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.cmdLoad.Location = new System.Drawing.Point(6, 19);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(50, 23);
            this.cmdLoad.TabIndex = 2;
            this.cmdLoad.Text = "Reload";
            this.cmdLoad.UseVisualStyleBackColor = false;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.groupBox2.Controls.Add(this.DG);
            this.groupBox2.Controls.Add(this.lbLoading);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(12, 111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 398);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "iPod songs";
            // 
            // lbLoading
            // 
            this.lbLoading.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLoading.Location = new System.Drawing.Point(6, 16);
            this.lbLoading.Name = "lbLoading";
            this.lbLoading.Size = new System.Drawing.Size(748, 379);
            this.lbLoading.TabIndex = 1;
            this.lbLoading.Text = "Loading...";
            this.lbLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(64)))), ((int)(((byte)(96)))));
            this.label6.Location = new System.Drawing.Point(12, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 12);
            this.label6.TabIndex = 10;
            this.label6.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.groupBox3.Controls.Add(this.txtPath);
            this.groupBox3.Controls.Add(this.cmdCopy);
            this.groupBox3.Controls.Add(this.cmdPath);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(378, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(394, 81);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Copy from iPod";
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPath.ForeColor = System.Drawing.Color.Azure;
            this.txtPath.Location = new System.Drawing.Point(6, 21);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(350, 20);
            this.txtPath.TabIndex = 8;
            // 
            // cmdCopy
            // 
            this.cmdCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.cmdCopy.Location = new System.Drawing.Point(6, 48);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(382, 27);
            this.cmdCopy.TabIndex = 7;
            this.cmdCopy.Text = "C o p y    s e l e c t e d    s o n g s    n o w";
            this.cmdCopy.UseVisualStyleBackColor = false;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // cmdPath
            // 
            this.cmdPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.cmdPath.Location = new System.Drawing.Point(362, 19);
            this.cmdPath.Name = "cmdPath";
            this.cmdPath.Size = new System.Drawing.Size(26, 23);
            this.cmdPath.TabIndex = 5;
            this.cmdPath.Text = "...";
            this.cmdPath.UseVisualStyleBackColor = false;
            this.cmdPath.Click += new System.EventHandler(this.cmdPath_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(64)))), ((int)(((byte)(96)))));
            this.label14.Location = new System.Drawing.Point(360, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(12, 12);
            this.label14.TabIndex = 12;
            this.label14.Visible = false;
            // 
            // nIco
            // 
            this.nIco.Text = "notifyIcon1";
            this.nIco.Visible = true;
            // 
            // tProgress
            // 
            this.tProgress.Enabled = true;
            this.tProgress.Tick += new System.EventHandler(this.tProgress_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(784, 521);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Azure;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Opacity = 0.95;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pUnPod - BETA";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.DG)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DG;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbVol1;
        private System.Windows.Forms.Label lbVol2;
        private System.Windows.Forms.Button cmdUnload;
        private System.Windows.Forms.Button cmdNext;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.Button cmdPlay;
        private System.Windows.Forms.Button cmdPrev;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.Label lbPrg1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbPrg2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button cmdCopy;
        private System.Windows.Forms.Button cmdPath;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbLoading;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn cArtist;
        private System.Windows.Forms.DataGridViewTextBoxColumn cAlbum;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPath;
        private System.Windows.Forms.NotifyIcon nIco;
        private System.Windows.Forms.Timer tProgress;
    }
}

