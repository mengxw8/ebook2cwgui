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
            button1 = new Button();
            copyBtn = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(75, 29);
            button1.Name = "button1";
            button1.Size = new Size(100, 23);
            button1.TabIndex = 0;
            button1.Text = "字符转音频";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // copyBtn
            // 
            copyBtn.Location = new Point(75, 75);
            copyBtn.Name = "copyBtn";
            copyBtn.Size = new Size(100, 23);
            copyBtn.TabIndex = 1;
            copyBtn.Text = "CW抄收练习";
            copyBtn.UseVisualStyleBackColor = true;
            copyBtn.Click += copyBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(263, 463);
            Controls.Add(copyBtn);
            Controls.Add(button1);
            Name = "Form1";
            Text = "CW 工具箱";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button copyBtn;
    }
}
