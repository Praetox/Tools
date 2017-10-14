namespace ZC
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
            this.cmdOptions = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.tplAppBox = new System.Windows.Forms.Panel();
            this.tplAppName = new System.Windows.Forms.Label();
            this.tplAppIcon = new System.Windows.Forms.PictureBox();
            this.tplAppText = new System.Windows.Forms.Label();
            this.tPollForClicks = new System.Windows.Forms.Timer(this.components);
            this.tHotkeys = new System.Windows.Forms.Timer(this.components);
            this.tplAppBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tplAppIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOptions
            // 
            this.cmdOptions.Location = new System.Drawing.Point(12, 231);
            this.cmdOptions.Name = "cmdOptions";
            this.cmdOptions.Size = new System.Drawing.Size(75, 23);
            this.cmdOptions.TabIndex = 3;
            this.cmdOptions.Text = "Options";
            this.cmdOptions.UseVisualStyleBackColor = true;
            this.cmdOptions.Click += new System.EventHandler(this.cmdOptions_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Location = new System.Drawing.Point(205, 231);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(75, 23);
            this.cmdExit.TabIndex = 4;
            this.cmdExit.Text = "Exit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // tplAppBox
            // 
            this.tplAppBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tplAppBox.Controls.Add(this.tplAppName);
            this.tplAppBox.Controls.Add(this.tplAppIcon);
            this.tplAppBox.Controls.Add(this.tplAppText);
            this.tplAppBox.Location = new System.Drawing.Point(12, 12);
            this.tplAppBox.Name = "tplAppBox";
            this.tplAppBox.Size = new System.Drawing.Size(253, 70);
            this.tplAppBox.TabIndex = 5;
            this.tplAppBox.Visible = false;
            // 
            // tplAppName
            // 
            this.tplAppName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tplAppName.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tplAppName.ForeColor = System.Drawing.Color.White;
            this.tplAppName.Location = new System.Drawing.Point(73, 3);
            this.tplAppName.Name = "tplAppName";
            this.tplAppName.Size = new System.Drawing.Size(174, 20);
            this.tplAppName.TabIndex = 6;
            this.tplAppName.Text = "Stepmania";
            this.tplAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tplAppIcon
            // 
            this.tplAppIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.tplAppIcon.Location = new System.Drawing.Point(3, 3);
            this.tplAppIcon.Name = "tplAppIcon";
            this.tplAppIcon.Size = new System.Drawing.Size(64, 64);
            this.tplAppIcon.TabIndex = 6;
            this.tplAppIcon.TabStop = false;
            // 
            // tplAppText
            // 
            this.tplAppText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tplAppText.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tplAppText.ForeColor = System.Drawing.Color.White;
            this.tplAppText.Location = new System.Drawing.Point(73, 23);
            this.tplAppText.Name = "tplAppText";
            this.tplAppText.Size = new System.Drawing.Size(174, 44);
            this.tplAppText.TabIndex = 0;
            this.tplAppText.Text = "Dance-machine simulator\r\nNew and improved DDR";
            this.tplAppText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tPollForClicks
            // 
            this.tPollForClicks.Enabled = true;
            this.tPollForClicks.Interval = 10;
            this.tPollForClicks.Tick += new System.EventHandler(this.tPollForClicks_Tick);
            // 
            // tHotkeys
            // 
            this.tHotkeys.Enabled = true;
            this.tHotkeys.Interval = 10;
            this.tHotkeys.Tick += new System.EventHandler(this.tHotkeys_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.tplAppBox);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMain";
            this.Opacity = 0.8;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tplAppBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tplAppIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdOptions;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Panel tplAppBox;
        private System.Windows.Forms.Label tplAppText;
        private System.Windows.Forms.PictureBox tplAppIcon;
        private System.Windows.Forms.Label tplAppName;
        private System.Windows.Forms.Timer tPollForClicks;
        private System.Windows.Forms.Timer tHotkeys;
    }
}

