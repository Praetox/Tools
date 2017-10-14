namespace PremoClie {
    partial class frmLogin {
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.guPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.guConnect = new System.Windows.Forms.Button();
            this.guServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.guPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.guUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.guSave = new System.Windows.Forms.Button();
            this.guProfile = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.guStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.guPort);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.guConnect);
            this.groupBox2.Controls.Add(this.guServer);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.guPass);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.guUser);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(205, 152);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quick Connect";
            // 
            // guPort
            // 
            this.guPort.Location = new System.Drawing.Point(99, 45);
            this.guPort.Name = "guPort";
            this.guPort.Size = new System.Drawing.Size(100, 20);
            this.guPort.TabIndex = 2;
            this.guPort.Text = "486";
            this.guPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Server port";
            // 
            // guConnect
            // 
            this.guConnect.Location = new System.Drawing.Point(99, 123);
            this.guConnect.Name = "guConnect";
            this.guConnect.Size = new System.Drawing.Size(100, 23);
            this.guConnect.TabIndex = 5;
            this.guConnect.Text = "Connect now";
            this.guConnect.UseVisualStyleBackColor = true;
            this.guConnect.Click += new System.EventHandler(this.guConnect_Click);
            // 
            // guServer
            // 
            this.guServer.Location = new System.Drawing.Point(99, 19);
            this.guServer.Name = "guServer";
            this.guServer.Size = new System.Drawing.Size(100, 20);
            this.guServer.TabIndex = 1;
            this.guServer.Text = "192.168.0.101";
            this.guServer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Server IP";
            // 
            // guPass
            // 
            this.guPass.Location = new System.Drawing.Point(99, 97);
            this.guPass.Name = "guPass";
            this.guPass.Size = new System.Drawing.Size(100, 20);
            this.guPass.TabIndex = 4;
            this.guPass.Text = "tsukihime";
            this.guPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            // 
            // guUser
            // 
            this.guUser.Location = new System.Drawing.Point(99, 71);
            this.guUser.Name = "guUser";
            this.guUser.Size = new System.Drawing.Size(100, 20);
            this.guUser.TabIndex = 3;
            this.guUser.Text = "anon";
            this.guUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Username";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.guSave);
            this.groupBox1.Controls.Add(this.guProfile);
            this.groupBox1.Location = new System.Drawing.Point(12, 170);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 75);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection profiles";
            // 
            // guSave
            // 
            this.guSave.Location = new System.Drawing.Point(6, 46);
            this.guSave.Name = "guSave";
            this.guSave.Size = new System.Drawing.Size(193, 23);
            this.guSave.TabIndex = 7;
            this.guSave.Text = "Save to this profile";
            this.guSave.UseVisualStyleBackColor = true;
            this.guSave.Click += new System.EventHandler(this.guSave_Click);
            // 
            // guProfile
            // 
            this.guProfile.FormattingEnabled = true;
            this.guProfile.Location = new System.Drawing.Point(6, 19);
            this.guProfile.Name = "guProfile";
            this.guProfile.Size = new System.Drawing.Size(193, 21);
            this.guProfile.TabIndex = 6;
            this.guProfile.SelectedIndexChanged += new System.EventHandler(this.guProfile_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.guStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 258);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(229, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // guStatus
            // 
            this.guStatus.Name = "guStatus";
            this.guStatus.Size = new System.Drawing.Size(46, 17);
            this.guStatus.Text = "Inactive";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 280);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect to server";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox guServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox guPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox guUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button guConnect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button guSave;
        private System.Windows.Forms.ComboBox guProfile;
        private System.Windows.Forms.TextBox guPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel guStatus;

    }
}