namespace pGlass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tMain = new System.Windows.Forms.Timer(this.components);
            this.Dummy = new System.Windows.Forms.Button();
            this.Tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.kWith = new System.Windows.Forms.TextBox();
            this.kTransparent = new System.Windows.Forms.TextBox();
            this.kOpaque = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmdExit = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tMain
            // 
            this.tMain.Enabled = true;
            this.tMain.Interval = 1;
            this.tMain.Tick += new System.EventHandler(this.tMain_Tick);
            // 
            // Dummy
            // 
            this.Dummy.Location = new System.Drawing.Point(100, -100);
            this.Dummy.Name = "Dummy";
            this.Dummy.Size = new System.Drawing.Size(75, 23);
            this.Dummy.TabIndex = 2;
            this.Dummy.Text = "button1";
            this.Dummy.UseVisualStyleBackColor = true;
            // 
            // kWith
            // 
            this.kWith.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.kWith.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kWith.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kWith.ForeColor = System.Drawing.Color.White;
            this.kWith.Location = new System.Drawing.Point(87, 65);
            this.kWith.Name = "kWith";
            this.kWith.Size = new System.Drawing.Size(40, 21);
            this.kWith.TabIndex = 11;
            this.kWith.Text = "17";
            this.kWith.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tooltip.SetToolTip(this.kWith, "Key required for other hotkeys to work.\r\nStandard value: 17 (Control)");
            this.kWith.KeyUp += new System.Windows.Forms.KeyEventHandler(this.kWith_KeyUp);
            this.kWith.MouseClick += new System.Windows.Forms.MouseEventHandler(this.kWith_MouseClick);
            // 
            // kTransparent
            // 
            this.kTransparent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.kTransparent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kTransparent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kTransparent.ForeColor = System.Drawing.Color.White;
            this.kTransparent.Location = new System.Drawing.Point(87, 38);
            this.kTransparent.Name = "kTransparent";
            this.kTransparent.Size = new System.Drawing.Size(40, 21);
            this.kTransparent.TabIndex = 9;
            this.kTransparent.Text = "40";
            this.kTransparent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tooltip.SetToolTip(this.kTransparent, "Hotkey to make focused application less opaque.\r\nStandard value: 40 (Arrow down)");
            // 
            // kOpaque
            // 
            this.kOpaque.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.kOpaque.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kOpaque.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kOpaque.ForeColor = System.Drawing.Color.White;
            this.kOpaque.Location = new System.Drawing.Point(87, 11);
            this.kOpaque.Name = "kOpaque";
            this.kOpaque.Size = new System.Drawing.Size(40, 21);
            this.kOpaque.TabIndex = 8;
            this.kOpaque.Text = "38";
            this.kOpaque.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tooltip.SetToolTip(this.kOpaque, "Hotkey to make focused application more opaque.\r\nStandard value: 38 (Arrow up)");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(48)))), ((int)(((byte)(56)))));
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.kWith);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.kTransparent);
            this.panel1.Controls.Add(this.kOpaque);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 126);
            this.panel1.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(104, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(8, 8);
            this.label5.TabIndex = 18;
            this.label5.Visible = false;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(104, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(8, 8);
            this.label9.TabIndex = 17;
            this.label9.Visible = false;
            // 
            // cmdExit
            // 
            this.cmdExit.Location = new System.Drawing.Point(20, 92);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(107, 23);
            this.cmdExit.TabIndex = 4;
            this.cmdExit.Text = "Exit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(133, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(8, 8);
            this.label8.TabIndex = 16;
            this.label8.Visible = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(133, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(8, 8);
            this.label6.TabIndex = 14;
            this.label6.Visible = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(8, 8);
            this.label4.TabIndex = 12;
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(17, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "With...";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Transparent";
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Opaque";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(168, 150);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Dummy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "pGlass";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Activated += new System.EventHandler(this.frmMain_Activated);
            this.Enter += new System.EventHandler(this.frmMain_Enter);
            this.Leave += new System.EventHandler(this.frmMain_Leave);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tMain;
        private System.Windows.Forms.Button Dummy;
        private System.Windows.Forms.ToolTip Tooltip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox kWith;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox kTransparent;
        private System.Windows.Forms.TextBox kOpaque;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdExit;
    }
}

