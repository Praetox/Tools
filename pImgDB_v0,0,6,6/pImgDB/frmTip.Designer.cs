namespace pImgDB
{
    partial class frmTip
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
            this.lbTip = new System.Windows.Forms.Label();
            this.tMain = new System.Windows.Forms.Timer(this.components);
            this.tHide = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lbTip
            // 
            this.lbTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbTip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTip.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTip.ForeColor = System.Drawing.Color.White;
            this.lbTip.Location = new System.Drawing.Point(2, 2);
            this.lbTip.Name = "lbTip";
            this.lbTip.Size = new System.Drawing.Size(590, 36);
            this.lbTip.TabIndex = 0;
            this.lbTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbTip.Click += new System.EventHandler(this.lbTip_Click);
            // 
            // tMain
            // 
            this.tMain.Enabled = true;
            this.tMain.Interval = 1;
            this.tMain.Tick += new System.EventHandler(this.tMain_Tick);
            // 
            // tHide
            // 
            this.tHide.Interval = 2000;
            this.tHide.Tick += new System.EventHandler(this.tHide_Tick);
            // 
            // frmTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(594, 40);
            this.ControlBox = false;
            this.Controls.Add(this.lbTip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTip";
            this.Opacity = 0.9;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmTip_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTip_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTip;
        private System.Windows.Forms.Timer tMain;
        private System.Windows.Forms.Timer tHide;
    }
}