namespace CW
{
    partial class NumberCopyingPractice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumberCopyingPractice));
            groupBox1 = new GroupBox();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            groupBox2 = new GroupBox();
            eqBox = new CheckedListBox();
            neBox = new CheckedListBox();
            eqRbtn = new RadioButton();
            neRbtn = new RadioButton();
            groupBox3 = new GroupBox();
            waveList = new ComboBox();
            label7 = new Label();
            msgEndTxb = new TextBox();
            label9 = new Label();
            msgStartTxb = new TextBox();
            label10 = new Label();
            label8 = new Label();
            noiseLevel = new NumericUpDown();
            extraWordSpacing = new NumericUpDown();
            label6 = new Label();
            effectiveSpeed = new NumericUpDown();
            label5 = new Label();
            checkAnserSpeed = new NumericUpDown();
            checkAnswerChb = new CheckBox();
            showAnswerChb = new CheckBox();
            EachGroup = new NumericUpDown();
            label4 = new Label();
            exportBtn = new Button();
            repeatRbtn = new CheckBox();
            continuousRbtn = new CheckBox();
            submitAnswerBtn = new Button();
            startBtn = new Button();
            speetBox = new NumericUpDown();
            toneBox = new NumericUpDown();
            groupNumBox = new NumericUpDown();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            stopBtn = new Button();
            groupBox4 = new GroupBox();
            answerBox = new RichTextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            groupBox5 = new GroupBox();
            rePlayBtn = new Button();
            continuePlayBtn = new Button();
            pauseBtn = new Button();
            clearAnswerBtn = new Button();
            groupBox6 = new GroupBox();
            radioButton4 = new RadioButton();
            radioButton3 = new RadioButton();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)noiseLevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)extraWordSpacing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)effectiveSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)checkAnserSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EachGroup).BeginInit();
            ((System.ComponentModel.ISupportInitialize)speetBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)toneBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupNumBox).BeginInit();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Font = new Font("Microsoft YaHei UI", 12F);
            groupBox1.Location = new Point(9, 3);
            groupBox1.Margin = new Padding(5, 4, 5, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(5, 4, 5, 4);
            groupBox1.Size = new Size(500, 85);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "练习模式";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new Font("Microsoft YaHei UI", 12F);
            radioButton2.Location = new Point(277, 32);
            radioButton2.Margin = new Padding(5, 4, 5, 4);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(203, 35);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "分组数字(短10)";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged_1;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Font = new Font("Microsoft YaHei UI", 12F);
            radioButton1.Location = new Point(13, 28);
            radioButton1.Margin = new Padding(5, 4, 5, 4);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(189, 35);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "分组数字(短5)";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += RadioButton1_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(eqBox);
            groupBox2.Controls.Add(neBox);
            groupBox2.Controls.Add(eqRbtn);
            groupBox2.Controls.Add(neRbtn);
            groupBox2.Font = new Font("Microsoft YaHei UI", 12F);
            groupBox2.Location = new Point(9, 96);
            groupBox2.Margin = new Padding(5, 4, 5, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(5, 4, 5, 4);
            groupBox2.Size = new Size(996, 195);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "个性化定制";
            // 
            // eqBox
            // 
            eqBox.Enabled = false;
            eqBox.Font = new Font("Microsoft YaHei UI", 12F);
            eqBox.FormattingEnabled = true;
            eqBox.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" });
            eqBox.Location = new Point(578, 25);
            eqBox.Margin = new Padding(5, 4, 5, 4);
            eqBox.Name = "eqBox";
            eqBox.Size = new Size(334, 109);
            eqBox.TabIndex = 3;
            // 
            // neBox
            // 
            neBox.Enabled = false;
            neBox.Font = new Font("Microsoft YaHei UI", 12F);
            neBox.FormattingEnabled = true;
            neBox.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" });
            neBox.Location = new Point(118, 28);
            neBox.Margin = new Padding(5, 4, 5, 4);
            neBox.Name = "neBox";
            neBox.Size = new Size(334, 109);
            neBox.TabIndex = 2;
            // 
            // eqRbtn
            // 
            eqRbtn.AutoSize = true;
            eqRbtn.Font = new Font("Microsoft YaHei UI", 12F);
            eqRbtn.Location = new Point(464, 41);
            eqRbtn.Margin = new Padding(5, 4, 5, 4);
            eqRbtn.Name = "eqRbtn";
            eqRbtn.Size = new Size(111, 35);
            eqRbtn.TabIndex = 1;
            eqRbtn.TabStop = true;
            eqRbtn.Text = "仅包含";
            eqRbtn.UseVisualStyleBackColor = true;
            eqRbtn.CheckedChanged += EqRbtn_CheckedChanged;
            // 
            // neRbtn
            // 
            neRbtn.AutoSize = true;
            neRbtn.Font = new Font("Microsoft YaHei UI", 12F);
            neRbtn.Location = new Point(30, 41);
            neRbtn.Margin = new Padding(5, 4, 5, 4);
            neRbtn.Name = "neRbtn";
            neRbtn.Size = new Size(87, 35);
            neRbtn.TabIndex = 0;
            neRbtn.TabStop = true;
            neRbtn.Text = "排除";
            neRbtn.UseVisualStyleBackColor = true;
            neRbtn.CheckedChanged += NeRbtn_CheckedChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(waveList);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(msgEndTxb);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(msgStartTxb);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(noiseLevel);
            groupBox3.Controls.Add(extraWordSpacing);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(effectiveSpeed);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(checkAnserSpeed);
            groupBox3.Controls.Add(checkAnswerChb);
            groupBox3.Controls.Add(showAnswerChb);
            groupBox3.Controls.Add(EachGroup);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(exportBtn);
            groupBox3.Controls.Add(repeatRbtn);
            groupBox3.Controls.Add(continuousRbtn);
            groupBox3.Controls.Add(submitAnswerBtn);
            groupBox3.Controls.Add(startBtn);
            groupBox3.Controls.Add(speetBox);
            groupBox3.Controls.Add(toneBox);
            groupBox3.Controls.Add(groupNumBox);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(label1);
            groupBox3.Font = new Font("Microsoft YaHei UI", 12F);
            groupBox3.Location = new Point(1025, 3);
            groupBox3.Margin = new Padding(5, 4, 5, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(5, 4, 5, 4);
            groupBox3.Size = new Size(905, 288);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "CW配置";
            // 
            // waveList
            // 
            waveList.DropDownStyle = ComboBoxStyle.DropDownList;
            waveList.FormattingEnabled = true;
            waveList.Items.AddRange(new object[] { "方波", "锯齿波", "正弦波" });
            waveList.Location = new Point(635, 226);
            waveList.Margin = new Padding(5, 4, 5, 4);
            waveList.Name = "waveList";
            waveList.Size = new Size(115, 39);
            waveList.TabIndex = 29;
            waveList.SelectedIndex = 0;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(557, 231);
            label7.Margin = new Padding(5, 0, 5, 0);
            label7.Name = "label7";
            label7.Size = new Size(68, 31);
            label7.TabIndex = 28;
            label7.Text = "波形:";
            // 
            // msgEndTxb
            // 
            msgEndTxb.Location = new Point(396, 227);
            msgEndTxb.Margin = new Padding(5, 4, 5, 4);
            msgEndTxb.Name = "msgEndTxb";
            msgEndTxb.Size = new Size(136, 38);
            msgEndTxb.TabIndex = 27;
            msgEndTxb.Text = "iii";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(295, 234);
            label9.Margin = new Padding(5, 0, 5, 0);
            label9.Name = "label9";
            label9.Size = new Size(86, 31);
            label9.TabIndex = 26;
            label9.Text = "报尾：";
            // 
            // msgStartTxb
            // 
            msgStartTxb.Location = new Point(398, 179);
            msgStartTxb.Margin = new Padding(5, 4, 5, 4);
            msgStartTxb.Name = "msgStartTxb";
            msgStartTxb.Size = new Size(136, 38);
            msgStartTxb.TabIndex = 25;
            msgStartTxb.Text = "MSG=";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(295, 185);
            label10.Margin = new Padding(5, 0, 5, 0);
            label10.Name = "label10";
            label10.Size = new Size(86, 31);
            label10.TabIndex = 24;
            label10.Text = "报头：";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(288, 131);
            label8.Margin = new Padding(5, 0, 5, 0);
            label8.Name = "label8";
            label8.Size = new Size(116, 31);
            label8.TabIndex = 23;
            label8.Text = "背景噪声:";
            // 
            // noiseLevel
            // 
            noiseLevel.Location = new Point(465, 133);
            noiseLevel.Margin = new Padding(5, 4, 5, 4);
            noiseLevel.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            noiseLevel.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            noiseLevel.Name = "noiseLevel";
            noiseLevel.Size = new Size(69, 38);
            noiseLevel.TabIndex = 22;
            // 
            // extraWordSpacing
            // 
            extraWordSpacing.DecimalPlaces = 1;
            extraWordSpacing.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            extraWordSpacing.Location = new Point(465, 83);
            extraWordSpacing.Margin = new Padding(5, 4, 5, 4);
            extraWordSpacing.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            extraWordSpacing.Name = "extraWordSpacing";
            extraWordSpacing.Size = new Size(69, 38);
            extraWordSpacing.TabIndex = 20;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(266, 83);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(164, 31);
            label6.TabIndex = 19;
            label6.Text = "词间额外间隔:";
            // 
            // effectiveSpeed
            // 
            effectiveSpeed.Location = new Point(467, 34);
            effectiveSpeed.Margin = new Padding(5, 4, 5, 4);
            effectiveSpeed.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            effectiveSpeed.Name = "effectiveSpeed";
            effectiveSpeed.Size = new Size(69, 38);
            effectiveSpeed.TabIndex = 18;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(266, 35);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(194, 31);
            label5.TabIndex = 17;
            label5.Text = "有效速度(WPM):";
            // 
            // checkAnserSpeed
            // 
            checkAnserSpeed.Enabled = false;
            checkAnserSpeed.Location = new Point(174, 230);
            checkAnserSpeed.Margin = new Padding(5, 4, 5, 4);
            checkAnserSpeed.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            checkAnserSpeed.Name = "checkAnserSpeed";
            checkAnserSpeed.Size = new Size(75, 38);
            checkAnserSpeed.TabIndex = 16;
            checkAnserSpeed.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // checkAnswerChb
            // 
            checkAnswerChb.AutoSize = true;
            checkAnswerChb.Location = new Point(25, 232);
            checkAnswerChb.Margin = new Padding(5, 4, 5, 4);
            checkAnswerChb.Name = "checkAnswerChb";
            checkAnswerChb.Size = new Size(88, 35);
            checkAnswerChb.TabIndex = 15;
            checkAnswerChb.Text = "校报";
            checkAnswerChb.UseVisualStyleBackColor = true;
            checkAnswerChb.CheckedChanged += CheckAnswerChb_CheckedChanged;
            // 
            // showAnswerChb
            // 
            showAnswerChb.AutoSize = true;
            showAnswerChb.Location = new Point(558, 123);
            showAnswerChb.Margin = new Padding(5, 4, 5, 4);
            showAnswerChb.Name = "showAnswerChb";
            showAnswerChb.Size = new Size(136, 35);
            showAnswerChb.TabIndex = 14;
            showAnswerChb.Text = "显示答案";
            showAnswerChb.UseVisualStyleBackColor = true;
            showAnswerChb.CheckedChanged += ShowAnswerChb_CheckedChanged;
            // 
            // EachGroup
            // 
            EachGroup.Location = new Point(176, 181);
            EachGroup.Margin = new Padding(5, 4, 5, 4);
            EachGroup.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            EachGroup.Name = "EachGroup";
            EachGroup.Size = new Size(74, 38);
            EachGroup.TabIndex = 13;
            EachGroup.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 182);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(160, 31);
            label4.TabIndex = 12;
            label4.Text = "数量(个/组)：";
            // 
            // exportBtn
            // 
            exportBtn.Location = new Point(739, 165);
            exportBtn.Margin = new Padding(5, 4, 5, 4);
            exportBtn.Name = "exportBtn";
            exportBtn.Size = new Size(157, 42);
            exportBtn.TabIndex = 10;
            exportBtn.Text = "导出";
            exportBtn.UseVisualStyleBackColor = true;
            exportBtn.Click += ExportBtn_Click;
            // 
            // repeatRbtn
            // 
            repeatRbtn.AutoSize = true;
            repeatRbtn.Location = new Point(558, 42);
            repeatRbtn.Margin = new Padding(5, 4, 5, 4);
            repeatRbtn.Name = "repeatRbtn";
            repeatRbtn.Size = new Size(160, 35);
            repeatRbtn.TabIndex = 9;
            repeatRbtn.Text = "同组无重复";
            repeatRbtn.UseVisualStyleBackColor = true;
            repeatRbtn.CheckedChanged += RepeatRbtn_CheckedChanged;
            // 
            // continuousRbtn
            // 
            continuousRbtn.AutoSize = true;
            continuousRbtn.Location = new Point(558, 82);
            continuousRbtn.Margin = new Padding(5, 4, 5, 4);
            continuousRbtn.Name = "continuousRbtn";
            continuousRbtn.Size = new Size(160, 35);
            continuousRbtn.TabIndex = 8;
            continuousRbtn.Text = "同组无连续";
            continuousRbtn.UseVisualStyleBackColor = true;
            // 
            // submitAnswerBtn
            // 
            submitAnswerBtn.Location = new Point(739, 86);
            submitAnswerBtn.Margin = new Padding(5, 4, 5, 4);
            submitAnswerBtn.Name = "submitAnswerBtn";
            submitAnswerBtn.Size = new Size(157, 42);
            submitAnswerBtn.TabIndex = 7;
            submitAnswerBtn.Text = "提交答案";
            submitAnswerBtn.UseVisualStyleBackColor = true;
            submitAnswerBtn.Click += SubmitAnswerBtn_Click;
            // 
            // startBtn
            // 
            startBtn.Location = new Point(739, 42);
            startBtn.Margin = new Padding(5, 4, 5, 4);
            startBtn.Name = "startBtn";
            startBtn.Size = new Size(157, 42);
            startBtn.TabIndex = 6;
            startBtn.Text = "开始抄收";
            startBtn.UseVisualStyleBackColor = true;
            startBtn.Click += StartBtn_Click;
            // 
            // speetBox
            // 
            speetBox.Location = new Point(174, 32);
            speetBox.Margin = new Padding(5, 4, 5, 4);
            speetBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            speetBox.Name = "speetBox";
            speetBox.Size = new Size(75, 38);
            speetBox.TabIndex = 5;
            speetBox.Value = new decimal(new int[] { 20, 0, 0, 0 });
            speetBox.ValueChanged += SpeetBox_ValueChanged;
            // 
            // toneBox
            // 
            toneBox.Location = new Point(174, 82);
            toneBox.Margin = new Padding(5, 4, 5, 4);
            toneBox.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            toneBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            toneBox.Name = "toneBox";
            toneBox.Size = new Size(75, 38);
            toneBox.TabIndex = 4;
            toneBox.Value = new decimal(new int[] { 600, 0, 0, 0 });
            toneBox.ValueChanged += ToneBox_ValueChanged;
            // 
            // groupNumBox
            // 
            groupNumBox.Location = new Point(174, 131);
            groupNumBox.Margin = new Padding(5, 4, 5, 4);
            groupNumBox.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            groupNumBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            groupNumBox.Name = "groupNumBox";
            groupNumBox.Size = new Size(75, 38);
            groupNumBox.TabIndex = 3;
            groupNumBox.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 133);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(126, 31);
            label3.TabIndex = 2;
            label3.Text = "数量(组)：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 83);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(115, 31);
            label2.TabIndex = 1;
            label2.Text = "频率(Hz):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 34);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(146, 31);
            label1.TabIndex = 0;
            label1.Text = "速度(WPM):";
            // 
            // stopBtn
            // 
            stopBtn.Location = new Point(28, 234);
            stopBtn.Margin = new Padding(5, 4, 5, 4);
            stopBtn.Name = "stopBtn";
            stopBtn.Size = new Size(157, 42);
            stopBtn.TabIndex = 11;
            stopBtn.Text = "结束抄收";
            stopBtn.UseVisualStyleBackColor = true;
            stopBtn.Click += StopBtn_Click;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.Controls.Add(answerBox);
            groupBox4.Font = new Font("Microsoft YaHei UI", 12F);
            groupBox4.Location = new Point(9, 299);
            groupBox4.Margin = new Padding(5, 4, 5, 4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(5, 4, 5, 4);
            groupBox4.Size = new Size(2978, 1166);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "抄收结果";
            // 
            // answerBox
            // 
            answerBox.Dock = DockStyle.Fill;
            answerBox.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            answerBox.Location = new Point(5, 35);
            answerBox.Margin = new Padding(5, 4, 5, 4);
            answerBox.Name = "answerBox";
            answerBox.Size = new Size(2968, 1127);
            answerBox.TabIndex = 0;
            answerBox.Text = " ";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(rePlayBtn);
            groupBox5.Controls.Add(continuePlayBtn);
            groupBox5.Controls.Add(pauseBtn);
            groupBox5.Controls.Add(clearAnswerBtn);
            groupBox5.Controls.Add(stopBtn);
            groupBox5.Font = new Font("Microsoft YaHei UI", 12F);
            groupBox5.Location = new Point(1939, 3);
            groupBox5.Margin = new Padding(5, 4, 5, 4);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(5, 4, 5, 4);
            groupBox5.Size = new Size(217, 288);
            groupBox5.TabIndex = 4;
            groupBox5.TabStop = false;
            groupBox5.Text = "控制";
            // 
            // rePlayBtn
            // 
            rePlayBtn.Enabled = false;
            rePlayBtn.Location = new Point(28, 184);
            rePlayBtn.Margin = new Padding(5, 4, 5, 4);
            rePlayBtn.Name = "rePlayBtn";
            rePlayBtn.Size = new Size(157, 42);
            rePlayBtn.TabIndex = 3;
            rePlayBtn.Text = "重播";
            rePlayBtn.UseVisualStyleBackColor = true;
            rePlayBtn.Click += ResumeBtn_Click;
            // 
            // continuePlayBtn
            // 
            continuePlayBtn.Enabled = false;
            continuePlayBtn.Location = new Point(28, 133);
            continuePlayBtn.Margin = new Padding(5, 4, 5, 4);
            continuePlayBtn.Name = "continuePlayBtn";
            continuePlayBtn.Size = new Size(157, 42);
            continuePlayBtn.TabIndex = 2;
            continuePlayBtn.Text = "继续播放";
            continuePlayBtn.UseVisualStyleBackColor = true;
            continuePlayBtn.Click += ContinuePlayBtn_Click;
            // 
            // pauseBtn
            // 
            pauseBtn.Enabled = false;
            pauseBtn.Location = new Point(28, 82);
            pauseBtn.Margin = new Padding(5, 4, 5, 4);
            pauseBtn.Name = "pauseBtn";
            pauseBtn.Size = new Size(157, 42);
            pauseBtn.TabIndex = 1;
            pauseBtn.Text = "暂停播放";
            pauseBtn.UseVisualStyleBackColor = true;
            pauseBtn.Click += PauseBtn_Click;
            // 
            // clearAnswerBtn
            // 
            clearAnswerBtn.Location = new Point(28, 31);
            clearAnswerBtn.Margin = new Padding(5, 4, 5, 4);
            clearAnswerBtn.Name = "clearAnswerBtn";
            clearAnswerBtn.Size = new Size(157, 42);
            clearAnswerBtn.TabIndex = 0;
            clearAnswerBtn.Text = "清空答案";
            clearAnswerBtn.UseVisualStyleBackColor = true;
            clearAnswerBtn.Click += ClearAnswer_Click;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(radioButton4);
            groupBox6.Controls.Add(radioButton3);
            groupBox6.Font = new Font("Microsoft YaHei UI", 12F);
            groupBox6.Location = new Point(519, 3);
            groupBox6.Margin = new Padding(5, 4, 5, 4);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(5, 4, 5, 4);
            groupBox6.Size = new Size(487, 85);
            groupBox6.TabIndex = 5;
            groupBox6.TabStop = false;
            groupBox6.Text = "报文来源";
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(273, 34);
            radioButton4.Margin = new Padding(5, 4, 5, 4);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(159, 35);
            radioButton4.TabIndex = 1;
            radioButton4.TabStop = true;
            radioButton4.Text = "自定义报文";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += radioButton4_CheckedChanged_1;
            radioButton4.Click += RadioButton4_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(41, 31);
            radioButton3.Margin = new Padding(5, 4, 5, 4);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(135, 35);
            radioButton3.TabIndex = 0;
            radioButton3.TabStop = true;
            radioButton3.Text = "随机生成";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += RadioButton3_CheckedChanged;
            // 
            // NumberCopyingPractice
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(2992, 1470);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            Name = "NumberCopyingPractice";
            Text = "数字短码抄收练习";
            FormClosed += NumberCopyingPractice_FormClosed;
            Load += NumberCopyingPractice_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)noiseLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)extraWordSpacing).EndInit();
            ((System.ComponentModel.ISupportInitialize)effectiveSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)checkAnserSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)EachGroup).EndInit();
            ((System.ComponentModel.ISupportInitialize)speetBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)toneBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupNumBox).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private RadioButton radioButton1;
        private GroupBox groupBox2;
        private RadioButton eqRbtn;
        private RadioButton neRbtn;
        private CheckedListBox eqBox;
        private CheckedListBox neBox;
        private GroupBox groupBox3;
        private Label label3;
        private Label label2;
        private Label label1;
        private GroupBox groupBox4;
        private NumericUpDown speetBox;
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
        private CheckBox showAnswerChb;
        private NumericUpDown checkAnserSpeed;
        private CheckBox checkAnswerChb;
        private System.Windows.Forms.Timer timer1;
        private NumericUpDown effectiveSpeed;
        private Label label5;
        private Label label6;
        private GroupBox groupBox5;
        private Button rePlayBtn;
        private Button continuePlayBtn;
        private Button pauseBtn;
        private Button clearAnswerBtn;
        private RichTextBox answerBox;
        private RadioButton radioButton2;
        private Label label8;
        private NumericUpDown noiseLevel;
        private NumericUpDown extraWordSpacing;
        private GroupBox groupBox6;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private TextBox msgEndTxb;
        private Label label9;
        private TextBox msgStartTxb;
        private Label label10;
        private ComboBox waveList;
        private Label label7;
    }
}