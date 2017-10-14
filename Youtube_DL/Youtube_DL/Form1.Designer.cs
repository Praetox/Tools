namespace Youtube_DL
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdIExplore = new System.Windows.Forms.Button();
            this.cmdFirefox = new System.Windows.Forms.Button();
            this.cmdOpera = new System.Windows.Forms.Button();
            this.tLink = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cGetname = new System.Windows.Forms.Button();
            this.tFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cDownload = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cQueue = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tQueue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdIExplore);
            this.groupBox1.Controls.Add(this.cmdFirefox);
            this.groupBox1.Controls.Add(this.cmdOpera);
            this.groupBox1.Controls.Add(this.tLink);
            this.groupBox1.ForeColor = System.Drawing.Color.LightBlue;
            this.groupBox1.Location = new System.Drawing.Point(12, 186);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(311, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Youtube link";
            // 
            // cmdIExplore
            // 
            this.cmdIExplore.ForeColor = System.Drawing.Color.Black;
            this.cmdIExplore.Location = new System.Drawing.Point(209, 45);
            this.cmdIExplore.Name = "cmdIExplore";
            this.cmdIExplore.Size = new System.Drawing.Size(96, 23);
            this.cmdIExplore.TabIndex = 3;
            this.cmdIExplore.Text = "From iExplore";
            this.ToolTip.SetToolTip(this.cmdIExplore, "Read YouTube URL from Internet Explorer");
            this.cmdIExplore.UseVisualStyleBackColor = true;
            this.cmdIExplore.Click += new System.EventHandler(this.cmdIExplore_Click);
            // 
            // cmdFirefox
            // 
            this.cmdFirefox.ForeColor = System.Drawing.Color.Black;
            this.cmdFirefox.Location = new System.Drawing.Point(108, 45);
            this.cmdFirefox.Name = "cmdFirefox";
            this.cmdFirefox.Size = new System.Drawing.Size(95, 23);
            this.cmdFirefox.TabIndex = 2;
            this.cmdFirefox.Text = "From Firefox";
            this.ToolTip.SetToolTip(this.cmdFirefox, "Read YouTube URL from Firefox");
            this.cmdFirefox.UseVisualStyleBackColor = true;
            this.cmdFirefox.Click += new System.EventHandler(this.cmdFirefox_Click);
            // 
            // cmdOpera
            // 
            this.cmdOpera.ForeColor = System.Drawing.Color.Black;
            this.cmdOpera.Location = new System.Drawing.Point(6, 45);
            this.cmdOpera.Name = "cmdOpera";
            this.cmdOpera.Size = new System.Drawing.Size(96, 23);
            this.cmdOpera.TabIndex = 1;
            this.cmdOpera.Text = "From Opera";
            this.ToolTip.SetToolTip(this.cmdOpera, "Read YouTube URL from Opera");
            this.cmdOpera.UseVisualStyleBackColor = true;
            this.cmdOpera.Click += new System.EventHandler(this.cmdOpera_Click);
            // 
            // tLink
            // 
            this.tLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tLink.ForeColor = System.Drawing.Color.LightBlue;
            this.tLink.Location = new System.Drawing.Point(6, 19);
            this.tLink.Name = "tLink";
            this.tLink.Size = new System.Drawing.Size(299, 20);
            this.tLink.TabIndex = 6;
            this.tLink.Text = "http://www.youtube.com/watch?v=P_N0KYb6GOU";
            this.tLink.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.tLink, "Enter the YouTube URL here");
            this.tLink.Enter += new System.EventHandler(this.tLink_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cGetname);
            this.groupBox2.Controls.Add(this.tFile);
            this.groupBox2.ForeColor = System.Drawing.Color.LightBlue;
            this.groupBox2.Location = new System.Drawing.Point(12, 279);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 45);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Save as";
            // 
            // cGetname
            // 
            this.cGetname.ForeColor = System.Drawing.Color.Black;
            this.cGetname.Location = new System.Drawing.Point(244, 19);
            this.cGetname.Name = "cGetname";
            this.cGetname.Size = new System.Drawing.Size(61, 20);
            this.cGetname.TabIndex = 4;
            this.cGetname.Text = "Get name";
            this.ToolTip.SetToolTip(this.cGetname, "Read movie name from YouTube");
            this.cGetname.UseVisualStyleBackColor = true;
            this.cGetname.Click += new System.EventHandler(this.cGetname_Click);
            // 
            // tFile
            // 
            this.tFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tFile.ForeColor = System.Drawing.Color.LightBlue;
            this.tFile.Location = new System.Drawing.Point(6, 19);
            this.tFile.Name = "tFile";
            this.tFile.Size = new System.Drawing.Size(232, 20);
            this.tFile.TabIndex = 7;
            this.tFile.Text = "PraetoxYTD_Movie";
            this.ToolTip.SetToolTip(this.tFile, "Your preferred filename for the movie");
            this.tFile.Enter += new System.EventHandler(this.tFile_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 263);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "  ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 327);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "  ";
            // 
            // cDownload
            // 
            this.cDownload.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cDownload.ForeColor = System.Drawing.Color.Black;
            this.cDownload.Location = new System.Drawing.Point(12, 343);
            this.cDownload.Name = "cDownload";
            this.cDownload.Size = new System.Drawing.Size(311, 44);
            this.cDownload.TabIndex = 5;
            this.cDownload.Text = "Download";
            this.ToolTip.SetToolTip(this.cDownload, "Start the download");
            this.cDownload.UseVisualStyleBackColor = true;
            this.cDownload.Click += new System.EventHandler(this.cDownload_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(311, 155);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.ToolTip.SetToolTip(this.pictureBox1, "Praetox YouTube Downloader\r\n\r\nhttp://www.praetox.com/");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "  ";
            // 
            // cQueue
            // 
            this.cQueue.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cQueue.ForeColor = System.Drawing.Color.Black;
            this.cQueue.Location = new System.Drawing.Point(375, 343);
            this.cQueue.Name = "cQueue";
            this.cQueue.Size = new System.Drawing.Size(311, 44);
            this.cQueue.TabIndex = 9;
            this.cQueue.Text = "Batch download";
            this.ToolTip.SetToolTip(this.cQueue, "Download all the movies above");
            this.cQueue.UseVisualStyleBackColor = true;
            this.cQueue.Click += new System.EventHandler(this.cQueue_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 398);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(675, 42);
            this.label4.TabIndex = 8;
            this.label4.Text = "You will need an .flv player to watch the movies. Media Player Classic does this," +
                " and much more...\r\nMPC is part of the CCCP bundle. Go to my website and click Ti" +
                "ps for the download link.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 440);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "  ";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label6.Location = new System.Drawing.Point(348, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(2, 375);
            this.label6.TabIndex = 10;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tQueue);
            this.groupBox3.ForeColor = System.Drawing.Color.LightBlue;
            this.groupBox3.Location = new System.Drawing.Point(375, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(311, 312);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Batch download";
            // 
            // tQueue
            // 
            this.tQueue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tQueue.ForeColor = System.Drawing.Color.LightBlue;
            this.tQueue.Location = new System.Drawing.Point(6, 19);
            this.tQueue.Multiline = true;
            this.tQueue.Name = "tQueue";
            this.tQueue.Size = new System.Drawing.Size(299, 287);
            this.tQueue.TabIndex = 8;
            this.ToolTip.SetToolTip(this.tQueue, "If you have a lot of movies to download:\r\n\r\nPaste all the YouTube URLs here, one " +
                    "on each line.");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(375, 327);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "  ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(329, 208);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "  ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(356, 208);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "  ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(698, 444);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cQueue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cDownload);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.LightBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Praetox YouTube Downloader v";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tLink;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cGetname;
        private System.Windows.Forms.TextBox tFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cDownload;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cQueue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdOpera;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tQueue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button cmdIExplore;
        private System.Windows.Forms.Button cmdFirefox;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}

