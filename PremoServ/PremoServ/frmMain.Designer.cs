namespace PremoServ {
    partial class frmMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.guAutostart = new System.Windows.Forms.CheckBox();
            this.guToggleServ = new System.Windows.Forms.Button();
            this.guPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.guPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.guUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.guMask = new System.Windows.Forms.TextBox();
            this.guDoMask = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tServerLog = new System.Windows.Forms.Timer(this.components);
            this.guLog = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.guAutostart);
            this.groupBox1.Controls.Add(this.guToggleServ);
            this.groupBox1.Controls.Add(this.guPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 97);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server setup";
            // 
            // guAutostart
            // 
            this.guAutostart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.guAutostart.Location = new System.Drawing.Point(6, 45);
            this.guAutostart.Name = "guAutostart";
            this.guAutostart.Size = new System.Drawing.Size(193, 17);
            this.guAutostart.TabIndex = 2;
            this.guAutostart.Text = "Autostart on launch";
            this.guAutostart.UseVisualStyleBackColor = true;
            // 
            // guToggleServ
            // 
            this.guToggleServ.Location = new System.Drawing.Point(99, 71);
            this.guToggleServ.Name = "guToggleServ";
            this.guToggleServ.Size = new System.Drawing.Size(100, 23);
            this.guToggleServ.TabIndex = 2;
            this.guToggleServ.Text = "Start server";
            this.guToggleServ.UseVisualStyleBackColor = true;
            this.guToggleServ.Click += new System.EventHandler(this.guToggleServ_Click);
            // 
            // guPort
            // 
            this.guPort.Location = new System.Drawing.Point(149, 19);
            this.guPort.Name = "guPort";
            this.guPort.Size = new System.Drawing.Size(50, 20);
            this.guPort.TabIndex = 2;
            this.guPort.Text = "486";
            this.guPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Run on port";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.guPass);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.guUser);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.guMask);
            this.groupBox2.Controls.Add(this.guDoMask);
            this.groupBox2.Location = new System.Drawing.Point(263, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(205, 97);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Security";
            // 
            // guPass
            // 
            this.guPass.Location = new System.Drawing.Point(99, 45);
            this.guPass.Name = "guPass";
            this.guPass.Size = new System.Drawing.Size(100, 20);
            this.guPass.TabIndex = 6;
            this.guPass.Text = "tsukihime";
            this.guPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            // 
            // guUser
            // 
            this.guUser.Location = new System.Drawing.Point(99, 19);
            this.guUser.Name = "guUser";
            this.guUser.Size = new System.Drawing.Size(100, 20);
            this.guUser.TabIndex = 4;
            this.guUser.Text = "anon";
            this.guUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Username";
            // 
            // guMask
            // 
            this.guMask.Location = new System.Drawing.Point(99, 71);
            this.guMask.Name = "guMask";
            this.guMask.Size = new System.Drawing.Size(100, 20);
            this.guMask.TabIndex = 3;
            this.guMask.Text = "sub.sub.sub.x";
            this.guMask.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // guDoMask
            // 
            this.guDoMask.AutoSize = true;
            this.guDoMask.Location = new System.Drawing.Point(6, 74);
            this.guDoMask.Name = "guDoMask";
            this.guDoMask.Size = new System.Drawing.Size(74, 17);
            this.guDoMask.TabIndex = 0;
            this.guDoMask.Text = "Only allow";
            this.guDoMask.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.guLog);
            this.groupBox3.Location = new System.Drawing.Point(12, 112);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(456, 148);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Server event log";
            // 
            // tServerLog
            // 
            this.tServerLog.Enabled = true;
            this.tServerLog.Tick += new System.EventHandler(this.tServerLog_Tick);
            // 
            // guLog
            // 
            this.guLog.AutoSize = true;
            this.guLog.Location = new System.Drawing.Point(6, 16);
            this.guLog.Name = "guLog";
            this.guLog.Size = new System.Drawing.Size(19, 130);
            this.guLog.TabIndex = 0;
            this.guLog.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 272);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pRemoSuite :: Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox guPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button guToggleServ;
        private System.Windows.Forms.CheckBox guAutostart;
        private System.Windows.Forms.CheckBox guDoMask;
        private System.Windows.Forms.TextBox guMask;
        private System.Windows.Forms.TextBox guPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox guUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Timer tServerLog;
        private System.Windows.Forms.Label guLog;
    }
}

