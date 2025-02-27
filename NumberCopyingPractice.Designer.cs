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
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Font = new Font("Microsoft YaHei UI", 12F);
            groupBox1.Location = new Point(6, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(634, 60);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "练习模式";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new Font("Microsoft YaHei UI", 12F);
            radioButton2.Location = new Point(176, 23);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(136, 25);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "分组数字(短10)";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Font = new Font("Microsoft YaHei UI", 12F);
            radioButton1.Location = new Point(8, 20);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(127, 25);
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
            groupBox2.Location = new Point(6, 68);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(634, 138);
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
            eqBox.Location = new Point(368, 18);
            eqBox.Name = "eqBox";
            eqBox.Size = new Size(214, 96);
            eqBox.TabIndex = 3;
            // 
            // neBox
            // 
            neBox.Enabled = false;
            neBox.Font = new Font("Microsoft YaHei UI", 12F);
            neBox.FormattingEnabled = true;
            neBox.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" });
            neBox.Location = new Point(75, 20);
            neBox.Name = "neBox";
            neBox.Size = new Size(214, 96);
            neBox.TabIndex = 2;
            // 
            // eqRbtn
            // 
            eqRbtn.AutoSize = true;
            eqRbtn.Font = new Font("Microsoft YaHei UI", 12F);
            eqRbtn.Location = new Point(295, 29);
            eqRbtn.Name = "eqRbtn";
            eqRbtn.Size = new Size(76, 25);
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
            neRbtn.Location = new Point(19, 29);
            neRbtn.Name = "neRbtn";
            neRbtn.Size = new Size(60, 25);
            neRbtn.TabIndex = 0;
            neRbtn.TabStop = true;
            neRbtn.Text = "排除";
            neRbtn.UseVisualStyleBackColor = true;
            neRbtn.CheckedChanged += NeRbtn_CheckedChanged;
            // 
            // groupBox3
            // 
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
            groupBox3.Location = new Point(652, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(576, 204);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "CW配置";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(183, 93);
            label8.Name = "label8";
            label8.Size = new Size(78, 21);
            label8.TabIndex = 23;
            label8.Text = "背景噪声:";
            // 
            // noiseLevel
            // 
            noiseLevel.Location = new Point(296, 94);
            noiseLevel.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            noiseLevel.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            noiseLevel.Name = "noiseLevel";
            noiseLevel.Size = new Size(44, 28);
            noiseLevel.TabIndex = 22;
            // 
            // extraWordSpacing
            // 
            extraWordSpacing.DecimalPlaces = 1;
            extraWordSpacing.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            extraWordSpacing.Location = new Point(296, 59);
            extraWordSpacing.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            extraWordSpacing.Name = "extraWordSpacing";
            extraWordSpacing.Size = new Size(44, 28);
            extraWordSpacing.TabIndex = 20;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(169, 59);
            label6.Name = "label6";
            label6.Size = new Size(110, 21);
            label6.TabIndex = 19;
            label6.Text = "词间额外间隔:";
            // 
            // effectiveSpeed
            // 
            effectiveSpeed.Location = new Point(297, 24);
            effectiveSpeed.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            effectiveSpeed.Name = "effectiveSpeed";
            effectiveSpeed.Size = new Size(44, 28);
            effectiveSpeed.TabIndex = 18;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(169, 25);
            label5.Name = "label5";
            label5.Size = new Size(130, 21);
            label5.TabIndex = 17;
            label5.Text = "有效速度(WPM):";
            // 
            // checkAnserSpeed
            // 
            checkAnserSpeed.Enabled = false;
            checkAnserSpeed.Location = new Point(111, 163);
            checkAnserSpeed.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            checkAnserSpeed.Name = "checkAnserSpeed";
            checkAnserSpeed.Size = new Size(48, 28);
            checkAnserSpeed.TabIndex = 16;
            checkAnserSpeed.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // checkAnswerChb
            // 
            checkAnswerChb.AutoSize = true;
            checkAnswerChb.Location = new Point(16, 164);
            checkAnswerChb.Name = "checkAnswerChb";
            checkAnswerChb.Size = new Size(61, 25);
            checkAnswerChb.TabIndex = 15;
            checkAnswerChb.Text = "校报";
            checkAnswerChb.UseVisualStyleBackColor = true;
            checkAnswerChb.CheckedChanged += CheckAnswerChb_CheckedChanged;
            // 
            // showAnswerChb
            // 
            showAnswerChb.AutoSize = true;
            showAnswerChb.Location = new Point(355, 87);
            showAnswerChb.Name = "showAnswerChb";
            showAnswerChb.Size = new Size(93, 25);
            showAnswerChb.TabIndex = 14;
            showAnswerChb.Text = "显示答案";
            showAnswerChb.UseVisualStyleBackColor = true;
            showAnswerChb.CheckedChanged += ShowAnswerChb_CheckedChanged;
            // 
            // EachGroup
            // 
            EachGroup.Location = new Point(112, 128);
            EachGroup.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            EachGroup.Name = "EachGroup";
            EachGroup.Size = new Size(47, 28);
            EachGroup.TabIndex = 13;
            EachGroup.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 129);
            label4.Name = "label4";
            label4.Size = new Size(107, 21);
            label4.TabIndex = 12;
            label4.Text = "数量(个/组)：";
            // 
            // exportBtn
            // 
            exportBtn.Location = new Point(470, 117);
            exportBtn.Name = "exportBtn";
            exportBtn.Size = new Size(100, 30);
            exportBtn.TabIndex = 10;
            exportBtn.Text = "导出";
            exportBtn.UseVisualStyleBackColor = true;
            exportBtn.Click += ExportBtn_Click;
            // 
            // repeatRbtn
            // 
            repeatRbtn.AutoSize = true;
            repeatRbtn.Location = new Point(355, 30);
            repeatRbtn.Name = "repeatRbtn";
            repeatRbtn.Size = new Size(109, 25);
            repeatRbtn.TabIndex = 9;
            repeatRbtn.Text = "同组无重复";
            repeatRbtn.UseVisualStyleBackColor = true;
            repeatRbtn.CheckedChanged += repeatRbtn_CheckedChanged;
            // 
            // continuousRbtn
            // 
            continuousRbtn.AutoSize = true;
            continuousRbtn.Location = new Point(355, 58);
            continuousRbtn.Name = "continuousRbtn";
            continuousRbtn.Size = new Size(109, 25);
            continuousRbtn.TabIndex = 8;
            continuousRbtn.Text = "同组无连续";
            continuousRbtn.UseVisualStyleBackColor = true;
            // 
            // submitAnswerBtn
            // 
            submitAnswerBtn.Location = new Point(470, 61);
            submitAnswerBtn.Name = "submitAnswerBtn";
            submitAnswerBtn.Size = new Size(100, 30);
            submitAnswerBtn.TabIndex = 7;
            submitAnswerBtn.Text = "提交答案";
            submitAnswerBtn.UseVisualStyleBackColor = true;
            submitAnswerBtn.Click += SubmitAnswerBtn_Click;
            // 
            // startBtn
            // 
            startBtn.Location = new Point(470, 30);
            startBtn.Name = "startBtn";
            startBtn.Size = new Size(100, 30);
            startBtn.TabIndex = 6;
            startBtn.Text = "开始抄收";
            startBtn.UseVisualStyleBackColor = true;
            startBtn.Click += StartBtn_Click;
            // 
            // speetBox
            // 
            speetBox.Location = new Point(111, 23);
            speetBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            speetBox.Name = "speetBox";
            speetBox.Size = new Size(48, 28);
            speetBox.TabIndex = 5;
            speetBox.Value = new decimal(new int[] { 20, 0, 0, 0 });
            speetBox.ValueChanged += SpeetBox_ValueChanged;
            // 
            // toneBox
            // 
            toneBox.Location = new Point(111, 58);
            toneBox.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            toneBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            toneBox.Name = "toneBox";
            toneBox.Size = new Size(48, 28);
            toneBox.TabIndex = 4;
            toneBox.Value = new decimal(new int[] { 600, 0, 0, 0 });
            toneBox.ValueChanged += toneBox_ValueChanged;
            // 
            // groupNumBox
            // 
            groupNumBox.Location = new Point(111, 93);
            groupNumBox.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            groupNumBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            groupNumBox.Name = "groupNumBox";
            groupNumBox.Size = new Size(48, 28);
            groupNumBox.TabIndex = 3;
            groupNumBox.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 94);
            label3.Name = "label3";
            label3.Size = new Size(84, 21);
            label3.TabIndex = 2;
            label3.Text = "数量(组)：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 59);
            label2.Name = "label2";
            label2.Size = new Size(76, 21);
            label2.TabIndex = 1;
            label2.Text = "频率(Hz):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 24);
            label1.Name = "label1";
            label1.Size = new Size(98, 21);
            label1.TabIndex = 0;
            label1.Text = "速度(WPM):";
            // 
            // stopBtn
            // 
            stopBtn.Location = new Point(18, 166);
            stopBtn.Name = "stopBtn";
            stopBtn.Size = new Size(100, 30);
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
            groupBox4.Location = new Point(6, 212);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(1895, 826);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "抄收结果";
            // 
            // answerBox
            // 
            answerBox.Dock = DockStyle.Fill;
            answerBox.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            answerBox.Location = new Point(3, 24);
            answerBox.Name = "answerBox";
            answerBox.Size = new Size(1889, 799);
            answerBox.TabIndex = 0;
            answerBox.Text = " ";
            // 
            // timer1
            // 

            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(rePlayBtn);
            groupBox5.Controls.Add(continuePlayBtn);
            groupBox5.Controls.Add(pauseBtn);
            groupBox5.Controls.Add(clearAnswerBtn);
            groupBox5.Controls.Add(stopBtn);
            groupBox5.Font = new Font("Microsoft YaHei UI", 12F);
            groupBox5.Location = new Point(1234, 2);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(138, 204);
            groupBox5.TabIndex = 4;
            groupBox5.TabStop = false;
            groupBox5.Text = "控制";
            // 
            // rePlayBtn
            // 
            rePlayBtn.Enabled = false;
            rePlayBtn.Location = new Point(18, 130);
            rePlayBtn.Name = "rePlayBtn";
            rePlayBtn.Size = new Size(100, 30);
            rePlayBtn.TabIndex = 3;
            rePlayBtn.Text = "重播";
            rePlayBtn.UseVisualStyleBackColor = true;
            rePlayBtn.Click += ResumeBtn_Click;
            // 
            // continuePlayBtn
            // 
            continuePlayBtn.Enabled = false;
            continuePlayBtn.Location = new Point(18, 94);
            continuePlayBtn.Name = "continuePlayBtn";
            continuePlayBtn.Size = new Size(100, 30);
            continuePlayBtn.TabIndex = 2;
            continuePlayBtn.Text = "继续播放";
            continuePlayBtn.UseVisualStyleBackColor = true;
            continuePlayBtn.Click += ContinuePlayBtn_Click;
            // 
            // pauseBtn
            // 
            pauseBtn.Enabled = false;
            pauseBtn.Location = new Point(18, 58);
            pauseBtn.Name = "pauseBtn";
            pauseBtn.Size = new Size(100, 30);
            pauseBtn.TabIndex = 1;
            pauseBtn.Text = "暂停播放";
            pauseBtn.UseVisualStyleBackColor = true;
            pauseBtn.Click += PauseBtn_Click;
            // 
            // clearAnswerBtn
            // 
            clearAnswerBtn.Location = new Point(18, 22);
            clearAnswerBtn.Name = "clearAnswerBtn";
            clearAnswerBtn.Size = new Size(100, 30);
            clearAnswerBtn.TabIndex = 0;
            clearAnswerBtn.Text = "清空答案";
            clearAnswerBtn.UseVisualStyleBackColor = true;
            clearAnswerBtn.Click += ClearAnswer_Click;
            // 
            // NumberCopyingPractice
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
    }
}