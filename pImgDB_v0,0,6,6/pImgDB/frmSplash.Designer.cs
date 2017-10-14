namespace pImgDB
{
    partial class frmSplash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplash));
            this.lbP = new System.Windows.Forms.Label();
            this.lbH1 = new System.Windows.Forms.Label();
            this.lbH2 = new System.Windows.Forms.Label();
            this.tNews = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lbP
            // 
            this.lbP.BackColor = System.Drawing.Color.Transparent;
            this.lbP.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(185)))), ((int)(((byte)(196)))));
            this.lbP.Location = new System.Drawing.Point(29, 83);
            this.lbP.Name = "lbP";
            this.lbP.Size = new System.Drawing.Size(454, 99);
            this.lbP.TabIndex = 0;
            this.lbP.Text = "Attempting to load news...\r\nYou may click here to close this.";
            this.lbP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbP.Click += new System.EventHandler(this.lbBody_Click);
            // 
            // lbH1
            // 
            this.lbH1.BackColor = System.Drawing.Color.Transparent;
            this.lbH1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(185)))), ((int)(((byte)(196)))));
            this.lbH1.Location = new System.Drawing.Point(21, 196);
            this.lbH1.Name = "lbH1";
            this.lbH1.Size = new System.Drawing.Size(212, 15);
            this.lbH1.TabIndex = 1;
            this.lbH1.Text = "Text goes here";
            this.lbH1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbH1.Click += new System.EventHandler(this.lbH1_Click);
            // 
            // lbH2
            // 
            this.lbH2.BackColor = System.Drawing.Color.Transparent;
            this.lbH2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(185)))), ((int)(((byte)(196)))));
            this.lbH2.Location = new System.Drawing.Point(279, 212);
            this.lbH2.Name = "lbH2";
            this.lbH2.Size = new System.Drawing.Size(212, 15);
            this.lbH2.TabIndex = 2;
            this.lbH2.Text = "Text goes here";
            this.lbH2.Click += new System.EventHandler(this.lbH2_Click);
            // 
            // tNews
            // 
            this.tNews.Enabled = true;
            this.tNews.Tick += new System.EventHandler(this.tNews_Tick);
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(512, 320);
            this.ControlBox = false;
            this.Controls.Add(this.lbH2);
            this.Controls.Add(this.lbH1);
            this.Controls.Add(this.lbP);
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmSplash_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmSplash_MouseClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSplash_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbP;
        private System.Windows.Forms.Label lbH1;
        private System.Windows.Forms.Label lbH2;
        private System.Windows.Forms.Timer tNews;

    }
}