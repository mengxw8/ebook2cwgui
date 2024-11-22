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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
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
            label7 = new Label();
            KochList = new ComboBox();
            eqBox = new CheckedListBox();
            neBox = new CheckedListBox();
            eqRbtn = new RadioButton();
            neRbtn = new RadioButton();
            groupBox3 = new GroupBox();
            symbolsChb = new CheckBox();
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
            dataGridView1 = new DataGridView();
            timer1 = new System.Windows.Forms.Timer(components);
            groupBox5 = new GroupBox();
            rePlayBtn = new Button();
            continuePlayBtn = new Button();
            pauseBtn = new Button();
            clearAnswerBtn = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extraWordSpacing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)effectiveSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)checkAnserSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EachGroup).BeginInit();
            ((System.ComponentModel.ISupportInitialize)speetBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)toneBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupNumBox).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
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
            groupBox1.Location = new Point(6, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(735, 60);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "练习模式";
            // 
            // radioButton7
            // 
            radioButton7.AutoSize = true;
            radioButton7.Location = new Point(636, 20);
            radioButton7.Name = "radioButton7";
            radioButton7.Size = new Size(91, 21);
            radioButton7.TabIndex = 8;
            radioButton7.TabStop = true;
            radioButton7.Text = "Koch训练法";
            radioButton7.UseVisualStyleBackColor = true;
            radioButton7.CheckedChanged += radioButton7_CheckedChanged;
            // 
            // individuationRbtn
            // 
            individuationRbtn.AutoSize = true;
            individuationRbtn.Location = new Point(568, 20);
            individuationRbtn.Name = "individuationRbtn";
            individuationRbtn.Size = new Size(62, 21);
            individuationRbtn.TabIndex = 7;
            individuationRbtn.TabStop = true;
            individuationRbtn.Text = "自定义";
            individuationRbtn.UseVisualStyleBackColor = true;
            individuationRbtn.CheckedChanged += individuationRbtn_CheckedChanged;
            // 
            // radioButton8
            // 
            radioButton8.AutoSize = true;
            radioButton8.Location = new Point(488, 20);
            radioButton8.Name = "radioButton8";
            radioButton8.Size = new Size(74, 21);
            radioButton8.TabIndex = 6;
            radioButton8.TabStop = true;
            radioButton8.Text = "随机单词";
            radioButton8.UseVisualStyleBackColor = true;
            radioButton8.CheckedChanged += radioButton8_CheckedChanged;
            // 
            // radioButton6
            // 
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(432, 20);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(50, 21);
            radioButton6.TabIndex = 5;
            radioButton6.TabStop = true;
            radioButton6.Text = "新闻";
            radioButton6.UseVisualStyleBackColor = true;
            radioButton6.CheckedChanged += radioButton6_CheckedChanged;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(290, 20);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(50, 21);
            radioButton5.TabIndex = 4;
            radioButton5.TabStop = true;
            radioButton5.Text = "符号";
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.CheckedChanged += radioButton5_CheckedChanged;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(349, 20);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(74, 21);
            radioButton4.TabIndex = 3;
            radioButton4.TabStop = true;
            radioButton4.Text = "英语文章";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += radioButton4_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(174, 20);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(107, 21);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Text = "数字+字母分组";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(91, 20);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(74, 21);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "分组字母";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
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
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(KochList);
            groupBox2.Controls.Add(eqBox);
            groupBox2.Controls.Add(neBox);
            groupBox2.Controls.Add(eqRbtn);
            groupBox2.Controls.Add(neRbtn);
            groupBox2.Location = new Point(6, 68);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(735, 100);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "个性化定制";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(572, 33);
            label7.Name = "label7";
            label7.Size = new Size(64, 17);
            label7.TabIndex = 5;
            label7.Text = "Koch课时:";
            // 
            // KochList
            // 
            KochList.DropDownStyle = ComboBoxStyle.DropDownList;
            KochList.Enabled = false;
            KochList.FormattingEnabled = true;
            KochList.Items.AddRange(new object[] { "第1课", "第2课", "第3课", "第4课", "第5课", "第6课", "第7课", "第8课", "第9课", "第10课", "第11课", "第12课", "第13课", "第14课", "第15课", "第16课", "第17课", "第18课", "第19课", "第20课", "第21课", "第22课", "第23课", "第24课", "第25课", "第26课", "第27课", "第28课", "第29课", "第30课", "第31课", "第32课", "第33课", "第34课", "第35课", "第36课", "第37课", "第38课", "第39课", "", "" });
            KochList.Location = new Point(638, 29);
            KochList.Name = "KochList";
            KochList.Size = new Size(88, 25);
            KochList.TabIndex = 4;
            KochList.SelectedIndexChanged += KochList_SelectedIndexChanged;
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
            eqRbtn.CheckedChanged += eqRbtn_CheckedChanged;
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
            neRbtn.CheckedChanged += neRbtn_CheckedChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(symbolsChb);
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
            groupBox3.Location = new Point(747, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(482, 166);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "CW配置";
            // 
            // symbolsChb
            // 
            symbolsChb.AutoSize = true;
            symbolsChb.Location = new Point(304, 113);
            symbolsChb.Name = "symbolsChb";
            symbolsChb.Size = new Size(87, 21);
            symbolsChb.TabIndex = 21;
            symbolsChb.Text = "文章含符号";
            symbolsChb.UseVisualStyleBackColor = true;
            // 
            // extraWordSpacing
            // 
            extraWordSpacing.DecimalPlaces = 1;
            extraWordSpacing.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            extraWordSpacing.Location = new Point(253, 55);
            extraWordSpacing.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            extraWordSpacing.Name = "extraWordSpacing";
            extraWordSpacing.Size = new Size(40, 23);
            extraWordSpacing.TabIndex = 20;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(153, 56);
            label6.Name = "label6";
            label6.Size = new Size(83, 17);
            label6.TabIndex = 19;
            label6.Text = "词间额外间隔:";
            // 
            // effectiveSpeed
            // 
            effectiveSpeed.Location = new Point(253, 24);
            effectiveSpeed.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            effectiveSpeed.Name = "effectiveSpeed";
            effectiveSpeed.Size = new Size(40, 23);
            effectiveSpeed.TabIndex = 18;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(153, 25);
            label5.Name = "label5";
            label5.Size = new Size(98, 17);
            label5.TabIndex = 17;
            label5.Text = "有效速度(WPM):";
            // 
            // checkAnserSpeed
            // 
            checkAnserSpeed.Enabled = false;
            checkAnserSpeed.Location = new Point(92, 139);
            checkAnserSpeed.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            checkAnserSpeed.Name = "checkAnserSpeed";
            checkAnserSpeed.Size = new Size(48, 23);
            checkAnserSpeed.TabIndex = 16;
            checkAnserSpeed.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // checkAnswerChb
            // 
            checkAnswerChb.AutoSize = true;
            checkAnswerChb.Location = new Point(16, 139);
            checkAnswerChb.Name = "checkAnswerChb";
            checkAnswerChb.Size = new Size(51, 21);
            checkAnswerChb.TabIndex = 15;
            checkAnswerChb.Text = "校报";
            checkAnswerChb.UseVisualStyleBackColor = true;
            checkAnswerChb.CheckedChanged += checkAnswerChb_CheckedChanged;
            // 
            // showAnswerChb
            // 
            showAnswerChb.AutoSize = true;
            showAnswerChb.Location = new Point(304, 83);
            showAnswerChb.Name = "showAnswerChb";
            showAnswerChb.Size = new Size(75, 21);
            showAnswerChb.TabIndex = 14;
            showAnswerChb.Text = "显示答案";
            showAnswerChb.UseVisualStyleBackColor = true;
            showAnswerChb.CheckedChanged += showAnswerChb_CheckedChanged;
            // 
            // EachGroup
            // 
            EachGroup.Location = new Point(93, 112);
            EachGroup.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            EachGroup.Name = "EachGroup";
            EachGroup.Size = new Size(47, 23);
            EachGroup.TabIndex = 13;
            EachGroup.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 114);
            label4.Name = "label4";
            label4.Size = new Size(81, 17);
            label4.TabIndex = 12;
            label4.Text = "数量(个/组)：";
            // 
            // exportBtn
            // 
            exportBtn.Location = new Point(398, 106);
            exportBtn.Name = "exportBtn";
            exportBtn.Size = new Size(75, 23);
            exportBtn.TabIndex = 10;
            exportBtn.Text = "导出";
            exportBtn.UseVisualStyleBackColor = true;
            exportBtn.Click += exportBtn_Click;
            // 
            // repeatRbtn
            // 
            repeatRbtn.AutoSize = true;
            repeatRbtn.Location = new Point(304, 26);
            repeatRbtn.Name = "repeatRbtn";
            repeatRbtn.Size = new Size(87, 21);
            repeatRbtn.TabIndex = 9;
            repeatRbtn.Text = "同组无重复";
            repeatRbtn.UseVisualStyleBackColor = true;
            // 
            // continuousRbtn
            // 
            continuousRbtn.AutoSize = true;
            continuousRbtn.Location = new Point(304, 54);
            continuousRbtn.Name = "continuousRbtn";
            continuousRbtn.Size = new Size(87, 21);
            continuousRbtn.TabIndex = 8;
            continuousRbtn.Text = "同组无连续";
            continuousRbtn.UseVisualStyleBackColor = true;
            // 
            // submitAnswerBtn
            // 
            submitAnswerBtn.Location = new Point(398, 56);
            submitAnswerBtn.Name = "submitAnswerBtn";
            submitAnswerBtn.Size = new Size(75, 23);
            submitAnswerBtn.TabIndex = 7;
            submitAnswerBtn.Text = "提交答案";
            submitAnswerBtn.UseVisualStyleBackColor = true;
            submitAnswerBtn.Click += submitAnswerBtn_Click;
            // 
            // startBtn
            // 
            startBtn.Location = new Point(398, 26);
            startBtn.Name = "startBtn";
            startBtn.Size = new Size(75, 23);
            startBtn.TabIndex = 6;
            startBtn.Text = "开始抄收";
            startBtn.UseVisualStyleBackColor = true;
            startBtn.Click += startBtn_Click;
            // 
            // speetBox
            // 
            speetBox.Location = new Point(92, 23);
            speetBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            speetBox.Name = "speetBox";
            speetBox.Size = new Size(48, 23);
            speetBox.TabIndex = 5;
            speetBox.Value = new decimal(new int[] { 20, 0, 0, 0 });
            speetBox.ValueChanged += speetBox_ValueChanged;
            // 
            // toneBox
            // 
            toneBox.Location = new Point(92, 54);
            toneBox.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            toneBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            toneBox.Name = "toneBox";
            toneBox.Size = new Size(48, 23);
            toneBox.TabIndex = 4;
            toneBox.Value = new decimal(new int[] { 600, 0, 0, 0 });
            // 
            // groupNumBox
            // 
            groupNumBox.Location = new Point(92, 82);
            groupNumBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            groupNumBox.Name = "groupNumBox";
            groupNumBox.Size = new Size(48, 23);
            groupNumBox.TabIndex = 3;
            groupNumBox.Value = new decimal(new int[] { 100, 0, 0, 0 });
            groupNumBox.Leave += groupNumBox_Leave;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 84);
            label3.Name = "label3";
            label3.Size = new Size(64, 17);
            label3.TabIndex = 2;
            label3.Text = "数量(组)：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 54);
            label2.Name = "label2";
            label2.Size = new Size(58, 17);
            label2.TabIndex = 1;
            label2.Text = "频率(Hz):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 24);
            label1.Name = "label1";
            label1.Size = new Size(74, 17);
            label1.TabIndex = 0;
            label1.Text = "速度(WPM):";
            // 
            // stopBtn
            // 
            stopBtn.Location = new Point(18, 134);
            stopBtn.Name = "stopBtn";
            stopBtn.Size = new Size(75, 23);
            stopBtn.TabIndex = 11;
            stopBtn.Text = "结束抄收";
            stopBtn.UseVisualStyleBackColor = true;
            stopBtn.Click += stopBtn_Click;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.Controls.Add(dataGridView1);
            groupBox4.Location = new Point(6, 174);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(1895, 864);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "抄收结果";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 19);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.Size = new Size(1889, 842);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellEnter += dataGridView1_CellEnter;
            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
            // 
            // timer1
            // 
            timer1.Interval = 5000;
            timer1.Tick += timer1_Tick;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(rePlayBtn);
            groupBox5.Controls.Add(continuePlayBtn);
            groupBox5.Controls.Add(pauseBtn);
            groupBox5.Controls.Add(clearAnswerBtn);
            groupBox5.Controls.Add(stopBtn);
            groupBox5.Location = new Point(1234, 2);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(109, 166);
            groupBox5.TabIndex = 4;
            groupBox5.TabStop = false;
            groupBox5.Text = "控制";
            // 
            // rePlayBtn
            // 
            rePlayBtn.Enabled = false;
            rePlayBtn.Location = new Point(18, 106);
            rePlayBtn.Name = "rePlayBtn";
            rePlayBtn.Size = new Size(75, 23);
            rePlayBtn.TabIndex = 3;
            rePlayBtn.Text = "重播";
            rePlayBtn.UseVisualStyleBackColor = true;
            rePlayBtn.Click += resumeBtn_Click;
            // 
            // continuePlayBtn
            // 
            continuePlayBtn.Enabled = false;
            continuePlayBtn.Location = new Point(18, 78);
            continuePlayBtn.Name = "continuePlayBtn";
            continuePlayBtn.Size = new Size(75, 23);
            continuePlayBtn.TabIndex = 2;
            continuePlayBtn.Text = "继续播放";
            continuePlayBtn.UseVisualStyleBackColor = true;
            continuePlayBtn.Click += continuePlayBtn_Click;
            // 
            // pauseBtn
            // 
            pauseBtn.Enabled = false;
            pauseBtn.Location = new Point(18, 50);
            pauseBtn.Name = "pauseBtn";
            pauseBtn.Size = new Size(75, 23);
            pauseBtn.TabIndex = 1;
            pauseBtn.Text = "暂停播放";
            pauseBtn.UseVisualStyleBackColor = true;
            pauseBtn.Click += pauseBtn_Click;
            // 
            // clearAnswerBtn
            // 
            clearAnswerBtn.Location = new Point(18, 22);
            clearAnswerBtn.Name = "clearAnswerBtn";
            clearAnswerBtn.Size = new Size(75, 23);
            clearAnswerBtn.TabIndex = 0;
            clearAnswerBtn.Text = "清空答案";
            clearAnswerBtn.UseVisualStyleBackColor = true;
            clearAnswerBtn.Click += clearAnswer_Click;
            // 
            // CopyingPractice
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
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
            ((System.ComponentModel.ISupportInitialize)extraWordSpacing).EndInit();
            ((System.ComponentModel.ISupportInitialize)effectiveSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)checkAnserSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)EachGroup).EndInit();
            ((System.ComponentModel.ISupportInitialize)speetBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)toneBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupNumBox).EndInit();
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
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
        private DataGridView dataGridView1;
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
    }
}