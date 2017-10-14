namespace Preenshot
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
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cmdPath = new System.Windows.Forms.Button();
            this.tHotkeys = new System.Windows.Forms.Timer(this.components);
            this.rTIF = new System.Windows.Forms.RadioButton();
            this.rGIF = new System.Windows.Forms.RadioButton();
            this.rPNG = new System.Windows.Forms.RadioButton();
            this.rJPG = new System.Windows.Forms.RadioButton();
            this.rBMP = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkCrop = new System.Windows.Forms.CheckBox();
            this.txtCrop = new System.Windows.Forms.TextBox();
            this.cmdCrop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbClipAll = new System.Windows.Forms.Label();
            this.chkUploadNotify = new System.Windows.Forms.CheckBox();
            this.chkUploadClip = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rUP_Imagehost = new System.Windows.Forms.RadioButton();
            this.rUP_Tinypic = new System.Windows.Forms.RadioButton();
            this.rUP_NA = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.chkResize = new System.Windows.Forms.CheckBox();
            this.txtResize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbClrMem = new System.Windows.Forms.Label();
            this.ttip = new System.Windows.Forms.ToolTip(this.components);
            this.pbHead = new System.Windows.Forms.PictureBox();
            this.nico = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.state = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHead)).BeginInit();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPath.ForeColor = System.Drawing.Color.White;
            this.txtPath.Location = new System.Drawing.Point(6, 22);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(249, 21);
            this.txtPath.TabIndex = 2;
            this.ttip.SetToolTip(this.txtPath, "Where capped images are saved");
            // 
            // cmdPath
            // 
            this.cmdPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.cmdPath.ForeColor = System.Drawing.Color.White;
            this.cmdPath.Location = new System.Drawing.Point(261, 19);
            this.cmdPath.Name = "cmdPath";
            this.cmdPath.Size = new System.Drawing.Size(31, 23);
            this.cmdPath.TabIndex = 0;
            this.cmdPath.Text = "...";
            this.ttip.SetToolTip(this.cmdPath, "Browse for a target directory");
            this.cmdPath.UseVisualStyleBackColor = false;
            this.cmdPath.Click += new System.EventHandler(this.cmdPath_Click);
            // 
            // tHotkeys
            // 
            this.tHotkeys.Enabled = true;
            this.tHotkeys.Interval = 1;
            this.tHotkeys.Tick += new System.EventHandler(this.tHotkeys_Tick);
            // 
            // rTIF
            // 
            this.rTIF.ForeColor = System.Drawing.Color.Silver;
            this.rTIF.Location = new System.Drawing.Point(246, 22);
            this.rTIF.Name = "rTIF";
            this.rTIF.Size = new System.Drawing.Size(54, 17);
            this.rTIF.TabIndex = 4;
            this.rTIF.Text = "TIF";
            this.ttip.SetToolTip(this.rTIF, "I don\'t know why I added this sh!t.");
            this.rTIF.UseVisualStyleBackColor = true;
            this.rTIF.CheckedChanged += new System.EventHandler(this.rTIF_CheckedChanged);
            // 
            // rGIF
            // 
            this.rGIF.Checked = true;
            this.rGIF.ForeColor = System.Drawing.Color.White;
            this.rGIF.Location = new System.Drawing.Point(186, 22);
            this.rGIF.Name = "rGIF";
            this.rGIF.Size = new System.Drawing.Size(54, 17);
            this.rGIF.TabIndex = 3;
            this.rGIF.TabStop = true;
            this.rGIF.Text = "GIF";
            this.ttip.SetToolTip(this.rGIF, "Only 256 colours, but creates small files.");
            this.rGIF.UseVisualStyleBackColor = true;
            this.rGIF.CheckedChanged += new System.EventHandler(this.rGIF_CheckedChanged);
            // 
            // rPNG
            // 
            this.rPNG.ForeColor = System.Drawing.Color.Silver;
            this.rPNG.Location = new System.Drawing.Point(126, 22);
            this.rPNG.Name = "rPNG";
            this.rPNG.Size = new System.Drawing.Size(54, 17);
            this.rPNG.TabIndex = 2;
            this.rPNG.Text = "PNG";
            this.ttip.SetToolTip(this.rPNG, "Best for text, drawings and basic grapghics.");
            this.rPNG.UseVisualStyleBackColor = true;
            this.rPNG.CheckedChanged += new System.EventHandler(this.rPNG_CheckedChanged);
            // 
            // rJPG
            // 
            this.rJPG.ForeColor = System.Drawing.Color.Silver;
            this.rJPG.Location = new System.Drawing.Point(66, 22);
            this.rJPG.Name = "rJPG";
            this.rJPG.Size = new System.Drawing.Size(54, 17);
            this.rJPG.TabIndex = 1;
            this.rJPG.Text = "JPG";
            this.ttip.SetToolTip(this.rJPG, "Best for detailed and/or real-life pictures.");
            this.rJPG.UseVisualStyleBackColor = true;
            this.rJPG.CheckedChanged += new System.EventHandler(this.rJPG_CheckedChanged);
            // 
            // rBMP
            // 
            this.rBMP.ForeColor = System.Drawing.Color.Silver;
            this.rBMP.Location = new System.Drawing.Point(6, 22);
            this.rBMP.Name = "rBMP";
            this.rBMP.Size = new System.Drawing.Size(54, 17);
            this.rBMP.TabIndex = 0;
            this.rBMP.Text = "BMP";
            this.ttip.SetToolTip(this.rBMP, "Never use bmp. It\'s crap.");
            this.rBMP.UseVisualStyleBackColor = true;
            this.rBMP.CheckedChanged += new System.EventHandler(this.rBMP_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cmdPath);
            this.panel1.Controls.Add(this.txtPath);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(17, 91);
            this.panel1.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(300, 51);
            this.panel1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(286, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Save image to";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.rTIF);
            this.panel2.Controls.Add(this.rGIF);
            this.panel2.Controls.Add(this.rPNG);
            this.panel2.Controls.Add(this.rJPG);
            this.panel2.Controls.Add(this.rBMP);
            this.panel2.Location = new System.Drawing.Point(17, 154);
            this.panel2.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(300, 47);
            this.panel2.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(286, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Image format";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.chkCrop);
            this.panel3.Controls.Add(this.txtCrop);
            this.panel3.Controls.Add(this.cmdCrop);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(17, 213);
            this.panel3.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(300, 51);
            this.panel3.TabIndex = 10;
            // 
            // chkCrop
            // 
            this.chkCrop.AutoSize = true;
            this.chkCrop.Location = new System.Drawing.Point(219, 23);
            this.chkCrop.Name = "chkCrop";
            this.chkCrop.Size = new System.Drawing.Size(73, 17);
            this.chkCrop.TabIndex = 4;
            this.chkCrop.Text = "after SS";
            this.ttip.SetToolTip(this.chkCrop, "Allows you to crop the image after each screenshot.");
            this.chkCrop.UseVisualStyleBackColor = true;
            this.chkCrop.CheckedChanged += new System.EventHandler(this.chkCrop_CheckedChanged);
            // 
            // txtCrop
            // 
            this.txtCrop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.txtCrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCrop.ForeColor = System.Drawing.Color.White;
            this.txtCrop.Location = new System.Drawing.Point(6, 22);
            this.txtCrop.Name = "txtCrop";
            this.txtCrop.Size = new System.Drawing.Size(154, 21);
            this.txtCrop.TabIndex = 3;
            this.ttip.SetToolTip(this.txtCrop, "Enter a region to crop the images.\r\nLleave empty to save entire image.\r\n\r\nFormat:" +
                    "    Top x Left | Width x Height");
            // 
            // cmdCrop
            // 
            this.cmdCrop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.cmdCrop.ForeColor = System.Drawing.Color.White;
            this.cmdCrop.Location = new System.Drawing.Point(166, 20);
            this.cmdCrop.Name = "cmdCrop";
            this.cmdCrop.Size = new System.Drawing.Size(47, 23);
            this.cmdCrop.TabIndex = 0;
            this.cmdCrop.Text = "Set";
            this.ttip.SetToolTip(this.cmdCrop, "Set the cropping area now.");
            this.cmdCrop.UseVisualStyleBackColor = false;
            this.cmdCrop.Click += new System.EventHandler(this.cmdCrop_Click);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(6, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(286, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Crop image";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lbClrMem);
            this.panel4.Controls.Add(this.lbClipAll);
            this.panel4.Controls.Add(this.chkUploadNotify);
            this.panel4.Controls.Add(this.chkUploadClip);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.rUP_Imagehost);
            this.panel4.Controls.Add(this.rUP_Tinypic);
            this.panel4.Controls.Add(this.rUP_NA);
            this.panel4.Location = new System.Drawing.Point(17, 339);
            this.panel4.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3);
            this.panel4.Size = new System.Drawing.Size(300, 93);
            this.panel4.TabIndex = 11;
            // 
            // lbClipAll
            // 
            this.lbClipAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbClipAll.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClipAll.ForeColor = System.Drawing.Color.Silver;
            this.lbClipAll.Location = new System.Drawing.Point(140, 70);
            this.lbClipAll.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lbClipAll.Name = "lbClipAll";
            this.lbClipAll.Size = new System.Drawing.Size(85, 13);
            this.lbClipAll.TabIndex = 8;
            this.lbClipAll.Text = "Clipboard all";
            this.lbClipAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ttip.SetToolTip(this.lbClipAll, "Put links to all uploaded images on clipboard NAO.");
            this.lbClipAll.Click += new System.EventHandler(this.lbClipAll_Click);
            // 
            // chkUploadNotify
            // 
            this.chkUploadNotify.AutoSize = true;
            this.chkUploadNotify.Checked = true;
            this.chkUploadNotify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUploadNotify.Location = new System.Drawing.Point(140, 45);
            this.chkUploadNotify.Name = "chkUploadNotify";
            this.chkUploadNotify.Size = new System.Drawing.Size(130, 17);
            this.chkUploadNotify.TabIndex = 7;
            this.chkUploadNotify.Text = "Notify when ready";
            this.ttip.SetToolTip(this.chkUploadNotify, "Plays a sound when the upload has completed.");
            this.chkUploadNotify.UseVisualStyleBackColor = true;
            this.chkUploadNotify.CheckedChanged += new System.EventHandler(this.chkUploadNotify_CheckedChanged);
            // 
            // chkUploadClip
            // 
            this.chkUploadClip.AutoSize = true;
            this.chkUploadClip.Checked = true;
            this.chkUploadClip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUploadClip.Location = new System.Drawing.Point(140, 22);
            this.chkUploadClip.Name = "chkUploadClip";
            this.chkUploadClip.Size = new System.Drawing.Size(111, 17);
            this.chkUploadClip.TabIndex = 6;
            this.chkUploadClip.Text = "Clipboard links";
            this.ttip.SetToolTip(this.chkUploadClip, "Put a link to the image on your clipboard as soon as it\'s uploaded.");
            this.chkUploadClip.UseVisualStyleBackColor = true;
            this.chkUploadClip.CheckedChanged += new System.EventHandler(this.chkUploadClip_CheckedChanged);
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(6, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(286, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Spread your FABULOUS screenshots";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rUP_Imagehost
            // 
            this.rUP_Imagehost.Checked = true;
            this.rUP_Imagehost.ForeColor = System.Drawing.Color.White;
            this.rUP_Imagehost.Location = new System.Drawing.Point(7, 68);
            this.rUP_Imagehost.Name = "rUP_Imagehost";
            this.rUP_Imagehost.Size = new System.Drawing.Size(126, 17);
            this.rUP_Imagehost.TabIndex = 2;
            this.rUP_Imagehost.TabStop = true;
            this.rUP_Imagehost.Text = "Imagehost.org";
            this.ttip.SetToolTip(this.rUP_Imagehost, "Upload images to http://imagehost.org/");
            this.rUP_Imagehost.UseVisualStyleBackColor = true;
            this.rUP_Imagehost.CheckedChanged += new System.EventHandler(this.rUP_Imagehost_CheckedChanged);
            // 
            // rUP_Tinypic
            // 
            this.rUP_Tinypic.Enabled = false;
            this.rUP_Tinypic.ForeColor = System.Drawing.Color.Silver;
            this.rUP_Tinypic.Location = new System.Drawing.Point(7, 45);
            this.rUP_Tinypic.Name = "rUP_Tinypic";
            this.rUP_Tinypic.Size = new System.Drawing.Size(126, 17);
            this.rUP_Tinypic.TabIndex = 1;
            this.rUP_Tinypic.Text = "Tinypic.com";
            this.rUP_Tinypic.UseVisualStyleBackColor = true;
            this.rUP_Tinypic.CheckedChanged += new System.EventHandler(this.rUP_Tinypic_CheckedChanged);
            // 
            // rUP_NA
            // 
            this.rUP_NA.ForeColor = System.Drawing.Color.Silver;
            this.rUP_NA.Location = new System.Drawing.Point(7, 22);
            this.rUP_NA.Name = "rUP_NA";
            this.rUP_NA.Size = new System.Drawing.Size(126, 17);
            this.rUP_NA.TabIndex = 0;
            this.rUP_NA.Text = "Not enabled";
            this.ttip.SetToolTip(this.rUP_NA, "Do not upload images.");
            this.rUP_NA.UseVisualStyleBackColor = true;
            this.rUP_NA.CheckedChanged += new System.EventHandler(this.rUP_NA_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.chkResize);
            this.panel5.Controls.Add(this.txtResize);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Location = new System.Drawing.Point(17, 276);
            this.panel5.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3);
            this.panel5.Size = new System.Drawing.Size(300, 51);
            this.panel5.TabIndex = 12;
            // 
            // chkResize
            // 
            this.chkResize.AutoSize = true;
            this.chkResize.Checked = true;
            this.chkResize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkResize.Location = new System.Drawing.Point(166, 23);
            this.chkResize.Name = "chkResize";
            this.chkResize.Size = new System.Drawing.Size(126, 17);
            this.chkResize.TabIndex = 5;
            this.chkResize.Text = "Keep aspect ratio";
            this.ttip.SetToolTip(this.chkResize, "Will not stretch the image when checked.");
            this.chkResize.UseVisualStyleBackColor = true;
            this.chkResize.CheckedChanged += new System.EventHandler(this.chkResize_CheckedChanged);
            // 
            // txtResize
            // 
            this.txtResize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.txtResize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResize.ForeColor = System.Drawing.Color.White;
            this.txtResize.Location = new System.Drawing.Point(6, 22);
            this.txtResize.Name = "txtResize";
            this.txtResize.Size = new System.Drawing.Size(154, 21);
            this.txtResize.TabIndex = 3;
            this.ttip.SetToolTip(this.txtResize, "Enter a size here to resize capped images.\r\nLeave empty to save image untouched.\r" +
                    "\n\r\nFormat:    Width x Height");
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Silver;
            this.label5.Location = new System.Drawing.Point(6, 3);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(286, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Resize image";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbClrMem
            // 
            this.lbClrMem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbClrMem.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClrMem.ForeColor = System.Drawing.Color.Silver;
            this.lbClrMem.Location = new System.Drawing.Point(231, 70);
            this.lbClrMem.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lbClrMem.Name = "lbClrMem";
            this.lbClrMem.Size = new System.Drawing.Size(61, 13);
            this.lbClrMem.TabIndex = 9;
            this.lbClrMem.Text = "Clr mem";
            this.lbClrMem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ttip.SetToolTip(this.lbClrMem, "Clear memory of previously uploaded images.");
            this.lbClrMem.Click += new System.EventHandler(this.lbClrMem_Click);
            // 
            // pbHead
            // 
            this.pbHead.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbHead.Image = ((System.Drawing.Image)(resources.GetObject("pbHead.Image")));
            this.pbHead.Location = new System.Drawing.Point(17, 15);
            this.pbHead.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.pbHead.Name = "pbHead";
            this.pbHead.Size = new System.Drawing.Size(300, 64);
            this.pbHead.TabIndex = 13;
            this.pbHead.TabStop = false;
            this.pbHead.Click += new System.EventHandler(this.pbHead_Click);
            // 
            // nico
            // 
            this.nico.Text = "Preenshot";
            this.nico.Visible = true;
            this.nico.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nico_MouseClick);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.state);
            this.panel6.Location = new System.Drawing.Point(17, 444);
            this.panel6.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3);
            this.panel6.Size = new System.Drawing.Size(300, 24);
            this.panel6.TabIndex = 14;
            // 
            // state
            // 
            this.state.ForeColor = System.Drawing.Color.White;
            this.state.Location = new System.Drawing.Point(6, 3);
            this.state.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.state.Name = "state";
            this.state.Size = new System.Drawing.Size(286, 13);
            this.state.TabIndex = 0;
            this.state.Text = "Gimme a mo\', checking for updates.";
            this.state.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(334, 483);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.pbHead);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preenshot BETA";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHead)).EndInit();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button cmdPath;
        private System.Windows.Forms.Timer tHotkeys;
        private System.Windows.Forms.RadioButton rTIF;
        private System.Windows.Forms.RadioButton rGIF;
        private System.Windows.Forms.RadioButton rPNG;
        private System.Windows.Forms.RadioButton rJPG;
        private System.Windows.Forms.RadioButton rBMP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button cmdCrop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkCrop;
        private System.Windows.Forms.TextBox txtCrop;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rUP_Imagehost;
        private System.Windows.Forms.RadioButton rUP_Tinypic;
        private System.Windows.Forms.RadioButton rUP_NA;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox chkResize;
        private System.Windows.Forms.TextBox txtResize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbClipAll;
        private System.Windows.Forms.CheckBox chkUploadNotify;
        private System.Windows.Forms.CheckBox chkUploadClip;
        private System.Windows.Forms.Label lbClrMem;
        private System.Windows.Forms.ToolTip ttip;
        private System.Windows.Forms.PictureBox pbHead;
        private System.Windows.Forms.NotifyIcon nico;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label state;
    }
}

