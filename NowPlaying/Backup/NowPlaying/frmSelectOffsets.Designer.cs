namespace NowPlaying {
    partial class frmSelectOffsets {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectOffsets));
            this.label3 = new System.Windows.Forms.Label();
            this.listTitle = new System.Windows.Forms.ListBox();
            this.listArtist = new System.Windows.Forms.ListBox();
            this.listAlbum = new System.Windows.Forms.ListBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbArtist = new System.Windows.Forms.Label();
            this.lbAlbum = new System.Windows.Forms.Label();
            this.tRefresh = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(667, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 20);
            this.label3.TabIndex = 12;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // listTitle
            // 
            this.listTitle.FormattingEnabled = true;
            this.listTitle.Location = new System.Drawing.Point(299, 12);
            this.listTitle.Name = "listTitle";
            this.listTitle.Size = new System.Drawing.Size(362, 121);
            this.listTitle.TabIndex = 13;
            this.listTitle.SelectedIndexChanged += new System.EventHandler(this.listTitle_SelectedIndexChanged);
            // 
            // listArtist
            // 
            this.listArtist.FormattingEnabled = true;
            this.listArtist.Location = new System.Drawing.Point(299, 139);
            this.listArtist.Name = "listArtist";
            this.listArtist.Size = new System.Drawing.Size(362, 121);
            this.listArtist.TabIndex = 14;
            this.listArtist.SelectedIndexChanged += new System.EventHandler(this.listArtist_SelectedIndexChanged);
            // 
            // listAlbum
            // 
            this.listAlbum.FormattingEnabled = true;
            this.listAlbum.Location = new System.Drawing.Point(299, 266);
            this.listAlbum.Name = "listAlbum";
            this.listAlbum.Size = new System.Drawing.Size(362, 121);
            this.listAlbum.TabIndex = 15;
            this.listAlbum.SelectedIndexChanged += new System.EventHandler(this.listAlbum_SelectedIndexChanged);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(12, 365);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(253, 23);
            this.cmdOK.TabIndex = 16;
            this.cmdOK.Text = "Confirm selections";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(12, 336);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(253, 23);
            this.cmdRefresh.TabIndex = 17;
            this.cmdRefresh.Text = "Refresh lists";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbTitle.ForeColor = System.Drawing.Color.Black;
            this.lbTitle.Location = new System.Drawing.Point(12, 14);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(54, 13);
            this.lbTitle.TabIndex = 18;
            this.lbTitle.Text = "(no value)";
            // 
            // lbArtist
            // 
            this.lbArtist.AutoSize = true;
            this.lbArtist.BackColor = System.Drawing.Color.Transparent;
            this.lbArtist.ForeColor = System.Drawing.Color.Black;
            this.lbArtist.Location = new System.Drawing.Point(12, 27);
            this.lbArtist.Name = "lbArtist";
            this.lbArtist.Size = new System.Drawing.Size(54, 13);
            this.lbArtist.TabIndex = 19;
            this.lbArtist.Text = "(no value)";
            // 
            // lbAlbum
            // 
            this.lbAlbum.AutoSize = true;
            this.lbAlbum.BackColor = System.Drawing.Color.Transparent;
            this.lbAlbum.ForeColor = System.Drawing.Color.Black;
            this.lbAlbum.Location = new System.Drawing.Point(12, 40);
            this.lbAlbum.Name = "lbAlbum";
            this.lbAlbum.Size = new System.Drawing.Size(54, 13);
            this.lbAlbum.TabIndex = 20;
            this.lbAlbum.Text = "(no value)";
            // 
            // tRefresh
            // 
            this.tRefresh.Enabled = true;
            this.tRefresh.Interval = 500;
            this.tRefresh.Tick += new System.EventHandler(this.tRefresh_Tick);
            // 
            // frmSelectOffsets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(700, 400);
            this.Controls.Add(this.lbAlbum);
            this.Controls.Add(this.lbArtist);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.listAlbum);
            this.Controls.Add(this.listArtist);
            this.Controls.Add(this.listTitle);
            this.Controls.Add(this.label3);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSelectOffsets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSelectOffsets";
            this.Load += new System.EventHandler(this.frmSelectOffsets_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listTitle;
        private System.Windows.Forms.ListBox listArtist;
        private System.Windows.Forms.ListBox listAlbum;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbArtist;
        private System.Windows.Forms.Label lbAlbum;
        private System.Windows.Forms.Timer tRefresh;
    }
}