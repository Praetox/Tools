namespace pImgSort
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
            this.Keeplist = new System.Windows.Forms.TextBox();
            this.tHotkeys = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Keeplist
            // 
            this.Keeplist.Location = new System.Drawing.Point(12, 12);
            this.Keeplist.Multiline = true;
            this.Keeplist.Name = "Keeplist";
            this.Keeplist.Size = new System.Drawing.Size(268, 245);
            this.Keeplist.TabIndex = 0;
            this.Keeplist.DoubleClick += new System.EventHandler(this.Keeplist_DoubleClick);
            // 
            // tHotkeys
            // 
            this.tHotkeys.Enabled = true;
            this.tHotkeys.Interval = 1;
            this.tHotkeys.Tick += new System.EventHandler(this.tHotkeys_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 269);
            this.Controls.Add(this.Keeplist);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Keeplist;
        private System.Windows.Forms.Timer tHotkeys;
    }
}

