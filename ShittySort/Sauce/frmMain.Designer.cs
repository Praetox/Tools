namespace ShittySort {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.gugRoot = new System.Windows.Forms.GroupBox();
            this.guResume = new System.Windows.Forms.Button();
            this.guRootOK = new System.Windows.Forms.Button();
            this.guRootB = new System.Windows.Forms.Button();
            this.guRoot = new System.Windows.Forms.TextBox();
            this.gugOrganize = new System.Windows.Forms.GroupBox();
            this.gugMask = new System.Windows.Forms.GroupBox();
            this.guMaskOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.guReplace2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.guReplace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.guSplit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.guMask = new System.Windows.Forms.TextBox();
            this.ttip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.prob = new System.Windows.Forms.Label();
            this.proa = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.gugRoot.SuspendLayout();
            this.gugOrganize.SuspendLayout();
            this.gugMask.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gugRoot
            // 
            this.gugRoot.Controls.Add(this.guResume);
            this.gugRoot.Controls.Add(this.guRootOK);
            this.gugRoot.Controls.Add(this.guRootB);
            this.gugRoot.Controls.Add(this.guRoot);
            this.gugRoot.Location = new System.Drawing.Point(12, 12);
            this.gugRoot.Name = "gugRoot";
            this.gugRoot.Size = new System.Drawing.Size(268, 76);
            this.gugRoot.TabIndex = 0;
            this.gugRoot.TabStop = false;
            this.gugRoot.Text = "1. Select root directory";
            // 
            // guResume
            // 
            this.guResume.Location = new System.Drawing.Point(6, 47);
            this.guResume.Name = "guResume";
            this.guResume.Size = new System.Drawing.Size(125, 23);
            this.guResume.TabIndex = 3;
            this.guResume.Text = "Resume sorting or...";
            this.ttip.SetToolTip(this.guResume, "Continue");
            this.guResume.UseVisualStyleBackColor = true;
            this.guResume.Click += new System.EventHandler(this.guResume_Click);
            // 
            // guRootOK
            // 
            this.guRootOK.Location = new System.Drawing.Point(137, 47);
            this.guRootOK.Name = "guRootOK";
            this.guRootOK.Size = new System.Drawing.Size(125, 23);
            this.guRootOK.TabIndex = 2;
            this.guRootOK.Text = "Select this folder";
            this.ttip.SetToolTip(this.guRootOK, "Continue");
            this.guRootOK.UseVisualStyleBackColor = true;
            this.guRootOK.Click += new System.EventHandler(this.guRootOK_Click);
            // 
            // guRootB
            // 
            this.guRootB.Location = new System.Drawing.Point(234, 19);
            this.guRootB.Name = "guRootB";
            this.guRootB.Size = new System.Drawing.Size(28, 23);
            this.guRootB.TabIndex = 1;
            this.guRootB.Text = "...";
            this.ttip.SetToolTip(this.guRootB, "Select a folder using a dialog");
            this.guRootB.UseVisualStyleBackColor = true;
            this.guRootB.Click += new System.EventHandler(this.guRootB_Click);
            // 
            // guRoot
            // 
            this.guRoot.Location = new System.Drawing.Point(6, 21);
            this.guRoot.Name = "guRoot";
            this.guRoot.Size = new System.Drawing.Size(222, 20);
            this.guRoot.TabIndex = 0;
            this.ttip.SetToolTip(this.guRoot, "The folder to start scanning from");
            // 
            // gugOrganize
            // 
            this.gugOrganize.Controls.Add(this.button1);
            this.gugOrganize.Controls.Add(this.button2);
            this.gugOrganize.Enabled = false;
            this.gugOrganize.Location = new System.Drawing.Point(12, 252);
            this.gugOrganize.Name = "gugOrganize";
            this.gugOrganize.Size = new System.Drawing.Size(268, 59);
            this.gugOrganize.TabIndex = 1;
            this.gugOrganize.TabStop = false;
            this.gugOrganize.Text = "3. Organize the images";
            // 
            // gugMask
            // 
            this.gugMask.Controls.Add(this.guMaskOK);
            this.gugMask.Controls.Add(this.label4);
            this.gugMask.Controls.Add(this.guReplace2);
            this.gugMask.Controls.Add(this.label3);
            this.gugMask.Controls.Add(this.guReplace);
            this.gugMask.Controls.Add(this.label2);
            this.gugMask.Controls.Add(this.guSplit);
            this.gugMask.Controls.Add(this.label1);
            this.gugMask.Controls.Add(this.guMask);
            this.gugMask.Enabled = false;
            this.gugMask.Location = new System.Drawing.Point(12, 94);
            this.gugMask.Name = "gugMask";
            this.gugMask.Size = new System.Drawing.Size(268, 152);
            this.gugMask.TabIndex = 2;
            this.gugMask.TabStop = false;
            this.gugMask.Text = "2. Tag formatting";
            // 
            // guMaskOK
            // 
            this.guMaskOK.Location = new System.Drawing.Point(6, 123);
            this.guMaskOK.Name = "guMaskOK";
            this.guMaskOK.Size = new System.Drawing.Size(256, 23);
            this.guMaskOK.TabIndex = 10;
            this.guMaskOK.Text = "Start collecting tags";
            this.ttip.SetToolTip(this.guMaskOK, "Continue");
            this.guMaskOK.UseVisualStyleBackColor = true;
            this.guMaskOK.Click += new System.EventHandler(this.guMaskOK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "...with these";
            // 
            // guReplace2
            // 
            this.guReplace2.Location = new System.Drawing.Point(75, 97);
            this.guReplace2.Name = "guReplace2";
            this.guReplace2.Size = new System.Drawing.Size(187, 20);
            this.guReplace2.TabIndex = 8;
            this.guReplace2.Text = " ";
            this.ttip.SetToolTip(this.guReplace2, "What characters to insert instead");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Replace...";
            // 
            // guReplace
            // 
            this.guReplace.Location = new System.Drawing.Point(76, 71);
            this.guReplace.Name = "guReplace";
            this.guReplace.Size = new System.Drawing.Size(186, 20);
            this.guReplace.TabIndex = 6;
            this.guReplace.Text = "_";
            this.ttip.SetToolTip(this.guReplace, "Characters that should be replaced in each tag");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Split tags by";
            // 
            // guSplit
            // 
            this.guSplit.Location = new System.Drawing.Point(76, 45);
            this.guSplit.Name = "guSplit";
            this.guSplit.Size = new System.Drawing.Size(186, 20);
            this.guSplit.TabIndex = 4;
            this.guSplit.Text = " ";
            this.ttip.SetToolTip(this.guSplit, "What each tag is delimited by");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name mask";
            // 
            // guMask
            // 
            this.guMask.Location = new System.Drawing.Point(75, 19);
            this.guMask.Name = "guMask";
            this.guMask.Size = new System.Drawing.Size(187, 20);
            this.guMask.TabIndex = 0;
            this.guMask.Text = "{tagsSrc}.com - {junk} - {tags}";
            this.ttip.SetToolTip(this.guMask, "How the filenames are formatted");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.prob);
            this.groupBox1.Controls.Add(this.proa);
            this.groupBox1.Location = new System.Drawing.Point(12, 317);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 61);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            // 
            // prob
            // 
            this.prob.AutoSize = true;
            this.prob.Location = new System.Drawing.Point(6, 45);
            this.prob.Name = "prob";
            this.prob.Size = new System.Drawing.Size(170, 13);
            this.prob.TabIndex = 4;
            this.prob.Text = "No action has been performed yet.";
            // 
            // proa
            // 
            this.proa.Location = new System.Drawing.Point(6, 19);
            this.proa.Name = "proa";
            this.proa.Size = new System.Drawing.Size(256, 23);
            this.proa.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 34);
            this.button1.TabIndex = 5;
            this.button1.Text = "Embed tags in images";
            this.ttip.SetToolTip(this.button1, "Continue");
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(137, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 34);
            this.button2.TabIndex = 4;
            this.button2.Text = "Sort to nested folders";
            this.ttip.SetToolTip(this.button2, "Continue");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 386);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gugMask);
            this.Controls.Add(this.gugOrganize);
            this.Controls.Add(this.gugRoot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Organize images by filename";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gugRoot.ResumeLayout(false);
            this.gugRoot.PerformLayout();
            this.gugOrganize.ResumeLayout(false);
            this.gugMask.ResumeLayout(false);
            this.gugMask.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gugRoot;
        private System.Windows.Forms.TextBox guRoot;
        private System.Windows.Forms.Button guRootOK;
        private System.Windows.Forms.Button guRootB;
        private System.Windows.Forms.GroupBox gugOrganize;
        private System.Windows.Forms.GroupBox gugMask;
        private System.Windows.Forms.TextBox guMask;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button guMaskOK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox guReplace2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox guReplace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox guSplit;
        private System.Windows.Forms.ToolTip ttip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label prob;
        private System.Windows.Forms.ProgressBar proa;
        private System.Windows.Forms.Button guResume;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

