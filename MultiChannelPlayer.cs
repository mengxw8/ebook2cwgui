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
        private readonly Label statusLabel = new();
        private bool suppressEditor;
        private bool audioReleased;
        private bool waveReady;
        private int sessionId;
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

            var transport = new FlowLayoutPanel { Dock = DockStyle.Left, AutoSize = true, WrapContents = false, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(0, 8, 0, 0) };
            transport.Controls.AddRange([playBtn, pauseBtn, stopBtn, replayBtn]);
            var fileButtons = new FlowLayoutPanel { Dock = DockStyle.Right, AutoSize = true, WrapContents = false, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(0, 8, 0, 0) };
            fileButtons.Controls.AddRange([openBtn, saveBtn]);
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
            channelList.Columns.Add("名称", 92);
            channelList.Columns.Add("角色", 52);
            channelList.Columns.Add("频率", 66);
            channelList.Columns.Add("速度", 54);
            channelList.Columns.Add("编码", 68);
            channelList.Columns.Add("静音", 52);
            channelList.Columns.Add("当前组", 110);
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

            var hint = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, ForeColor = SystemColors.GrayText, Text = "每路可单独选择 txt 文件，也可以直接在下方预览区粘贴或编辑报文。" };
            fields.Controls.Add(hint, 0, 3); fields.SetColumnSpan(hint, 8);
            return fields;
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
            var lane = new ChannelLane
            {
                Name = $"路{index + 1}",
                Frequency = FrequencySteps[index % FrequencySteps.Length],
                IsPrimary = isPrimary ?? lanes.Count == 0,
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
            item.SubItems.Add("");
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
            item.SubItems.Add(lane.Muted ? "是" : "");
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

        private void ChooseTextFile()
        {
            if (Selected is not ChannelLane lane)
                return;

            var initialDirectory = Directory.Exists("./text")
                ? Path.GetFullPath("./text")
                : Environment.CurrentDirectory;
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
            if (!Directory.Exists("./text"))
                return [];
            return Directory.GetFiles("./text", "*.txt")
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
                item.SubItems[5].Text = lane.Muted ? "是" : "";
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
    }
}















