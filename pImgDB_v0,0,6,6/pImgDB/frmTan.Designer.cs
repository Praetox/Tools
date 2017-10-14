namespace pImgDB
{
    partial class frmTan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTan));
            this.tMove = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tMove
            // 
            this.tMove.Enabled = true;
            this.tMove.Interval = 10;
            this.tMove.Tick += new System.EventHandler(this.tMove_Tick);
            // 
            // frmTan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(72, 81);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTan";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "frmTan";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Click += new System.EventHandler(this.frmTan_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tMove;
    }
}