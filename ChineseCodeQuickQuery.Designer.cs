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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChineseCodeQuickQuery));
            groupBox1 = new GroupBox();
            cleanBtn = new Button();
            codeLab = new Label();
            ChineseLab = new Label();
            queryBox = new TextBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            historyTable = new DataGridView();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)historyTable).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(cleanBtn);
            groupBox1.Controls.Add(codeLab);
            groupBox1.Controls.Add(ChineseLab);
            groupBox1.Controls.Add(queryBox);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(7, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1256, 100);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "查询";
            // 
            // cleanBtn
            // 
            cleanBtn.Font = new Font("Microsoft YaHei UI", 12F);
            cleanBtn.Location = new Point(955, 42);
            cleanBtn.Name = "cleanBtn";
            cleanBtn.Size = new Size(102, 28);
            cleanBtn.TabIndex = 4;
            cleanBtn.Text = "清空历史";
            cleanBtn.UseVisualStyleBackColor = true;
            cleanBtn.Click += cleanBtn_Click;
            // 
            // codeLab
            // 
            codeLab.AutoSize = true;
            codeLab.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Bold);
            codeLab.Location = new Point(686, 61);
            codeLab.Name = "codeLab";
            codeLab.Size = new Size(0, 27);
            codeLab.TabIndex = 3;
            // 
            // ChineseLab
            // 
            ChineseLab.AutoSize = true;
            ChineseLab.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Bold);
            ChineseLab.Location = new Point(698, 25);
            ChineseLab.Name = "ChineseLab";
            ChineseLab.Size = new Size(0, 27);
            ChineseLab.TabIndex = 2;
            // 
            // queryBox
            // 
            queryBox.Font = new Font("Microsoft YaHei UI", 15F);
            queryBox.Location = new Point(409, 37);
            queryBox.Name = "queryBox";
            queryBox.Size = new Size(150, 33);
            queryBox.TabIndex = 1;
            queryBox.KeyDown += textBox1_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F);
            label1.Location = new Point(275, 44);
            label1.Name = "label1";
            label1.Size = new Size(138, 21);
            label1.TabIndex = 0;
            label1.Text = "输入中文或代码：";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.AutoSize = true;
            groupBox2.Controls.Add(historyTable);
            groupBox2.Location = new Point(7, 111);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1256, 696);
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
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            historyTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            historyTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            historyTable.DefaultCellStyle = dataGridViewCellStyle4;
            historyTable.Location = new Point(3, 19);
            historyTable.Name = "historyTable";
            historyTable.ReadOnly = true;
            historyTable.Size = new Size(1250, 674);
            historyTable.TabIndex = 0;
            // 
            // ChineseCodeQuickQuery
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1264, 681);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ChineseCodeQuickQuery";
            Text = "中文标准电码速查 (基于1998年12月人民邮电出版的《标准电码本》)";
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
        private Label label1;
        private Label codeLab;
        private Label ChineseLab;
        private Button cleanBtn;
        private DataGridView historyTable;
    }
}