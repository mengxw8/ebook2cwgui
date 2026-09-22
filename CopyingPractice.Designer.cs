namespace CW
{
    partial class CopyingPractice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyingPractice));
            groupBox1 = new GroupBox();
            radioButton7 = new RadioButton();
            individuationRbtn = new RadioButton();
            radioButton8 = new RadioButton();
            radioButton6 = new RadioButton();
            radioButton5 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            groupBox2 = new GroupBox();
            msgEndTxb = new TextBox();
            label9 = new Label();
            msgStartTxb = new TextBox();
            label10 = new Label();
            label7 = new Label();
            KochList = new ComboBox();
            eqBox = new CheckedListBox();
            neBox = new CheckedListBox();
            eqRbtn = new RadioButton();
            neRbtn = new RadioButton();
            groupBox3 = new GroupBox();
            waveList = new ComboBox();
            label5 = new Label();
            label8 = new Label();
            noiseLevel = new NumericUpDown();
            noiseCheckBox = new CheckBox();
            symbolsChb = new CheckBox();
            extraWordSpacing = new NumericUpDown();
            label6 = new Label();
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
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)noiseLevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)extraWordSpacing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)checkAnserSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EachGroup).BeginInit();
            ((System.ComponentModel.ISupportInitialize)speetBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)toneBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupNumBox).BeginInit();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton7);
            groupBox1.Controls.Add(individuationRbtn);
            groupBox1.Controls.Add(radioButton8);
            groupBox1.Controls.Add(radioButton6);
            groupBox1.Controls.Add(radioButton5);
            groupBox1.Controls.Add(radioButton4);
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Location = new Point(9, 3);
            groupBox1.Margin = new Padding(5, 4, 5, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(5, 4, 5, 4);
            groupBox1.Size = new Size(1155, 85);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "练习模式";
            // 
            // radioButton7
            // 
            radioButton7.AutoSize = true;
            radioButton7.Location = new Point(999, 28);
            radioButton7.Margin = new Padding(5, 4, 5, 4);
            radioButton7.Name = "radioButton7";
            radioButton7.Size = new Size(131, 28);
            radioButton7.TabIndex = 8;
            radioButton7.TabStop = true;
            radioButton7.Text = "Koch训练法";
            radioButton7.UseVisualStyleBackColor = true;
            radioButton7.CheckedChanged += RadioButton7_CheckedChanged;
            // 
            // individuationRbtn
            // 
            individuationRbtn.AutoSize = true;
            individuationRbtn.Location = new Point(893, 28);
            individuationRbtn.Margin = new Padding(5, 4, 5, 4);
            individuationRbtn.Name = "individuationRbtn";
            individuationRbtn.Size = new Size(89, 28);
            individuationRbtn.TabIndex = 7;
            individuationRbtn.TabStop = true;
            individuationRbtn.Text = "自定义";
            individuationRbtn.UseVisualStyleBackColor = true;
            individuationRbtn.CheckedChanged += IndividuationRbtn_CheckedChanged;
            // 
            // radioButton8
            // 
            radioButton8.AutoSize = true;
            radioButton8.Location = new Point(767, 28);
            radioButton8.Margin = new Padding(5, 4, 5, 4);
            radioButton8.Name = "radioButton8";
            radioButton8.Size = new Size(107, 28);
            radioButton8.TabIndex = 6;
            radioButton8.TabStop = true;
            radioButton8.Text = "随机单词";
            radioButton8.UseVisualStyleBackColor = true;
            radioButton8.CheckedChanged += RadioButton8_CheckedChanged;
            // 
            // radioButton6
            // 
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(679, 28);
            radioButton6.Margin = new Padding(5, 4, 5, 4);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(71, 28);
            radioButton6.TabIndex = 5;
            radioButton6.TabStop = true;
            radioButton6.Text = "新闻";
            radioButton6.UseVisualStyleBackColor = true;
            radioButton6.CheckedChanged += RadioButton6_CheckedChanged;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(456, 28);
            radioButton5.Margin = new Padding(5, 4, 5, 4);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(71, 28);
            radioButton5.TabIndex = 4;
            radioButton5.TabStop = true;
            radioButton5.Text = "符号";
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.CheckedChanged += RadioButton5_CheckedChanged;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(548, 28);
            radioButton4.Margin = new Padding(5, 4, 5, 4);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(107, 28);
            radioButton4.TabIndex = 3;
            radioButton4.TabStop = true;
            radioButton4.Text = "英语文章";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += RadioButton4_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(273, 28);
            radioButton3.Margin = new Padding(5, 4, 5, 4);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(156, 28);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Text = "数字+字母分组";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += RadioButton3_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(143, 28);
            radioButton2.Margin = new Padding(5, 4, 5, 4);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(107, 28);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "分组字母";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += RadioButton2_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(13, 28);
            radioButton1.Margin = new Padding(5, 4, 5, 4);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(107, 28);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "分组数字";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += RadioButton1_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(msgEndTxb);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(msgStartTxb);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(KochList);
            groupBox2.Controls.Add(eqBox);
            groupBox2.Controls.Add(neBox);
            groupBox2.Controls.Add(eqRbtn);
            groupBox2.Controls.Add(neRbtn);
            groupBox2.Location = new Point(9, 96);
            groupBox2.Margin = new Padding(5, 4, 5, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(5, 4, 5, 4);
            groupBox2.Size = new Size(1155, 141);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "个性化定制";
            // 
            // msgEndTxb
            // 
            msgEndTxb.Location = new Point(999, 99);
            msgEndTxb.Margin = new Padding(5, 4, 5, 4);
            msgEndTxb.Name = "msgEndTxb";
            msgEndTxb.Size = new Size(136, 30);
            msgEndTxb.TabIndex = 14;
            msgEndTxb.Text = "iii";
            msgEndTxb.TextChanged += msgEndTxb_TextChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(935, 103);
            label9.Margin = new Padding(5, 0, 5, 0);
            label9.Name = "label9";
            label9.Size = new Size(64, 24);
            label9.TabIndex = 13;
            label9.Text = "报尾：";
            // 
            // msgStartTxb
            // 
            msgStartTxb.Location = new Point(999, 62);
            msgStartTxb.Margin = new Padding(5, 4, 5, 4);
            msgStartTxb.Name = "msgStartTxb";
            msgStartTxb.Size = new Size(136, 30);
            msgStartTxb.TabIndex = 12;
            msgStartTxb.Text = "msg=";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(935, 68);
            label10.Margin = new Padding(5, 0, 5, 0);
            label10.Name = "label10";
            label10.Size = new Size(64, 24);
            label10.TabIndex = 11;
            label10.Text = "报头：";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(896, 30);
            label7.Margin = new Padding(5, 0, 5, 0);
            label7.Name = "label7";
            label7.Size = new Size(92, 24);
            label7.TabIndex = 5;
            label7.Text = "Koch课时:";
            // 
            // KochList
            // 
            KochList.DropDownStyle = ComboBoxStyle.DropDownList;
            KochList.Enabled = false;
            KochList.FormattingEnabled = true;
            KochList.Items.AddRange(new object[] { "第1课", "第2课", "第3课", "第4课", "第5课", "第6课", "第7课", "第8课", "第9课", "第10课", "第11课", "第12课", "第13课", "第14课", "第15课", "第16课", "第17课", "第18课", "第19课", "第20课", "第21课", "第22课", "第23课", "第24课", "第25课", "第26课", "第27课", "第28课", "第29课", "第30课", "第31课", "第32课", "第33课", "第34课", "第35课", "第36课", "第37课", "第38课", "第39课", "", "" });
            KochList.Location = new Point(999, 24);
            KochList.Margin = new Padding(5, 4, 5, 4);
            KochList.Name = "KochList";
            KochList.Size = new Size(136, 32);
            KochList.TabIndex = 4;
            KochList.SelectedIndexChanged += KochList_SelectedIndexChanged;
            // 
            // eqBox
            // 
            eqBox.Enabled = false;
            eqBox.FormattingEnabled = true;
            eqBox.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" });
            eqBox.Location = new Point(544, 25);
            eqBox.Margin = new Padding(5, 4, 5, 4);
            eqBox.Name = "eqBox";
            eqBox.Size = new Size(334, 85);
            eqBox.TabIndex = 3;
            // 
            // neBox
            // 
            neBox.Enabled = false;
            neBox.Font = new Font("Microsoft YaHei UI", 9F);
            neBox.FormattingEnabled = true;
            neBox.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" });
            neBox.Location = new Point(104, 28);
            neBox.Margin = new Padding(5, 4, 5, 4);
            neBox.Name = "neBox";
            neBox.Size = new Size(334, 85);
            neBox.TabIndex = 2;
            // 
            // eqRbtn
            // 
            eqRbtn.AutoSize = true;
            eqRbtn.Location = new Point(449, 41);
            eqRbtn.Margin = new Padding(5, 4, 5, 4);
            eqRbtn.Name = "eqRbtn";
            eqRbtn.Size = new Size(89, 28);
            eqRbtn.TabIndex = 1;
            eqRbtn.TabStop = true;
            eqRbtn.Text = "仅包含";
            eqRbtn.UseVisualStyleBackColor = true;
            eqRbtn.CheckedChanged += EqRbtn_CheckedChanged;
            // 
            // neRbtn
            // 
            neRbtn.AutoSize = true;
            neRbtn.Location = new Point(16, 41);
            neRbtn.Margin = new Padding(5, 4, 5, 4);
            neRbtn.Name = "neRbtn";
            neRbtn.Size = new Size(71, 28);
            neRbtn.TabIndex = 0;
            neRbtn.TabStop = true;
            neRbtn.Text = "排除";
            neRbtn.UseVisualStyleBackColor = true;
            neRbtn.CheckedChanged += NeRbtn_CheckedChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(waveList);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(noiseLevel);
            groupBox3.Controls.Add(noiseCheckBox);
            groupBox3.Controls.Add(symbolsChb);
            groupBox3.Controls.Add(extraWordSpacing);
            groupBox3.Controls.Add(label6);
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
            groupBox3.Location = new Point(1174, 3);
            groupBox3.Margin = new Padding(5, 4, 5, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(5, 4, 5, 4);
            groupBox3.Size = new Size(757, 234);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "CW配置";
            // 
            // waveList
            // 
            waveList.DropDownStyle = ComboBoxStyle.DropDownList;
            waveList.FormattingEnabled = true;
            waveList.Items.AddRange(new object[] { "正弦波", "锯齿波", "方波" });
            waveList.Location = new Point(362, 123);
            waveList.Margin = new Padding(5, 4, 5, 4);
            waveList.Name = "waveList";
            waveList.Size = new Size(100, 32);
            waveList.TabIndex = 25;
            waveList.SelectedIndexChanged += waveList_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(273, 128);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(50, 24);
            label5.TabIndex = 24;
            label5.Text = "波形:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(262, 82);
            label8.Margin = new Padding(5, 0, 5, 0);
            label8.Name = "label8";
            label8.Size = new Size(103, 24);
            label8.TabIndex = 23;
            label8.Text = "信噪比(dB):";
            // 
            // noiseLevel
            // 
            noiseLevel.Enabled = false;
            noiseLevel.Location = new Point(398, 80);
            noiseLevel.Margin = new Padding(5, 4, 5, 4);
            noiseLevel.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            noiseLevel.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            noiseLevel.Name = "noiseLevel";
            noiseLevel.Size = new Size(63, 30);
            noiseLevel.TabIndex = 22;
            noiseLevel.ValueChanged += NoiseSettingsChanged;
            // 
            // noiseCheckBox
            // 
            noiseCheckBox.AutoSize = true;
            noiseCheckBox.Location = new Point(478, 197);
            noiseCheckBox.Margin = new Padding(5, 4, 5, 4);
            noiseCheckBox.Name = "noiseCheckBox";
            noiseCheckBox.Size = new Size(108, 28);
            noiseCheckBox.TabIndex = 26;
            noiseCheckBox.Text = "启用噪声";
            noiseCheckBox.UseVisualStyleBackColor = true;
            noiseCheckBox.CheckedChanged += NoiseSettingsChanged;
            // 
            // symbolsChb
            // 
            symbolsChb.AutoSize = true;
            symbolsChb.Location = new Point(478, 158);
            symbolsChb.Margin = new Padding(5, 4, 5, 4);
            symbolsChb.Name = "symbolsChb";
            symbolsChb.Size = new Size(126, 28);
            symbolsChb.TabIndex = 21;
            symbolsChb.Text = "文章含符号";
            symbolsChb.UseVisualStyleBackColor = true;
            // 
            // extraWordSpacing
            // 
            extraWordSpacing.DecimalPlaces = 1;
            extraWordSpacing.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            extraWordSpacing.Location = new Point(398, 37);
            extraWordSpacing.Margin = new Padding(5, 4, 5, 4);
            extraWordSpacing.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            extraWordSpacing.Name = "extraWordSpacing";
            extraWordSpacing.Size = new Size(63, 30);
            extraWordSpacing.TabIndex = 20;
            extraWordSpacing.ValueChanged += extraWordSpacing_ValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(240, 38);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(122, 24);
            label6.TabIndex = 19;
            label6.Text = "词间额外间隔:";
            // 
            // checkAnserSpeed
            // 
            checkAnserSpeed.Enabled = false;
            checkAnserSpeed.Location = new Point(145, 196);
            checkAnserSpeed.Margin = new Padding(5, 4, 5, 4);
            checkAnserSpeed.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            checkAnserSpeed.Name = "checkAnserSpeed";
            checkAnserSpeed.Size = new Size(75, 30);
            checkAnserSpeed.TabIndex = 16;
            checkAnserSpeed.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // checkAnswerChb
            // 
            checkAnswerChb.AutoSize = true;
            checkAnswerChb.Location = new Point(25, 196);
            checkAnswerChb.Margin = new Padding(5, 4, 5, 4);
            checkAnswerChb.Name = "checkAnswerChb";
            checkAnswerChb.Size = new Size(72, 28);
            checkAnswerChb.TabIndex = 15;
            checkAnswerChb.Text = "校报";
            checkAnswerChb.UseVisualStyleBackColor = true;
            checkAnswerChb.CheckedChanged += CheckAnswerChb_CheckedChanged;
            // 
            // showAnswerChb
            // 
            showAnswerChb.AutoSize = true;
            showAnswerChb.Location = new Point(478, 117);
            showAnswerChb.Margin = new Padding(5, 4, 5, 4);
            showAnswerChb.Name = "showAnswerChb";
            showAnswerChb.Size = new Size(108, 28);
            showAnswerChb.TabIndex = 14;
            showAnswerChb.Text = "显示答案";
            showAnswerChb.UseVisualStyleBackColor = true;
            showAnswerChb.CheckedChanged += ShowAnswerChb_CheckedChanged;
            // 
            // EachGroup
            // 
            EachGroup.Location = new Point(146, 158);
            EachGroup.Margin = new Padding(5, 4, 5, 4);
            EachGroup.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            EachGroup.Name = "EachGroup";
            EachGroup.Size = new Size(74, 30);
            EachGroup.TabIndex = 13;
            EachGroup.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 161);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(120, 24);
            label4.TabIndex = 12;
            label4.Text = "数量(个/组)：";
            // 
            // exportBtn
            // 
            exportBtn.Location = new Point(625, 150);
            exportBtn.Margin = new Padding(5, 4, 5, 4);
            exportBtn.Name = "exportBtn";
            exportBtn.Size = new Size(118, 32);
            exportBtn.TabIndex = 10;
            exportBtn.Text = "导出";
            exportBtn.UseVisualStyleBackColor = true;
            exportBtn.Click += ExportBtn_Click;
            // 
            // repeatRbtn
            // 
            repeatRbtn.AutoSize = true;
            repeatRbtn.Location = new Point(478, 37);
            repeatRbtn.Margin = new Padding(5, 4, 5, 4);
            repeatRbtn.Name = "repeatRbtn";
            repeatRbtn.Size = new Size(126, 28);
            repeatRbtn.TabIndex = 9;
            repeatRbtn.Text = "同组无重复";
            repeatRbtn.UseVisualStyleBackColor = true;
            // 
            // continuousRbtn
            // 
            continuousRbtn.AutoSize = true;
            continuousRbtn.Location = new Point(478, 76);
            continuousRbtn.Margin = new Padding(5, 4, 5, 4);
            continuousRbtn.Name = "continuousRbtn";
            continuousRbtn.Size = new Size(126, 28);
            continuousRbtn.TabIndex = 8;
            continuousRbtn.Text = "同组无连续";
            continuousRbtn.UseVisualStyleBackColor = true;
            // 
            // submitAnswerBtn
            // 
            submitAnswerBtn.Location = new Point(625, 79);
            submitAnswerBtn.Margin = new Padding(5, 4, 5, 4);
            submitAnswerBtn.Name = "submitAnswerBtn";
            submitAnswerBtn.Size = new Size(118, 32);
            submitAnswerBtn.TabIndex = 7;
            submitAnswerBtn.Text = "提交答案";
            submitAnswerBtn.UseVisualStyleBackColor = true;
            submitAnswerBtn.Click += SubmitAnswerBtn_Click;
            // 
            // startBtn
            // 
            startBtn.Location = new Point(625, 37);
            startBtn.Margin = new Padding(5, 4, 5, 4);
            startBtn.Name = "startBtn";
            startBtn.Size = new Size(118, 32);
            startBtn.TabIndex = 6;
            startBtn.Text = "开始抄收";
            startBtn.UseVisualStyleBackColor = true;
            startBtn.Click += StartBtn_Click;
            // 
            // speetBox
            // 
            speetBox.Location = new Point(145, 32);
            speetBox.Margin = new Padding(5, 4, 5, 4);
            speetBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            speetBox.Name = "speetBox";
            speetBox.Size = new Size(75, 30);
            speetBox.TabIndex = 5;
            speetBox.Value = new decimal(new int[] { 20, 0, 0, 0 });
            speetBox.ValueChanged += SpeetBox_ValueChanged;
            // 
            // toneBox
            // 
            toneBox.Location = new Point(145, 76);
            toneBox.Margin = new Padding(5, 4, 5, 4);
            toneBox.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            toneBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            toneBox.Name = "toneBox";
            toneBox.Size = new Size(75, 30);
            toneBox.TabIndex = 4;
            toneBox.Value = new decimal(new int[] { 600, 0, 0, 0 });
            toneBox.ValueChanged += toneBox_ValueChanged;
            // 
            // groupNumBox
            // 
            groupNumBox.Location = new Point(145, 116);
            groupNumBox.Margin = new Padding(5, 4, 5, 4);
            groupNumBox.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            groupNumBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            groupNumBox.Name = "groupNumBox";
            groupNumBox.Size = new Size(75, 30);
            groupNumBox.TabIndex = 3;
            groupNumBox.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 119);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(94, 24);
            label3.TabIndex = 2;
            label3.Text = "数量(组)：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 76);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(85, 24);
            label2.TabIndex = 1;
            label2.Text = "频率(Hz):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 34);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(109, 24);
            label1.TabIndex = 0;
            label1.Text = "速度(WPM):";
            // 
            // stopBtn
            // 
            stopBtn.Location = new Point(28, 189);
            stopBtn.Margin = new Padding(5, 4, 5, 4);
            stopBtn.Name = "stopBtn";
            stopBtn.Size = new Size(118, 32);
            stopBtn.TabIndex = 11;
            stopBtn.Text = "结束抄收";
            stopBtn.UseVisualStyleBackColor = true;
            stopBtn.Click += StopBtn_Click;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.Controls.Add(answerBox);
            groupBox4.Location = new Point(9, 246);
            groupBox4.Margin = new Padding(5, 4, 5, 4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(5, 4, 5, 4);
            groupBox4.Size = new Size(2978, 1220);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "抄收结果";
            // 
            // answerBox
            // 
            answerBox.Dock = DockStyle.Fill;
            answerBox.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            answerBox.Location = new Point(5, 27);
            answerBox.Margin = new Padding(5, 4, 5, 4);
            answerBox.Name = "answerBox";
            answerBox.Size = new Size(2968, 1189);
            answerBox.TabIndex = 0;
            answerBox.Text = " ";
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
            groupBox5.Location = new Point(1939, 3);
            groupBox5.Margin = new Padding(5, 4, 5, 4);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(5, 4, 5, 4);
            groupBox5.Size = new Size(171, 234);
            groupBox5.TabIndex = 4;
            groupBox5.TabStop = false;
            groupBox5.Text = "控制";
            // 
            // rePlayBtn
            // 
            rePlayBtn.Enabled = false;
            rePlayBtn.Location = new Point(28, 150);
            rePlayBtn.Margin = new Padding(5, 4, 5, 4);
            rePlayBtn.Name = "rePlayBtn";
            rePlayBtn.Size = new Size(118, 32);
            rePlayBtn.TabIndex = 3;
            rePlayBtn.Text = "重播";
            rePlayBtn.UseVisualStyleBackColor = true;
            rePlayBtn.Click += ResumeBtn_Click;
            // 
            // continuePlayBtn
            // 
            continuePlayBtn.Enabled = false;
            continuePlayBtn.Location = new Point(28, 110);
            continuePlayBtn.Margin = new Padding(5, 4, 5, 4);
            continuePlayBtn.Name = "continuePlayBtn";
            continuePlayBtn.Size = new Size(118, 32);
            continuePlayBtn.TabIndex = 2;
            continuePlayBtn.Text = "继续播放";
            continuePlayBtn.UseVisualStyleBackColor = true;
            continuePlayBtn.Click += ContinuePlayBtn_Click;
            // 
            // pauseBtn
            // 
            pauseBtn.Enabled = false;
            pauseBtn.Location = new Point(28, 71);
            pauseBtn.Margin = new Padding(5, 4, 5, 4);
            pauseBtn.Name = "pauseBtn";
            pauseBtn.Size = new Size(118, 32);
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
            clearAnswerBtn.Size = new Size(118, 32);
            clearAnswerBtn.TabIndex = 0;
            clearAnswerBtn.Text = "清空答案";
            clearAnswerBtn.UseVisualStyleBackColor = true;
            clearAnswerBtn.Click += ClearAnswer_Click;
            // 
            // CopyingPractice
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2992, 1470);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimumSize = new Size(720, 420);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            Name = "CopyingPractice";
            Text = "抄收练习";
            FormClosed += CopyingPractice_FormClosed;
            Load += CopyingPractice_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)noiseLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)extraWordSpacing).EndInit();
            ((System.ComponentModel.ISupportInitialize)checkAnserSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)EachGroup).EndInit();
            ((System.ComponentModel.ISupportInitialize)speetBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)toneBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupNumBox).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
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
        private NumericUpDown extraWordSpacing;
        private Label label6;
        private RadioButton radioButton6;
        private CheckBox symbolsChb;
        private RadioButton radioButton8;
        private GroupBox groupBox5;
        private Button rePlayBtn;
        private Button continuePlayBtn;
        private Button pauseBtn;
        private Button clearAnswerBtn;
        private RadioButton individuationRbtn;
        private RadioButton radioButton7;
        private Label label7;
        private ComboBox KochList;
        private RichTextBox answerBox;
        private Label label8;
        private NumericUpDown noiseLevel;
        private CheckBox noiseCheckBox;
        private TextBox msgEndTxb;
        private Label label9;
        private TextBox msgStartTxb;
        private Label label10;
        private ComboBox waveList;
        private Label label5;
    }
}