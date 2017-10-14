namespace ZC
{
    partial class frmBackground
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
            this.BGImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BGImage)).BeginInit();
            this.SuspendLayout();
            // 
            // BGImage
            // 
            this.BGImage.Location = new System.Drawing.Point(12, 12);
            this.BGImage.Name = "BGImage";
            this.BGImage.Size = new System.Drawing.Size(100, 50);
            this.BGImage.TabIndex = 3;
            this.BGImage.TabStop = false;
            // 
            // frmBackground
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.BGImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBackground";
            this.Text = "frmBackground";
            this.Load += new System.EventHandler(this.frmBackground_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BGImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BGImage;
    }
}