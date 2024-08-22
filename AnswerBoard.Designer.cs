namespace CW
{
    partial class AnswerBoard
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
            answerBox = new RichTextBox();
            SuspendLayout();
            // 
            // answerBox
            // 
            answerBox.Dock = DockStyle.Fill;
            answerBox.Location = new Point(0, 0);
            answerBox.Name = "answerBox";
            answerBox.Size = new Size(1264, 681);
            answerBox.TabIndex = 0;
            answerBox.Text = "";
            // 
            // AnswerBoard
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(answerBox);
            Name = "AnswerBoard";
            Text = "答案";
            Load += AnswerBoard_Load;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox answerBox;
    }
}