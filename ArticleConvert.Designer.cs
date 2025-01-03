namespace CW
{
    partial class ArticleConvert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArticleConvert));
            label1 = new Label();
            label2 = new Label();
            inputFilePathTxb = new TextBox();
            inputFilePathBtn = new Button();
            outputFilePathTxb = new TextBox();
            outputFilePathBtn = new Button();
            label3 = new Label();
            separatorTxb = new TextBox();
            groupBox1 = new GroupBox();
            waveform = new ComboBox();
            label9 = new Label();
            toneTxb = new NumericUpDown();
            label8 = new Label();
            QRQ = new NumericUpDown();
            label7 = new Label();
            extraSpaceTxb = new NumericUpDown();
            label6 = new Label();
            effSpeedTxb = new NumericUpDown();
            label5 = new Label();
            speedTxb = new NumericUpDown();
            label4 = new Label();
            groupBox2 = new GroupBox();
            dateTxb = new TextBox();
            commentTxb = new TextBox();
            titleTxb = new TextBox();
            authorTxb = new TextBox();
            fileNameTxb = new TextBox();
            fileFormat = new ComboBox();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label16 = new Label();
            wordsLimitTxb = new NumericUpDown();
            label17 = new Label();
            timeLimitTxb = new NumericUpDown();
            resetSpeedCbx = new CheckBox();
            disableBtCbx = new CheckBox();
            label18 = new Label();
            parametersTxb = new TextBox();
            ConvertBtn = new Button();
            label20 = new Label();
            linkLabel1 = new LinkLabel();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)toneTxb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)QRQ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)extraSpaceTxb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)effSpeedTxb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)speedTxb).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)wordsLimitTxb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)timeLimitTxb).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 3);
            label1.Name = "label1";
            label1.Size = new Size(92, 17);
            label1.TabIndex = 0;
            label1.Text = "待转换文件路径";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 58);
            label2.Name = "label2";
            label2.Size = new Size(80, 17);
            label2.TabIndex = 1;
            label2.Text = "输出文件路径";
            // 
            // inputFilePathTxb
            // 
            inputFilePathTxb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            inputFilePathTxb.Location = new Point(8, 21);
            inputFilePathTxb.Name = "inputFilePathTxb";
            inputFilePathTxb.Size = new Size(497, 23);
            inputFilePathTxb.TabIndex = 2;
            // 
            // inputFilePathBtn
            // 
            inputFilePathBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            inputFilePathBtn.Location = new Point(511, 21);
            inputFilePathBtn.Name = "inputFilePathBtn";
            inputFilePathBtn.Size = new Size(89, 23);
            inputFilePathBtn.TabIndex = 3;
            inputFilePathBtn.Text = "选择";
            inputFilePathBtn.UseVisualStyleBackColor = true;
            inputFilePathBtn.Click += InputFilePathBtn_Click;
            // 
            // outputFilePathTxb
            // 
            outputFilePathTxb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            outputFilePathTxb.Location = new Point(8, 78);
            outputFilePathTxb.Name = "outputFilePathTxb";
            outputFilePathTxb.Size = new Size(497, 23);
            outputFilePathTxb.TabIndex = 4;
            // 
            // outputFilePathBtn
            // 
            outputFilePathBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            outputFilePathBtn.Location = new Point(511, 78);
            outputFilePathBtn.Name = "outputFilePathBtn";
            outputFilePathBtn.Size = new Size(89, 23);
            outputFilePathBtn.TabIndex = 5;
            outputFilePathBtn.Text = "选择";
            outputFilePathBtn.UseVisualStyleBackColor = true;
            outputFilePathBtn.Click += OutputFilePathBtn_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(4, 116);
            label3.Name = "label3";
            label3.Size = new Size(68, 17);
            label3.TabIndex = 6;
            label3.Text = "段落分隔符";
            // 
            // separatorTxb
            // 
            separatorTxb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            separatorTxb.Location = new Point(78, 113);
            separatorTxb.Name = "separatorTxb";
            separatorTxb.Size = new Size(522, 23);
            separatorTxb.TabIndex = 7;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(waveform);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(toneTxb);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(QRQ);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(extraSpaceTxb);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(effSpeedTxb);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(speedTxb);
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new Point(3, 149);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(236, 197);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "CW参数";
            // 
            // waveform
            // 
            waveform.DropDownStyle = ComboBoxStyle.DropDownList;
            waveform.Items.AddRange(new object[] { "正弦波", "锯齿波", "方波" });
            waveform.Location = new Point(152, 165);
            waveform.Name = "waveform";
            waveform.Size = new Size(72, 25);
            waveform.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(5, 168);
            label9.Name = "label9";
            label9.Size = new Size(44, 17);
            label9.TabIndex = 10;
            label9.Text = "波形：";
            // 
            // toneTxb
            // 
            toneTxb.Location = new Point(153, 139);
            toneTxb.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            toneTxb.Name = "toneTxb";
            toneTxb.Size = new Size(71, 23);
            toneTxb.TabIndex = 9;
            toneTxb.Value = new decimal(new int[] { 600, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(5, 139);
            label8.Name = "label8";
            label8.Size = new Size(67, 17);
            label8.TabIndex = 8;
            label8.Text = "频率(Hz)：";
            // 
            // QRQ
            // 
            QRQ.Location = new Point(153, 110);
            QRQ.Name = "QRQ";
            QRQ.Size = new Size(71, 23);
            QRQ.TabIndex = 7;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(5, 110);
            label7.Name = "label7";
            label7.Size = new Size(132, 17);
            label7.TabIndex = 6;
            label7.Text = "QRQ(分钟，0=关闭)：";
            // 
            // extraSpaceTxb
            // 
            extraSpaceTxb.Location = new Point(153, 80);
            extraSpaceTxb.Name = "extraSpaceTxb";
            extraSpaceTxb.Size = new Size(71, 23);
            extraSpaceTxb.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(5, 82);
            label6.Name = "label6";
            label6.Size = new Size(92, 17);
            label6.TabIndex = 4;
            label6.Text = "单字额外停顿：";
            // 
            // effSpeedTxb
            // 
            effSpeedTxb.Location = new Point(153, 52);
            effSpeedTxb.Name = "effSpeedTxb";
            effSpeedTxb.Size = new Size(71, 23);
            effSpeedTxb.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(5, 54);
            label5.Name = "label5";
            label5.Size = new Size(108, 17);
            label5.TabIndex = 2;
            label5.Text = "有效速度(WpM)：";
            // 
            // speedTxb
            // 
            speedTxb.Location = new Point(153, 23);
            speedTxb.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            speedTxb.Name = "speedTxb";
            speedTxb.Size = new Size(71, 23);
            speedTxb.TabIndex = 1;
            speedTxb.Value = new decimal(new int[] { 20, 0, 0, 0 });
            speedTxb.Leave += SpeedTxb_Leave;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(5, 25);
            label4.Name = "label4";
            label4.Size = new Size(84, 17);
            label4.TabIndex = 0;
            label4.Text = "速度(WpM)：";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox2.Controls.Add(dateTxb);
            groupBox2.Controls.Add(commentTxb);
            groupBox2.Controls.Add(titleTxb);
            groupBox2.Controls.Add(authorTxb);
            groupBox2.Controls.Add(fileNameTxb);
            groupBox2.Controls.Add(fileFormat);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(label15);
            groupBox2.Location = new Point(292, 149);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(308, 197);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "输出文件信息";
            // 
            // dateTxb
            // 
            dateTxb.Location = new Point(67, 136);
            dateTxb.Name = "dateTxb";
            dateTxb.Size = new Size(235, 23);
            dateTxb.TabIndex = 17;
            // 
            // commentTxb
            // 
            commentTxb.Location = new Point(67, 106);
            commentTxb.Name = "commentTxb";
            commentTxb.Size = new Size(235, 23);
            commentTxb.TabIndex = 16;
            // 
            // titleTxb
            // 
            titleTxb.Location = new Point(67, 79);
            titleTxb.Name = "titleTxb";
            titleTxb.Size = new Size(235, 23);
            titleTxb.TabIndex = 15;
            titleTxb.Text = "cw";
            // 
            // authorTxb
            // 
            authorTxb.Location = new Point(67, 50);
            authorTxb.Name = "authorTxb";
            authorTxb.Size = new Size(235, 23);
            authorTxb.TabIndex = 14;
            // 
            // fileNameTxb
            // 
            fileNameTxb.Location = new Point(67, 21);
            fileNameTxb.Name = "fileNameTxb";
            fileNameTxb.Size = new Size(235, 23);
            fileNameTxb.TabIndex = 13;
            fileNameTxb.Text = "输出音频";
            // 
            // fileFormat
            // 
            fileFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            fileFormat.Items.AddRange(new object[] { "MP3", "OGG" });
            fileFormat.Location = new Point(67, 168);
            fileFormat.Name = "fileFormat";
            fileFormat.Size = new Size(235, 25);
            fileFormat.TabIndex = 12;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(5, 168);
            label10.Name = "label10";
            label10.Size = new Size(68, 17);
            label10.TabIndex = 10;
            label10.Text = "文件格式：";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(5, 139);
            label11.Name = "label11";
            label11.Size = new Size(32, 17);
            label11.TabIndex = 8;
            label11.Text = "年：";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(5, 110);
            label12.Name = "label12";
            label12.Size = new Size(44, 17);
            label12.TabIndex = 6;
            label12.Text = "备注：";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(5, 82);
            label13.Name = "label13";
            label13.Size = new Size(44, 17);
            label13.TabIndex = 4;
            label13.Text = "标题：";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(5, 54);
            label14.Name = "label14";
            label14.Size = new Size(44, 17);
            label14.TabIndex = 2;
            label14.Text = "作者：";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(5, 25);
            label15.Name = "label15";
            label15.Size = new Size(56, 17);
            label15.TabIndex = 0;
            label15.Text = "文件名：";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(3, 356);
            label16.Name = "label16";
            label16.Size = new Size(152, 17);
            label16.TabIndex = 13;
            label16.Text = "每段字符数限制(0=关闭)：";
            // 
            // wordsLimitTxb
            // 
            wordsLimitTxb.Location = new Point(156, 354);
            wordsLimitTxb.Name = "wordsLimitTxb";
            wordsLimitTxb.Size = new Size(82, 23);
            wordsLimitTxb.TabIndex = 14;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(4, 389);
            label17.Name = "label17";
            label17.Size = new Size(164, 17);
            label17.TabIndex = 15;
            label17.Text = "每段时长限制(秒，0=关闭)：";
            // 
            // timeLimitTxb
            // 
            timeLimitTxb.Location = new Point(158, 387);
            timeLimitTxb.Name = "timeLimitTxb";
            timeLimitTxb.Size = new Size(82, 23);
            timeLimitTxb.TabIndex = 16;
            // 
            // resetSpeedCbx
            // 
            resetSpeedCbx.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            resetSpeedCbx.AutoSize = true;
            resetSpeedCbx.Checked = true;
            resetSpeedCbx.CheckState = CheckState.Checked;
            resetSpeedCbx.Location = new Point(291, 354);
            resetSpeedCbx.Name = "resetSpeedCbx";
            resetSpeedCbx.Size = new Size(99, 21);
            resetSpeedCbx.TabIndex = 17;
            resetSpeedCbx.Text = "重置每段速度";
            resetSpeedCbx.UseVisualStyleBackColor = true;
            // 
            // disableBtCbx
            // 
            disableBtCbx.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            disableBtCbx.AutoSize = true;
            disableBtCbx.Location = new Point(291, 381);
            disableBtCbx.Name = "disableBtCbx";
            disableBtCbx.Size = new Size(120, 21);
            disableBtCbx.TabIndex = 18;
            disableBtCbx.Text = "禁用段落的<BT>";
            disableBtCbx.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(4, 418);
            label18.Name = "label18";
            label18.Size = new Size(68, 17);
            label18.TabIndex = 19;
            label18.Text = "附加参数：";
            // 
            // parametersTxb
            // 
            parametersTxb.Location = new Point(83, 414);
            parametersTxb.Name = "parametersTxb";
            parametersTxb.Size = new Size(511, 23);
            parametersTxb.TabIndex = 20;
            // 
            // ConvertBtn
            // 
            ConvertBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ConvertBtn.Location = new Point(226, 468);
            ConvertBtn.Name = "ConvertBtn";
            ConvertBtn.Size = new Size(105, 25);
            ConvertBtn.TabIndex = 21;
            ConvertBtn.Text = "开始转换";
            ConvertBtn.UseVisualStyleBackColor = true;
            ConvertBtn.Click += ConvertBtn_Click;
            // 
            // label20
            // 
            label20.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label20.AutoSize = true;
            label20.Location = new Point(406, 502);
            label20.Name = "label20";
            label20.Size = new Size(188, 17);
            label20.TabIndex = 23;
            label20.Text = "by Fabian Kurz,DJ1YFK, BI8EGZ";
            // 
            // linkLabel1
            // 
            linkLabel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(406, 485);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(164, 17);
            linkLabel1.TabIndex = 24;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "汉化自ebook2cw-gui v0.1.2";
            linkLabel1.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel1.LinkClicked += LinkLabel1_LinkClicked;
            // 
            // Convert
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(616, 528);
            Controls.Add(linkLabel1);
            Controls.Add(label20);
            Controls.Add(ConvertBtn);
            Controls.Add(parametersTxb);
            Controls.Add(label18);
            Controls.Add(disableBtCbx);
            Controls.Add(resetSpeedCbx);
            Controls.Add(timeLimitTxb);
            Controls.Add(label17);
            Controls.Add(wordsLimitTxb);
            Controls.Add(label16);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(separatorTxb);
            Controls.Add(label3);
            Controls.Add(outputFilePathBtn);
            Controls.Add(outputFilePathTxb);
            Controls.Add(inputFilePathBtn);
            Controls.Add(inputFilePathTxb);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Convert";
            Text = "文本转换CW";
            Load += Convert_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)toneTxb).EndInit();
            ((System.ComponentModel.ISupportInitialize)QRQ).EndInit();
            ((System.ComponentModel.ISupportInitialize)extraSpaceTxb).EndInit();
            ((System.ComponentModel.ISupportInitialize)effSpeedTxb).EndInit();
            ((System.ComponentModel.ISupportInitialize)speedTxb).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)wordsLimitTxb).EndInit();
            ((System.ComponentModel.ISupportInitialize)timeLimitTxb).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox inputFilePathTxb;
        private Button inputFilePathBtn;
        private TextBox outputFilePathTxb;
        private Button outputFilePathBtn;
        private Label label3;
        private TextBox separatorTxb;
        private GroupBox groupBox1;
        private Label label5;
        private NumericUpDown speedTxb;
        private Label label4;
        private Label label7;
        private NumericUpDown extraSpaceTxb;
        private Label label6;
        private NumericUpDown effSpeedTxb;
        private ComboBox waveform;
        private Label label9;
        private NumericUpDown toneTxb;
        private Label label8;
        private NumericUpDown QRQ;
        private GroupBox groupBox2;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private TextBox dateTxb;
        private TextBox commentTxb;
        private TextBox titleTxb;
        private TextBox authorTxb;
        private TextBox fileNameTxb;
        private ComboBox fileFormat;
        private Label label16;
        private NumericUpDown wordsLimitTxb;
        private Label label17;
        private NumericUpDown timeLimitTxb;
        private CheckBox resetSpeedCbx;
        private CheckBox disableBtCbx;
        private Label label18;
        private TextBox parametersTxb;
        private Button ConvertBtn;
        private Label label20;
        private LinkLabel linkLabel1;
    }
}