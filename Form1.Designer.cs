﻿namespace CW
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
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(94, 31);
            button1.Name = "button1";
            button1.Size = new Size(100, 23);
            button1.TabIndex = 0;
            button1.Text = "字符转音频";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // copyBtn
            // 
            copyBtn.Location = new Point(94, 72);
            copyBtn.Name = "copyBtn";
            copyBtn.Size = new Size(100, 23);
            copyBtn.TabIndex = 1;
            copyBtn.Text = "CW抄收练习";
            copyBtn.UseVisualStyleBackColor = true;
            copyBtn.Click += CopyBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 357);
            label1.Name = "label1";
            label1.Size = new Size(179, 17);
            label1.TabIndex = 4;
            label1.Text = "本软件基于GPL-2.0 license开源";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(49, 374);
            label2.Name = "label2";
            label2.Size = new Size(164, 17);
            label2.TabIndex = 5;
            label2.Text = "使用该软件代表同意相关协议";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(94, 391);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(56, 17);
            linkLabel1.TabIndex = 6;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "反馈问题";
            linkLabel1.LinkClicked += LinkLabel1_LinkClicked;
            // 
            // sendBtn
            // 
            sendBtn.Location = new Point(94, 113);
            sendBtn.Name = "sendBtn";
            sendBtn.Size = new Size(100, 23);
            sendBtn.TabIndex = 2;
            sendBtn.Text = "CW发报练习";
            sendBtn.UseVisualStyleBackColor = true;
            sendBtn.Click += SendBtn_Click;
            // 
            // shortNumberBtn
            // 
            shortNumberBtn.Location = new Point(94, 154);
            shortNumberBtn.Name = "shortNumberBtn";
            shortNumberBtn.Size = new Size(100, 23);
            shortNumberBtn.TabIndex = 3;
            shortNumberBtn.Text = "数字短码练习";
            shortNumberBtn.UseVisualStyleBackColor = true;
            shortNumberBtn.Click += ShortNumberBtn_Click;
            // 
            // chineseCodeQuickQueryBtn
            // 
            chineseCodeQuickQueryBtn.Location = new Point(94, 196);
            chineseCodeQuickQueryBtn.Name = "chineseCodeQuickQueryBtn";
            chineseCodeQuickQueryBtn.Size = new Size(100, 23);
            chineseCodeQuickQueryBtn.TabIndex = 7;
            chineseCodeQuickQueryBtn.Text = "中文电码本快查";
            chineseCodeQuickQueryBtn.UseVisualStyleBackColor = true;
            chineseCodeQuickQueryBtn.Click += chineseCodeQuickQueryBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(282, 463);
            Controls.Add(chineseCodeQuickQueryBtn);
            Controls.Add(shortNumberBtn);
            Controls.Add(sendBtn);
            Controls.Add(linkLabel1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(copyBtn);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
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
    }
}
