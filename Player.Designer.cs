namespace CW
{
    partial class Player
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Player));
            groupBox5 = new GroupBox();
            groupBox4 = new GroupBox();
            ExportBtn = new Button();
            groupBox3 = new GroupBox();
            StartBtn = new Button();
            StopBtn = new Button();
            ReplayBtn = new Button();
            ContinueBtn = new Button();
            PauseBtn = new Button();
            groupBox2 = new GroupBox();
            CodingDefinitionBtn = new Button();
            speedBox = new NumericUpDown();
            toneBox = new NumericUpDown();
            label2 = new Label();
            label1 = new Label();
            groupBox1 = new GroupBox();
            FilePathLbl = new Label();
            label3 = new Label();
            SelectFileBtn = new Button();
            groupBox6 = new GroupBox();
            ContentTxb = new RichTextBox();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)speedBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)toneBox).BeginInit();
            groupBox1.SuspendLayout();
            groupBox6.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Controls.Add(groupBox4);
            groupBox5.Controls.Add(groupBox3);
            groupBox5.Controls.Add(groupBox2);
            groupBox5.Controls.Add(groupBox1);
            groupBox5.Location = new Point(1, 1);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(1314, 111);
            groupBox5.TabIndex = 4;
            groupBox5.TabStop = false;
            groupBox5.Text = "配置";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(ExportBtn);
            groupBox4.Location = new Point(677, 14);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(123, 90);
            groupBox4.TabIndex = 7;
            groupBox4.TabStop = false;
            groupBox4.Text = "导出";
            // 
            // ExportBtn
            // 
            ExportBtn.Location = new Point(24, 42);
            ExportBtn.Name = "ExportBtn";
            ExportBtn.Size = new Size(75, 23);
            ExportBtn.TabIndex = 0;
            ExportBtn.Text = "导出音频";
            ExportBtn.UseVisualStyleBackColor = true;
            ExportBtn.Click += ExportBtn_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(StartBtn);
            groupBox3.Controls.Add(StopBtn);
            groupBox3.Controls.Add(ReplayBtn);
            groupBox3.Controls.Add(ContinueBtn);
            groupBox3.Controls.Add(PauseBtn);
            groupBox3.Location = new Point(504, 14);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(169, 90);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "播放设置";
            // 
            // StartBtn
            // 
            StartBtn.Location = new Point(6, 16);
            StartBtn.Name = "StartBtn";
            StartBtn.Size = new Size(75, 23);
            StartBtn.TabIndex = 4;
            StartBtn.Text = "开始";
            StartBtn.UseVisualStyleBackColor = true;
            StartBtn.Click += StartBtn_Click;
            // 
            // StopBtn
            // 
            StopBtn.Location = new Point(87, 61);
            StopBtn.Name = "StopBtn";
            StopBtn.Size = new Size(75, 23);
            StopBtn.TabIndex = 3;
            StopBtn.Text = "停止";
            StopBtn.UseVisualStyleBackColor = true;
            StopBtn.Click += StopBtn_Click;
            // 
            // ReplayBtn
            // 
            ReplayBtn.Location = new Point(87, 37);
            ReplayBtn.Name = "ReplayBtn";
            ReplayBtn.Size = new Size(75, 23);
            ReplayBtn.TabIndex = 2;
            ReplayBtn.Text = "重播";
            ReplayBtn.UseVisualStyleBackColor = true;
            ReplayBtn.Click += ReplayBtn_Click;
            // 
            // ContinueBtn
            // 
            ContinueBtn.Location = new Point(6, 62);
            ContinueBtn.Name = "ContinueBtn";
            ContinueBtn.Size = new Size(75, 23);
            ContinueBtn.TabIndex = 1;
            ContinueBtn.Text = "继续";
            ContinueBtn.UseVisualStyleBackColor = true;
            ContinueBtn.Click += ContinueBtn_Click;
            // 
            // PauseBtn
            // 
            PauseBtn.Location = new Point(6, 39);
            PauseBtn.Name = "PauseBtn";
            PauseBtn.Size = new Size(75, 23);
            PauseBtn.TabIndex = 0;
            PauseBtn.Text = "暂停";
            PauseBtn.UseVisualStyleBackColor = true;
            PauseBtn.Click += PauseBtn_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(CodingDefinitionBtn);
            groupBox2.Controls.Add(speedBox);
            groupBox2.Controls.Add(toneBox);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(7, 56);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(491, 48);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "莫尔斯配置";
            // 
            // CodingDefinitionBtn
            // 
            CodingDefinitionBtn.Location = new Point(386, 15);
            CodingDefinitionBtn.Name = "CodingDefinitionBtn";
            CodingDefinitionBtn.Size = new Size(83, 23);
            CodingDefinitionBtn.TabIndex = 6;
            CodingDefinitionBtn.Text = "编码设置";
            CodingDefinitionBtn.UseVisualStyleBackColor = true;
            CodingDefinitionBtn.Click += CodingDefinitionBtn_Click;
            // 
            // speedBox
            // 
            speedBox.Location = new Point(87, 18);
            speedBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            speedBox.Name = "speedBox";
            speedBox.Size = new Size(48, 23);
            speedBox.TabIndex = 2;
            speedBox.Value = new decimal(new int[] { 20, 0, 0, 0 });
            speedBox.ValueChanged += SpeedBox_ValueChanged;
            // 
            // toneBox
            // 
            toneBox.Location = new Point(192, 18);
            toneBox.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            toneBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            toneBox.Name = "toneBox";
            toneBox.Size = new Size(46, 23);
            toneBox.TabIndex = 4;
            toneBox.Value = new decimal(new int[] { 600, 0, 0, 0 });
            toneBox.ValueChanged += ToneBox_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(135, 21);
            label2.Name = "label2";
            label2.Size = new Size(58, 17);
            label2.TabIndex = 5;
            label2.Text = "频率(Hz):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 21);
            label1.Name = "label1";
            label1.Size = new Size(74, 17);
            label1.TabIndex = 3;
            label1.Text = "速度(WPM):";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(FilePathLbl);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(SelectFileBtn);
            groupBox1.Location = new Point(7, 14);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(491, 42);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "播放源";
            // 
            // FilePathLbl
            // 
            FilePathLbl.AutoSize = true;
            FilePathLbl.Location = new Point(148, 20);
            FilePathLbl.Name = "FilePathLbl";
            FilePathLbl.Size = new Size(44, 17);
            FilePathLbl.TabIndex = 2;
            FilePathLbl.Text = "未选择";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(88, 19);
            label3.Name = "label3";
            label3.Size = new Size(68, 17);
            label3.TabIndex = 1;
            label3.Text = "文件路径：";
            // 
            // SelectFileBtn
            // 
            SelectFileBtn.Location = new Point(9, 16);
            SelectFileBtn.Name = "SelectFileBtn";
            SelectFileBtn.Size = new Size(75, 23);
            SelectFileBtn.TabIndex = 0;
            SelectFileBtn.Text = "选择文件";
            SelectFileBtn.UseVisualStyleBackColor = true;
            SelectFileBtn.Click += SelectFileBtn_Click;
            // 
            // groupBox6
            // 
            groupBox6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox6.AutoSize = true;
            groupBox6.Controls.Add(ContentTxb);
            groupBox6.Location = new Point(1, 118);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(1320, 3892);
            groupBox6.TabIndex = 5;
            groupBox6.TabStop = false;
            groupBox6.Text = "内容";
            // 
            // ContentTxb
            // 
            ContentTxb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ContentTxb.Font = new Font("Microsoft YaHei UI", 50F);
            ContentTxb.Location = new Point(3, 19);
            ContentTxb.Name = "ContentTxb";
            ContentTxb.Size = new Size(1314, 3870);
            ContentTxb.TabIndex = 0;
            ContentTxb.Text = "";
            // 
            // Player
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1320, 729);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Player";
            Text = "滴答播放器";
            Load += Player_Load;
            groupBox5.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)speedBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)toneBox).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox6.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox5;
        private GroupBox groupBox4;
        private Button ExportBtn;
        private GroupBox groupBox3;
        private Button StopBtn;
        private Button ReplayBtn;
        private Button ContinueBtn;
        private Button PauseBtn;
        private GroupBox groupBox2;
        private Button CodingDefinitionBtn;
        private NumericUpDown speedBox;
        private NumericUpDown toneBox;
        private Label label2;
        private Label label1;
        private GroupBox groupBox1;
        private Label FilePathLbl;
        private Label label3;
        private Button SelectFileBtn;
        private GroupBox groupBox6;
        private RichTextBox ContentTxb;
        private Button StartBtn;
    }
}