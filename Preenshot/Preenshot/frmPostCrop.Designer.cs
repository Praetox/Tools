namespace Preenshot
{
    partial class frmPostCrop
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
            this.SuspendLayout();
            // 
            // frmPostCrop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPostCrop";
            this.Text = "frmPostCrop";
            this.Load += new System.EventHandler(this.frmPostCrop_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmPostCrop_MouseUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmPostCrop_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmPostCrop_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}