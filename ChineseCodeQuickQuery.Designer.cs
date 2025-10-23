namespace CW
{
    partial class ChineseCodeQuickQuery
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChineseCodeQuickQuery));
            groupBox1 = new GroupBox();
            queryBox = new TextBox();
            cleanBtn = new Button();
            groupBox2 = new GroupBox();
            historyTable = new DataGridView();
            toChineseBtn = new Button();
            toCodeBtn = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)historyTable).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.AutoSize = true;
            groupBox1.Controls.Add(queryBox);
            groupBox1.Location = new Point(7, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1256, 121);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "查询";
            // 
            // queryBox
            // 
            queryBox.Dock = DockStyle.Fill;
            queryBox.Font = new Font("Microsoft YaHei UI", 15F);
            queryBox.Location = new Point(3, 19);
            queryBox.Multiline = true;
            queryBox.Name = "queryBox";
            queryBox.Size = new Size(1250, 99);
            queryBox.TabIndex = 1;
            // 
            // cleanBtn
            // 
            cleanBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cleanBtn.Font = new Font("Microsoft YaHei UI", 12F);
            cleanBtn.Location = new Point(780, 153);
            cleanBtn.Name = "cleanBtn";
            cleanBtn.Size = new Size(102, 28);
            cleanBtn.TabIndex = 4;
            cleanBtn.Text = "清空";
            cleanBtn.UseVisualStyleBackColor = true;
            cleanBtn.Click += CleanBtn_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.AutoSize = true;
            groupBox2.Controls.Add(historyTable);
            groupBox2.Location = new Point(7, 189);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1256, 480);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "记录";
            // 
            // historyTable
            // 
            historyTable.AllowUserToAddRows = false;
            historyTable.AllowUserToDeleteRows = false;
            historyTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            historyTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            historyTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            historyTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            historyTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            historyTable.DefaultCellStyle = dataGridViewCellStyle2;
            historyTable.Location = new Point(3, 19);
            historyTable.Name = "historyTable";
            historyTable.ReadOnly = true;
            historyTable.Size = new Size(1250, 455);
            historyTable.TabIndex = 0;
            // 
            // toChineseBtn
            // 
            toChineseBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            toChineseBtn.Location = new Point(364, 153);
            toChineseBtn.Name = "toChineseBtn";
            toChineseBtn.Size = new Size(75, 23);
            toChineseBtn.TabIndex = 5;
            toChineseBtn.Text = "转中文";
            toChineseBtn.UseVisualStyleBackColor = true;
            toChineseBtn.Click += toChineseBtn_Click;
            // 
            // toCodeBtn
            // 
            toCodeBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            toCodeBtn.Location = new Point(574, 153);
            toCodeBtn.Name = "toCodeBtn";
            toCodeBtn.Size = new Size(75, 23);
            toCodeBtn.TabIndex = 6;
            toCodeBtn.Text = "转代码";
            toCodeBtn.UseVisualStyleBackColor = true;
            toCodeBtn.Click += toCodeBtn_Click;
            // 
            // ChineseCodeQuickQuery
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1264, 681);
            Controls.Add(toCodeBtn);
            Controls.Add(toChineseBtn);
            Controls.Add(cleanBtn);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ChineseCodeQuickQuery";
            Text = "中文标准电码速查 (基于1998年12月人民邮电出版的《标准电码本》)";
            Load += ChineseCodeQuickQuery_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)historyTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox queryBox;
        private Button cleanBtn;
        private DataGridView historyTable;
        private Button toChineseBtn;
        private Button toCodeBtn;
    }
}