using Newtonsoft.Json;
using NAudio.Wave;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace CW
{
    [DesignerCategory("Form")]
    public partial class MultiChannelPlayer : Form
    {
        private const int MaxChannels = 8;
        private static readonly (string Name, int Weight, int Minimum)[] ChannelColumns =
        [
            ("名称", 16, 72),
            ("角色", 9, 44),
            ("频率", 12, 52),
            ("速度", 10, 48),
            ("编码", 12, 60),
            ("音量", 9, 48),
            ("当前组", 32, 80),
        ];
        private static readonly int[] FrequencySteps = [600, 800, 1000, 500, 1200, 700, 900, 450];

        private readonly List<ChannelLane> lanes = [];
        private readonly MultiChannelMixer mixer = new();
        private readonly WaveOutEvent waveOut = new();
        private readonly System.Windows.Forms.Timer groupTimer = new() { Interval = 200 };
        private readonly ListView channelList = new();
        private readonly Button addBtn = new();
        private readonly Button deleteBtn = new();
        private readonly Button upBtn = new();
        private readonly Button downBtn = new();
        private readonly TextBox nameBox = new();
        private readonly CheckBox primaryCheck = new();
        private readonly CheckBox loopCheck = new();
        private readonly CheckBox muteCheck = new();
        private readonly NumericUpDown speedBox = new();
        private readonly NumericUpDown frequencyBox = new();
        private readonly ComboBox waveformBox = new();
        private readonly NumericUpDown volumeBox = new();
        private readonly CheckBox noiseCheck = new();
        private readonly NumericUpDown snrBox = new();
        private readonly Button encodingBtn = new();
        private readonly NumericUpDown randomEachBox = new();
        private readonly NumericUpDown randomGroupBox = new();
        private readonly CheckBox randomUniqueCheck = new();
        private readonly CheckBox randomUniqueGroupCheck = new();
        private readonly CheckBox randomLetterCheck = new();
        private readonly CheckBox randomNumberCheck = new();
        private readonly CheckBox randomSymbolCheck = new();
        private readonly TextBox randomCustomBox = new();
        private readonly Button randomGenerateBtn = new();
        private readonly TextBox textBox = new();
        private readonly TextBox filePathBox = new();
        private readonly Button loadFileBtn = new();
        private readonly Label previewLabel = new();
        private readonly Button playBtn = new();
        private readonly Button pauseBtn = new();
        private readonly Button stopBtn = new();
        private readonly Button replayBtn = new();
        private readonly Button openBtn = new();
        private readonly Button saveBtn = new();
        private readonly Button exportBtn = new();
        private readonly Label statusLabel = new();
        private CancellationTokenSource? exportCts;
        private bool exporting;
        private bool suppressEditor;
        private bool audioReleased;
        private bool waveReady;
        private int sessionId;
        private bool fittingColumns;
        private PlayState state = PlayState.Stopped;

        public MultiChannelPlayer()
        {
            InitializeComponent();
            if (IsInDesigner)
                return;

            mixer.SetUiContext(SynchronizationContext.Current);
            groupTimer.Tick += (_, _) => RefreshCurrentGroups();
            groupTimer.Start();
            // 默认创建空通道，避免启动时自动选中文本文件。用户选择文件后才加载报文。
            AddLane(null, isPrimary: true, loop: false);
            AddLane(null, isPrimary: false, loop: true);
            channelList.Items[0].Selected = true;
            UpdateTransport();
            Shown += (_, _) => FitChannelColumns();
        }

        private bool IsInDesigner => LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode;
        private void BuildLayout()
        {
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Microsoft YaHei UI", 9F);

            var bottom = new Panel { Dock = DockStyle.Bottom, Height = 72, Padding = new Padding(16, 10, 16, 10), BackColor = Color.White };
            // 使用百分比分栏而不是 SplitContainer，避免设计器预览尺寸较小时触发分隔线约束异常。
            var split = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = BackColor,
                Padding = new Padding(16, 16, 16, 12),
            };
            split.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            split.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            split.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            var left = new GroupBox { Dock = DockStyle.Fill, Text = "播放通道", Padding = new Padding(12, 22, 12, 12) };
            var editor = new GroupBox { Dock = DockStyle.Fill, Text = "通道设置与报文", Padding = new Padding(16, 24, 16, 12) };
            var fields = CreateFieldGrid();

            previewLabel.Text = "预览";
            previewLabel.Dock = DockStyle.Top;
            previewLabel.Height = 30;
            previewLabel.TextAlign = ContentAlignment.BottomLeft;
            previewLabel.Padding = new Padding(0, 0, 0, 6);

            var fileRow = new Panel { Dock = DockStyle.Top, Height = 42, Padding = new Padding(0, 4, 0, 4) };
            var fileLabel = new Label { Text = "报文文件", Dock = DockStyle.Left, Width = 84, TextAlign = ContentAlignment.MiddleLeft };
            StyleButton(loadFileBtn, "选择文件", 104);
            loadFileBtn.Dock = DockStyle.Right;
            filePathBox.Dock = DockStyle.Fill;
            filePathBox.ReadOnly = true;
            filePathBox.PlaceholderText = "未选择 txt 文件";
            fileRow.Controls.Add(loadFileBtn);
            fileRow.Controls.Add(filePathBox);
            fileRow.Controls.Add(fileLabel);
            loadFileBtn.Click += (_, _) => ChooseTextFile();

            StyleButton(playBtn, "播放");
            StyleButton(pauseBtn, "暂停");
            StyleButton(stopBtn, "停止");
            StyleButton(replayBtn, "重播");
            StyleButton(openBtn, "打开配置", 104);
            StyleButton(saveBtn, "保存配置", 104);
            StyleButton(exportBtn, "导出音频", 104);
            statusLabel.Text = "已停止";
            statusLabel.Dock = DockStyle.Fill;
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            statusLabel.Padding = new Padding(8, 0, 0, 0);
            playBtn.Click += (_, _) => Play();
            pauseBtn.Click += (_, _) => Pause();
            stopBtn.Click += (_, _) => StopPlayback();
            replayBtn.Click += (_, _) => Replay();
            openBtn.Click += (_, _) => OpenPreset();
            saveBtn.Click += (_, _) => SavePreset();
            exportBtn.Click += (_, _) => ExportOrCancel();

            var transport = new FlowLayoutPanel { Dock = DockStyle.Left, AutoSize = true, WrapContents = false, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(0, 8, 0, 0) };
            transport.Controls.AddRange([playBtn, pauseBtn, stopBtn, replayBtn]);
            var fileButtons = new FlowLayoutPanel { Dock = DockStyle.Right, AutoSize = true, WrapContents = false, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(0, 8, 0, 0) };
            fileButtons.Controls.AddRange([openBtn, saveBtn, exportBtn]);
            bottom.Controls.Add(fileButtons);
            bottom.Controls.Add(transport);
            bottom.Controls.Add(statusLabel);

            channelList.Dock = DockStyle.Fill;
            channelList.View = View.Details;
            channelList.FullRowSelect = true;
            channelList.HideSelection = false;
            channelList.MultiSelect = false;
            channelList.GridLines = true;
            channelList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            foreach (var column in ChannelColumns)
            {
                var header = channelList.Columns.Add(column.Name, column.Minimum);
                header.TextAlign = HorizontalAlignment.Center;
            }
            channelList.OwnerDraw = true;
            channelList.DrawColumnHeader += ChannelList_DrawColumnHeader;
            channelList.DrawItem += (_, e) => e.DrawDefault = false;
            channelList.DrawSubItem += ChannelList_DrawSubItem;
            channelList.Resize += (_, _) => FitChannelColumns();
            channelList.SelectedIndexChanged += (_, _) => LoadEditor();

            var listButtons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 54, WrapContents = false, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(0, 10, 0, 0) };
            StyleButton(addBtn, "添加", 82);
            StyleButton(deleteBtn, "删除", 82);
            StyleButton(upBtn, "上移", 82);
            StyleButton(downBtn, "下移", 82);
            addBtn.Click += (_, _) => AddLane();
            deleteBtn.Click += (_, _) => DeleteSelected();
            upBtn.Click += (_, _) => MoveSelected(-1);
            downBtn.Click += (_, _) => MoveSelected(1);
            listButtons.Controls.AddRange([addBtn, deleteBtn, upBtn, downBtn]);
            left.Controls.Add(channelList);
            left.Controls.Add(listButtons);

            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.AcceptsReturn = true;
            editor.Controls.Add(textBox);
            editor.Controls.Add(previewLabel);
            editor.Controls.Add(fileRow);
            editor.Controls.Add(CreateRandomPanel());
            editor.Controls.Add(fields);

            nameBox.TextChanged += (_, _) => ApplyName();
            primaryCheck.CheckedChanged += (_, _) => ApplyPrimary();
            loopCheck.CheckedChanged += (_, _) => ApplyLoop();
            muteCheck.CheckedChanged += (_, _) => ApplyMute();
            speedBox.ValueChanged += (_, _) => ApplySpeed();
            frequencyBox.ValueChanged += (_, _) => ApplyFrequency();
            waveformBox.SelectedIndexChanged += (_, _) => ApplyWaveform();
            volumeBox.ValueChanged += (_, _) => ApplyVolume();
            noiseCheck.CheckedChanged += (_, _) => ApplyNoise();
            snrBox.ValueChanged += (_, _) => ApplyNoise();
            textBox.TextChanged += (_, _) => ApplyText();
            encodingBtn.Click += (_, _) => EditEncoding();
            randomEachBox.ValueChanged += (_, _) => ApplyRandomSettings();
            randomGroupBox.ValueChanged += (_, _) => ApplyRandomSettings();
            randomUniqueCheck.CheckedChanged += (_, _) => ApplyRandomSettings();
            randomUniqueGroupCheck.CheckedChanged += (_, _) => ApplyRandomSettings();
            randomLetterCheck.CheckedChanged += (_, _) => ApplyRandomSettings();
            randomNumberCheck.CheckedChanged += (_, _) => ApplyRandomSettings();
            randomSymbolCheck.CheckedChanged += (_, _) => ApplyRandomSettings();
            randomCustomBox.TextChanged += (_, _) => ApplyRandomSettings();
            randomGenerateBtn.Click += (_, _) => GenerateRandom();

            split.Controls.Add(left, 0, 0);
            split.Controls.Add(editor, 1, 0);
            Controls.Add(split);
            Controls.Add(bottom);
        }

        private TableLayoutPanel CreateFieldGrid()
        {
            var fields = new TableLayoutPanel { Dock = DockStyle.Top, Height = 194, ColumnCount = 8, RowCount = 4, Padding = new Padding(0, 0, 0, 2) };
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 54));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 54));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 54));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 54));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 54));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1));
            for (int i = 0; i < 3; i++) fields.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            fields.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            ConfigureNumber(speedBox, 1, 100, 20);
            ConfigureNumber(frequencyBox, 1, 99999, 600);
            ConfigureNumber(volumeBox, 0, 100, 50);
            ConfigureNumber(snrBox, -10, 10, 0);
            waveformBox.DropDownStyle = ComboBoxStyle.DropDownList;
            waveformBox.Items.AddRange(["正弦波", "锯齿波", "方波"]);
            primaryCheck.Text = "主路"; loopCheck.Text = "循环"; muteCheck.Text = "静音"; noiseCheck.Text = "噪声";
            primaryCheck.AutoSize = true; loopCheck.AutoSize = true; muteCheck.AutoSize = true; noiseCheck.AutoSize = true;
            StyleButton(encodingBtn, "编码设置", 104);

            AddField(fields, FieldLabel("名称"), 0, 0); AddField(fields, nameBox, 1, 0);
            AddField(fields, FieldLabel("速度"), 3, 0); AddField(fields, speedBox, 4, 0);
            AddField(fields, FieldLabel("频率"), 6, 0); AddField(fields, frequencyBox, 7, 0);
            AddField(fields, FieldLabel("波形"), 0, 1); AddField(fields, waveformBox, 1, 1);
            AddField(fields, FieldLabel("音量"), 3, 1); AddField(fields, volumeBox, 4, 1);
            AddField(fields, FieldLabel("信噪比"), 6, 1); AddField(fields, snrBox, 7, 1);

            var options = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(0, 6, 0, 0) };
            primaryCheck.Margin = new Padding(0, 5, 20, 0); loopCheck.Margin = new Padding(0, 5, 20, 0); muteCheck.Margin = new Padding(0, 5, 20, 0); noiseCheck.Margin = new Padding(0, 5, 20, 0);
            options.Controls.AddRange([primaryCheck, loopCheck, muteCheck, noiseCheck, encodingBtn]);
            fields.Controls.Add(options, 0, 2); fields.SetColumnSpan(options, 8);

            var hint = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, ForeColor = SystemColors.GrayText, Text = "每路可选择 txt、粘贴报文，或按自己的组数和字符范围随机生成。" };
            fields.Controls.Add(hint, 0, 3); fields.SetColumnSpan(hint, 8);
            return fields;
        }

        private Panel CreateRandomPanel()
        {
            ConfigureNumber(randomEachBox, 1, 40, 4);
            ConfigureNumber(randomGroupBox, 1, 500, 10);
            randomUniqueCheck.Text = "组内不重复";
            randomUniqueGroupCheck.Text = "组不重复";
            randomLetterCheck.Text = "字母";
            randomNumberCheck.Text = "数字";
            randomSymbolCheck.Text = "符号";
            randomUniqueCheck.AutoSize = true;
            randomUniqueGroupCheck.AutoSize = true;
            randomLetterCheck.AutoSize = true;
            randomNumberCheck.AutoSize = true;
            randomSymbolCheck.AutoSize = true;
            randomLetterCheck.Checked = true;
            randomCustomBox.Width = 220;
            randomCustomBox.PlaceholderText = "可追加，如 KMRA";
            randomGenerateBtn.Text = "生成";
            randomGenerateBtn.Size = new Size(82, 28);
            randomGenerateBtn.Margin = new Padding(8, 6, 0, 0);

            var panel = new Panel { Dock = DockStyle.Top, Height = 84, Padding = new Padding(0, 2, 0, 2) };
            var row1 = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, WrapContents = false, FlowDirection = FlowDirection.LeftToRight };
            var row2 = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, FlowDirection = FlowDirection.LeftToRight };
            row1.Controls.AddRange([
                InlineLabel("随机"),
                InlineLabel("每组"),
                InlineNumber(randomEachBox),
                InlineLabel("组数"),
                InlineNumber(randomGroupBox),
                InlineCheck(randomUniqueCheck),
                InlineCheck(randomUniqueGroupCheck),
                randomGenerateBtn,
            ]);
            row2.Controls.AddRange([
                InlineLabel("范围"),
                InlineCheck(randomLetterCheck),
                InlineCheck(randomNumberCheck),
                InlineCheck(randomSymbolCheck),
                InlineLabel("自定义"),
                InlineNumber(randomCustomBox),
            ]);
            panel.Controls.Add(row2);
            panel.Controls.Add(row1);
            return panel;
        }

        private static Label InlineLabel(string text) => new()
        {
            Text = text,
            AutoSize = true,
            Margin = new Padding(0, 10, 6, 0),
        };

        private static Control InlineNumber(Control control)
        {
            control.Margin = new Padding(0, 6, 12, 0);
            control.Width = control is TextBox ? 220 : 68;
            control.Height = 28;
            return control;
        }

        private static Control InlineCheck(CheckBox check)
        {
            check.Margin = new Padding(0, 8, 14, 0);
            return check;
        }

        private static void AddField(TableLayoutPanel fields, Control control, int column, int row)
        {
            if (control is Label)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 8, 8, 8);
            }
            else
            {
                control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                control.Height = 28;
                control.Margin = new Padding(0, 12, 16, 12);
            }

            fields.Controls.Add(control, column, row);
        }

        private static Label FieldLabel(string text) => new()
        {
            Text = text,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleLeft,
            Margin = new Padding(0, 10, 8, 10),
        };

        private static void StyleButton(Button button, string text, int width = 96)
        {
            button.Text = text;
            button.AutoSize = false;
            button.Size = new Size(width, 36);
            button.Margin = new Padding(0, 0, 12, 0);
        }

        private static void ConfigureNumber(NumericUpDown box, int min, int max, int value)
        {
            box.Minimum = min;
            box.Maximum = max;
            box.Value = value;
            box.DecimalPlaces = 0;
        }

        private ChannelLane? Selected =>
            channelList.SelectedItems.Count == 0 ? null : channelList.SelectedItems[0].Tag as ChannelLane;

        private void AddLane(string? filePath = null, bool? isPrimary = null, bool loop = false)
        {
            if (lanes.Count >= MaxChannels)
            {
                MessageBox.Show($"最多 {MaxChannels} 路。", "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int index = lanes.Count;
            bool primary = isPrimary ?? lanes.Count == 0;
            var lane = new ChannelLane
            {
                Name = $"路{index + 1}",
                Frequency = FrequencySteps[index % FrequencySteps.Length],
                IsPrimary = primary,
                Volume = primary ? 80 : 50,
                Loop = loop,
                Waveform = "正弦波",
            };
            if (!string.IsNullOrWhiteSpace(filePath))
                ApplyFile(lane, filePath, rename: true);
            lane.Voice.IsPrimary = lane.IsPrimary;
            lane.Voice.Loop = lane.Loop;
            lanes.Add(lane);
            var item = new ListViewItem(lane.Name);
            item.SubItems.Add(lane.IsPrimary ? "主" : "次");
            item.SubItems.Add(lane.Frequency.ToString());
            item.SubItems.Add(lane.Speed.ToString());
            item.SubItems.Add(EncodingName(lane.EncodingType));
            item.SubItems.Add(lane.Volume.ToString());
            item.SubItems.Add("");
            item.Tag = lane;
            channelList.Items.Add(item);
            item.Selected = true;
            UpdateTransport();
        }

        private void DeleteSelected()
        {
            if (state != PlayState.Stopped || Selected is not ChannelLane lane || lanes.Count <= 1)
                return;

            int index = lanes.IndexOf(lane);
            lanes.RemoveAt(index);
            channelList.Items.RemoveAt(index);
            if (!lanes.Any(item => item.IsPrimary))
            {
                lanes[0].IsPrimary = true;
                lanes[0].Voice.IsPrimary = true;
            }

            RefreshRows();
            int next = Math.Min(index, lanes.Count - 1);
            channelList.Items[next].Selected = true;
            UpdateTransport();
        }

        private void MoveSelected(int delta)
        {
            if (state != PlayState.Stopped || Selected is not ChannelLane lane)
                return;

            int index = lanes.IndexOf(lane);
            int target = index + delta;
            if (target < 0 || target >= lanes.Count)
                return;

            (lanes[index], lanes[target]) = (lanes[target], lanes[index]);
            channelList.BeginUpdate();
            channelList.Items.Clear();
            foreach (var item in lanes)
                channelList.Items.Add(CreateItem(item));
            channelList.EndUpdate();
            channelList.Items[target].Selected = true;
            RefreshRows();
        }

        private static ListViewItem CreateItem(ChannelLane lane)
        {
            var item = new ListViewItem(lane.Name) { Tag = lane };
            item.SubItems.Add(lane.IsPrimary ? "主" : "次");
            item.SubItems.Add(lane.Frequency.ToString());
            item.SubItems.Add(lane.Speed.ToString());
            item.SubItems.Add(EncodingName(lane.EncodingType));
            item.SubItems.Add(lane.Volume.ToString());
            item.SubItems.Add(lane.Voice.CurrentGroup);
            return item;
        }

        private void LoadEditor()
        {
            var lane = Selected;
            suppressEditor = true;
            bool hasLane = lane != null;
            nameBox.Enabled = hasLane;
            textBox.Enabled = hasLane;
            loadFileBtn.Enabled = hasLane;
            filePathBox.Enabled = hasLane;
            primaryCheck.Enabled = hasLane && state == PlayState.Stopped;
            loopCheck.Enabled = hasLane;
            // 主路必须始终可发声，因此不提供静音选项。
            muteCheck.Enabled = hasLane && lane != null && !lane.IsPrimary;
            speedBox.Enabled = hasLane;
            frequencyBox.Enabled = hasLane;
            waveformBox.Enabled = hasLane;
            volumeBox.Enabled = hasLane;
            noiseCheck.Enabled = hasLane;
            encodingBtn.Enabled = hasLane;
            randomEachBox.Enabled = hasLane;
            randomGroupBox.Enabled = hasLane;
            randomUniqueCheck.Enabled = hasLane;
            randomUniqueGroupCheck.Enabled = hasLane;
            randomLetterCheck.Enabled = hasLane;
            randomNumberCheck.Enabled = hasLane;
            randomSymbolCheck.Enabled = hasLane;
            randomCustomBox.Enabled = hasLane;
            randomGenerateBtn.Enabled = hasLane;
            if (lane == null)
            {
                suppressEditor = false;
                UpdateTransport();
                return;
            }

            nameBox.Text = lane.Name;
            primaryCheck.Checked = lane.IsPrimary;
            loopCheck.Checked = lane.Loop;
            muteCheck.Checked = lane.IsPrimary ? false : lane.Muted;
            speedBox.Value = lane.Speed;
            frequencyBox.Value = lane.Frequency;
            waveformBox.SelectedItem = waveformBox.Items.Contains(lane.Waveform) ? lane.Waveform : "正弦波";
            volumeBox.Value = lane.Volume;
            noiseCheck.Checked = lane.NoiseEnabled;
            snrBox.Value = lane.NoiseSnr;
            snrBox.Enabled = lane.NoiseEnabled;
            filePathBox.Text = lane.FilePath;
            textBox.Text = lane.Text;
            randomEachBox.Value = lane.RandomGroupSize;
            randomGroupBox.Value = lane.RandomGroupCount;
            randomUniqueCheck.Checked = lane.RandomUniqueInGroup;
            randomUniqueGroupCheck.Checked = lane.RandomUniqueAcrossGroups;
            randomLetterCheck.Checked = lane.RandomLetters;
            randomNumberCheck.Checked = lane.RandomNumbers;
            randomSymbolCheck.Checked = lane.RandomSymbols;
            randomCustomBox.Text = lane.RandomCustom;
            previewLabel.Text = lane.PreviewDirty ? "预览（已粘贴修改，播放使用这里的内容）" : "预览";
            suppressEditor = false;
            UpdateTransport();
        }

        private void ApplyName()
        {
            if (suppressEditor || Selected is not ChannelLane lane)
                return;
            lane.Name = nameBox.Text;
            RefreshRows();
        }

        private void ApplyPrimary()
        {
            if (suppressEditor || Selected is not ChannelLane lane || state != PlayState.Stopped)
                return;

            if (!primaryCheck.Checked)
            {
                if (lane.IsPrimary)
                {
                    suppressEditor = true;
                    primaryCheck.Checked = true;
                    suppressEditor = false;
                }
                return;
            }

            foreach (var item in lanes)
            {
                item.IsPrimary = item == lane;
                item.Voice.IsPrimary = item.IsPrimary;
            }
            RefreshRows();
        }

        private void ApplyLoop()
        {
            if (suppressEditor || Selected is not ChannelLane lane)
                return;
            lane.Loop = loopCheck.Checked;
            lane.Voice.Loop = lane.Loop;
        }

        private void ApplyMute()
        {
            if (suppressEditor || Selected is not ChannelLane lane)
                return;
            if (lane.IsPrimary)
            {
                suppressEditor = true;
                muteCheck.Checked = false;
                suppressEditor = false;
                lane.Muted = false;
                lane.Voice.Muted = false;
                RefreshRows();
                return;
            }
            lane.Muted = muteCheck.Checked;
            // 主路永远不静音，确保从配置文件恢复后仍满足规则。
            lane.Voice.Muted = lane.IsPrimary ? false : lane.Muted;
            RefreshRows();
        }

        private void ApplySpeed()
        {
            if (suppressEditor || Selected is not ChannelLane lane)
                return;
            lane.Speed = (int)speedBox.Value;
            lane.Voice.SetSpeed(lane.Speed);
            RefreshRows();
        }

        private void ApplyFrequency()
        {
            if (suppressEditor || Selected is not ChannelLane lane)
                return;
            lane.Frequency = (int)frequencyBox.Value;
            lane.Voice.SetFrequency(lane.Frequency);
            RefreshRows();
        }

        private void ApplyWaveform()
        {
            if (suppressEditor || Selected is not ChannelLane lane || waveformBox.SelectedItem is not string waveform)
                return;
            lane.Waveform = waveform;
            lane.Voice.SetWaveform(waveform);
        }

        private void ApplyVolume()
        {
            if (suppressEditor || Selected is not ChannelLane lane)
                return;
            lane.Volume = (int)volumeBox.Value;
            lane.Voice.SetVolume(lane.Volume / 100f);
            RefreshRows();
        }

        private void ApplyNoise()
        {
            if (suppressEditor || Selected is not ChannelLane lane)
                return;
            lane.NoiseEnabled = noiseCheck.Checked;
            lane.NoiseSnr = (int)snrBox.Value;
            snrBox.Enabled = lane.NoiseEnabled;
            lane.Voice.SetNoise(lane.NoiseEnabled, lane.NoiseSnr);
        }

        private void ApplyText()
        {
            if (suppressEditor || Selected is not ChannelLane lane)
                return;
            lane.Text = textBox.Text;
            lane.PreviewDirty = true;
            previewLabel.Text = "预览（已粘贴修改，播放使用这里的内容）";
        }

        private void ApplyRandomSettings()
        {
            if (suppressEditor || Selected is not ChannelLane lane)
                return;
            lane.RandomGroupSize = (int)randomEachBox.Value;
            lane.RandomGroupCount = (int)randomGroupBox.Value;
            lane.RandomUniqueInGroup = randomUniqueCheck.Checked;
            lane.RandomUniqueAcrossGroups = randomUniqueGroupCheck.Checked;
            lane.RandomLetters = randomLetterCheck.Checked;
            lane.RandomNumbers = randomNumberCheck.Checked;
            lane.RandomSymbols = randomSymbolCheck.Checked;
            lane.RandomCustom = randomCustomBox.Text;
        }

        private void GenerateRandom()
        {
            if (Selected is not ChannelLane lane)
                return;

            ApplyRandomSettings();
            bool requested = lane.RandomLetters || lane.RandomNumbers || lane.RandomSymbols || !string.IsNullOrWhiteSpace(lane.RandomCustom);
            var charset = CollectPlayableChars(lane, out string skipped);
            if (charset.Length == 0 && requested)
            {
                MessageBox.Show("所选字符都不在当前编码中，无法生成。", "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!GroupTextGenerator.TryGenerate(charset, lane.RandomGroupSize, lane.RandomGroupCount, lane.RandomUniqueInGroup, lane.RandomUniqueAcrossGroups, Random.Shared, out string text, out string? error))
            {
                MessageBox.Show(error, "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            suppressEditor = true;
            lane.FilePath = "";
            lane.Text = text;
            lane.PreviewDirty = false;
            filePathBox.Text = "";
            textBox.Text = text;
            previewLabel.Text = state == PlayState.Stopped
                ? "预览（已随机生成）"
                : "预览（已随机生成，重播后使用这里的内容）";
            suppressEditor = false;
            if (skipped.Length > 0)
                MessageBox.Show("这些字符不在当前编码中，已跳过：\n" + skipped, "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static char[] CollectPlayableChars(ChannelLane lane, out string skipped)
        {
            var seen = new HashSet<char>();
            var list = new List<char>();
            var skippedChars = new List<char>();
            void Consider(char c, bool fromCustom)
            {
                if (char.IsWhiteSpace(c))
                    return;
                if (!lane.Code.TryGetValue(c, out string? pattern) || string.IsNullOrEmpty(pattern))
                {
                    if (fromCustom && !skippedChars.Contains(c))
                        skippedChars.Add(c);
                    return;
                }

                if (seen.Add(c))
                    list.Add(c);
            }

            if (lane.RandomLetters)
            {
                foreach (char c in Constant.alphabet.Keys)
                    Consider(c, false);
            }

            if (lane.RandomNumbers)
            {
                foreach (char c in Constant.number.Keys)
                    Consider(c, false);
            }

            if (lane.RandomSymbols)
            {
                foreach (char c in Constant.symbol.Keys)
                    Consider(c, false);
            }

            foreach (char c in lane.RandomCustom.ToUpperInvariant())
                Consider(c, true);

            skipped = string.Concat(skippedChars);
            return [.. list];
        }

        private void ChooseTextFile()
        {
            if (Selected is not ChannelLane lane)
                return;

            var initialDirectory = Directory.Exists(Constant.ArticlePath)
                ? Constant.ArticlePath
                : AppContext.BaseDirectory;
            if (!string.IsNullOrWhiteSpace(lane.FilePath))
            {
                var directory = Path.GetDirectoryName(lane.FilePath);
                if (!string.IsNullOrWhiteSpace(directory) && Directory.Exists(directory))
                    initialDirectory = directory;
            }

            using var dialog = new OpenFileDialog
            {
                Filter = "报文文本 (*.txt)|*.txt",
                Title = "选择报文文件",
                InitialDirectory = initialDirectory,
            };
            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            if (!ApplyFile(lane, dialog.FileName, rename: IsDefaultName(lane)))
                return;

            suppressEditor = true;
            filePathBox.Text = lane.FilePath;
            textBox.Text = lane.Text;
            nameBox.Text = lane.Name;
            previewLabel.Text = "预览";
            suppressEditor = false;
            RefreshRows();
        }

        private static bool IsDefaultName(ChannelLane lane) =>
            System.Text.RegularExpressions.Regex.IsMatch(lane.Name, @"^路\d+$");

        private static bool ApplyFile(ChannelLane lane, string path, bool rename)
        {
            try
            {
                var text = ReadPracticeText(path);
                lane.FilePath = path;
                lane.Text = text;
                lane.PreviewDirty = false;
                if (rename)
                    lane.Name = Path.GetFileNameWithoutExtension(path);
                if (string.IsNullOrWhiteSpace(text))
                    MessageBox.Show("这个文件没有可播放的内容。", "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法读取文件：" + ex.Message, "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private bool ReloadUneditedFiles()
        {
            var failed = new List<string>();
            foreach (var lane in lanes)
            {
                if (string.IsNullOrWhiteSpace(lane.FilePath) || lane.PreviewDirty)
                    continue;
                if (!File.Exists(lane.FilePath))
                {
                    failed.Add(lane.FilePath);
                    lane.Text = "";
                    continue;
                }

                try
                {
                    lane.Text = ReadPracticeText(lane.FilePath);
                }
                catch (Exception)
                {
                    failed.Add(lane.FilePath);
                    lane.Text = "";
                }
            }

            if (Selected is ChannelLane selected)
            {
                suppressEditor = true;
                textBox.Text = selected.Text;
                suppressEditor = false;
            }

            if (failed.Count == 0)
                return true;

            MessageBox.Show("以下文件无法读取：\\n" + string.Join("\\n", failed), "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private static string[] SampleTextFiles()
        {
            if (!Directory.Exists(Constant.ArticlePath))
                return [];
            return Directory.GetFiles(Constant.ArticlePath, "*.txt")
                .OrderBy(Path.GetFileName, StringComparer.CurrentCultureIgnoreCase)
                .Take(2)
                .ToArray();
        }

        internal static string ReadPracticeText(string path)
        {
            var bytes = File.ReadAllBytes(path);
            if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
                return Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);

            var utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
            try
            {
                return utf8.GetString(bytes);
            }
            catch (DecoderFallbackException)
            {
                return Encoding.GetEncoding(0).GetString(bytes);
            }
        }

        private void EditEncoding()
        {
            if (Selected is not ChannelLane lane)
                return;

            using var dialog = new EncodingConfiguration(lane.EncodingType, lane.Code);
            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            lane.EncodingType = dialog.EncodingType;
            lane.Code = new Dictionary<char, string>(dialog.Code);
            RefreshRows();
        }

        private static string EncodingName(int encodingType) => encodingType switch
        {
            1 => "短5改",
            2 => "短10改",
            3 => "自定义",
            _ => "默认",
        };

        private void Play()
        {
            if (state == PlayState.Paused)
            {
                waveOut.Play();
                state = PlayState.Playing;
                statusLabel.Text = "播放中";
                UpdateTransport();
                return;
            }

            if (state == PlayState.Playing || !EnsureAudio() || !TryStartSession())
                return;

            waveOut.Play();
            state = PlayState.Playing;
            statusLabel.Text = "播放中";
            UpdateTransport();
        }

        private void Pause()
        {
            if (state != PlayState.Playing)
                return;
            waveOut.Pause();
            state = PlayState.Paused;
            statusLabel.Text = "已暂停";
            UpdateTransport();
        }

        private void StopPlayback()
        {
            if (state == PlayState.Stopped)
                return;
            sessionId++;
            state = PlayState.Stopped;
            waveOut.Stop();
            foreach (var lane in lanes)
                lane.Voice.ClearDisplay();
            statusLabel.Text = "已停止";
            RefreshCurrentGroups();
            UpdateTransport();
        }

        private void Replay()
        {
            sessionId++;
            if (state != PlayState.Stopped)
                waveOut.Stop();
            state = PlayState.Stopped;
            if (!TryStartSession())
            {
                statusLabel.Text = "已停止";
                UpdateTransport();
                return;
            }

            if (!EnsureAudio())
            {
                statusLabel.Text = "已停止";
                UpdateTransport();
                return;
            }

            waveOut.Play();
            state = PlayState.Playing;
            statusLabel.Text = "播放中";
            UpdateTransport();
        }

        private bool EnsureAudio()
        {
            if (waveReady)
                return true;

            try
            {
                waveOut.Init(mixer);
                waveReady = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法打开音频设备：" + ex.Message, "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool TryStartSession()
        {
            ReloadUneditedFiles();
            var primary = lanes.FirstOrDefault(lane => lane.IsPrimary) ?? lanes[0];
            if (string.IsNullOrWhiteSpace(primary.Text))
            {
                MessageBox.Show("主路还没有报文。请先选择一个 txt 文件。", "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            foreach (var lane in lanes)
            {
                lane.Voice.IsPrimary = lane.IsPrimary;
                lane.Voice.Loop = lane.Loop;
                // 主路永远不静音，确保从配置文件恢复后仍满足规则。
            lane.Voice.Muted = lane.IsPrimary ? false : lane.Muted;
                lane.Voice.Prepare(
                    lane.Text,
                    lane.Code,
                    lane.Speed,
                    lane.Waveform,
                    lane.Frequency,
                    lane.Volume / 100f,
                    lane.NoiseEnabled,
                    lane.NoiseSnr);
            }

            int id = ++sessionId;
            mixer.SessionEnded = () => OnNaturalEnd(id);
            mixer.BeginSession(lanes.Select(lane => lane.Voice).ToArray());
            return true;
        }

        private void OnNaturalEnd(int id)
        {
            if (IsDisposed || id != sessionId || state != PlayState.Playing)
                return;

            state = PlayState.Stopped;
            try
            {
                if (waveOut.PlaybackState != PlaybackState.Stopped)
                    waveOut.Stop();
            }
            catch (Exception)
            {
                // 设备已经因读完而停止时，再停一次可以忽略。
            }

            statusLabel.Text = "已停止";
            UpdateTransport();
        }

        private void ChannelList_DrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawBackground();
            TextRenderer.DrawText(
                e.Graphics,
                e.Header?.Text ?? "",
                channelList.Font,
                e.Bounds,
                SystemColors.ControlText,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        }

        private void ChannelList_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
        {
            bool selected = e.Item?.Selected == true;
            bool focused = channelList.Focused;
            Color back = selected
                ? (focused ? SystemColors.Highlight : SystemColors.Control)
                : channelList.BackColor;
            Color fore = selected && focused ? SystemColors.HighlightText : channelList.ForeColor;
            using (var brush = new SolidBrush(back))
                e.Graphics.FillRectangle(brush, e.Bounds);

            var textBounds = e.Bounds;
            textBounds.Inflate(-4, 0);
            TextRenderer.DrawText(
                e.Graphics,
                e.SubItem?.Text ?? "",
                e.Item?.Font ?? channelList.Font,
                textBounds,
                fore,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);

            if (channelList.GridLines)
            {
                using var pen = new Pen(SystemColors.ControlLight);
                e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
                e.Graphics.DrawLine(pen, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom);
            }
        }

        private void FitChannelColumns()
        {
            if (fittingColumns || channelList.Columns.Count != ChannelColumns.Length)
                return;

            int available = channelList.ClientSize.Width;
            if (available <= 1)
                return;

            fittingColumns = true;
            try
            {
                int weightSum = 0;
                foreach (var column in ChannelColumns)
                    weightSum += column.Weight;

                int used = 0;
                for (int i = 0; i < ChannelColumns.Length - 1; i++)
                {
                    int width = available * ChannelColumns[i].Weight / weightSum;
                    if (width < ChannelColumns[i].Minimum)
                        width = ChannelColumns[i].Minimum;
                    channelList.Columns[i].Width = width;
                    used += width;
                }

                int last = available - used;
                int deficit = ChannelColumns[^1].Minimum - last;
                for (int i = ChannelColumns.Length - 2; i >= 0 && deficit > 0; i--)
                {
                    int spare = channelList.Columns[i].Width - ChannelColumns[i].Minimum;
                    if (spare <= 0)
                        continue;
                    int cut = Math.Min(spare, deficit);
                    channelList.Columns[i].Width -= cut;
                    used -= cut;
                    deficit -= cut;
                }

                channelList.Columns[^1].Width = Math.Max(1, available - used);
            }
            finally
            {
                fittingColumns = false;
            }
        }

        private void RefreshCurrentGroups()
        {
            if (state == PlayState.Playing && waveOut.PlaybackState == PlaybackState.Stopped)
                OnNaturalEnd(sessionId);

            if (channelList.Items.Count != lanes.Count)
                return;

            for (int i = 0; i < lanes.Count; i++)
            {
                string group = lanes[i].Voice.CurrentGroup;
                var sub = channelList.Items[i].SubItems[6];
                if (sub.Text != group)
                    sub.Text = group;
            }
        }

        private void RefreshRows()
        {
            if (channelList.Items.Count != lanes.Count)
                return;

            for (int i = 0; i < lanes.Count; i++)
            {
                var lane = lanes[i];
                var item = channelList.Items[i];
                item.Text = lane.Name;
                item.SubItems[1].Text = lane.IsPrimary ? "主" : "次";
                item.SubItems[2].Text = lane.Frequency.ToString();
                item.SubItems[3].Text = lane.Speed.ToString();
                item.SubItems[4].Text = EncodingName(lane.EncodingType);
                item.SubItems[5].Text = lane.Volume.ToString();
            }
        }

        private void UpdateTransport()
        {
            playBtn.Enabled = state != PlayState.Playing;
            pauseBtn.Enabled = state == PlayState.Playing;
            stopBtn.Enabled = state != PlayState.Stopped;
            addBtn.Enabled = state == PlayState.Stopped && lanes.Count < MaxChannels;
            deleteBtn.Enabled = state == PlayState.Stopped && lanes.Count > 1 && Selected != null;
            int index = Selected == null ? -1 : lanes.IndexOf(Selected);
            upBtn.Enabled = state == PlayState.Stopped && index > 0;
            downBtn.Enabled = state == PlayState.Stopped && index >= 0 && index < lanes.Count - 1;
            primaryCheck.Enabled = Selected != null && state == PlayState.Stopped;
        }

        private void ExportOrCancel()
        {
            if (exporting)
            {
                exportCts?.Cancel();
                exportBtn.Enabled = false;
                return;
            }

            _ = ExportAudioAsync();
        }

        private async Task ExportAudioAsync()
        {
            ReloadUneditedFiles();
            var primary = lanes.FirstOrDefault(lane => lane.IsPrimary) ?? lanes.FirstOrDefault();
            if (primary == null || string.IsNullOrWhiteSpace(primary.Text))
            {
                MessageBox.Show("主路还没有报文。请先选择一个 txt 文件。", "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "MP3 音频 (*.mp3)|*.mp3|WAV 音频 (*.wav)|*.wav",
                Title = "导出混合音频",
                FileName = SafeFileName(primary.Name) + "-混音.mp3",
            };
            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            string path = EnsureAudioExtension(dialog.FileName, dialog.FilterIndex);
            var sources = SnapshotMix();
            exporting = true;
            exportCts = new CancellationTokenSource();
            exportBtn.Text = "取消导出";
            statusLabel.Text = "正在导出 00:00:00";
            var progress = new Progress<TimeSpan>(time =>
            {
                if (!IsDisposed && exporting)
                    statusLabel.Text = "正在导出 " + time.ToString(@"hh\:mm\:ss");
            });

            try
            {
                var result = await Task.Run(() => MultiChannelExporter.Export(sources, path, progress, exportCts.Token));
                if (IsDisposed)
                    return;

                if (result == MixExportResult.Empty)
                {
                    statusLabel.Text = PlaybackStatusText();
                    MessageBox.Show("没有生成可保存的音频。", "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                statusLabel.Text = state == PlayState.Stopped ? "已导出" : PlaybackStatusText();
                MessageBox.Show("已导出主路播放一遍的混合音频：\n" + path, "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                if (!IsDisposed)
                    statusLabel.Text = "已取消导出";
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                {
                    statusLabel.Text = PlaybackStatusText();
                    MessageBox.Show("导出失败：" + ex.Message, "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                exporting = false;
                exportCts.Dispose();
                exportCts = null;
                if (!IsDisposed)
                {
                    exportBtn.Text = "导出音频";
                    exportBtn.Enabled = true;
                    UpdateTransport();
                }
            }
        }

        private List<ChannelMixSource> SnapshotMix()
        {
            var sources = lanes.Select(lane => new ChannelMixSource
            {
                Text = lane.Text,
                Code = new Dictionary<char, string>(lane.Code),
                Speed = lane.Speed,
                Waveform = lane.Waveform,
                Frequency = lane.Frequency,
                Volume = lane.Volume / 100f,
                NoiseEnabled = lane.NoiseEnabled,
                NoiseSnr = lane.NoiseSnr,
                IsPrimary = lane.IsPrimary,
                Loop = lane.Loop,
                Muted = lane.IsPrimary ? false : lane.Muted,
            }).ToList();
            if (sources.Count > 0 && !sources.Exists(item => item.IsPrimary))
                sources[0].IsPrimary = true;
            return sources;
        }

        private static string EnsureAudioExtension(string path, int filterIndex)
        {
            if (path.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || path.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
                return path;
            return path + (filterIndex == 2 ? ".wav" : ".mp3");
        }

        private static string SafeFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            name = name.Trim();
            return string.IsNullOrWhiteSpace(name) ? "多路混音" : name;
        }

        private string PlaybackStatusText() => state switch
        {
            PlayState.Playing => "播放中",
            PlayState.Paused => "已暂停",
            _ => "已停止",
        };

        private void SavePreset()
        {
            using var dialog = new SaveFileDialog
            {
                Filter = "多路配置 (*.json)|*.json",
                FileName = "多路播放.json",
            };
            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            var preset = new MultiChannelPreset
            {
                Channels = lanes.Select(lane => new MultiChannelPresetItem
                {
                    Name = lane.Name,
                    FilePath = lane.FilePath,
                    Text = lane.Text,
                    EncodingType = lane.EncodingType,
                    CustomCode = lane.EncodingType == 3
                        ? lane.Code.ToDictionary(pair => pair.Key.ToString(), pair => pair.Value)
                        : null,
                    Volume = lane.Volume,
                    NoiseEnabled = lane.NoiseEnabled,
                    NoiseSnr = lane.NoiseSnr,
                    Speed = lane.Speed,
                    Waveform = lane.Waveform,
                    Frequency = lane.Frequency,
                    IsPrimary = lane.IsPrimary,
                    Loop = lane.Loop,
                    Muted = lane.Muted,
                    RandomGroupSize = lane.RandomGroupSize,
                    RandomGroupCount = lane.RandomGroupCount,
                    RandomUniqueInGroup = lane.RandomUniqueInGroup,
                    RandomUniqueAcrossGroups = lane.RandomUniqueAcrossGroups,
                    RandomLetters = lane.RandomLetters,
                    RandomNumbers = lane.RandomNumbers,
                    RandomSymbols = lane.RandomSymbols,
                    RandomCustom = lane.RandomCustom,
                }).ToList()
            };
            File.WriteAllText(dialog.FileName, JsonConvert.SerializeObject(preset, Formatting.Indented));
        }

        private void OpenPreset()
        {
            if (state != PlayState.Stopped)
                StopPlayback();

            using var dialog = new OpenFileDialog
            {
                Filter = "多路配置 (*.json)|*.json",
            };
            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            MultiChannelPreset? preset;
            try
            {
                preset = JsonConvert.DeserializeObject<MultiChannelPreset>(File.ReadAllText(dialog.FileName));
            }
            catch (Exception)
            {
                MessageBox.Show("配置文件无法读取，当前内容未更改。", "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (preset?.Channels == null || preset.Channels.Count == 0 || preset.Channels.Count > MaxChannels)
            {
                MessageBox.Show("配置文件无法读取，当前内容未更改。", "多路播放", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var loaded = new List<ChannelLane>();
            foreach (var item in preset.Channels)
            {
                var lane = ChannelLane.FromPreset(item);
                loaded.Add(lane);
            }

            if (!loaded.Any(lane => lane.IsPrimary))
                loaded[0].IsPrimary = true;
            else
            {
                bool seen = false;
                foreach (var lane in loaded)
                {
                    if (!lane.IsPrimary)
                        continue;
                    if (seen)
                        lane.IsPrimary = false;
                    seen = true;
                }
            }

            foreach (var lane in loaded)
                lane.Voice.IsPrimary = lane.IsPrimary;

            lanes.Clear();
            lanes.AddRange(loaded);
            channelList.BeginUpdate();
            channelList.Items.Clear();
            foreach (var lane in lanes)
                channelList.Items.Add(CreateItem(lane));
            channelList.EndUpdate();
            channelList.Items[0].Selected = true;
            LoadEditor();
            UpdateTransport();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            exportCts?.Cancel();
            if (!audioReleased)
            {
                audioReleased = true;
                sessionId++;
                groupTimer.Stop();
                groupTimer.Dispose();
                try
                {
                    waveOut.Stop();
                }
                catch (Exception)
                {
                    // 关闭时设备可能已经释放。
                }
                waveOut.Dispose();
            }

            base.OnFormClosing(e);
        }

        private enum PlayState
        {
            Stopped,
            Playing,
            Paused,
        }
    }

    internal sealed class ChannelLane
    {
        public string Name { get; set; } = "路1";
        public string FilePath { get; set; } = "";
        public string Text { get; set; } = "";
        public bool PreviewDirty { get; set; }
        public int EncodingType { get; set; }
        public Dictionary<char, string> Code { get; set; } = MorseCodeSet.Create(0);
        public int Volume { get; set; } = 50;
        public bool NoiseEnabled { get; set; }
        public int NoiseSnr { get; set; }
        public int Speed { get; set; } = 20;
        public string Waveform { get; set; } = "正弦波";
        public int Frequency { get; set; } = 600;
        public bool IsPrimary { get; set; }
        public bool Loop { get; set; }
        public bool Muted { get; set; }
        public int RandomGroupSize { get; set; } = 4;
        public int RandomGroupCount { get; set; } = 10;
        public bool RandomUniqueInGroup { get; set; }
        public bool RandomUniqueAcrossGroups { get; set; }
        public bool RandomLetters { get; set; } = true;
        public bool RandomNumbers { get; set; }
        public bool RandomSymbols { get; set; }
        public string RandomCustom { get; set; } = "";
        public ChannelVoice Voice { get; } = new(600, 20, "正弦波");

        public static ChannelLane FromPreset(MultiChannelPresetItem item)
        {
            int encodingType = item.EncodingType is 1 or 2 or 3 ? item.EncodingType : 0;
            Dictionary<char, string>? custom = null;
            if (encodingType == 3 && item.CustomCode != null)
            {
                custom = [];
                foreach (var pair in item.CustomCode)
                {
                    if (pair.Key.Length == 1)
                        custom[pair.Key[0]] = pair.Value ?? "";
                }
            }

            string waveform = item.Waveform is "锯齿波" or "方波" ? item.Waveform : "正弦波";
            var lane = new ChannelLane
            {
                Name = string.IsNullOrWhiteSpace(item.Name) ? "路" : item.Name,
                FilePath = item.FilePath ?? "",
                Text = item.Text ?? "",
                PreviewDirty = !string.IsNullOrWhiteSpace(item.FilePath) && !File.Exists(item.FilePath),
                EncodingType = encodingType,
                Code = MorseCodeSet.Create(encodingType, custom),
                Volume = Math.Clamp(item.Volume, 0, 100),
                NoiseEnabled = item.NoiseEnabled,
                NoiseSnr = Math.Clamp(item.NoiseSnr, -10, 10),
                Speed = Math.Clamp(item.Speed, 1, 100),
                Waveform = waveform,
                Frequency = Math.Clamp(item.Frequency, 1, 99999),
                IsPrimary = item.IsPrimary,
                Loop = item.Loop,
                // 兼容旧配置：主路的静音标记一律忽略。
                Muted = item.IsPrimary ? false : item.Muted,
                RandomGroupSize = item.RandomGroupSize < 1 ? 4 : Math.Clamp(item.RandomGroupSize, 1, 40),
                RandomGroupCount = item.RandomGroupCount < 1 ? 10 : Math.Clamp(item.RandomGroupCount, 1, 500),
                RandomUniqueInGroup = item.RandomUniqueInGroup,
                RandomUniqueAcrossGroups = item.RandomUniqueAcrossGroups,
                RandomLetters = item.RandomLetters ?? true,
                RandomNumbers = item.RandomNumbers,
                RandomSymbols = item.RandomSymbols,
                RandomCustom = item.RandomCustom ?? "",
            };
            lane.Voice.IsPrimary = lane.IsPrimary;
            lane.Voice.Loop = lane.Loop;
            // 主路永远不静音，确保从配置文件恢复后仍满足规则。
            lane.Voice.Muted = lane.IsPrimary ? false : lane.Muted;
            if (!string.IsNullOrWhiteSpace(lane.FilePath) && File.Exists(lane.FilePath))
            {
                try
                {
                    lane.Text = MultiChannelPlayer.ReadPracticeText(lane.FilePath);
                    lane.PreviewDirty = false;
                }
                catch (Exception)
                {
                    lane.PreviewDirty = true;
                }
            }
            return lane;
        }
    }

    internal sealed class MultiChannelPreset
    {
        public List<MultiChannelPresetItem> Channels { get; set; } = [];
    }

    internal sealed class MultiChannelPresetItem
    {
        public string Name { get; set; } = "";
        public string FilePath { get; set; } = "";
        public string Text { get; set; } = "";
        public int EncodingType { get; set; }
        public Dictionary<string, string>? CustomCode { get; set; }
        public int Volume { get; set; } = 50;
        public bool NoiseEnabled { get; set; }
        public int NoiseSnr { get; set; }
        public int Speed { get; set; } = 20;
        public string Waveform { get; set; } = "正弦波";
        public int Frequency { get; set; } = 600;
        public bool IsPrimary { get; set; }
        public bool Loop { get; set; }
        public bool Muted { get; set; }
        public int RandomGroupSize { get; set; }
        public int RandomGroupCount { get; set; }
        public bool RandomUniqueInGroup { get; set; }
        public bool RandomUniqueAcrossGroups { get; set; }
        public bool? RandomLetters { get; set; }
        public bool RandomNumbers { get; set; }
        public bool RandomSymbols { get; set; }
        public string? RandomCustom { get; set; }
    }
}















