namespace GRebind
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
            this.Logo = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Target_dd = new System.Windows.Forms.ComboBox();
            this.Profile_cmdSave = new System.Windows.Forms.Button();
            this.Profile_cmdLoad = new System.Windows.Forms.Button();
            this.Keys_cmdRem = new System.Windows.Forms.Button();
            this.Keys_cmdAdd = new System.Windows.Forms.Button();
            this.Keys_lst = new System.Windows.Forms.ListBox();
            this.tPollFocus = new System.Windows.Forms.Timer(this.components);
            this.tPollProc = new System.Windows.Forms.Timer(this.components);
            this.cmdDummy = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Options_Rapid_txtUF = new System.Windows.Forms.TextBox();
            this.Options_Rapid_txtDF = new System.Windows.Forms.TextBox();
            this.Options_Rapid_txtU = new System.Windows.Forms.TextBox();
            this.Options_Rapid_txtD = new System.Windows.Forms.TextBox();
            this.Options_chkRapid = new System.Windows.Forms.CheckBox();
            this.Options_chkInject = new System.Windows.Forms.CheckBox();
            this.tRapid = new System.Windows.Forms.Timer(this.components);
            this.Target_pn = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.Profile_dd = new System.Windows.Forms.ComboBox();
            this.Profile_pn = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.nIco = new System.Windows.Forms.NotifyIcon(this.components);
            this.msg = new System.Windows.Forms.Label();
            this.Keys_pn = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Options_pn = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.tHide = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.Options_CharCodeMap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.Target_pn.SuspendLayout();
            this.Profile_pn.SuspendLayout();
            this.Keys_pn.SuspendLayout();
            this.Options_pn.SuspendLayout();
            this.SuspendLayout();
            // 
            // Logo
            // 
            this.Logo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Logo.Image = ((System.Drawing.Image)(resources.GetObject("Logo.Image")));
            this.Logo.Location = new System.Drawing.Point(17, 15);
            this.Logo.Margin = new System.Windows.Forms.Padding(6);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(194, 93);
            this.Logo.TabIndex = 0;
            this.Logo.TabStop = false;
            this.Logo.Click += new System.EventHandler(this.Logo_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 12);
            this.label2.TabIndex = 12;
            this.label2.Visible = false;
            // 
            // Target_dd
            // 
            this.Target_dd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Target_dd.DropDownHeight = 212;
            this.Target_dd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Target_dd.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Target_dd.ForeColor = System.Drawing.Color.White;
            this.Target_dd.FormattingEnabled = true;
            this.Target_dd.IntegralHeight = false;
            this.Target_dd.Items.AddRange(new object[] {
            "All applications"});
            this.Target_dd.Location = new System.Drawing.Point(7, 25);
            this.Target_dd.Name = "Target_dd";
            this.Target_dd.Size = new System.Drawing.Size(178, 24);
            this.Target_dd.TabIndex = 3;
            this.Target_dd.SelectedIndexChanged += new System.EventHandler(this.Target_dd_SelectedIndexChanged);
            // 
            // Profile_cmdSave
            // 
            this.Profile_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Profile_cmdSave.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Profile_cmdSave.Location = new System.Drawing.Point(99, 55);
            this.Profile_cmdSave.Name = "Profile_cmdSave";
            this.Profile_cmdSave.Size = new System.Drawing.Size(86, 23);
            this.Profile_cmdSave.TabIndex = 6;
            this.Profile_cmdSave.Text = "Save";
            this.Profile_cmdSave.UseVisualStyleBackColor = false;
            this.Profile_cmdSave.Click += new System.EventHandler(this.Profile_cmdSave_Click);
            // 
            // Profile_cmdLoad
            // 
            this.Profile_cmdLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Profile_cmdLoad.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Profile_cmdLoad.Location = new System.Drawing.Point(7, 55);
            this.Profile_cmdLoad.Name = "Profile_cmdLoad";
            this.Profile_cmdLoad.Size = new System.Drawing.Size(86, 23);
            this.Profile_cmdLoad.TabIndex = 5;
            this.Profile_cmdLoad.Text = "Load";
            this.Profile_cmdLoad.UseVisualStyleBackColor = false;
            this.Profile_cmdLoad.Click += new System.EventHandler(this.Profile_cmdLoad_Click);
            // 
            // Keys_cmdRem
            // 
            this.Keys_cmdRem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Keys_cmdRem.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Keys_cmdRem.Location = new System.Drawing.Point(101, 228);
            this.Keys_cmdRem.Name = "Keys_cmdRem";
            this.Keys_cmdRem.Size = new System.Drawing.Size(88, 25);
            this.Keys_cmdRem.TabIndex = 2;
            this.Keys_cmdRem.Text = "REM";
            this.Keys_cmdRem.UseVisualStyleBackColor = false;
            this.Keys_cmdRem.Click += new System.EventHandler(this.Keys_cmdRem_Click);
            // 
            // Keys_cmdAdd
            // 
            this.Keys_cmdAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Keys_cmdAdd.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Keys_cmdAdd.Location = new System.Drawing.Point(7, 228);
            this.Keys_cmdAdd.Name = "Keys_cmdAdd";
            this.Keys_cmdAdd.Size = new System.Drawing.Size(88, 25);
            this.Keys_cmdAdd.TabIndex = 1;
            this.Keys_cmdAdd.Text = "ADD";
            this.Keys_cmdAdd.UseVisualStyleBackColor = false;
            this.Keys_cmdAdd.Click += new System.EventHandler(this.Keys_cmdAdd_Click);
            // 
            // Keys_lst
            // 
            this.Keys_lst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Keys_lst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Keys_lst.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Keys_lst.ForeColor = System.Drawing.Color.White;
            this.Keys_lst.FormattingEnabled = true;
            this.Keys_lst.Location = new System.Drawing.Point(7, 25);
            this.Keys_lst.Name = "Keys_lst";
            this.Keys_lst.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.Keys_lst.Size = new System.Drawing.Size(182, 197);
            this.Keys_lst.TabIndex = 1;
            this.Keys_lst.DoubleClick += new System.EventHandler(this.Keys_lst_DoubleClick);
            // 
            // tPollFocus
            // 
            this.tPollFocus.Tick += new System.EventHandler(this.tPollFocus_Tick);
            // 
            // tPollProc
            // 
            this.tPollProc.Interval = 3000;
            this.tPollProc.Tick += new System.EventHandler(this.tPollProc_Tick);
            // 
            // cmdDummy
            // 
            this.cmdDummy.Location = new System.Drawing.Point(-100, 0);
            this.cmdDummy.Name = "cmdDummy";
            this.cmdDummy.Size = new System.Drawing.Size(50, 23);
            this.cmdDummy.TabIndex = 14;
            this.cmdDummy.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(430, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 266);
            this.label4.TabIndex = 17;
            this.label4.Visible = false;
            // 
            // Options_Rapid_txtUF
            // 
            this.Options_Rapid_txtUF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Options_Rapid_txtUF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Options_Rapid_txtUF.ForeColor = System.Drawing.Color.White;
            this.Options_Rapid_txtUF.Location = new System.Drawing.Point(148, 81);
            this.Options_Rapid_txtUF.Name = "Options_Rapid_txtUF";
            this.Options_Rapid_txtUF.Size = new System.Drawing.Size(41, 20);
            this.Options_Rapid_txtUF.TabIndex = 19;
            this.Options_Rapid_txtUF.Text = "7";
            this.Options_Rapid_txtUF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Options_Rapid_txtUF.TextChanged += new System.EventHandler(this.Options_Rapid_txtUF_TextChanged);
            // 
            // Options_Rapid_txtDF
            // 
            this.Options_Rapid_txtDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Options_Rapid_txtDF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Options_Rapid_txtDF.ForeColor = System.Drawing.Color.White;
            this.Options_Rapid_txtDF.Location = new System.Drawing.Point(101, 81);
            this.Options_Rapid_txtDF.Name = "Options_Rapid_txtDF";
            this.Options_Rapid_txtDF.Size = new System.Drawing.Size(41, 20);
            this.Options_Rapid_txtDF.TabIndex = 18;
            this.Options_Rapid_txtDF.Text = "7";
            this.Options_Rapid_txtDF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Options_Rapid_txtDF.TextChanged += new System.EventHandler(this.Options_Rapid_txtDF_TextChanged);
            // 
            // Options_Rapid_txtU
            // 
            this.Options_Rapid_txtU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Options_Rapid_txtU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Options_Rapid_txtU.ForeColor = System.Drawing.Color.White;
            this.Options_Rapid_txtU.Location = new System.Drawing.Point(54, 81);
            this.Options_Rapid_txtU.Name = "Options_Rapid_txtU";
            this.Options_Rapid_txtU.Size = new System.Drawing.Size(41, 20);
            this.Options_Rapid_txtU.TabIndex = 17;
            this.Options_Rapid_txtU.Text = "50";
            this.Options_Rapid_txtU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Options_Rapid_txtU.TextChanged += new System.EventHandler(this.Options_Rapid_txtU_TextChanged);
            // 
            // Options_Rapid_txtD
            // 
            this.Options_Rapid_txtD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Options_Rapid_txtD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Options_Rapid_txtD.ForeColor = System.Drawing.Color.White;
            this.Options_Rapid_txtD.Location = new System.Drawing.Point(7, 81);
            this.Options_Rapid_txtD.Name = "Options_Rapid_txtD";
            this.Options_Rapid_txtD.Size = new System.Drawing.Size(41, 20);
            this.Options_Rapid_txtD.TabIndex = 16;
            this.Options_Rapid_txtD.Text = "50";
            this.Options_Rapid_txtD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Options_Rapid_txtD.TextChanged += new System.EventHandler(this.Options_Rapid_txtD_TextChanged);
            // 
            // Options_chkRapid
            // 
            this.Options_chkRapid.AutoSize = true;
            this.Options_chkRapid.ForeColor = System.Drawing.Color.White;
            this.Options_chkRapid.Location = new System.Drawing.Point(7, 58);
            this.Options_chkRapid.Name = "Options_chkRapid";
            this.Options_chkRapid.Size = new System.Drawing.Size(144, 17);
            this.Options_chkRapid.TabIndex = 15;
            this.Options_chkRapid.Text = "Rapid fire mode (all keys)";
            this.Options_chkRapid.UseVisualStyleBackColor = true;
            this.Options_chkRapid.CheckedChanged += new System.EventHandler(this.Options_chkRapid_CheckedChanged);
            // 
            // Options_chkInject
            // 
            this.Options_chkInject.AutoSize = true;
            this.Options_chkInject.ForeColor = System.Drawing.Color.White;
            this.Options_chkInject.Location = new System.Drawing.Point(7, 25);
            this.Options_chkInject.Name = "Options_chkInject";
            this.Options_chkInject.Size = new System.Drawing.Size(153, 17);
            this.Options_chkInject.TabIndex = 0;
            this.Options_chkInject.Text = "Forcefully inject keystrokes";
            this.Options_chkInject.UseVisualStyleBackColor = true;
            this.Options_chkInject.CheckedChanged += new System.EventHandler(this.Options_chkInject_CheckedChanged);
            // 
            // tRapid
            // 
            this.tRapid.Interval = 1;
            this.tRapid.Tick += new System.EventHandler(this.tRapid_Tick);
            // 
            // Target_pn
            // 
            this.Target_pn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Target_pn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Target_pn.Controls.Add(this.Target_dd);
            this.Target_pn.Controls.Add(this.label6);
            this.Target_pn.ForeColor = System.Drawing.Color.White;
            this.Target_pn.Location = new System.Drawing.Point(15, 120);
            this.Target_pn.Margin = new System.Windows.Forms.Padding(6);
            this.Target_pn.Name = "Target_pn";
            this.Target_pn.Padding = new System.Windows.Forms.Padding(4);
            this.Target_pn.Size = new System.Drawing.Size(198, 58);
            this.Target_pn.TabIndex = 54;
            this.Target_pn.Tag = "c2";
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.Silver;
            this.label6.Location = new System.Drawing.Point(7, 4);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(178, 15);
            this.label6.TabIndex = 35;
            this.label6.Tag = "c4";
            this.label6.Text = "Target application";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Profile_dd
            // 
            this.Profile_dd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Profile_dd.DropDownHeight = 212;
            this.Profile_dd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Profile_dd.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Profile_dd.ForeColor = System.Drawing.Color.White;
            this.Profile_dd.FormattingEnabled = true;
            this.Profile_dd.IntegralHeight = false;
            this.Profile_dd.Location = new System.Drawing.Point(7, 25);
            this.Profile_dd.Name = "Profile_dd";
            this.Profile_dd.Size = new System.Drawing.Size(178, 24);
            this.Profile_dd.TabIndex = 3;
            this.Profile_dd.SelectedIndexChanged += new System.EventHandler(this.Profile_dd_SelectedIndexChanged);
            // 
            // Profile_pn
            // 
            this.Profile_pn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Profile_pn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Profile_pn.Controls.Add(this.Profile_cmdSave);
            this.Profile_pn.Controls.Add(this.label8);
            this.Profile_pn.Controls.Add(this.Profile_cmdLoad);
            this.Profile_pn.Controls.Add(this.Profile_dd);
            this.Profile_pn.ForeColor = System.Drawing.Color.White;
            this.Profile_pn.Location = new System.Drawing.Point(15, 190);
            this.Profile_pn.Margin = new System.Windows.Forms.Padding(6);
            this.Profile_pn.Name = "Profile_pn";
            this.Profile_pn.Padding = new System.Windows.Forms.Padding(4);
            this.Profile_pn.Size = new System.Drawing.Size(198, 87);
            this.Profile_pn.TabIndex = 56;
            this.Profile_pn.Tag = "c2";
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.Silver;
            this.label8.Location = new System.Drawing.Point(7, 4);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(178, 15);
            this.label8.TabIndex = 35;
            this.label8.Tag = "c4";
            this.label8.Text = "Predefined profile";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // nIco
            // 
            this.nIco.Icon = ((System.Drawing.Icon)(resources.GetObject("nIco.Icon")));
            this.nIco.Text = "GRebind";
            this.nIco.Visible = true;
            this.nIco.Click += new System.EventHandler(this.nIco_Click);
            // 
            // msg
            // 
            this.msg.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msg.Location = new System.Drawing.Point(12, 9);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(4, 4);
            this.msg.TabIndex = 57;
            this.msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.msg.Visible = false;
            // 
            // Keys_pn
            // 
            this.Keys_pn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Keys_pn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Keys_pn.Controls.Add(this.label1);
            this.Keys_pn.Controls.Add(this.Keys_cmdRem);
            this.Keys_pn.Controls.Add(this.Keys_lst);
            this.Keys_pn.Controls.Add(this.Keys_cmdAdd);
            this.Keys_pn.ForeColor = System.Drawing.Color.White;
            this.Keys_pn.Location = new System.Drawing.Point(221, 15);
            this.Keys_pn.Margin = new System.Windows.Forms.Padding(6);
            this.Keys_pn.Name = "Keys_pn";
            this.Keys_pn.Padding = new System.Windows.Forms.Padding(4);
            this.Keys_pn.Size = new System.Drawing.Size(198, 262);
            this.Keys_pn.TabIndex = 58;
            this.Keys_pn.Tag = "c2";
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(7, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 15);
            this.label1.TabIndex = 35;
            this.label1.Tag = "c4";
            this.label1.Text = "Mapped keys";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Options_pn
            // 
            this.Options_pn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Options_pn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Options_pn.Controls.Add(this.Options_CharCodeMap);
            this.Options_pn.Controls.Add(this.button1);
            this.Options_pn.Controls.Add(this.Options_Rapid_txtUF);
            this.Options_pn.Controls.Add(this.label3);
            this.Options_pn.Controls.Add(this.Options_Rapid_txtDF);
            this.Options_pn.Controls.Add(this.Options_chkInject);
            this.Options_pn.Controls.Add(this.Options_Rapid_txtU);
            this.Options_pn.Controls.Add(this.Options_Rapid_txtD);
            this.Options_pn.Controls.Add(this.Options_chkRapid);
            this.Options_pn.ForeColor = System.Drawing.Color.White;
            this.Options_pn.Location = new System.Drawing.Point(431, 15);
            this.Options_pn.Margin = new System.Windows.Forms.Padding(6);
            this.Options_pn.Name = "Options_pn";
            this.Options_pn.Padding = new System.Windows.Forms.Padding(4);
            this.Options_pn.Size = new System.Drawing.Size(198, 262);
            this.Options_pn.TabIndex = 59;
            this.Options_pn.Tag = "c2";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(7, 4);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 15);
            this.label3.TabIndex = 35;
            this.label3.Tag = "c4";
            this.label3.Text = "Options";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tHide
            // 
            this.tHide.Interval = 10;
            this.tHide.Tick += new System.EventHandler(this.tHide_Tick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(7, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 4);
            this.button1.TabIndex = 36;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // Options_CharCodeMap
            // 
            this.Options_CharCodeMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Options_CharCodeMap.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Options_CharCodeMap.Location = new System.Drawing.Point(7, 230);
            this.Options_CharCodeMap.Name = "Options_CharCodeMap";
            this.Options_CharCodeMap.Size = new System.Drawing.Size(182, 23);
            this.Options_CharCodeMap.TabIndex = 38;
            this.Options_CharCodeMap.Text = "Show charcode map";
            this.Options_CharCodeMap.UseVisualStyleBackColor = false;
            this.Options_CharCodeMap.Click += new System.EventHandler(this.Options_CharCodeMap_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(644, 292);
            this.Controls.Add(this.Options_pn);
            this.Controls.Add(this.Keys_pn);
            this.Controls.Add(this.msg);
            this.Controls.Add(this.Profile_pn);
            this.Controls.Add(this.Target_pn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.cmdDummy);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GRebind v";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.Target_pn.ResumeLayout(false);
            this.Profile_pn.ResumeLayout(false);
            this.Keys_pn.ResumeLayout(false);
            this.Options_pn.ResumeLayout(false);
            this.Options_pn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Target_dd;
        private System.Windows.Forms.Button Profile_cmdSave;
        private System.Windows.Forms.Button Profile_cmdLoad;
        private System.Windows.Forms.Button Keys_cmdRem;
        private System.Windows.Forms.Button Keys_cmdAdd;
        private System.Windows.Forms.ListBox Keys_lst;
        private System.Windows.Forms.Timer tPollFocus;
        private System.Windows.Forms.Timer tPollProc;
        private System.Windows.Forms.Button cmdDummy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Options_Rapid_txtUF;
        private System.Windows.Forms.TextBox Options_Rapid_txtDF;
        private System.Windows.Forms.TextBox Options_Rapid_txtU;
        private System.Windows.Forms.TextBox Options_Rapid_txtD;
        private System.Windows.Forms.CheckBox Options_chkRapid;
        private System.Windows.Forms.CheckBox Options_chkInject;
        private System.Windows.Forms.Timer tRapid;
        private System.Windows.Forms.Panel Target_pn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox Profile_dd;
        private System.Windows.Forms.Panel Profile_pn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NotifyIcon nIco;
        private System.Windows.Forms.Label msg;
        private System.Windows.Forms.Panel Keys_pn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Options_pn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer tHide;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Options_CharCodeMap;
    }
}

