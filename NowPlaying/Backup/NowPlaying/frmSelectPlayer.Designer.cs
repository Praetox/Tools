namespace NowPlaying {
    partial class frmSelectPlayer {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectPlayer));
            this.iTunesMem = new System.Windows.Forms.PictureBox();
            this.Winamp = new System.Windows.Forms.PictureBox();
            this.Foobar = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Spotify = new System.Windows.Forms.PictureBox();
            this.txtProc = new System.Windows.Forms.TextBox();
            this.txtMask = new System.Windows.Forms.TextBox();
            this.cmdContinue = new System.Windows.Forms.Button();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.SpotifyMem = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.memContinue = new System.Windows.Forms.Button();
            this.MemProc = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.iTunesMem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Winamp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Foobar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Spotify)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpotifyMem)).BeginInit();
            this.SuspendLayout();
            // 
            // iTunesMem
            // 
            this.iTunesMem.BackColor = System.Drawing.Color.Transparent;
            this.iTunesMem.Image = ((System.Drawing.Image)(resources.GetObject("iTunesMem.Image")));
            this.iTunesMem.Location = new System.Drawing.Point(545, 242);
            this.iTunesMem.Margin = new System.Windows.Forms.Padding(12);
            this.iTunesMem.Name = "iTunesMem";
            this.iTunesMem.Size = new System.Drawing.Size(48, 48);
            this.iTunesMem.TabIndex = 0;
            this.iTunesMem.TabStop = false;
            this.iTunesMem.Click += new System.EventHandler(this.iTunes_Click);
            // 
            // Winamp
            // 
            this.Winamp.BackColor = System.Drawing.Color.Transparent;
            this.Winamp.Image = ((System.Drawing.Image)(resources.GetObject("Winamp.Image")));
            this.Winamp.Location = new System.Drawing.Point(401, 49);
            this.Winamp.Margin = new System.Windows.Forms.Padding(12);
            this.Winamp.Name = "Winamp";
            this.Winamp.Size = new System.Drawing.Size(48, 48);
            this.Winamp.TabIndex = 1;
            this.Winamp.TabStop = false;
            this.Winamp.Click += new System.EventHandler(this.Winamp_Click);
            // 
            // Foobar
            // 
            this.Foobar.BackColor = System.Drawing.Color.Transparent;
            this.Foobar.Image = ((System.Drawing.Image)(resources.GetObject("Foobar.Image")));
            this.Foobar.Location = new System.Drawing.Point(473, 49);
            this.Foobar.Margin = new System.Windows.Forms.Padding(12);
            this.Foobar.Name = "Foobar";
            this.Foobar.Size = new System.Drawing.Size(48, 48);
            this.Foobar.TabIndex = 2;
            this.Foobar.TabStop = false;
            this.Foobar.Click += new System.EventHandler(this.Foobar_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(667, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 20);
            this.label1.TabIndex = 4;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Spotify
            // 
            this.Spotify.BackColor = System.Drawing.Color.Transparent;
            this.Spotify.Image = ((System.Drawing.Image)(resources.GetObject("Spotify.Image")));
            this.Spotify.Location = new System.Drawing.Point(545, 49);
            this.Spotify.Margin = new System.Windows.Forms.Padding(12);
            this.Spotify.Name = "Spotify";
            this.Spotify.Size = new System.Drawing.Size(48, 48);
            this.Spotify.TabIndex = 5;
            this.Spotify.TabStop = false;
            this.Spotify.Click += new System.EventHandler(this.Spotify_Click);
            // 
            // txtProc
            // 
            this.txtProc.Location = new System.Drawing.Point(401, 112);
            this.txtProc.Name = "txtProc";
            this.txtProc.Size = new System.Drawing.Size(192, 20);
            this.txtProc.TabIndex = 6;
            // 
            // txtMask
            // 
            this.txtMask.Location = new System.Drawing.Point(401, 138);
            this.txtMask.Name = "txtMask";
            this.txtMask.Size = new System.Drawing.Size(192, 20);
            this.txtMask.TabIndex = 7;
            // 
            // cmdContinue
            // 
            this.cmdContinue.Location = new System.Drawing.Point(518, 164);
            this.cmdContinue.Name = "cmdContinue";
            this.cmdContinue.Size = new System.Drawing.Size(75, 23);
            this.cmdContinue.TabIndex = 8;
            this.cmdContinue.Text = "Continue";
            this.cmdContinue.UseVisualStyleBackColor = true;
            this.cmdContinue.Click += new System.EventHandler(this.cmdContinue_Click);
            // 
            // chkRemember
            // 
            this.chkRemember.AutoSize = true;
            this.chkRemember.BackColor = System.Drawing.Color.Transparent;
            this.chkRemember.Checked = true;
            this.chkRemember.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRemember.ForeColor = System.Drawing.Color.White;
            this.chkRemember.Location = new System.Drawing.Point(401, 168);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(77, 17);
            this.chkRemember.TabIndex = 14;
            this.chkRemember.Text = "Remember";
            this.chkRemember.UseVisualStyleBackColor = false;
            // 
            // SpotifyMem
            // 
            this.SpotifyMem.BackColor = System.Drawing.Color.Transparent;
            this.SpotifyMem.Image = ((System.Drawing.Image)(resources.GetObject("SpotifyMem.Image")));
            this.SpotifyMem.Location = new System.Drawing.Point(473, 242);
            this.SpotifyMem.Margin = new System.Windows.Forms.Padding(12);
            this.SpotifyMem.Name = "SpotifyMem";
            this.SpotifyMem.Size = new System.Drawing.Size(48, 48);
            this.SpotifyMem.TabIndex = 15;
            this.SpotifyMem.TabStop = false;
            this.SpotifyMem.Click += new System.EventHandler(this.SpotifyMem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(398, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 26);
            this.label2.TabIndex = 16;
            this.label2.Text = "Memory\r\nbased";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // memContinue
            // 
            this.memContinue.Location = new System.Drawing.Point(518, 305);
            this.memContinue.Name = "memContinue";
            this.memContinue.Size = new System.Drawing.Size(75, 23);
            this.memContinue.TabIndex = 17;
            this.memContinue.Text = "Continue";
            this.memContinue.UseVisualStyleBackColor = true;
            this.memContinue.Click += new System.EventHandler(this.memContinue_Click);
            // 
            // MemProc
            // 
            this.MemProc.Location = new System.Drawing.Point(401, 307);
            this.MemProc.Name = "MemProc";
            this.MemProc.Size = new System.Drawing.Size(111, 20);
            this.MemProc.TabIndex = 18;
            // 
            // frmSelectPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(700, 400);
            this.Controls.Add(this.MemProc);
            this.Controls.Add(this.memContinue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SpotifyMem);
            this.Controls.Add(this.chkRemember);
            this.Controls.Add(this.cmdContinue);
            this.Controls.Add(this.txtMask);
            this.Controls.Add(this.txtProc);
            this.Controls.Add(this.Spotify);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Foobar);
            this.Controls.Add(this.Winamp);
            this.Controls.Add(this.iTunesMem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSelectPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSelectPlayer";
            this.Load += new System.EventHandler(this.frmSelectPlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iTunesMem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Winamp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Foobar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Spotify)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpotifyMem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox iTunesMem;
        private System.Windows.Forms.PictureBox Winamp;
        private System.Windows.Forms.PictureBox Foobar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Spotify;
        private System.Windows.Forms.TextBox txtProc;
        private System.Windows.Forms.TextBox txtMask;
        private System.Windows.Forms.Button cmdContinue;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.PictureBox SpotifyMem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button memContinue;
        private System.Windows.Forms.TextBox MemProc;

    }
}