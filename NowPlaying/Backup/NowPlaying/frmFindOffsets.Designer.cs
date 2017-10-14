namespace NowPlaying {
    partial class frmFindOffsets {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFindOffsets));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txTitle = new System.Windows.Forms.TextBox();
            this.txArtist = new System.Windows.Forms.TextBox();
            this.txAlbum = new System.Windows.Forms.TextBox();
            this.cmdGo = new System.Windows.Forms.Button();
            this.prg = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.lbState = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(342, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(342, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Artist";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(342, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Album";
            // 
            // txTitle
            // 
            this.txTitle.Location = new System.Drawing.Point(390, 145);
            this.txTitle.Name = "txTitle";
            this.txTitle.Size = new System.Drawing.Size(248, 20);
            this.txTitle.TabIndex = 3;
            this.txTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txTitle_KeyDown);
            // 
            // txArtist
            // 
            this.txArtist.Location = new System.Drawing.Point(390, 171);
            this.txArtist.Name = "txArtist";
            this.txArtist.Size = new System.Drawing.Size(248, 20);
            this.txArtist.TabIndex = 4;
            this.txArtist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txArtist_KeyDown);
            // 
            // txAlbum
            // 
            this.txAlbum.Location = new System.Drawing.Point(390, 197);
            this.txAlbum.Name = "txAlbum";
            this.txAlbum.Size = new System.Drawing.Size(248, 20);
            this.txAlbum.TabIndex = 5;
            this.txAlbum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txAlbum_KeyDown);
            // 
            // cmdGo
            // 
            this.cmdGo.Location = new System.Drawing.Point(390, 223);
            this.cmdGo.Name = "cmdGo";
            this.cmdGo.Size = new System.Drawing.Size(248, 30);
            this.cmdGo.TabIndex = 6;
            this.cmdGo.Text = "Try to allocate offsets";
            this.cmdGo.UseVisualStyleBackColor = true;
            this.cmdGo.Click += new System.EventHandler(this.cmdGo_Click);
            // 
            // prg
            // 
            this.prg.Location = new System.Drawing.Point(12, 365);
            this.prg.Name = "prg";
            this.prg.Size = new System.Drawing.Size(676, 23);
            this.prg.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(667, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 20);
            this.label4.TabIndex = 8;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // lbState
            // 
            this.lbState.AutoSize = true;
            this.lbState.BackColor = System.Drawing.Color.Transparent;
            this.lbState.ForeColor = System.Drawing.Color.White;
            this.lbState.Location = new System.Drawing.Point(12, 349);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(56, 13);
            this.lbState.TabIndex = 9;
            this.lbState.Text = "Not active";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(387, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(252, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Note: Spotify users need only apply the \"Artist\" field.";
            // 
            // frmFindOffsets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(700, 400);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbState);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.prg);
            this.Controls.Add(this.cmdGo);
            this.Controls.Add(this.txAlbum);
            this.Controls.Add(this.txArtist);
            this.Controls.Add(this.txTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmFindOffsets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSelectOffsets";
            this.Load += new System.EventHandler(this.frmFindOffsets_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txTitle;
        private System.Windows.Forms.TextBox txArtist;
        private System.Windows.Forms.TextBox txAlbum;
        private System.Windows.Forms.Button cmdGo;
        private System.Windows.Forms.ProgressBar prg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbState;
        private System.Windows.Forms.Label label5;
    }
}