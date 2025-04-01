namespace CW
{
    partial class SendPractice
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
            components = new System.ComponentModel.Container();
            groupBox1 = new GroupBox();
            individuationRbtn = new RadioButton();
            radioButton8 = new RadioButton();
            radioButton6 = new RadioButton();
            radioButton5 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            groupBox2 = new GroupBox();
            eqBox = new CheckedListBox();
            neBox = new CheckedListBox();
            eqRbtn = new RadioButton();
            neRbtn = new RadioButton();
            groupBox3 = new GroupBox();
            label7 = new Label();
            bgmCbx = new CheckBox();
            speedBox = new NumericUpDown();
            toneBox = new NumericUpDown();
            label2 = new Label();
            label1 = new Label();
            volumeTrackBar = new TrackBar();
            symbolsChb = new CheckBox();
            EachGroup = new NumericUpDown();
            label4 = new Label();
            exportBtn = new Button();
            repeatRbtn = new CheckBox();
            continuousRbtn = new CheckBox();
            submitAnswerBtn = new Button();
            startBtn = new Button();
            groupNumBox = new NumericUpDown();
            label3 = new Label();
            stopBtn = new Button();
            groupBox4 = new GroupBox();
            replicationBox6 = new RichTextBox();
            answerLbl6 = new Label();
            replicationBox5 = new RichTextBox();
            answerLbl5 = new Label();
            replicationBox4 = new RichTextBox();
            answerLbl4 = new Label();
            replicationBox3 = new RichTextBox();
            answerLbl3 = new Label();
            replicationBox2 = new RichTextBox();
            answerLbl2 = new Label();
            replicationBox1 = new RichTextBox();
            answerLbl1 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            groupBox5 = new GroupBox();
            rePlayBtn = new Button();
            continuePlayBtn = new Button();
            pauseBtn = new Button();
            clearAnswerBtn = new Button();
            sendBtn = new Button();
            visualizedBox = new PictureBox();
            timer2 = new System.Windows.Forms.Timer(components);
            groupBox6 = new GroupBox();
            groupBox7 = new GroupBox();
            recordingChb = new CheckBox();
            keyTypeLbl = new Label();
            panel1 = new Panel();
            autoKey = new RadioButton();
            ordinaryKey = new RadioButton();
            strictCbx = new CheckBox();
            label16 = new Label();
            sendToneBox = new TextBox();
            label17 = new Label();
            label14 = new Label();
            charInterval = new TextBox();
            label15 = new Label();
            label12 = new Label();
            keyInterval = new TextBox();
            label13 = new Label();
            label10 = new Label();
            sendDaLength = new TextBox();
            label11 = new Label();
            label8 = new Label();
            sendDiLength = new TextBox();
            label9 = new Label();
            label6 = new Label();
            sendSpeedTxb = new TextBox();
            label5 = new Label();
            groupBox8 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)speedBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)toneBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)volumeTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EachGroup).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupNumBox).BeginInit();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)visualizedBox).BeginInit();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            panel1.SuspendLayout();
            groupBox8.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(individuationRbtn);
            groupBox1.Controls.Add(radioButton8);
            groupBox1.Controls.Add(radioButton6);
            groupBox1.Controls.Add(radioButton5);
            groupBox1.Controls.Add(radioButton4);
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Location = new Point(6, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(605, 60);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "练习模式";
            // 
            // individuationRbtn
            // 
            individuationRbtn.AutoSize = true;
            individuationRbtn.Location = new Point(539, 20);
            individuationRbtn.Name = "individuationRbtn";
            individuationRbtn.Size = new Size(62, 21);
            individuationRbtn.TabIndex = 7;
            individuationRbtn.TabStop = true;
            individuationRbtn.Text = "自定义";
            individuationRbtn.UseVisualStyleBackColor = true;
            individuationRbtn.CheckedChanged += IndividuationRbtn_CheckedChanged;
            // 
            // radioButton8
            // 
            radioButton8.AutoSize = true;
            radioButton8.Location = new Point(458, 20);
            radioButton8.Name = "radioButton8";
            radioButton8.Size = new Size(74, 21);
            radioButton8.TabIndex = 6;
            radioButton8.TabStop = true;
            radioButton8.Text = "随机单词";
            radioButton8.UseVisualStyleBackColor = true;
            radioButton8.CheckedChanged += RadioButton8_CheckedChanged;
            // 
            // radioButton6
            // 
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(400, 20);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(50, 21);
            radioButton6.TabIndex = 5;
            radioButton6.TabStop = true;
            radioButton6.Text = "新闻";
            radioButton6.UseVisualStyleBackColor = true;
            radioButton6.CheckedChanged += RadioButton6_CheckedChanged;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(266, 20);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(50, 21);
            radioButton5.TabIndex = 3;
            radioButton5.TabStop = true;
            radioButton5.Text = "符号";
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.CheckedChanged += RadioButton5_CheckedChanged;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(320, 20);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(74, 21);
            radioButton4.TabIndex = 4;
            radioButton4.TabStop = true;
            radioButton4.Text = "英语文章";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += RadioButton4_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(157, 20);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(107, 21);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Text = "数字+字母分组";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += RadioButton3_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(82, 20);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(74, 21);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "分组字母";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += RadioButton2_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(8, 20);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(74, 21);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "分组数字";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += RadioButton1_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(eqBox);
            groupBox2.Controls.Add(neBox);
            groupBox2.Controls.Add(eqRbtn);
            groupBox2.Controls.Add(neRbtn);
            groupBox2.Location = new Point(6, 68);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(605, 100);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "个性化定制";
            // 
            // eqBox
            // 
            eqBox.Enabled = false;
            eqBox.FormattingEnabled = true;
            eqBox.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" });
            eqBox.Location = new Point(346, 18);
            eqBox.Name = "eqBox";
            eqBox.Size = new Size(214, 76);
            eqBox.TabIndex = 3;
            // 
            // neBox
            // 
            neBox.Enabled = false;
            neBox.Font = new Font("Microsoft YaHei UI", 9F);
            neBox.FormattingEnabled = true;
            neBox.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" });
            neBox.Location = new Point(66, 20);
            neBox.Name = "neBox";
            neBox.Size = new Size(214, 76);
            neBox.TabIndex = 2;
            // 
            // eqRbtn
            // 
            eqRbtn.AutoSize = true;
            eqRbtn.Location = new Point(286, 29);
            eqRbtn.Name = "eqRbtn";
            eqRbtn.Size = new Size(62, 21);
            eqRbtn.TabIndex = 1;
            eqRbtn.TabStop = true;
            eqRbtn.Text = "仅包含";
            eqRbtn.UseVisualStyleBackColor = true;
            eqRbtn.CheckedChanged += EqRbtn_CheckedChanged;
            // 
            // neRbtn
            // 
            neRbtn.AutoSize = true;
            neRbtn.Location = new Point(10, 29);
            neRbtn.Name = "neRbtn";
            neRbtn.Size = new Size(50, 21);
            neRbtn.TabIndex = 0;
            neRbtn.TabStop = true;
            neRbtn.Text = "排除";
            neRbtn.UseVisualStyleBackColor = true;
            neRbtn.CheckedChanged += NeRbtn_CheckedChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(bgmCbx);
            groupBox3.Controls.Add(speedBox);
            groupBox3.Controls.Add(toneBox);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(volumeTrackBar);
            groupBox3.Location = new Point(617, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(292, 78);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "背景音配置";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(9, 47);
            label7.Name = "label7";
            label7.Size = new Size(59, 17);
            label7.TabIndex = 3;
            label7.Text = "背景音量:";
            // 
            // bgmCbx
            // 
            bgmCbx.AutoSize = true;
            bgmCbx.Location = new Point(233, 20);
            bgmCbx.Name = "bgmCbx";
            bgmCbx.Size = new Size(51, 21);
            bgmCbx.TabIndex = 2;
            bgmCbx.Text = "开启";
            bgmCbx.UseVisualStyleBackColor = true;
            // 
            // speedBox
            // 
            speedBox.Location = new Point(81, 17);
            speedBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            speedBox.Name = "speedBox";
            speedBox.Size = new Size(48, 23);
            speedBox.TabIndex = 0;
            speedBox.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // toneBox
            // 
            toneBox.Location = new Point(186, 19);
            toneBox.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            toneBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            toneBox.Name = "toneBox";
            toneBox.Size = new Size(46, 23);
            toneBox.TabIndex = 1;
            toneBox.Value = new decimal(new int[] { 600, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(129, 21);
            label2.Name = "label2";
            label2.Size = new Size(58, 17);
            label2.TabIndex = 1;
            label2.Text = "频率(Hz):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 19);
            label1.Name = "label1";
            label1.Size = new Size(74, 17);
            label1.TabIndex = 0;
            label1.Text = "速度(WPM):";
            // 
            // volumeTrackBar
            // 
            volumeTrackBar.AutoSize = false;
            volumeTrackBar.Location = new Point(67, 43);
            volumeTrackBar.Name = "volumeTrackBar";
            volumeTrackBar.Size = new Size(213, 29);
            volumeTrackBar.TabIndex = 4;
            volumeTrackBar.Value = 8;
            volumeTrackBar.ValueChanged += volumeTrackBar_ValueChanged;
            // 
            // symbolsChb
            // 
            symbolsChb.AutoSize = true;
            symbolsChb.Location = new Point(125, 57);
            symbolsChb.Name = "symbolsChb";
            symbolsChb.Size = new Size(87, 21);
            symbolsChb.TabIndex = 4;
            symbolsChb.Text = "文章含符号";
            symbolsChb.UseVisualStyleBackColor = true;
            // 
            // EachGroup
            // 
            EachGroup.Location = new Point(75, 51);
            EachGroup.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            EachGroup.Name = "EachGroup";
            EachGroup.Size = new Size(47, 23);
            EachGroup.TabIndex = 1;
            EachGroup.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1, 53);
            label4.Name = "label4";
            label4.Size = new Size(81, 17);
            label4.TabIndex = 12;
            label4.Text = "数量(个/组)：";
            // 
            // exportBtn
            // 
            exportBtn.Location = new Point(212, 56);
            exportBtn.Name = "exportBtn";
            exportBtn.Size = new Size(75, 23);
            exportBtn.TabIndex = 7;
            exportBtn.Text = "导出";
            exportBtn.UseVisualStyleBackColor = true;
            exportBtn.Click += ExportBtn_Click;
            // 
            // repeatRbtn
            // 
            repeatRbtn.AutoSize = true;
            repeatRbtn.Location = new Point(125, 13);
            repeatRbtn.Name = "repeatRbtn";
            repeatRbtn.Size = new Size(87, 21);
            repeatRbtn.TabIndex = 2;
            repeatRbtn.Text = "同组无重复";
            repeatRbtn.UseVisualStyleBackColor = true;
            // 
            // continuousRbtn
            // 
            continuousRbtn.AutoSize = true;
            continuousRbtn.Location = new Point(125, 35);
            continuousRbtn.Name = "continuousRbtn";
            continuousRbtn.Size = new Size(87, 21);
            continuousRbtn.TabIndex = 3;
            continuousRbtn.Text = "同组无连续";
            continuousRbtn.UseVisualStyleBackColor = true;
            // 
            // submitAnswerBtn
            // 
            submitAnswerBtn.Location = new Point(212, 33);
            submitAnswerBtn.Name = "submitAnswerBtn";
            submitAnswerBtn.Size = new Size(75, 23);
            submitAnswerBtn.TabIndex = 6;
            submitAnswerBtn.Text = "提交答案";
            submitAnswerBtn.UseVisualStyleBackColor = true;
            // 
            // startBtn
            // 
            startBtn.Location = new Point(212, 10);
            startBtn.Name = "startBtn";
            startBtn.Size = new Size(75, 23);
            startBtn.TabIndex = 5;
            startBtn.Text = "生成报文";
            startBtn.UseVisualStyleBackColor = true;
            startBtn.Click += StartBtn_Click;
            // 
            // groupNumBox
            // 
            groupNumBox.Location = new Point(74, 20);
            groupNumBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            groupNumBox.Name = "groupNumBox";
            groupNumBox.Size = new Size(48, 23);
            groupNumBox.TabIndex = 0;
            groupNumBox.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 22);
            label3.Name = "label3";
            label3.Size = new Size(64, 17);
            label3.TabIndex = 2;
            label3.Text = "数量(组)：";
            // 
            // stopBtn
            // 
            stopBtn.Location = new Point(6, 134);
            stopBtn.Name = "stopBtn";
            stopBtn.Size = new Size(75, 23);
            stopBtn.TabIndex = 4;
            stopBtn.Text = "停止";
            stopBtn.UseVisualStyleBackColor = true;
            stopBtn.Click += StopBtn_Click;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.Controls.Add(replicationBox6);
            groupBox4.Controls.Add(answerLbl6);
            groupBox4.Controls.Add(replicationBox5);
            groupBox4.Controls.Add(answerLbl5);
            groupBox4.Controls.Add(replicationBox4);
            groupBox4.Controls.Add(answerLbl4);
            groupBox4.Controls.Add(replicationBox3);
            groupBox4.Controls.Add(answerLbl3);
            groupBox4.Controls.Add(replicationBox2);
            groupBox4.Controls.Add(answerLbl2);
            groupBox4.Controls.Add(replicationBox1);
            groupBox4.Controls.Add(answerLbl1);
            groupBox4.Location = new Point(6, 174);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(1895, 864);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "拍发";
            // 
            // replicationBox6
            // 
            replicationBox6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            replicationBox6.BorderStyle = BorderStyle.FixedSingle;
            replicationBox6.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            replicationBox6.Location = new Point(10, 748);
            replicationBox6.Margin = new Padding(3, 0, 3, 0);
            replicationBox6.Name = "replicationBox6";
            replicationBox6.ScrollBars = RichTextBoxScrollBars.None;
            replicationBox6.Size = new Size(1885, 77);
            replicationBox6.TabIndex = 11;
            replicationBox6.Text = "";
            // 
            // answerLbl6
            // 
            answerLbl6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            answerLbl6.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            answerLbl6.Location = new Point(6, 691);
            answerLbl6.Name = "answerLbl6";
            answerLbl6.Size = new Size(1889, 54);
            answerLbl6.TabIndex = 10;
            // 
            // replicationBox5
            // 
            replicationBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            replicationBox5.BorderStyle = BorderStyle.FixedSingle;
            replicationBox5.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            replicationBox5.Location = new Point(10, 614);
            replicationBox5.Margin = new Padding(3, 0, 3, 0);
            replicationBox5.Name = "replicationBox5";
            replicationBox5.ScrollBars = RichTextBoxScrollBars.None;
            replicationBox5.Size = new Size(1885, 77);
            replicationBox5.TabIndex = 9;
            replicationBox5.Text = "";
            // 
            // answerLbl5
            // 
            answerLbl5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            answerLbl5.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            answerLbl5.Location = new Point(6, 557);
            answerLbl5.Name = "answerLbl5";
            answerLbl5.Size = new Size(1889, 54);
            answerLbl5.TabIndex = 8;
            // 
            // replicationBox4
            // 
            replicationBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            replicationBox4.BorderStyle = BorderStyle.FixedSingle;
            replicationBox4.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            replicationBox4.Location = new Point(10, 480);
            replicationBox4.Margin = new Padding(3, 0, 3, 0);
            replicationBox4.Name = "replicationBox4";
            replicationBox4.ScrollBars = RichTextBoxScrollBars.None;
            replicationBox4.Size = new Size(1885, 77);
            replicationBox4.TabIndex = 7;
            replicationBox4.Text = "";
            // 
            // answerLbl4
            // 
            answerLbl4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            answerLbl4.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            answerLbl4.Location = new Point(6, 423);
            answerLbl4.Name = "answerLbl4";
            answerLbl4.Size = new Size(1889, 54);
            answerLbl4.TabIndex = 6;
            // 
            // replicationBox3
            // 
            replicationBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            replicationBox3.BorderStyle = BorderStyle.FixedSingle;
            replicationBox3.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            replicationBox3.Location = new Point(10, 346);
            replicationBox3.Margin = new Padding(3, 0, 3, 0);
            replicationBox3.Name = "replicationBox3";
            replicationBox3.ScrollBars = RichTextBoxScrollBars.None;
            replicationBox3.Size = new Size(1885, 77);
            replicationBox3.TabIndex = 5;
            replicationBox3.Text = "";
            // 
            // answerLbl3
            // 
            answerLbl3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            answerLbl3.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            answerLbl3.Location = new Point(6, 289);
            answerLbl3.Name = "answerLbl3";
            answerLbl3.Size = new Size(1889, 54);
            answerLbl3.TabIndex = 4;
            // 
            // replicationBox2
            // 
            replicationBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            replicationBox2.BorderStyle = BorderStyle.FixedSingle;
            replicationBox2.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            replicationBox2.Location = new Point(10, 212);
            replicationBox2.Margin = new Padding(3, 0, 3, 0);
            replicationBox2.Name = "replicationBox2";
            replicationBox2.ScrollBars = RichTextBoxScrollBars.None;
            replicationBox2.Size = new Size(1885, 77);
            replicationBox2.TabIndex = 3;
            replicationBox2.Text = "";
            // 
            // answerLbl2
            // 
            answerLbl2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            answerLbl2.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            answerLbl2.Location = new Point(6, 155);
            answerLbl2.Name = "answerLbl2";
            answerLbl2.Size = new Size(1889, 54);
            answerLbl2.TabIndex = 2;
            // 
            // replicationBox1
            // 
            replicationBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            replicationBox1.BorderStyle = BorderStyle.FixedSingle;
            replicationBox1.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            replicationBox1.Location = new Point(10, 78);
            replicationBox1.Margin = new Padding(3, 0, 3, 0);
            replicationBox1.Name = "replicationBox1";
            replicationBox1.ScrollBars = RichTextBoxScrollBars.None;
            replicationBox1.Size = new Size(1885, 77);
            replicationBox1.TabIndex = 1;
            replicationBox1.Text = "";
            // 
            // answerLbl1
            // 
            answerLbl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            answerLbl1.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            answerLbl1.Location = new Point(6, 21);
            answerLbl1.Name = "answerLbl1";
            answerLbl1.Size = new Size(1889, 54);
            answerLbl1.TabIndex = 0;
            // 
            // timer1
            // 
            timer1.Interval = 5000;
            timer1.Tick += Timer1_Tick;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(rePlayBtn);
            groupBox5.Controls.Add(continuePlayBtn);
            groupBox5.Controls.Add(pauseBtn);
            groupBox5.Controls.Add(clearAnswerBtn);
            groupBox5.Controls.Add(stopBtn);
            groupBox5.Location = new Point(914, 2);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(88, 166);
            groupBox5.TabIndex = 4;
            groupBox5.TabStop = false;
            groupBox5.Text = "播放控制";
            // 
            // rePlayBtn
            // 
            rePlayBtn.Enabled = false;
            rePlayBtn.Location = new Point(6, 106);
            rePlayBtn.Name = "rePlayBtn";
            rePlayBtn.Size = new Size(75, 23);
            rePlayBtn.TabIndex = 3;
            rePlayBtn.Text = "重播";
            rePlayBtn.UseVisualStyleBackColor = true;
            rePlayBtn.Click += ResumeBtn_Click;
            // 
            // continuePlayBtn
            // 
            continuePlayBtn.Enabled = false;
            continuePlayBtn.Location = new Point(6, 78);
            continuePlayBtn.Name = "continuePlayBtn";
            continuePlayBtn.Size = new Size(75, 23);
            continuePlayBtn.TabIndex = 2;
            continuePlayBtn.Text = "继续播放";
            continuePlayBtn.UseVisualStyleBackColor = true;
            continuePlayBtn.Click += ContinuePlayBtn_Click;
            // 
            // pauseBtn
            // 
            pauseBtn.Enabled = false;
            pauseBtn.Location = new Point(6, 50);
            pauseBtn.Name = "pauseBtn";
            pauseBtn.Size = new Size(75, 23);
            pauseBtn.TabIndex = 1;
            pauseBtn.Text = "暂停播放";
            pauseBtn.UseVisualStyleBackColor = true;
            pauseBtn.Click += PauseBtn_Click;
            // 
            // clearAnswerBtn
            // 
            clearAnswerBtn.Location = new Point(6, 22);
            clearAnswerBtn.Name = "clearAnswerBtn";
            clearAnswerBtn.Size = new Size(75, 23);
            clearAnswerBtn.TabIndex = 0;
            clearAnswerBtn.Text = "清空答案";
            clearAnswerBtn.UseVisualStyleBackColor = true;
            clearAnswerBtn.Click += ClearAnswer_Click;
            // 
            // sendBtn
            // 
            sendBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            sendBtn.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Bold);
            sendBtn.Location = new Point(1795, 96);
            sendBtn.Name = "sendBtn";
            sendBtn.Size = new Size(106, 70);
            sendBtn.TabIndex = 7;
            sendBtn.Text = "发送";
            sendBtn.UseVisualStyleBackColor = true;
            sendBtn.MouseDown += SendBtn_MouseDown;
            sendBtn.MouseUp += SendBtn_MouseUp;
            // 
            // visualizedBox
            // 
            visualizedBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            visualizedBox.Location = new Point(3, 19);
            visualizedBox.Name = "visualizedBox";
            visualizedBox.Size = new Size(887, 53);
            visualizedBox.SizeMode = PictureBoxSizeMode.StretchImage;
            visualizedBox.TabIndex = 7;
            visualizedBox.TabStop = false;
            // 
            // timer2
            // 
            timer2.Enabled = true;
            timer2.Interval = 1;
            timer2.Tick += Timer2_Tick;
            // 
            // groupBox6
            // 
            groupBox6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox6.Controls.Add(visualizedBox);
            groupBox6.Location = new Point(1008, 2);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(893, 78);
            groupBox6.TabIndex = 5;
            groupBox6.TabStop = false;
            groupBox6.Text = "可视化";
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(recordingChb);
            groupBox7.Controls.Add(keyTypeLbl);
            groupBox7.Controls.Add(panel1);
            groupBox7.Controls.Add(strictCbx);
            groupBox7.Controls.Add(label16);
            groupBox7.Controls.Add(sendToneBox);
            groupBox7.Controls.Add(label17);
            groupBox7.Controls.Add(label14);
            groupBox7.Controls.Add(charInterval);
            groupBox7.Controls.Add(label15);
            groupBox7.Controls.Add(label12);
            groupBox7.Controls.Add(keyInterval);
            groupBox7.Controls.Add(label13);
            groupBox7.Controls.Add(label10);
            groupBox7.Controls.Add(sendDaLength);
            groupBox7.Controls.Add(label11);
            groupBox7.Controls.Add(label8);
            groupBox7.Controls.Add(sendDiLength);
            groupBox7.Controls.Add(label9);
            groupBox7.Controls.Add(label6);
            groupBox7.Controls.Add(sendSpeedTxb);
            groupBox7.Controls.Add(label5);
            groupBox7.Location = new Point(1011, 86);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(778, 82);
            groupBox7.TabIndex = 6;
            groupBox7.TabStop = false;
            groupBox7.Text = "CW规则";
            // 
            // recordingChb
            // 
            recordingChb.AutoSize = true;
            recordingChb.Location = new Point(203, 49);
            recordingChb.Name = "recordingChb";
            recordingChb.Size = new Size(75, 21);
            recordingChb.TabIndex = 20;
            recordingChb.Text = "记录发报";
            recordingChb.UseVisualStyleBackColor = true;
            // 
            // keyTypeLbl
            // 
            keyTypeLbl.AutoSize = true;
            keyTypeLbl.Location = new Point(10, 50);
            keyTypeLbl.Name = "keyTypeLbl";
            keyTypeLbl.Size = new Size(47, 17);
            keyTypeLbl.TabIndex = 19;
            keyTypeLbl.Text = "键类型:";
            // 
            // panel1
            // 
            panel1.Controls.Add(autoKey);
            panel1.Controls.Add(ordinaryKey);
            panel1.Location = new Point(58, 42);
            panel1.Name = "panel1";
            panel1.Size = new Size(130, 31);
            panel1.TabIndex = 18;
            // 
            // autoKey
            // 
            autoKey.AutoSize = true;
            autoKey.Location = new Point(60, 5);
            autoKey.Name = "autoKey";
            autoKey.Size = new Size(62, 21);
            autoKey.TabIndex = 1;
            autoKey.TabStop = true;
            autoKey.Text = "自动键";
            autoKey.UseVisualStyleBackColor = true;
            autoKey.CheckedChanged += ordinaryKey_CheckedChanged;
            // 
            // ordinaryKey
            // 
            ordinaryKey.AutoSize = true;
            ordinaryKey.Location = new Point(3, 5);
            ordinaryKey.Name = "ordinaryKey";
            ordinaryKey.Size = new Size(50, 21);
            ordinaryKey.TabIndex = 0;
            ordinaryKey.TabStop = true;
            ordinaryKey.Text = "手键";
            ordinaryKey.UseVisualStyleBackColor = true;
            ordinaryKey.CheckedChanged += ordinaryKey_CheckedChanged;
            // 
            // strictCbx
            // 
            strictCbx.AutoSize = true;
            strictCbx.Location = new Point(705, 17);
            strictCbx.Name = "strictCbx";
            strictCbx.Size = new Size(51, 21);
            strictCbx.TabIndex = 6;
            strictCbx.Text = "严格";
            strictCbx.UseVisualStyleBackColor = true;
            strictCbx.CheckedChanged += StrictCbx_CheckedChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(676, 18);
            label16.Name = "label16";
            label16.Size = new Size(31, 17);
            label16.TabIndex = 17;
            label16.Text = "(Hz)";
            // 
            // sendToneBox
            // 
            sendToneBox.Location = new Point(615, 15);
            sendToneBox.MaxLength = 5;
            sendToneBox.Name = "sendToneBox";
            sendToneBox.Size = new Size(59, 23);
            sendToneBox.TabIndex = 5;
            sendToneBox.Text = "650";
            sendToneBox.TextChanged += SendToneBox_TextChanged;
            sendToneBox.KeyPress += NumberTxb_KeyPress;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(580, 18);
            label17.Name = "label17";
            label17.Size = new Size(35, 17);
            label17.TabIndex = 15;
            label17.Text = "频率:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(549, 18);
            label14.Name = "label14";
            label14.Size = new Size(33, 17);
            label14.TabIndex = 14;
            label14.Text = "(ms)";
            // 
            // charInterval
            // 
            charInterval.Location = new Point(519, 15);
            charInterval.MaxLength = 3;
            charInterval.Name = "charInterval";
            charInterval.Size = new Size(29, 23);
            charInterval.TabIndex = 4;
            charInterval.Text = "420";
            charInterval.TextChanged += CharInterval_TextChanged;
            charInterval.KeyPress += NumberTxb_KeyPress;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(464, 18);
            label15.Name = "label15";
            label15.Size = new Size(59, 17);
            label15.TabIndex = 12;
            label15.Text = "字符间隔:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(433, 18);
            label12.Name = "label12";
            label12.Size = new Size(33, 17);
            label12.TabIndex = 11;
            label12.Text = "(ms)";
            // 
            // keyInterval
            // 
            keyInterval.Location = new Point(403, 15);
            keyInterval.MaxLength = 3;
            keyInterval.Name = "keyInterval";
            keyInterval.Size = new Size(29, 23);
            keyInterval.TabIndex = 3;
            keyInterval.Text = "60";
            keyInterval.TextChanged += KeyInterval_TextChanged;
            keyInterval.KeyPress += NumberTxb_KeyPress;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(348, 18);
            label13.Name = "label13";
            label13.Size = new Size(59, 17);
            label13.TabIndex = 9;
            label13.Text = "键击间隔:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(316, 18);
            label10.Name = "label10";
            label10.Size = new Size(33, 17);
            label10.TabIndex = 8;
            label10.Text = "(ms)";
            // 
            // sendDaLength
            // 
            sendDaLength.Location = new Point(287, 15);
            sendDaLength.MaxLength = 3;
            sendDaLength.Name = "sendDaLength";
            sendDaLength.Size = new Size(29, 23);
            sendDaLength.TabIndex = 2;
            sendDaLength.Text = "180";
            sendDaLength.TextChanged += sendDaLength_TextChanged;
            sendDaLength.KeyPress += NumberTxb_KeyPress;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(232, 18);
            label11.Name = "label11";
            label11.Size = new Size(57, 17);
            label11.TabIndex = 6;
            label11.Text = "\"嗒\"声长:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(203, 18);
            label8.Name = "label8";
            label8.Size = new Size(33, 17);
            label8.TabIndex = 5;
            label8.Text = "(ms)";
            // 
            // sendDiLength
            // 
            sendDiLength.Location = new Point(173, 15);
            sendDiLength.MaxLength = 3;
            sendDiLength.Name = "sendDiLength";
            sendDiLength.Size = new Size(29, 23);
            sendDiLength.TabIndex = 1;
            sendDiLength.Text = "60";
            sendDiLength.TextChanged += sendDiLength_TextChanged;
            sendDiLength.KeyPress += NumberTxb_KeyPress;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(118, 18);
            label9.Name = "label9";
            label9.Size = new Size(57, 17);
            label9.TabIndex = 3;
            label9.Text = "\"嘀\"声长:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(72, 18);
            label6.Name = "label6";
            label6.Size = new Size(47, 17);
            label6.TabIndex = 2;
            label6.Text = "(WPM)";
            // 
            // sendSpeedTxb
            // 
            sendSpeedTxb.Location = new Point(42, 15);
            sendSpeedTxb.MaxLength = 2;
            sendSpeedTxb.Name = "sendSpeedTxb";
            sendSpeedTxb.Size = new Size(29, 23);
            sendSpeedTxb.TabIndex = 0;
            sendSpeedTxb.Text = "20";
            sendSpeedTxb.KeyPress += NumberTxb_KeyPress;
            sendSpeedTxb.Leave += SnedSpeedTxb_Leave;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 18);
            label5.Name = "label5";
            label5.Size = new Size(35, 17);
            label5.TabIndex = 0;
            label5.Text = "键速:";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(EachGroup);
            groupBox8.Controls.Add(label4);
            groupBox8.Controls.Add(symbolsChb);
            groupBox8.Controls.Add(continuousRbtn);
            groupBox8.Controls.Add(startBtn);
            groupBox8.Controls.Add(groupNumBox);
            groupBox8.Controls.Add(submitAnswerBtn);
            groupBox8.Controls.Add(label3);
            groupBox8.Controls.Add(exportBtn);
            groupBox8.Controls.Add(repeatRbtn);
            groupBox8.Location = new Point(617, 86);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(292, 82);
            groupBox8.TabIndex = 3;
            groupBox8.TabStop = false;
            groupBox8.Text = "报文控制";
            // 
            // SendPractice
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(groupBox3);
            Controls.Add(groupBox7);
            Controls.Add(groupBox6);
            Controls.Add(groupBox8);
            Controls.Add(sendBtn);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "SendPractice";
            Text = "拍发练习";
            FormClosed += CopyingPractice_FormClosed;
            Load += CopyingPractice_Load;
            SizeChanged += SendPractice_SizeChanged;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)speedBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)toneBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)volumeTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)EachGroup).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupNumBox).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)visualizedBox).EndInit();
            groupBox6.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private RadioButton radioButton1;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private GroupBox groupBox2;
        private RadioButton eqRbtn;
        private RadioButton neRbtn;
        private CheckedListBox eqBox;
        private CheckedListBox neBox;
        private RadioButton radioButton5;
        private GroupBox groupBox3;
        private Label label3;
        private Label label2;
        private Label label1;
        private GroupBox groupBox4;
        private NumericUpDown speedBox;
        private NumericUpDown toneBox;
        private NumericUpDown groupNumBox;
        private Button submitAnswerBtn;
        private Button startBtn;
        private CheckBox continuousRbtn;
        private Button exportBtn;
        private CheckBox repeatRbtn;
        private Button stopBtn;
        private NumericUpDown EachGroup;
        private Label label4;
        private System.Windows.Forms.Timer timer1;
        private RadioButton radioButton6;
        private CheckBox symbolsChb;
        private RadioButton radioButton8;
        private GroupBox groupBox5;
        private Button rePlayBtn;
        private Button continuePlayBtn;
        private Button pauseBtn;
        private Button clearAnswerBtn;
        private RadioButton individuationRbtn;
        private Label answerLbl1;
        private RichTextBox replicationBox1;
        private Button sendBtn;
        private PictureBox visualizedBox;
        private System.Windows.Forms.Timer timer2;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private TextBox sendSpeedTxb;
        private Label label5;
        private Label label6;
        private Label label12;
        private TextBox keyInterval;
        private Label label13;
        private Label label10;
        private TextBox sendDaLength;
        private Label label11;
        private Label label8;
        private TextBox sendDiLength;
        private Label label9;
        private Label label14;
        private TextBox charInterval;
        private Label label15;
        private Label label16;
        private TextBox sendToneBox;
        private Label label17;
        private GroupBox groupBox8;
        private CheckBox bgmCbx;
        private CheckBox strictCbx;
        private RichTextBox replicationBox6;
        private Label answerLbl6;
        private RichTextBox replicationBox5;
        private Label answerLbl5;
        private RichTextBox replicationBox4;
        private Label answerLbl4;
        private RichTextBox replicationBox3;
        private Label answerLbl3;
        private RichTextBox replicationBox2;
        private Label answerLbl2;
        private Label label7;
        private TrackBar volumeTrackBar;
        private Panel panel1;
        private RadioButton autoKey;
        private RadioButton ordinaryKey;
        private Label keyTypeLbl;
        private CheckBox recordingChb;
    }
}