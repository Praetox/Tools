namespace pZDock
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
            this.Cover = new System.Windows.Forms.Label();
            this.tHider = new System.Windows.Forms.Timer(this.components);
            this.tMover = new System.Windows.Forms.Timer(this.components);
            this.TTip = new System.Windows.Forms.ToolTip(this.components);
            this.tAppStart = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Cover
            // 
            this.Cover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cover.Location = new System.Drawing.Point(0, 0);
            this.Cover.Name = "Cover";
            this.Cover.Size = new System.Drawing.Size(126, 66);
            this.Cover.TabIndex = 0;
            this.Cover.Text = "   ";
            this.Cover.Click += new System.EventHandler(this.Cover_Click);
            // 
            // tHider
            // 
            this.tHider.Enabled = true;
            this.tHider.Tick += new System.EventHandler(this.tHider_Tick);
            // 
            // tMover
            // 
            this.tMover.Enabled = true;
            this.tMover.Interval = 15;
            this.tMover.Tick += new System.EventHandler(this.tMover_Tick);
            // 
            // TTip
            // 
            this.TTip.AutomaticDelay = 0;
            this.TTip.BackColor = System.Drawing.Color.Black;
            this.TTip.ForeColor = System.Drawing.Color.LightBlue;
            this.TTip.IsBalloon = true;
            this.TTip.UseAnimation = false;
            this.TTip.UseFading = false;
            // 
            // tAppStart
            // 
            this.tAppStart.Enabled = true;
            this.tAppStart.Interval = 1;
            this.tAppStart.Tick += new System.EventHandler(this.tAppStart_Tick);
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(126, 66);
            this.ControlBox = false;
            this.Controls.Add(this.Cover);
            this.ForeColor = System.Drawing.Color.LightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.Opacity = 0.5;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmMain_DragDrop);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Cover;
        private System.Windows.Forms.Timer tHider;
        private System.Windows.Forms.Timer tMover;
        private System.Windows.Forms.ToolTip TTip;
        private System.Windows.Forms.Timer tAppStart;

    }
}

