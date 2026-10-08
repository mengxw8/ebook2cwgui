namespace CW
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            copyBtn = new Button();
            label1 = new Label();
            label2 = new Label();
            linkLabel1 = new LinkLabel();
            sendBtn = new Button();
            shortNumberBtn = new Button();
            chineseCodeQuickQueryBtn = new Button();
            abbreviationQuickSearchBtn = new Button();
            ToPlayerBtn = new Button();
            multiChannelBtn = new Button();
            themeLabel = new Label();
            themeCombo = new ComboBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(148, 44);
            button1.Margin = new Padding(5, 4, 5, 4);
            button1.Name = "button1";
            button1.Size = new Size(157, 32);
            button1.TabIndex = 0;
            button1.Text = "字符转音频";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // copyBtn
            // 
            copyBtn.Location = new Point(148, 102);
            copyBtn.Margin = new Padding(5, 4, 5, 4);
            copyBtn.Name = "copyBtn";
            copyBtn.Size = new Size(157, 32);
            copyBtn.TabIndex = 1;
            copyBtn.Text = "CW抄收练习";
            copyBtn.UseVisualStyleBackColor = true;
            copyBtn.Click += CopyBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(68, 636);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(266, 24);
            label1.TabIndex = 4;
            label1.Text = "本软件基于GPL-2.0 license开源";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(77, 660);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(244, 24);
            label2.TabIndex = 5;
            label2.Text = "使用该软件代表同意相关协议";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(148, 684);
            linkLabel1.Margin = new Padding(5, 0, 5, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(82, 24);
            linkLabel1.TabIndex = 6;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "反馈问题";
            linkLabel1.LinkClicked += LinkLabel1_LinkClicked;
            // 
            // sendBtn
            // 
            sendBtn.Location = new Point(148, 160);
            sendBtn.Margin = new Padding(5, 4, 5, 4);
            sendBtn.Name = "sendBtn";
            sendBtn.Size = new Size(157, 32);
            sendBtn.TabIndex = 2;
            sendBtn.Text = "CW发报练习";
            sendBtn.UseVisualStyleBackColor = true;
            sendBtn.Click += SendBtn_Click;
            // 
            // shortNumberBtn
            // 
            shortNumberBtn.Location = new Point(148, 217);
            shortNumberBtn.Margin = new Padding(5, 4, 5, 4);
            shortNumberBtn.Name = "shortNumberBtn";
            shortNumberBtn.Size = new Size(157, 32);
            shortNumberBtn.TabIndex = 3;
            shortNumberBtn.Text = "数字短码练习";
            shortNumberBtn.UseVisualStyleBackColor = true;
            shortNumberBtn.Click += ShortNumberBtn_Click;
            // 
            // chineseCodeQuickQueryBtn
            // 
            chineseCodeQuickQueryBtn.Location = new Point(148, 277);
            chineseCodeQuickQueryBtn.Margin = new Padding(5, 4, 5, 4);
            chineseCodeQuickQueryBtn.Name = "chineseCodeQuickQueryBtn";
            chineseCodeQuickQueryBtn.Size = new Size(157, 32);
            chineseCodeQuickQueryBtn.TabIndex = 7;
            chineseCodeQuickQueryBtn.Text = "中文电码本快查";
            chineseCodeQuickQueryBtn.UseVisualStyleBackColor = true;
            chineseCodeQuickQueryBtn.Click += ChineseCodeQuickQueryBtn_Click;
            // 
            // abbreviationQuickSearchBtn
            // 
            abbreviationQuickSearchBtn.Location = new Point(148, 340);
            abbreviationQuickSearchBtn.Margin = new Padding(5, 4, 5, 4);
            abbreviationQuickSearchBtn.Name = "abbreviationQuickSearchBtn";
            abbreviationQuickSearchBtn.Size = new Size(157, 32);
            abbreviationQuickSearchBtn.TabIndex = 8;
            abbreviationQuickSearchBtn.Text = "简语速查";
            abbreviationQuickSearchBtn.UseVisualStyleBackColor = true;
            abbreviationQuickSearchBtn.Click += AbbreviationQuickSearchBtn_Click;
            // 
            // ToPlayerBtn
            // 
            ToPlayerBtn.Location = new Point(148, 401);
            ToPlayerBtn.Margin = new Padding(5, 4, 5, 4);
            ToPlayerBtn.Name = "ToPlayerBtn";
            ToPlayerBtn.Size = new Size(157, 32);
            ToPlayerBtn.TabIndex = 9;
            ToPlayerBtn.Text = "莫尔斯播放器";
            ToPlayerBtn.UseVisualStyleBackColor = true;
            ToPlayerBtn.Click += ToPlayerBtn_Click;
            // 
            // multiChannelBtn
            // 
            multiChannelBtn.Location = new Point(148, 459);
            multiChannelBtn.Margin = new Padding(5, 4, 5, 4);
            multiChannelBtn.Name = "multiChannelBtn";
            multiChannelBtn.Size = new Size(157, 32);
            multiChannelBtn.TabIndex = 10;
            multiChannelBtn.Text = "多路播放";
            multiChannelBtn.UseVisualStyleBackColor = true;
            multiChannelBtn.Click += MultiChannelBtn_Click;
            // 
            // themeLabel
            // 
            themeLabel.AutoSize = true;
            themeLabel.Location = new Point(63, 591);
            themeLabel.Margin = new Padding(5, 0, 5, 0);
            themeLabel.Name = "themeLabel";
            themeLabel.Size = new Size(86, 24);
            themeLabel.TabIndex = 12;
            themeLabel.Text = "界面主题:";
            // 
            // themeCombo
            // 
            themeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            themeCombo.Items.AddRange(new object[] { "浅色模式", "深色模式", "高对比度模式", "跟随系统" });
            themeCombo.Location = new Point(173, 585);
            themeCombo.Margin = new Padding(5, 4, 5, 4);
            themeCombo.Name = "themeCombo";
            themeCombo.Size = new Size(199, 32);
            themeCombo.TabIndex = 11;
            themeCombo.SelectedIndexChanged += ThemeCombo_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(443, 734);
            Controls.Add(multiChannelBtn);
            Controls.Add(themeCombo);
            Controls.Add(themeLabel);
            Controls.Add(ToPlayerBtn);
            Controls.Add(abbreviationQuickSearchBtn);
            Controls.Add(chineseCodeQuickQueryBtn);
            Controls.Add(shortNumberBtn);
            Controls.Add(sendBtn);
            Controls.Add(linkLabel1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(copyBtn);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "CW工具箱";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button copyBtn;
        private Label label1;
        private Label label2;
        private LinkLabel linkLabel1;
        private Button sendBtn;
        private Button shortNumberBtn;
        private Button chineseCodeQuickQueryBtn;
        private Button abbreviationQuickSearchBtn;
        private Button ToPlayerBtn;
        private Button multiChannelBtn;
        private Label themeLabel;
        private ComboBox themeCombo;
    }
}
