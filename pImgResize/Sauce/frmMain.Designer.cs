namespace pImgResize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.R13 = new System.Windows.Forms.RadioButton();
            this.R12 = new System.Windows.Forms.RadioButton();
            this.R11 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.R23 = new System.Windows.Forms.RadioButton();
            this.R22 = new System.Windows.Forms.RadioButton();
            this.R21 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.R33 = new System.Windows.Forms.RadioButton();
            this.R32 = new System.Windows.Forms.RadioButton();
            this.R31 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmdGo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkAspect = new System.Windows.Forms.CheckBox();
            this.txtRes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pLogo = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.R13);
            this.groupBox1.Controls.Add(this.R12);
            this.groupBox1.Controls.Add(this.R11);
            this.groupBox1.ForeColor = System.Drawing.Color.Azure;
            this.groupBox1.Location = new System.Drawing.Point(306, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 42);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Antialias";
            // 
            // R13
            // 
            this.R13.AutoSize = true;
            this.R13.Location = new System.Drawing.Point(187, 19);
            this.R13.Name = "R13";
            this.R13.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.R13.Size = new System.Drawing.Size(63, 17);
            this.R13.TabIndex = 2;
            this.R13.Text = "None";
            this.R13.UseVisualStyleBackColor = true;
            // 
            // R12
            // 
            this.R12.AutoSize = true;
            this.R12.Checked = true;
            this.R12.Location = new System.Drawing.Point(100, 19);
            this.R12.Name = "R12";
            this.R12.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.R12.Size = new System.Drawing.Size(65, 17);
            this.R12.TabIndex = 1;
            this.R12.TabStop = true;
            this.R12.Text = "Sharp";
            this.R12.UseVisualStyleBackColor = true;
            // 
            // R11
            // 
            this.R11.AutoSize = true;
            this.R11.Location = new System.Drawing.Point(6, 19);
            this.R11.Name = "R11";
            this.R11.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.R11.Size = new System.Drawing.Size(73, 17);
            this.R11.TabIndex = 0;
            this.R11.Text = "Smooth";
            this.R11.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.R23);
            this.groupBox2.Controls.Add(this.R22);
            this.groupBox2.Controls.Add(this.R21);
            this.groupBox2.ForeColor = System.Drawing.Color.Azure;
            this.groupBox2.Location = new System.Drawing.Point(306, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 42);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Save images to";
            // 
            // R23
            // 
            this.R23.AutoSize = true;
            this.R23.Checked = true;
            this.R23.Location = new System.Drawing.Point(187, 19);
            this.R23.Name = "R23";
            this.R23.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.R23.Size = new System.Drawing.Size(59, 17);
            this.R23.TabIndex = 2;
            this.R23.TabStop = true;
            this.R23.Text = "Both";
            this.R23.UseVisualStyleBackColor = true;
            // 
            // R22
            // 
            this.R22.AutoSize = true;
            this.R22.Location = new System.Drawing.Point(100, 19);
            this.R22.Name = "R22";
            this.R22.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.R22.Size = new System.Drawing.Size(81, 17);
            this.R22.TabIndex = 1;
            this.R22.Text = "Clipboard";
            this.R22.UseVisualStyleBackColor = true;
            // 
            // R21
            // 
            this.R21.AutoSize = true;
            this.R21.Location = new System.Drawing.Point(6, 19);
            this.R21.Name = "R21";
            this.R21.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.R21.Size = new System.Drawing.Size(88, 17);
            this.R21.TabIndex = 0;
            this.R21.Text = "Own folder";
            this.R21.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(56)))));
            this.label1.Location = new System.Drawing.Point(306, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 12);
            this.label1.TabIndex = 2;
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(56)))));
            this.label2.Location = new System.Drawing.Point(306, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 12);
            this.label2.TabIndex = 3;
            this.label2.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.R33);
            this.groupBox3.Controls.Add(this.R32);
            this.groupBox3.Controls.Add(this.R31);
            this.groupBox3.ForeColor = System.Drawing.Color.Azure;
            this.groupBox3.Location = new System.Drawing.Point(306, 195);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(270, 42);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Save image as";
            // 
            // R33
            // 
            this.R33.AutoSize = true;
            this.R33.Location = new System.Drawing.Point(187, 19);
            this.R33.Name = "R33";
            this.R33.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.R33.Size = new System.Drawing.Size(54, 17);
            this.R33.TabIndex = 2;
            this.R33.Text = "GIF";
            this.R33.UseVisualStyleBackColor = true;
            // 
            // R32
            // 
            this.R32.AutoSize = true;
            this.R32.Checked = true;
            this.R32.Location = new System.Drawing.Point(100, 19);
            this.R32.Name = "R32";
            this.R32.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.R32.Size = new System.Drawing.Size(60, 17);
            this.R32.TabIndex = 1;
            this.R32.TabStop = true;
            this.R32.Text = "PNG";
            this.R32.UseVisualStyleBackColor = true;
            // 
            // R31
            // 
            this.R31.AutoSize = true;
            this.R31.Location = new System.Drawing.Point(6, 19);
            this.R31.Name = "R31";
            this.R31.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.R31.Size = new System.Drawing.Size(57, 17);
            this.R31.TabIndex = 0;
            this.R31.Text = "JPG";
            this.R31.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmdGo);
            this.groupBox4.ForeColor = System.Drawing.Color.Azure;
            this.groupBox4.Location = new System.Drawing.Point(12, 120);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(270, 117);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Resize image";
            // 
            // cmdGo
            // 
            this.cmdGo.AllowDrop = true;
            this.cmdGo.Location = new System.Drawing.Point(6, 19);
            this.cmdGo.Name = "cmdGo";
            this.cmdGo.Size = new System.Drawing.Size(258, 92);
            this.cmdGo.TabIndex = 0;
            this.cmdGo.Text = "Click here to resize image on clipboard\r\n--OR--\r\nDrag and drop any image file her" +
                "e";
            this.cmdGo.UseVisualStyleBackColor = true;
            this.cmdGo.Click += new System.EventHandler(this.cmdGo_Click);
            this.cmdGo.DragDrop += new System.Windows.Forms.DragEventHandler(this.cmdGo_DragDrop);
            this.cmdGo.DragEnter += new System.Windows.Forms.DragEventHandler(this.cmdGo_DragEnter);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(56)))));
            this.label4.Location = new System.Drawing.Point(306, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 12);
            this.label4.TabIndex = 8;
            this.label4.Visible = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkAspect);
            this.groupBox5.Controls.Add(this.txtRes);
            this.groupBox5.ForeColor = System.Drawing.Color.Azure;
            this.groupBox5.Location = new System.Drawing.Point(306, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(270, 45);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "New resolution";
            // 
            // chkAspect
            // 
            this.chkAspect.AutoSize = true;
            this.chkAspect.Checked = true;
            this.chkAspect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAspect.Location = new System.Drawing.Point(178, 20);
            this.chkAspect.Name = "chkAspect";
            this.chkAspect.Size = new System.Drawing.Size(86, 17);
            this.chkAspect.TabIndex = 1;
            this.chkAspect.Text = "Keep aspect";
            this.chkAspect.UseVisualStyleBackColor = true;
            // 
            // txtRes
            // 
            this.txtRes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(56)))));
            this.txtRes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRes.ForeColor = System.Drawing.Color.Azure;
            this.txtRes.Location = new System.Drawing.Point(6, 19);
            this.txtRes.Name = "txtRes";
            this.txtRes.Size = new System.Drawing.Size(166, 20);
            this.txtRes.TabIndex = 0;
            this.txtRes.Text = "1920x1200";
            this.txtRes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(56)))));
            this.label3.Location = new System.Drawing.Point(288, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 12);
            this.label3.TabIndex = 9;
            this.label3.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(56)))));
            this.label5.Location = new System.Drawing.Point(12, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 12);
            this.label5.TabIndex = 10;
            this.label5.Visible = false;
            // 
            // pLogo
            // 
            this.pLogo.Image = ((System.Drawing.Image)(resources.GetObject("pLogo.Image")));
            this.pLogo.Location = new System.Drawing.Point(12, 12);
            this.pLogo.Name = "pLogo";
            this.pLogo.Size = new System.Drawing.Size(270, 90);
            this.pLogo.TabIndex = 11;
            this.pLogo.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(588, 249);
            this.Controls.Add(this.pLogo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.Azure;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Opacity = 0.9;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pImgResize";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton R11;
        private System.Windows.Forms.RadioButton R13;
        private System.Windows.Forms.RadioButton R12;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton R23;
        private System.Windows.Forms.RadioButton R22;
        private System.Windows.Forms.RadioButton R21;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton R33;
        private System.Windows.Forms.RadioButton R32;
        private System.Windows.Forms.RadioButton R31;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button cmdGo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtRes;
        private System.Windows.Forms.CheckBox chkAspect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pLogo;
    }
}

