namespace CW
{
    partial class MultiChannelPlayer
    {
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiChannelPlayer));
            nameColumn = new ColumnHeader();
            roleColumn = new ColumnHeader();
            frequencyColumn = new ColumnHeader();
            speedColumn = new ColumnHeader();
            encodingColumn = new ColumnHeader();
            volumeColumn = new ColumnHeader();
            currentGroupColumn = new ColumnHeader();
            mainLayout = new TableLayoutPanel();
            channelGroup = new GroupBox();
            channelList = new ListView();
            channelButtons = new FlowLayoutPanel();
            addBtn = new Button();
            deleteBtn = new Button();
            upBtn = new Button();
            downBtn = new Button();
            editorGroup = new GroupBox();
            textBox = new TextBox();
            previewLabel = new Label();
            filePanel = new TableLayoutPanel();
            filePathBox = new TextBox();
            loadFileBtn = new Button();
            fileLabel = new Label();
            randomPanel = new Panel();
            randomRangeRow = new FlowLayoutPanel();
            rangeLabel = new Label();
            randomLetterCheck = new CheckBox();
            randomNumberCheck = new CheckBox();
            randomSymbolCheck = new CheckBox();
            customLabel = new Label();
            randomCustomBox = new TextBox();
            randomCountRow = new FlowLayoutPanel();
            randomLabel = new Label();
            groupSizeLabel = new Label();
            randomEachBox = new NumericUpDown();
            groupCountLabel = new Label();
            randomGroupBox = new NumericUpDown();
            randomUniqueCheck = new CheckBox();
            randomUniqueGroupCheck = new CheckBox();
            randomGenerateBtn = new Button();
            fieldGrid = new TableLayoutPanel();
            nameLabel = new Label();
            nameBox = new TextBox();
            speedLabel = new Label();
            speedBox = new NumericUpDown();
            frequencyLabel = new Label();
            frequencyBox = new NumericUpDown();
            waveformLabel = new Label();
            waveformBox = new ComboBox();
            volumeLabel = new Label();
            volumeBox = new NumericUpDown();
            snrLabel = new Label();
            snrBox = new NumericUpDown();
            channelOptions = new FlowLayoutPanel();
            primaryCheck = new CheckBox();
            loopCheck = new CheckBox();
            muteCheck = new CheckBox();
            noiseCheck = new CheckBox();
            encodingBtn = new Button();
            sourceHintLabel = new Label();
            bottomPanel = new Panel();
            statusLabel = new Label();
            transportButtons = new FlowLayoutPanel();
            playBtn = new Button();
            pauseBtn = new Button();
            stopBtn = new Button();
            replayBtn = new Button();
            presetButtons = new FlowLayoutPanel();
            openBtn = new Button();
            saveBtn = new Button();
            exportBtn = new Button();
            mainLayout.SuspendLayout();
            channelGroup.SuspendLayout();
            channelButtons.SuspendLayout();
            editorGroup.SuspendLayout();
            filePanel.SuspendLayout();
            randomPanel.SuspendLayout();
            randomRangeRow.SuspendLayout();
            randomCountRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)randomEachBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)randomGroupBox).BeginInit();
            fieldGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)speedBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)frequencyBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)volumeBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)snrBox).BeginInit();
            channelOptions.SuspendLayout();
            bottomPanel.SuspendLayout();
            transportButtons.SuspendLayout();
            presetButtons.SuspendLayout();
            SuspendLayout();
            // 
            // nameColumn
            // 
            nameColumn.Text = "名称";
            nameColumn.Width = 72;
            // 
            // roleColumn
            // 
            roleColumn.Text = "角色";
            roleColumn.TextAlign = HorizontalAlignment.Center;
            roleColumn.Width = 44;
            // 
            // frequencyColumn
            // 
            frequencyColumn.Text = "频率";
            frequencyColumn.TextAlign = HorizontalAlignment.Center;
            frequencyColumn.Width = 52;
            // 
            // speedColumn
            // 
            speedColumn.Text = "速度";
            speedColumn.TextAlign = HorizontalAlignment.Center;
            speedColumn.Width = 48;
            // 
            // encodingColumn
            // 
            encodingColumn.Text = "编码";
            encodingColumn.TextAlign = HorizontalAlignment.Center;
            // 
            // volumeColumn
            // 
            volumeColumn.Text = "音量";
            volumeColumn.TextAlign = HorizontalAlignment.Center;
            volumeColumn.Width = 48;
            // 
            // currentGroupColumn
            // 
            currentGroupColumn.Text = "当前组";
            currentGroupColumn.TextAlign = HorizontalAlignment.Center;
            currentGroupColumn.Width = 80;
            // 
            // mainLayout
            // 
            mainLayout.BackColor = Color.FromArgb(245, 247, 250);
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            mainLayout.Controls.Add(channelGroup, 0, 0);
            mainLayout.Controls.Add(editorGroup, 1, 0);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ForeColor = SystemColors.ControlText;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(16, 16, 16, 12);
            mainLayout.RowCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(1440, 888);
            mainLayout.TabIndex = 0;
            // 
            // channelGroup
            // 
            channelGroup.BackColor = Color.FromArgb(245, 247, 250);
            channelGroup.Controls.Add(channelList);
            channelGroup.Controls.Add(channelButtons);
            channelGroup.Dock = DockStyle.Fill;
            channelGroup.ForeColor = SystemColors.ControlText;
            channelGroup.Location = new Point(19, 19);
            channelGroup.Name = "channelGroup";
            channelGroup.Padding = new Padding(12, 22, 12, 12);
            channelGroup.Size = new Size(557, 854);
            channelGroup.TabIndex = 0;
            channelGroup.TabStop = false;
            channelGroup.Text = "播放通道";
            // 
            // channelList
            // 
            channelList.BackColor = SystemColors.Window;
            channelList.Columns.AddRange(new ColumnHeader[] { nameColumn, roleColumn, frequencyColumn, speedColumn, encodingColumn, volumeColumn, currentGroupColumn });
            channelList.Dock = DockStyle.Fill;
            channelList.ForeColor = SystemColors.WindowText;
            channelList.FullRowSelect = true;
            channelList.GridLines = true;
            channelList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            channelList.Location = new Point(12, 38);
            channelList.MultiSelect = false;
            channelList.Name = "channelList";
            channelList.Size = new Size(533, 750);
            channelList.TabIndex = 0;
            channelList.UseCompatibleStateImageBehavior = false;
            channelList.View = View.Details;
            // 
            // channelButtons
            // 
            channelButtons.BackColor = Color.FromArgb(245, 247, 250);
            channelButtons.Controls.Add(addBtn);
            channelButtons.Controls.Add(deleteBtn);
            channelButtons.Controls.Add(upBtn);
            channelButtons.Controls.Add(downBtn);
            channelButtons.Dock = DockStyle.Bottom;
            channelButtons.ForeColor = SystemColors.ControlText;
            channelButtons.Location = new Point(12, 788);
            channelButtons.Name = "channelButtons";
            channelButtons.Padding = new Padding(0, 10, 0, 0);
            channelButtons.Size = new Size(533, 54);
            channelButtons.TabIndex = 1;
            channelButtons.WrapContents = false;
            // 
            // addBtn
            // 
            addBtn.BackColor = Color.FromArgb(245, 247, 250);
            addBtn.ForeColor = SystemColors.ControlText;
            addBtn.Location = new Point(0, 10);
            addBtn.Margin = new Padding(0, 0, 12, 0);
            addBtn.Name = "addBtn";
            addBtn.Size = new Size(82, 36);
            addBtn.TabIndex = 0;
            addBtn.Text = "添加";
            addBtn.UseVisualStyleBackColor = false;
            // 
            // deleteBtn
            // 
            deleteBtn.BackColor = Color.FromArgb(245, 247, 250);
            deleteBtn.ForeColor = SystemColors.ControlText;
            deleteBtn.Location = new Point(94, 10);
            deleteBtn.Margin = new Padding(0, 0, 12, 0);
            deleteBtn.Name = "deleteBtn";
            deleteBtn.Size = new Size(82, 36);
            deleteBtn.TabIndex = 1;
            deleteBtn.Text = "删除";
            deleteBtn.UseVisualStyleBackColor = false;
            // 
            // upBtn
            // 
            upBtn.BackColor = Color.FromArgb(245, 247, 250);
            upBtn.ForeColor = SystemColors.ControlText;
            upBtn.Location = new Point(188, 10);
            upBtn.Margin = new Padding(0, 0, 12, 0);
            upBtn.Name = "upBtn";
            upBtn.Size = new Size(82, 36);
            upBtn.TabIndex = 2;
            upBtn.Text = "上移";
            upBtn.UseVisualStyleBackColor = false;
            // 
            // downBtn
            // 
            downBtn.BackColor = Color.FromArgb(245, 247, 250);
            downBtn.ForeColor = SystemColors.ControlText;
            downBtn.Location = new Point(282, 10);
            downBtn.Margin = new Padding(0, 0, 12, 0);
            downBtn.Name = "downBtn";
            downBtn.Size = new Size(82, 36);
            downBtn.TabIndex = 3;
            downBtn.Text = "下移";
            downBtn.UseVisualStyleBackColor = false;
            // 
            // editorGroup
            // 
            editorGroup.BackColor = Color.FromArgb(245, 247, 250);
            editorGroup.Controls.Add(textBox);
            editorGroup.Controls.Add(previewLabel);
            editorGroup.Controls.Add(filePanel);
            editorGroup.Controls.Add(randomPanel);
            editorGroup.Controls.Add(fieldGrid);
            editorGroup.Dock = DockStyle.Fill;
            editorGroup.ForeColor = SystemColors.ControlText;
            editorGroup.Location = new Point(582, 19);
            editorGroup.Name = "editorGroup";
            editorGroup.Padding = new Padding(16, 24, 16, 12);
            editorGroup.Size = new Size(839, 854);
            editorGroup.TabIndex = 1;
            editorGroup.TabStop = false;
            editorGroup.Text = "通道设置与报文";
            // 
            // textBox
            // 
            textBox.BackColor = SystemColors.Window;
            textBox.Dock = DockStyle.Fill;
            textBox.ForeColor = SystemColors.WindowText;
            textBox.Location = new Point(16, 390);
            textBox.Multiline = true;
            textBox.Name = "textBox";
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Size = new Size(807, 452);
            textBox.TabIndex = 0;
            // 
            // previewLabel
            // 
            previewLabel.BackColor = Color.FromArgb(245, 247, 250);
            previewLabel.Dock = DockStyle.Top;
            previewLabel.ForeColor = SystemColors.ControlText;
            previewLabel.Location = new Point(16, 360);
            previewLabel.Name = "previewLabel";
            previewLabel.Padding = new Padding(0, 0, 0, 6);
            previewLabel.Size = new Size(807, 30);
            previewLabel.TabIndex = 1;
            previewLabel.Text = "预览";
            previewLabel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // filePanel
            // 
            filePanel.BackColor = Color.FromArgb(245, 247, 250);
            filePanel.ColumnCount = 3;
            filePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 84F));
            filePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            filePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 104F));
            filePanel.Controls.Add(filePathBox, 1, 0);
            filePanel.Controls.Add(loadFileBtn, 2, 0);
            filePanel.Controls.Add(fileLabel, 0, 0);
            filePanel.Dock = DockStyle.Top;
            filePanel.ForeColor = SystemColors.ControlText;
            filePanel.Location = new Point(16, 318);
            filePanel.Name = "filePanel";
            filePanel.Padding = new Padding(0, 7, 0, 7);
            filePanel.RowCount = 1;
            filePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            filePanel.Size = new Size(807, 42);
            filePanel.TabIndex = 2;
            // 
            // filePathBox
            // 
            filePathBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            filePathBox.BackColor = SystemColors.Control;
            filePathBox.ForeColor = SystemColors.WindowText;
            filePathBox.Location = new Point(84, 9);
            filePathBox.Margin = new Padding(0);
            filePathBox.Name = "filePathBox";
            filePathBox.PlaceholderText = "未选择 txt 或音频文件";
            filePathBox.ReadOnly = true;
            filePathBox.Size = new Size(619, 23);
            filePathBox.TabIndex = 1;
            // 
            // loadFileBtn
            // 
            loadFileBtn.Anchor = AnchorStyles.None;
            loadFileBtn.BackColor = Color.FromArgb(245, 247, 250);
            loadFileBtn.ForeColor = SystemColors.ControlText;
            loadFileBtn.Location = new Point(703, 7);
            loadFileBtn.Margin = new Padding(0);
            loadFileBtn.Name = "loadFileBtn";
            loadFileBtn.Size = new Size(104, 28);
            loadFileBtn.TabIndex = 0;
            loadFileBtn.Text = "选择文件";
            loadFileBtn.UseVisualStyleBackColor = false;
            // 
            // fileLabel
            // 
            fileLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            fileLabel.BackColor = Color.FromArgb(245, 247, 250);
            fileLabel.ForeColor = SystemColors.ControlText;
            fileLabel.Location = new Point(0, 7);
            fileLabel.Margin = new Padding(0);
            fileLabel.Name = "fileLabel";
            fileLabel.Size = new Size(84, 28);
            fileLabel.TabIndex = 2;
            fileLabel.Text = "播放文件";
            fileLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // randomPanel
            // 
            randomPanel.BackColor = Color.FromArgb(245, 247, 250);
            randomPanel.Controls.Add(randomRangeRow);
            randomPanel.Controls.Add(randomCountRow);
            randomPanel.Dock = DockStyle.Top;
            randomPanel.ForeColor = SystemColors.ControlText;
            randomPanel.Location = new Point(16, 234);
            randomPanel.Name = "randomPanel";
            randomPanel.Padding = new Padding(0, 2, 0, 2);
            randomPanel.Size = new Size(807, 84);
            randomPanel.TabIndex = 3;
            // 
            // randomRangeRow
            // 
            randomRangeRow.BackColor = Color.FromArgb(245, 247, 250);
            randomRangeRow.Controls.Add(rangeLabel);
            randomRangeRow.Controls.Add(randomLetterCheck);
            randomRangeRow.Controls.Add(randomNumberCheck);
            randomRangeRow.Controls.Add(randomSymbolCheck);
            randomRangeRow.Controls.Add(customLabel);
            randomRangeRow.Controls.Add(randomCustomBox);
            randomRangeRow.Dock = DockStyle.Fill;
            randomRangeRow.ForeColor = SystemColors.ControlText;
            randomRangeRow.Location = new Point(0, 42);
            randomRangeRow.Name = "randomRangeRow";
            randomRangeRow.Size = new Size(807, 40);
            randomRangeRow.TabIndex = 0;
            randomRangeRow.WrapContents = false;
            // 
            // rangeLabel
            // 
            rangeLabel.AutoSize = true;
            rangeLabel.BackColor = Color.FromArgb(245, 247, 250);
            rangeLabel.ForeColor = SystemColors.ControlText;
            rangeLabel.Location = new Point(0, 10);
            rangeLabel.Margin = new Padding(0, 10, 6, 0);
            rangeLabel.Name = "rangeLabel";
            rangeLabel.Size = new Size(32, 17);
            rangeLabel.TabIndex = 0;
            rangeLabel.Text = "范围";
            // 
            // randomLetterCheck
            // 
            randomLetterCheck.AutoSize = true;
            randomLetterCheck.BackColor = Color.FromArgb(245, 247, 250);
            randomLetterCheck.Checked = true;
            randomLetterCheck.CheckState = CheckState.Checked;
            randomLetterCheck.ForeColor = SystemColors.ControlText;
            randomLetterCheck.Location = new Point(38, 8);
            randomLetterCheck.Margin = new Padding(0, 8, 14, 0);
            randomLetterCheck.Name = "randomLetterCheck";
            randomLetterCheck.Size = new Size(51, 21);
            randomLetterCheck.TabIndex = 1;
            randomLetterCheck.Text = "字母";
            randomLetterCheck.UseVisualStyleBackColor = false;
            // 
            // randomNumberCheck
            // 
            randomNumberCheck.AutoSize = true;
            randomNumberCheck.BackColor = Color.FromArgb(245, 247, 250);
            randomNumberCheck.ForeColor = SystemColors.ControlText;
            randomNumberCheck.Location = new Point(103, 8);
            randomNumberCheck.Margin = new Padding(0, 8, 14, 0);
            randomNumberCheck.Name = "randomNumberCheck";
            randomNumberCheck.Size = new Size(51, 21);
            randomNumberCheck.TabIndex = 2;
            randomNumberCheck.Text = "数字";
            randomNumberCheck.UseVisualStyleBackColor = false;
            // 
            // randomSymbolCheck
            // 
            randomSymbolCheck.AutoSize = true;
            randomSymbolCheck.BackColor = Color.FromArgb(245, 247, 250);
            randomSymbolCheck.ForeColor = SystemColors.ControlText;
            randomSymbolCheck.Location = new Point(168, 8);
            randomSymbolCheck.Margin = new Padding(0, 8, 14, 0);
            randomSymbolCheck.Name = "randomSymbolCheck";
            randomSymbolCheck.Size = new Size(51, 21);
            randomSymbolCheck.TabIndex = 3;
            randomSymbolCheck.Text = "符号";
            randomSymbolCheck.UseVisualStyleBackColor = false;
            // 
            // customLabel
            // 
            customLabel.AutoSize = true;
            customLabel.BackColor = Color.FromArgb(245, 247, 250);
            customLabel.ForeColor = SystemColors.ControlText;
            customLabel.Location = new Point(233, 10);
            customLabel.Margin = new Padding(0, 10, 6, 0);
            customLabel.Name = "customLabel";
            customLabel.Size = new Size(44, 17);
            customLabel.TabIndex = 4;
            customLabel.Text = "自定义";
            // 
            // randomCustomBox
            // 
            randomCustomBox.BackColor = SystemColors.Window;
            randomCustomBox.ForeColor = SystemColors.WindowText;
            randomCustomBox.Location = new Point(283, 6);
            randomCustomBox.Margin = new Padding(0, 6, 12, 0);
            randomCustomBox.Name = "randomCustomBox";
            randomCustomBox.PlaceholderText = "可追加，如 KMRA";
            randomCustomBox.Size = new Size(220, 23);
            randomCustomBox.TabIndex = 5;
            // 
            // randomCountRow
            // 
            randomCountRow.BackColor = Color.FromArgb(245, 247, 250);
            randomCountRow.Controls.Add(randomLabel);
            randomCountRow.Controls.Add(groupSizeLabel);
            randomCountRow.Controls.Add(randomEachBox);
            randomCountRow.Controls.Add(groupCountLabel);
            randomCountRow.Controls.Add(randomGroupBox);
            randomCountRow.Controls.Add(randomUniqueCheck);
            randomCountRow.Controls.Add(randomUniqueGroupCheck);
            randomCountRow.Controls.Add(randomGenerateBtn);
            randomCountRow.Dock = DockStyle.Top;
            randomCountRow.ForeColor = SystemColors.ControlText;
            randomCountRow.Location = new Point(0, 2);
            randomCountRow.Name = "randomCountRow";
            randomCountRow.Size = new Size(807, 40);
            randomCountRow.TabIndex = 1;
            randomCountRow.WrapContents = false;
            // 
            // randomLabel
            // 
            randomLabel.AutoSize = true;
            randomLabel.BackColor = Color.FromArgb(245, 247, 250);
            randomLabel.ForeColor = SystemColors.ControlText;
            randomLabel.Location = new Point(0, 10);
            randomLabel.Margin = new Padding(0, 10, 6, 0);
            randomLabel.Name = "randomLabel";
            randomLabel.Size = new Size(32, 17);
            randomLabel.TabIndex = 0;
            randomLabel.Text = "随机";
            // 
            // groupSizeLabel
            // 
            groupSizeLabel.AutoSize = true;
            groupSizeLabel.BackColor = Color.FromArgb(245, 247, 250);
            groupSizeLabel.ForeColor = SystemColors.ControlText;
            groupSizeLabel.Location = new Point(38, 10);
            groupSizeLabel.Margin = new Padding(0, 10, 6, 0);
            groupSizeLabel.Name = "groupSizeLabel";
            groupSizeLabel.Size = new Size(32, 17);
            groupSizeLabel.TabIndex = 1;
            groupSizeLabel.Text = "每组";
            // 
            // randomEachBox
            // 
            randomEachBox.BackColor = SystemColors.Window;
            randomEachBox.ForeColor = SystemColors.WindowText;
            randomEachBox.Location = new Point(76, 6);
            randomEachBox.Margin = new Padding(0, 6, 12, 0);
            randomEachBox.Maximum = new decimal(new int[] { 40, 0, 0, 0 });
            randomEachBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            randomEachBox.Name = "randomEachBox";
            randomEachBox.Size = new Size(68, 23);
            randomEachBox.TabIndex = 2;
            randomEachBox.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // groupCountLabel
            // 
            groupCountLabel.AutoSize = true;
            groupCountLabel.BackColor = Color.FromArgb(245, 247, 250);
            groupCountLabel.ForeColor = SystemColors.ControlText;
            groupCountLabel.Location = new Point(156, 10);
            groupCountLabel.Margin = new Padding(0, 10, 6, 0);
            groupCountLabel.Name = "groupCountLabel";
            groupCountLabel.Size = new Size(32, 17);
            groupCountLabel.TabIndex = 3;
            groupCountLabel.Text = "组数";
            // 
            // randomGroupBox
            // 
            randomGroupBox.BackColor = SystemColors.Window;
            randomGroupBox.ForeColor = SystemColors.WindowText;
            randomGroupBox.Location = new Point(194, 6);
            randomGroupBox.Margin = new Padding(0, 6, 12, 0);
            randomGroupBox.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            randomGroupBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            randomGroupBox.Name = "randomGroupBox";
            randomGroupBox.Size = new Size(68, 23);
            randomGroupBox.TabIndex = 4;
            randomGroupBox.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // randomUniqueCheck
            // 
            randomUniqueCheck.AutoSize = true;
            randomUniqueCheck.BackColor = Color.FromArgb(245, 247, 250);
            randomUniqueCheck.ForeColor = SystemColors.ControlText;
            randomUniqueCheck.Location = new Point(274, 8);
            randomUniqueCheck.Margin = new Padding(0, 8, 14, 0);
            randomUniqueCheck.Name = "randomUniqueCheck";
            randomUniqueCheck.Size = new Size(87, 21);
            randomUniqueCheck.TabIndex = 5;
            randomUniqueCheck.Text = "组内不重复";
            randomUniqueCheck.UseVisualStyleBackColor = false;
            // 
            // randomUniqueGroupCheck
            // 
            randomUniqueGroupCheck.AutoSize = true;
            randomUniqueGroupCheck.BackColor = Color.FromArgb(245, 247, 250);
            randomUniqueGroupCheck.ForeColor = SystemColors.ControlText;
            randomUniqueGroupCheck.Location = new Point(375, 8);
            randomUniqueGroupCheck.Margin = new Padding(0, 8, 14, 0);
            randomUniqueGroupCheck.Name = "randomUniqueGroupCheck";
            randomUniqueGroupCheck.Size = new Size(75, 21);
            randomUniqueGroupCheck.TabIndex = 6;
            randomUniqueGroupCheck.Text = "组不重复";
            randomUniqueGroupCheck.UseVisualStyleBackColor = false;
            // 
            // randomGenerateBtn
            // 
            randomGenerateBtn.BackColor = Color.FromArgb(245, 247, 250);
            randomGenerateBtn.ForeColor = SystemColors.ControlText;
            randomGenerateBtn.Location = new Point(472, 6);
            randomGenerateBtn.Margin = new Padding(8, 6, 0, 0);
            randomGenerateBtn.Name = "randomGenerateBtn";
            randomGenerateBtn.Size = new Size(104, 28);
            randomGenerateBtn.TabIndex = 7;
            randomGenerateBtn.Text = "生成";
            randomGenerateBtn.UseVisualStyleBackColor = false;
            // 
            // fieldGrid
            // 
            fieldGrid.BackColor = Color.FromArgb(245, 247, 250);
            fieldGrid.ColumnCount = 8;
            fieldGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 54F));
            fieldGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            fieldGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 54F));
            fieldGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 54F));
            fieldGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            fieldGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 54F));
            fieldGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 54F));
            fieldGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            fieldGrid.Controls.Add(nameLabel, 0, 0);
            fieldGrid.Controls.Add(nameBox, 1, 0);
            fieldGrid.Controls.Add(speedLabel, 3, 0);
            fieldGrid.Controls.Add(speedBox, 4, 0);
            fieldGrid.Controls.Add(frequencyLabel, 6, 0);
            fieldGrid.Controls.Add(frequencyBox, 7, 0);
            fieldGrid.Controls.Add(waveformLabel, 0, 1);
            fieldGrid.Controls.Add(waveformBox, 1, 1);
            fieldGrid.Controls.Add(volumeLabel, 3, 1);
            fieldGrid.Controls.Add(volumeBox, 4, 1);
            fieldGrid.Controls.Add(snrLabel, 6, 1);
            fieldGrid.Controls.Add(snrBox, 7, 1);
            fieldGrid.Controls.Add(channelOptions, 0, 2);
            fieldGrid.Controls.Add(sourceHintLabel, 0, 3);
            fieldGrid.Dock = DockStyle.Top;
            fieldGrid.ForeColor = SystemColors.ControlText;
            fieldGrid.Location = new Point(16, 40);
            fieldGrid.Name = "fieldGrid";
            fieldGrid.Padding = new Padding(0, 0, 0, 2);
            fieldGrid.RowCount = 4;
            fieldGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            fieldGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            fieldGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            fieldGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            fieldGrid.Size = new Size(807, 194);
            fieldGrid.TabIndex = 4;
            // 
            // nameLabel
            // 
            nameLabel.BackColor = Color.FromArgb(245, 247, 250);
            nameLabel.Dock = DockStyle.Fill;
            nameLabel.ForeColor = SystemColors.ControlText;
            nameLabel.Location = new Point(0, 8);
            nameLabel.Margin = new Padding(0, 8, 8, 8);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(46, 32);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "名称";
            nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nameBox
            // 
            nameBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            nameBox.BackColor = SystemColors.Window;
            nameBox.ForeColor = SystemColors.WindowText;
            nameBox.Location = new Point(54, 12);
            nameBox.Margin = new Padding(0, 12, 16, 12);
            nameBox.Name = "nameBox";
            nameBox.Size = new Size(163, 23);
            nameBox.TabIndex = 1;
            // 
            // speedLabel
            // 
            speedLabel.BackColor = Color.FromArgb(245, 247, 250);
            speedLabel.Dock = DockStyle.Fill;
            speedLabel.ForeColor = SystemColors.ControlText;
            speedLabel.Location = new Point(287, 8);
            speedLabel.Margin = new Padding(0, 8, 8, 8);
            speedLabel.Name = "speedLabel";
            speedLabel.Size = new Size(46, 32);
            speedLabel.TabIndex = 2;
            speedLabel.Text = "速度";
            speedLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // speedBox
            // 
            speedBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            speedBox.BackColor = SystemColors.Window;
            speedBox.ForeColor = SystemColors.WindowText;
            speedBox.Location = new Point(341, 12);
            speedBox.Margin = new Padding(0, 12, 16, 12);
            speedBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            speedBox.Name = "speedBox";
            speedBox.Size = new Size(163, 23);
            speedBox.TabIndex = 3;
            speedBox.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // frequencyLabel
            // 
            frequencyLabel.BackColor = Color.FromArgb(245, 247, 250);
            frequencyLabel.Dock = DockStyle.Fill;
            frequencyLabel.ForeColor = SystemColors.ControlText;
            frequencyLabel.Location = new Point(574, 8);
            frequencyLabel.Margin = new Padding(0, 8, 8, 8);
            frequencyLabel.Name = "frequencyLabel";
            frequencyLabel.Size = new Size(46, 32);
            frequencyLabel.TabIndex = 4;
            frequencyLabel.Text = "频率";
            frequencyLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frequencyBox
            // 
            frequencyBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            frequencyBox.BackColor = SystemColors.Window;
            frequencyBox.ForeColor = SystemColors.WindowText;
            frequencyBox.Location = new Point(628, 12);
            frequencyBox.Margin = new Padding(0, 12, 16, 12);
            frequencyBox.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            frequencyBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            frequencyBox.Name = "frequencyBox";
            frequencyBox.Size = new Size(163, 23);
            frequencyBox.TabIndex = 5;
            frequencyBox.Value = new decimal(new int[] { 600, 0, 0, 0 });
            // 
            // waveformLabel
            // 
            waveformLabel.BackColor = Color.FromArgb(245, 247, 250);
            waveformLabel.Dock = DockStyle.Fill;
            waveformLabel.ForeColor = SystemColors.ControlText;
            waveformLabel.Location = new Point(0, 56);
            waveformLabel.Margin = new Padding(0, 8, 8, 8);
            waveformLabel.Name = "waveformLabel";
            waveformLabel.Size = new Size(46, 32);
            waveformLabel.TabIndex = 6;
            waveformLabel.Text = "波形";
            waveformLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // waveformBox
            // 
            waveformBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            waveformBox.BackColor = SystemColors.Window;
            waveformBox.DropDownStyle = ComboBoxStyle.DropDownList;
            waveformBox.ForeColor = SystemColors.WindowText;
            waveformBox.Items.AddRange(new object[] { "正弦波", "锯齿波", "方波" });
            waveformBox.Location = new Point(54, 60);
            waveformBox.Margin = new Padding(0, 12, 16, 12);
            waveformBox.Name = "waveformBox";
            waveformBox.Size = new Size(163, 25);
            waveformBox.TabIndex = 7;
            // 
            // volumeLabel
            // 
            volumeLabel.BackColor = Color.FromArgb(245, 247, 250);
            volumeLabel.Dock = DockStyle.Fill;
            volumeLabel.ForeColor = SystemColors.ControlText;
            volumeLabel.Location = new Point(287, 56);
            volumeLabel.Margin = new Padding(0, 8, 8, 8);
            volumeLabel.Name = "volumeLabel";
            volumeLabel.Size = new Size(46, 32);
            volumeLabel.TabIndex = 8;
            volumeLabel.Text = "音量";
            volumeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // volumeBox
            // 
            volumeBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            volumeBox.BackColor = SystemColors.Window;
            volumeBox.ForeColor = SystemColors.WindowText;
            volumeBox.Location = new Point(341, 60);
            volumeBox.Margin = new Padding(0, 12, 16, 12);
            volumeBox.Name = "volumeBox";
            volumeBox.Size = new Size(163, 23);
            volumeBox.TabIndex = 9;
            volumeBox.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // snrLabel
            // 
            snrLabel.BackColor = Color.FromArgb(245, 247, 250);
            snrLabel.Dock = DockStyle.Fill;
            snrLabel.ForeColor = SystemColors.ControlText;
            snrLabel.Location = new Point(574, 56);
            snrLabel.Margin = new Padding(0, 8, 8, 8);
            snrLabel.Name = "snrLabel";
            snrLabel.Size = new Size(46, 32);
            snrLabel.TabIndex = 10;
            snrLabel.Text = "信噪比";
            snrLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // snrBox
            // 
            snrBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            snrBox.BackColor = SystemColors.Window;
            snrBox.ForeColor = SystemColors.WindowText;
            snrBox.Location = new Point(628, 60);
            snrBox.Margin = new Padding(0, 12, 16, 12);
            snrBox.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            snrBox.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            snrBox.Name = "snrBox";
            snrBox.Size = new Size(163, 23);
            snrBox.TabIndex = 11;
            // 
            // channelOptions
            // 
            channelOptions.BackColor = Color.FromArgb(245, 247, 250);
            fieldGrid.SetColumnSpan(channelOptions, 8);
            channelOptions.Controls.Add(primaryCheck);
            channelOptions.Controls.Add(loopCheck);
            channelOptions.Controls.Add(muteCheck);
            channelOptions.Controls.Add(noiseCheck);
            channelOptions.Controls.Add(encodingBtn);
            channelOptions.Dock = DockStyle.Fill;
            channelOptions.ForeColor = SystemColors.ControlText;
            channelOptions.Location = new Point(3, 99);
            channelOptions.Name = "channelOptions";
            channelOptions.Padding = new Padding(0, 6, 0, 0);
            channelOptions.Size = new Size(801, 42);
            channelOptions.TabIndex = 12;
            channelOptions.WrapContents = false;
            // 
            // primaryCheck
            // 
            primaryCheck.AutoSize = true;
            primaryCheck.BackColor = Color.FromArgb(245, 247, 250);
            primaryCheck.ForeColor = SystemColors.ControlText;
            primaryCheck.Location = new Point(0, 11);
            primaryCheck.Margin = new Padding(0, 5, 20, 0);
            primaryCheck.Name = "primaryCheck";
            primaryCheck.Size = new Size(51, 21);
            primaryCheck.TabIndex = 0;
            primaryCheck.Text = "主路";
            primaryCheck.UseVisualStyleBackColor = false;
            // 
            // loopCheck
            // 
            loopCheck.AutoSize = true;
            loopCheck.BackColor = Color.FromArgb(245, 247, 250);
            loopCheck.ForeColor = SystemColors.ControlText;
            loopCheck.Location = new Point(71, 11);
            loopCheck.Margin = new Padding(0, 5, 20, 0);
            loopCheck.Name = "loopCheck";
            loopCheck.Size = new Size(51, 21);
            loopCheck.TabIndex = 1;
            loopCheck.Text = "循环";
            loopCheck.UseVisualStyleBackColor = false;
            // 
            // muteCheck
            // 
            muteCheck.AutoSize = true;
            muteCheck.BackColor = Color.FromArgb(245, 247, 250);
            muteCheck.ForeColor = SystemColors.ControlText;
            muteCheck.Location = new Point(142, 11);
            muteCheck.Margin = new Padding(0, 5, 20, 0);
            muteCheck.Name = "muteCheck";
            muteCheck.Size = new Size(51, 21);
            muteCheck.TabIndex = 2;
            muteCheck.Text = "静音";
            muteCheck.UseVisualStyleBackColor = false;
            // 
            // noiseCheck
            // 
            noiseCheck.AutoSize = true;
            noiseCheck.BackColor = Color.FromArgb(245, 247, 250);
            noiseCheck.ForeColor = SystemColors.ControlText;
            noiseCheck.Location = new Point(213, 11);
            noiseCheck.Margin = new Padding(0, 5, 20, 0);
            noiseCheck.Name = "noiseCheck";
            noiseCheck.Size = new Size(51, 21);
            noiseCheck.TabIndex = 3;
            noiseCheck.Text = "噪声";
            noiseCheck.UseVisualStyleBackColor = false;
            // 
            // encodingBtn
            // 
            encodingBtn.BackColor = Color.FromArgb(245, 247, 250);
            encodingBtn.ForeColor = SystemColors.ControlText;
            encodingBtn.Location = new Point(284, 6);
            encodingBtn.Margin = new Padding(0, 0, 12, 0);
            encodingBtn.Name = "encodingBtn";
            encodingBtn.Size = new Size(104, 28);
            encodingBtn.TabIndex = 4;
            encodingBtn.Text = "编码设置";
            encodingBtn.UseVisualStyleBackColor = false;
            // 
            // sourceHintLabel
            // 
            sourceHintLabel.BackColor = Color.FromArgb(245, 247, 250);
            fieldGrid.SetColumnSpan(sourceHintLabel, 8);
            sourceHintLabel.Dock = DockStyle.Fill;
            sourceHintLabel.ForeColor = SystemColors.GrayText;
            sourceHintLabel.Location = new Point(3, 144);
            sourceHintLabel.Name = "sourceHintLabel";
            sourceHintLabel.Size = new Size(801, 50);
            sourceHintLabel.TabIndex = 13;
            sourceHintLabel.Text = "每路可选择 txt、粘贴报文，或按自己的组数和字符范围随机生成。";
            sourceHintLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // bottomPanel
            // 
            bottomPanel.BackColor = Color.FromArgb(255, 255, 255);
            bottomPanel.Controls.Add(statusLabel);
            bottomPanel.Controls.Add(transportButtons);
            bottomPanel.Controls.Add(presetButtons);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.ForeColor = SystemColors.ControlText;
            bottomPanel.Location = new Point(0, 888);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Padding = new Padding(16, 10, 16, 10);
            bottomPanel.Size = new Size(1440, 72);
            bottomPanel.TabIndex = 1;
            // 
            // statusLabel
            // 
            statusLabel.BackColor = Color.FromArgb(255, 255, 255);
            statusLabel.Dock = DockStyle.Fill;
            statusLabel.ForeColor = SystemColors.ControlText;
            statusLabel.Location = new Point(448, 10);
            statusLabel.Name = "statusLabel";
            statusLabel.Padding = new Padding(8, 0, 0, 0);
            statusLabel.Size = new Size(628, 52);
            statusLabel.TabIndex = 2;
            statusLabel.Text = "已停止";
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // transportButtons
            // 
            transportButtons.AutoSize = true;
            transportButtons.BackColor = Color.FromArgb(255, 255, 255);
            transportButtons.Controls.Add(playBtn);
            transportButtons.Controls.Add(pauseBtn);
            transportButtons.Controls.Add(stopBtn);
            transportButtons.Controls.Add(replayBtn);
            transportButtons.Dock = DockStyle.Left;
            transportButtons.ForeColor = SystemColors.ControlText;
            transportButtons.Location = new Point(16, 10);
            transportButtons.Name = "transportButtons";
            transportButtons.Padding = new Padding(0, 8, 0, 0);
            transportButtons.Size = new Size(432, 52);
            transportButtons.TabIndex = 1;
            transportButtons.WrapContents = false;
            // 
            // playBtn
            // 
            playBtn.BackColor = Color.FromArgb(255, 255, 255);
            playBtn.ForeColor = SystemColors.ControlText;
            playBtn.Location = new Point(0, 8);
            playBtn.Margin = new Padding(0, 0, 12, 0);
            playBtn.Name = "playBtn";
            playBtn.Size = new Size(96, 36);
            playBtn.TabIndex = 0;
            playBtn.Text = "播放";
            playBtn.UseVisualStyleBackColor = false;
            // 
            // pauseBtn
            // 
            pauseBtn.BackColor = Color.FromArgb(255, 255, 255);
            pauseBtn.ForeColor = SystemColors.ControlText;
            pauseBtn.Location = new Point(108, 8);
            pauseBtn.Margin = new Padding(0, 0, 12, 0);
            pauseBtn.Name = "pauseBtn";
            pauseBtn.Size = new Size(96, 36);
            pauseBtn.TabIndex = 1;
            pauseBtn.Text = "暂停";
            pauseBtn.UseVisualStyleBackColor = false;
            // 
            // stopBtn
            // 
            stopBtn.BackColor = Color.FromArgb(255, 255, 255);
            stopBtn.ForeColor = SystemColors.ControlText;
            stopBtn.Location = new Point(216, 8);
            stopBtn.Margin = new Padding(0, 0, 12, 0);
            stopBtn.Name = "stopBtn";
            stopBtn.Size = new Size(96, 36);
            stopBtn.TabIndex = 2;
            stopBtn.Text = "停止";
            stopBtn.UseVisualStyleBackColor = false;
            // 
            // replayBtn
            // 
            replayBtn.BackColor = Color.FromArgb(255, 255, 255);
            replayBtn.ForeColor = SystemColors.ControlText;
            replayBtn.Location = new Point(324, 8);
            replayBtn.Margin = new Padding(0, 0, 12, 0);
            replayBtn.Name = "replayBtn";
            replayBtn.Size = new Size(96, 36);
            replayBtn.TabIndex = 3;
            replayBtn.Text = "重播";
            replayBtn.UseVisualStyleBackColor = false;
            // 
            // presetButtons
            // 
            presetButtons.AutoSize = true;
            presetButtons.BackColor = Color.FromArgb(255, 255, 255);
            presetButtons.Controls.Add(openBtn);
            presetButtons.Controls.Add(saveBtn);
            presetButtons.Controls.Add(exportBtn);
            presetButtons.Dock = DockStyle.Right;
            presetButtons.ForeColor = SystemColors.ControlText;
            presetButtons.Location = new Point(1076, 10);
            presetButtons.Name = "presetButtons";
            presetButtons.Padding = new Padding(0, 8, 0, 0);
            presetButtons.Size = new Size(348, 52);
            presetButtons.TabIndex = 0;
            presetButtons.WrapContents = false;
            // 
            // openBtn
            // 
            openBtn.BackColor = Color.FromArgb(255, 255, 255);
            openBtn.ForeColor = SystemColors.ControlText;
            openBtn.Location = new Point(0, 8);
            openBtn.Margin = new Padding(0, 0, 12, 0);
            openBtn.Name = "openBtn";
            openBtn.Size = new Size(104, 36);
            openBtn.TabIndex = 0;
            openBtn.Text = "打开配置";
            openBtn.UseVisualStyleBackColor = false;
            // 
            // saveBtn
            // 
            saveBtn.BackColor = Color.FromArgb(255, 255, 255);
            saveBtn.ForeColor = SystemColors.ControlText;
            saveBtn.Location = new Point(116, 8);
            saveBtn.Margin = new Padding(0, 0, 12, 0);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(104, 36);
            saveBtn.TabIndex = 1;
            saveBtn.Text = "保存配置";
            saveBtn.UseVisualStyleBackColor = false;
            // 
            // exportBtn
            // 
            exportBtn.BackColor = Color.FromArgb(255, 255, 255);
            exportBtn.ForeColor = SystemColors.ControlText;
            exportBtn.Location = new Point(232, 8);
            exportBtn.Margin = new Padding(0, 0, 12, 0);
            exportBtn.Name = "exportBtn";
            exportBtn.Size = new Size(104, 36);
            exportBtn.TabIndex = 2;
            exportBtn.Text = "导出音频";
            exportBtn.UseVisualStyleBackColor = false;
            // 
            // MultiChannelPlayer
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1440, 960);
            Controls.Add(mainLayout);
            Controls.Add(bottomPanel);
            Font = new Font("Microsoft YaHei UI", 9F);
            ForeColor = SystemColors.ControlText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1280, 860);
            Name = "MultiChannelPlayer";
            StartPosition = FormStartPosition.CenterParent;
            Text = "多路播放";
            mainLayout.ResumeLayout(false);
            channelGroup.ResumeLayout(false);
            channelButtons.ResumeLayout(false);
            editorGroup.ResumeLayout(false);
            editorGroup.PerformLayout();
            filePanel.ResumeLayout(false);
            filePanel.PerformLayout();
            randomPanel.ResumeLayout(false);
            randomRangeRow.ResumeLayout(false);
            randomRangeRow.PerformLayout();
            randomCountRow.ResumeLayout(false);
            randomCountRow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)randomEachBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)randomGroupBox).EndInit();
            fieldGrid.ResumeLayout(false);
            fieldGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)speedBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)frequencyBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)volumeBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)snrBox).EndInit();
            channelOptions.ResumeLayout(false);
            channelOptions.PerformLayout();
            bottomPanel.ResumeLayout(false);
            bottomPanel.PerformLayout();
            transportButtons.ResumeLayout(false);
            presetButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private ColumnHeader nameColumn = null!;
        private ColumnHeader roleColumn = null!;
        private ColumnHeader frequencyColumn = null!;
        private ColumnHeader speedColumn = null!;
        private ColumnHeader encodingColumn = null!;
        private ColumnHeader volumeColumn = null!;
        private ColumnHeader currentGroupColumn = null!;
        private TableLayoutPanel mainLayout = null!;
        private GroupBox channelGroup = null!;
        private ListView channelList = null!;
        private FlowLayoutPanel channelButtons = null!;
        private Button addBtn = null!;
        private Button deleteBtn = null!;
        private Button upBtn = null!;
        private Button downBtn = null!;
        private GroupBox editorGroup = null!;
        private TextBox textBox = null!;
        private Label previewLabel = null!;
        private TableLayoutPanel filePanel = null!;
        private Button loadFileBtn = null!;
        private TextBox filePathBox = null!;
        private Label fileLabel = null!;
        private Panel randomPanel = null!;
        private FlowLayoutPanel randomRangeRow = null!;
        private Label rangeLabel = null!;
        private CheckBox randomLetterCheck = null!;
        private CheckBox randomNumberCheck = null!;
        private CheckBox randomSymbolCheck = null!;
        private Label customLabel = null!;
        private TextBox randomCustomBox = null!;
        private FlowLayoutPanel randomCountRow = null!;
        private Label randomLabel = null!;
        private Label groupSizeLabel = null!;
        private NumericUpDown randomEachBox = null!;
        private Label groupCountLabel = null!;
        private NumericUpDown randomGroupBox = null!;
        private CheckBox randomUniqueCheck = null!;
        private CheckBox randomUniqueGroupCheck = null!;
        private Button randomGenerateBtn = null!;
        private TableLayoutPanel fieldGrid = null!;
        private Label nameLabel = null!;
        private TextBox nameBox = null!;
        private Label speedLabel = null!;
        private NumericUpDown speedBox = null!;
        private Label frequencyLabel = null!;
        private NumericUpDown frequencyBox = null!;
        private Label waveformLabel = null!;
        private ComboBox waveformBox = null!;
        private Label volumeLabel = null!;
        private NumericUpDown volumeBox = null!;
        private Label snrLabel = null!;
        private NumericUpDown snrBox = null!;
        private FlowLayoutPanel channelOptions = null!;
        private CheckBox primaryCheck = null!;
        private CheckBox loopCheck = null!;
        private CheckBox muteCheck = null!;
        private CheckBox noiseCheck = null!;
        private Button encodingBtn = null!;
        private Label sourceHintLabel = null!;
        private Panel bottomPanel = null!;
        private FlowLayoutPanel presetButtons = null!;
        private Button openBtn = null!;
        private Button saveBtn = null!;
        private Button exportBtn = null!;
        private FlowLayoutPanel transportButtons = null!;
        private Button playBtn = null!;
        private Button pauseBtn = null!;
        private Button stopBtn = null!;
        private Button replayBtn = null!;
        private Label statusLabel = null!;
    }
}
