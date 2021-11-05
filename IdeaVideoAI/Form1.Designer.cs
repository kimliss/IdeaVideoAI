﻿namespace IdeaVideoAI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openVideoFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbRepeatLog = new System.Windows.Forms.TextBox();
            this.nUDSetptsV2 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSetpts = new System.Windows.Forms.CheckBox();
            this.nUDSetptsV1 = new System.Windows.Forms.NumericUpDown();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.button9 = new System.Windows.Forms.Button();
            this.cbOverlay = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.button7 = new System.Windows.Forms.Button();
            this.cbBackground = new System.Windows.Forms.CheckBox();
            this.nUDBrightnessV2 = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.cbBrightness = new System.Windows.Forms.CheckBox();
            this.nUDBrightnessV1 = new System.Windows.Forms.NumericUpDown();
            this.nUDSaturationV2 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.cbSaturation = new System.Windows.Forms.CheckBox();
            this.nUDSaturationV1 = new System.Windows.Forms.NumericUpDown();
            this.nUDContrastV2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cbContrast = new System.Windows.Forms.CheckBox();
            this.nUDContrastV1 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.btnRepeat = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSetptsV2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSetptsV1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDBrightnessV2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDBrightnessV1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSaturationV2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSaturationV1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDContrastV2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDContrastV1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1192, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openVideoFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.fileToolStripMenuItem.Text = "文件";
            // 
            // openVideoFileToolStripMenuItem
            // 
            this.openVideoFileToolStripMenuItem.Name = "openVideoFileToolStripMenuItem";
            this.openVideoFileToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.openVideoFileToolStripMenuItem.Text = "打开视频";
            this.openVideoFileToolStripMenuItem.Click += new System.EventHandler(this.openVideoFileToolStripMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(242, 769);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "文件名";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "状态";
            this.columnHeader2.Width = 80;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 777);
            this.panel1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(242, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "单个去水印";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(242, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "批量去水印";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 76);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(242, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(3, 105);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(182, 19);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "右击图片，打开下一个视频";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "进度";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(3, 159);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(242, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "撤销水印标注";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(3, 130);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(247, 19);
            this.checkBox2.TabIndex = 10;
            this.checkBox2.Text = "自动标注水印（采用上一个视频标注）";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.checkBox2);
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.checkBox1);
            this.panel3.Controls.Add(this.progressBar1);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Location = new System.Drawing.Point(648, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(250, 737);
            this.panel3.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(268, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(912, 777);
            this.tabControl1.TabIndex = 6;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(904, 749);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "去水印";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(6, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(636, 734);
            this.panel2.TabIndex = 4;
            this.panel2.Tag = "";
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(3, 706);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(628, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "下一个视频";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(3, 679);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(628, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "上一个视频";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(3, 650);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(628, 23);
            this.textBox1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(628, 641);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbRepeatLog);
            this.tabPage2.Controls.Add(this.nUDSetptsV2);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.cbSetpts);
            this.tabPage2.Controls.Add(this.nUDSetptsV1);
            this.tabPage2.Controls.Add(this.checkBox12);
            this.tabPage2.Controls.Add(this.button9);
            this.tabPage2.Controls.Add(this.cbOverlay);
            this.tabPage2.Controls.Add(this.label23);
            this.tabPage2.Controls.Add(this.label22);
            this.tabPage2.Controls.Add(this.button8);
            this.tabPage2.Controls.Add(this.checkBox10);
            this.tabPage2.Controls.Add(this.button7);
            this.tabPage2.Controls.Add(this.cbBackground);
            this.tabPage2.Controls.Add(this.nUDBrightnessV2);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.cbBrightness);
            this.tabPage2.Controls.Add(this.nUDBrightnessV1);
            this.tabPage2.Controls.Add(this.nUDSaturationV2);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.cbSaturation);
            this.tabPage2.Controls.Add(this.nUDSaturationV1);
            this.tabPage2.Controls.Add(this.nUDContrastV2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cbContrast);
            this.tabPage2.Controls.Add(this.nUDContrastV1);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.numericUpDown5);
            this.tabPage2.Controls.Add(this.numericUpDown6);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.btnRepeat);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(904, 749);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "去重";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbRepeatLog
            // 
            this.tbRepeatLog.Location = new System.Drawing.Point(587, 6);
            this.tbRepeatLog.Multiline = true;
            this.tbRepeatLog.Name = "tbRepeatLog";
            this.tbRepeatLog.Size = new System.Drawing.Size(311, 740);
            this.tbRepeatLog.TabIndex = 85;
            // 
            // nUDSetptsV2
            // 
            this.nUDSetptsV2.DecimalPlaces = 2;
            this.nUDSetptsV2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nUDSetptsV2.Location = new System.Drawing.Point(215, 52);
            this.nUDSetptsV2.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nUDSetptsV2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUDSetptsV2.Name = "nUDSetptsV2";
            this.nUDSetptsV2.Size = new System.Drawing.Size(73, 23);
            this.nUDSetptsV2.TabIndex = 83;
            this.nUDSetptsV2.Value = new decimal(new int[] {
            110,
            0,
            0,
            131072});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 15);
            this.label4.TabIndex = 82;
            this.label4.Text = "~";
            // 
            // cbSetpts
            // 
            this.cbSetpts.AutoSize = true;
            this.cbSetpts.Location = new System.Drawing.Point(15, 54);
            this.cbSetpts.Name = "cbSetpts";
            this.cbSetpts.Size = new System.Drawing.Size(66, 19);
            this.cbSetpts.TabIndex = 80;
            this.cbSetpts.Text = "倍速(1)";
            this.cbSetpts.UseVisualStyleBackColor = true;
            // 
            // nUDSetptsV1
            // 
            this.nUDSetptsV1.DecimalPlaces = 2;
            this.nUDSetptsV1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nUDSetptsV1.Location = new System.Drawing.Point(113, 52);
            this.nUDSetptsV1.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUDSetptsV1.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nUDSetptsV1.Name = "nUDSetptsV1";
            this.nUDSetptsV1.Size = new System.Drawing.Size(64, 23);
            this.nUDSetptsV1.TabIndex = 81;
            this.nUDSetptsV1.Value = new decimal(new int[] {
            90,
            0,
            0,
            131072});
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Location = new System.Drawing.Point(210, 282);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(78, 19);
            this.checkBox12.TabIndex = 79;
            this.checkBox12.Text = "随机开始";
            this.checkBox12.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(111, 280);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 78;
            this.button9.Text = "视频目录";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // cbOverlay
            // 
            this.cbOverlay.AutoSize = true;
            this.cbOverlay.Location = new System.Drawing.Point(15, 282);
            this.cbOverlay.Name = "cbOverlay";
            this.cbOverlay.Size = new System.Drawing.Size(78, 19);
            this.cbOverlay.TabIndex = 77;
            this.cbOverlay.Text = "叠加视频";
            this.cbOverlay.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(15, 337);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(59, 15);
            this.label23.TabIndex = 76;
            this.label23.Text = "局部随机";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(15, 15);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 15);
            this.label22.TabIndex = 75;
            this.label22.Text = "全局随机";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(111, 391);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 68;
            this.button8.Text = "片段目录";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Location = new System.Drawing.Point(15, 393);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(52, 19);
            this.checkBox10.TabIndex = 67;
            this.checkBox10.Text = "插播";
            this.checkBox10.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(111, 232);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 66;
            this.button7.Text = "音乐目录";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // cbBackground
            // 
            this.cbBackground.AutoSize = true;
            this.cbBackground.Location = new System.Drawing.Point(15, 234);
            this.cbBackground.Name = "cbBackground";
            this.cbBackground.Size = new System.Drawing.Size(78, 19);
            this.cbBackground.TabIndex = 61;
            this.cbBackground.Text = "背景音乐";
            this.cbBackground.UseVisualStyleBackColor = true;
            // 
            // nUDBrightnessV2
            // 
            this.nUDBrightnessV2.DecimalPlaces = 2;
            this.nUDBrightnessV2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nUDBrightnessV2.Location = new System.Drawing.Point(215, 182);
            this.nUDBrightnessV2.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUDBrightnessV2.Name = "nUDBrightnessV2";
            this.nUDBrightnessV2.Size = new System.Drawing.Size(73, 23);
            this.nUDBrightnessV2.TabIndex = 55;
            this.nUDBrightnessV2.Value = new decimal(new int[] {
            10,
            0,
            0,
            131072});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(183, 186);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 15);
            this.label12.TabIndex = 54;
            this.label12.Text = "~";
            // 
            // cbBrightness
            // 
            this.cbBrightness.AutoSize = true;
            this.cbBrightness.Location = new System.Drawing.Point(15, 184);
            this.cbBrightness.Name = "cbBrightness";
            this.cbBrightness.Size = new System.Drawing.Size(66, 19);
            this.cbBrightness.TabIndex = 51;
            this.cbBrightness.Text = "亮度(0)";
            this.cbBrightness.UseVisualStyleBackColor = true;
            // 
            // nUDBrightnessV1
            // 
            this.nUDBrightnessV1.DecimalPlaces = 2;
            this.nUDBrightnessV1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nUDBrightnessV1.Location = new System.Drawing.Point(113, 182);
            this.nUDBrightnessV1.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nUDBrightnessV1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nUDBrightnessV1.Name = "nUDBrightnessV1";
            this.nUDBrightnessV1.Size = new System.Drawing.Size(64, 23);
            this.nUDBrightnessV1.TabIndex = 53;
            // 
            // nUDSaturationV2
            // 
            this.nUDSaturationV2.DecimalPlaces = 2;
            this.nUDSaturationV2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nUDSaturationV2.Location = new System.Drawing.Point(215, 134);
            this.nUDSaturationV2.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nUDSaturationV2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUDSaturationV2.Name = "nUDSaturationV2";
            this.nUDSaturationV2.Size = new System.Drawing.Size(73, 23);
            this.nUDSaturationV2.TabIndex = 50;
            this.nUDSaturationV2.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(183, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 15);
            this.label5.TabIndex = 49;
            this.label5.Text = "~";
            // 
            // cbSaturation
            // 
            this.cbSaturation.AutoSize = true;
            this.cbSaturation.Location = new System.Drawing.Point(15, 136);
            this.cbSaturation.Name = "cbSaturation";
            this.cbSaturation.Size = new System.Drawing.Size(79, 19);
            this.cbSaturation.TabIndex = 46;
            this.cbSaturation.Text = "饱和度(1)";
            this.cbSaturation.UseVisualStyleBackColor = true;
            // 
            // nUDSaturationV1
            // 
            this.nUDSaturationV1.DecimalPlaces = 2;
            this.nUDSaturationV1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nUDSaturationV1.Location = new System.Drawing.Point(113, 134);
            this.nUDSaturationV1.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUDSaturationV1.Name = "nUDSaturationV1";
            this.nUDSaturationV1.Size = new System.Drawing.Size(64, 23);
            this.nUDSaturationV1.TabIndex = 48;
            this.nUDSaturationV1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nUDContrastV2
            // 
            this.nUDContrastV2.DecimalPlaces = 2;
            this.nUDContrastV2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nUDContrastV2.Location = new System.Drawing.Point(215, 91);
            this.nUDContrastV2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nUDContrastV2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUDContrastV2.Name = "nUDContrastV2";
            this.nUDContrastV2.Size = new System.Drawing.Size(73, 23);
            this.nUDContrastV2.TabIndex = 45;
            this.nUDContrastV2.Value = new decimal(new int[] {
            115,
            0,
            0,
            131072});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 15);
            this.label3.TabIndex = 44;
            this.label3.Text = "~";
            // 
            // cbContrast
            // 
            this.cbContrast.AutoSize = true;
            this.cbContrast.Location = new System.Drawing.Point(15, 93);
            this.cbContrast.Name = "cbContrast";
            this.cbContrast.Size = new System.Drawing.Size(79, 19);
            this.cbContrast.TabIndex = 41;
            this.cbContrast.Text = "对比度(1)";
            this.cbContrast.UseVisualStyleBackColor = true;
            // 
            // nUDContrastV1
            // 
            this.nUDContrastV1.DecimalPlaces = 2;
            this.nUDContrastV1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nUDContrastV1.Location = new System.Drawing.Point(113, 91);
            this.nUDContrastV1.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUDContrastV1.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nUDContrastV1.Name = "nUDContrastV1";
            this.nUDContrastV1.Size = new System.Drawing.Size(64, 23);
            this.nUDContrastV1.TabIndex = 43;
            this.nUDContrastV1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(346, 395);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 15);
            this.label9.TabIndex = 17;
            this.label9.Text = "插入时间";
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(417, 391);
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(53, 23);
            this.numericUpDown5.TabIndex = 18;
            this.numericUpDown5.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(281, 391);
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(53, 23);
            this.numericUpDown6.TabIndex = 16;
            this.numericUpDown6.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(210, 395);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 15);
            this.label10.TabIndex = 15;
            this.label10.Text = "插入时长";
            // 
            // btnRepeat
            // 
            this.btnRepeat.Location = new System.Drawing.Point(6, 707);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(575, 36);
            this.btnRepeat.TabIndex = 9;
            this.btnRepeat.Text = "去重";
            this.btnRepeat.UseVisualStyleBackColor = true;
            this.btnRepeat.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 816);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "IdeaVideoAI";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSetptsV2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSetptsV1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDBrightnessV2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDBrightnessV1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSaturationV2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSaturationV1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDContrastV2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDContrastV1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openVideoFileToolStripMenuItem;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Panel panel1;
        private Button button1;
        private Button button2;
        private ProgressBar progressBar1;
        private CheckBox checkBox1;
        private Label label6;
        private Button button5;
        private CheckBox checkBox2;
        private Panel panel3;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Panel panel2;
        private Button button4;
        private Button button3;
        private TextBox textBox1;
        private PictureBox pictureBox1;
        private TabPage tabPage2;
        private Button btnRepeat;
        private Label label9;
        private NumericUpDown numericUpDown5;
        private NumericUpDown numericUpDown6;
        private Label label10;
        private CheckBox cbBackground;
        private NumericUpDown nUDBrightnessV2;
        private Label label12;
        private CheckBox cbBrightness;
        private NumericUpDown nUDBrightnessV1;
        private NumericUpDown nUDSaturationV2;
        private Label label5;
        private CheckBox cbSaturation;
        private NumericUpDown nUDSaturationV1;
        private NumericUpDown nUDContrastV2;
        private Label label3;
        private CheckBox cbContrast;
        private NumericUpDown nUDContrastV1;
        private Button button7;
        private Button button8;
        private CheckBox checkBox10;
        private Label label23;
        private Label label22;
        private Button button9;
        private CheckBox cbOverlay;
        private CheckBox checkBox12;
        private NumericUpDown nUDSetptsV2;
        private Label label4;
        private CheckBox cbSetpts;
        private NumericUpDown nUDSetptsV1;
        private TextBox tbRepeatLog;
    }
}