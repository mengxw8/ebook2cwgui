namespace CW
{
    partial class EncodingConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncodingConfiguration));
            label1 = new Label();
            groupBox1 = new GroupBox();
            customizeBtn = new RadioButton();
            number10Btn = new RadioButton();
            number5Btn = new RadioButton();
            defaultBtn = new RadioButton();
            SaveBtn = new Button();
            groupBox2 = new GroupBox();
            codeConfigTxb = new RichTextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 31);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 0;
            label1.Text = "编码方式：";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.AutoSize = true;
            groupBox1.Controls.Add(customizeBtn);
            groupBox1.Controls.Add(number10Btn);
            groupBox1.Controls.Add(number5Btn);
            groupBox1.Controls.Add(defaultBtn);
            groupBox1.Controls.Add(SaveBtn);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(798, 72);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "配置";
            // 
            // customizeBtn
            // 
            customizeBtn.AutoSize = true;
            customizeBtn.Location = new Point(262, 29);
            customizeBtn.Name = "customizeBtn";
            customizeBtn.Size = new Size(62, 21);
            customizeBtn.TabIndex = 5;
            customizeBtn.Text = "自定义";
            customizeBtn.UseVisualStyleBackColor = true;
            customizeBtn.CheckedChanged += CustomizeBtn_CheckedChanged;
            // 
            // number10Btn
            // 
            number10Btn.AutoSize = true;
            number10Btn.Location = new Point(192, 29);
            number10Btn.Name = "number10Btn";
            number10Btn.Size = new Size(64, 21);
            number10Btn.TabIndex = 4;
            number10Btn.Text = "短10改";
            number10Btn.UseVisualStyleBackColor = true;
            number10Btn.CheckedChanged += Number10Btn_CheckedChanged;
            // 
            // number5Btn
            // 
            number5Btn.AutoSize = true;
            number5Btn.Location = new Point(129, 29);
            number5Btn.Name = "number5Btn";
            number5Btn.Size = new Size(57, 21);
            number5Btn.TabIndex = 3;
            number5Btn.Text = "短5改";
            number5Btn.UseVisualStyleBackColor = true;
            number5Btn.CheckedChanged += Number5Btn_CheckedChanged;
            // 
            // defaultBtn
            // 
            defaultBtn.AutoSize = true;
            defaultBtn.Checked = true;
            defaultBtn.Location = new Point(73, 29);
            defaultBtn.Name = "defaultBtn";
            defaultBtn.Size = new Size(50, 21);
            defaultBtn.TabIndex = 2;
            defaultBtn.TabStop = true;
            defaultBtn.Text = "默认";
            defaultBtn.UseVisualStyleBackColor = true;
            defaultBtn.CheckedChanged += DefaultBtn_CheckedChanged;
            // 
            // SaveBtn
            // 
            SaveBtn.Location = new Point(697, 22);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(75, 23);
            SaveBtn.TabIndex = 1;
            SaveBtn.Text = "应用";
            SaveBtn.UseVisualStyleBackColor = true;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.AutoSize = true;
            groupBox2.Controls.Add(codeConfigTxb);
            groupBox2.Location = new Point(3, 76);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(798, 370);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "详细配置";
            // 
            // codeConfigTxb
            // 
            codeConfigTxb.Dock = DockStyle.Fill;
            codeConfigTxb.Font = new Font("Microsoft YaHei UI", 20F);
            codeConfigTxb.Location = new Point(3, 19);
            codeConfigTxb.Name = "codeConfigTxb";
            codeConfigTxb.Size = new Size(792, 348);
            codeConfigTxb.TabIndex = 0;
            codeConfigTxb.Text = "";
            // 
            // EncodingConfiguration
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EncodingConfiguration";
            StartPosition = FormStartPosition.CenterParent;
            Text = "编码配置";
            Load += EncodingConfiguration_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private RadioButton customizeBtn;
        private RadioButton number10Btn;
        private RadioButton number5Btn;
        private RadioButton defaultBtn;
        private Button SaveBtn;
        private GroupBox groupBox2;
        private RichTextBox codeConfigTxb;
    }
}