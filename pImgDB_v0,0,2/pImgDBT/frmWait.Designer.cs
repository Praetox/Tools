namespace pImgDBT
{
    partial class frmWait
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWait));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbMain = new System.Windows.Forms.Label();
            this.lbFooter = new System.Windows.Forms.Label();
            this.lbHeader = new System.Windows.Forms.Label();
            this.tMain = new System.Windows.Forms.Timer(this.components);
            this.tInstant = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbMain);
            this.panel1.Controls.Add(this.lbFooter);
            this.panel1.Controls.Add(this.lbHeader);
            this.panel1.Location = new System.Drawing.Point(7, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(466, 204);
            this.panel1.TabIndex = 0;
            // 
            // lbMain
            // 
            this.lbMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMain.ForeColor = System.Drawing.Color.White;
            this.lbMain.Location = new System.Drawing.Point(3, 32);
            this.lbMain.Name = "lbMain";
            this.lbMain.Size = new System.Drawing.Size(460, 140);
            this.lbMain.TabIndex = 2;
            this.lbMain.Text = "Main";
            this.lbMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbFooter
            // 
            this.lbFooter.Location = new System.Drawing.Point(3, 172);
            this.lbFooter.Name = "lbFooter";
            this.lbFooter.Size = new System.Drawing.Size(460, 32);
            this.lbFooter.TabIndex = 1;
            this.lbFooter.Text = "Footer";
            this.lbFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbHeader
            // 
            this.lbHeader.Location = new System.Drawing.Point(3, 0);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(460, 32);
            this.lbHeader.TabIndex = 0;
            this.lbHeader.Text = "Header";
            this.lbHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tMain
            // 
            this.tMain.Enabled = true;
            this.tMain.Tick += new System.EventHandler(this.tMain_Tick);
            // 
            // tInstant
            // 
            this.tInstant.Enabled = true;
            this.tInstant.Interval = 1;
            this.tInstant.Tick += new System.EventHandler(this.tInstant_Tick);
            // 
            // frmWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(480, 272);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWait";
            this.Opacity = 0.8;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pImgDB - Busy";
            this.Load += new System.EventHandler(this.frmWait_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmWait_FormClosing);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbHeader;
        private System.Windows.Forms.Label lbMain;
        private System.Windows.Forms.Label lbFooter;
        private System.Windows.Forms.Timer tMain;
        private System.Windows.Forms.Timer tInstant;
    }
}