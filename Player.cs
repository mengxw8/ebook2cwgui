using CW.morse;
using Microsoft.Identity.Client;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace CW
{
    public partial class Player : Form
    {
        //编码方式，默认为正常编码
        private Dictionary<char, string> code = new Dictionary<char, string>[]{Constant.header,Constant.allCharCode
    }.SelectMany(disc => disc).ToDictionary(
                group => group.Key,
                group => group.Value // 取最后一个值（覆盖冲突键）
            );
        private int encodingType = 0;
        private readonly MorsePlayer player = new(600, MorseConfig.Create(20));
        // 创建 WaveOutEvent 对象来播放音频
        private readonly WaveOutEvent playerWave = new();
        private ComboBox channelModeBox = null!;
        private NumericUpDown channelStrengthBox = null!;
        private Label channelStrengthLabel = null!;
        private ToolTip shortcutTips = null!;

        private int currentGroupIndex = -1;
        private System.Text.RegularExpressions.MatchCollection contentGroups =
            System.Text.RegularExpressions.Regex.Matches(string.Empty, @"\S+");
        private int playbackGeneration;
        private int lastHighlightStart = -1;
        private int lastHighlightLength = 0;

        public Player()
        {
            InitializeComponent();
            ThemeManager.Apply(this);
            player.OnGroupPlay += OnGroupPlay;
            channelModeBox = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(1110, 28), Size = new Size(150, 25) };
            channelModeBox.Items.AddRange(new object[] { "白噪声", "衰落信道", "脉冲干扰", "多径回声" });
            channelModeBox.SelectedIndex = 0;
            channelModeBox.SelectedIndexChanged += (_, _) => NoiseSettingsChanged(this, EventArgs.Empty);
            Controls.Add(channelModeBox);
            channelStrengthLabel = new Label { Text = "强度(%):", AutoSize = true, Location = new Point(1110, 58) };
            channelStrengthBox = new NumericUpDown { Minimum = 0, Maximum = 200, Value = 100, Increment = 5, Location = new Point(1170, 55), Size = new Size(70, 23) };
            channelStrengthBox.ValueChanged += (_, _) => NoiseSettingsChanged(this, EventArgs.Empty);
            Controls.Add(channelStrengthLabel);
            Controls.Add(channelStrengthBox);
            shortcutTips = new ToolTip();
            shortcutTips.SetToolTip(StartBtn, "开始播放 (Enter)");
            shortcutTips.SetToolTip(ContinueBtn, "继续播放 (Space)");
            shortcutTips.SetToolTip(PauseBtn, "暂停播放 (Space)");
            shortcutTips.SetToolTip(ReplayBtn, "重新播放 (R)");
            shortcutTips.SetToolTip(StopBtn, "停止播放 (S 或 Esc)");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 播放器快捷键：空格暂停/继续，R 重播，S 停止，Esc 停止。
            if (keyData == Keys.Space) { if (playerWave.PlaybackState == PlaybackState.Playing) PauseBtn_Click(this, EventArgs.Empty); else ContinueBtn_Click(this, EventArgs.Empty); return true; }
            if (keyData == Keys.Enter) { StartBtn_Click(this, EventArgs.Empty); return true; }
            if (keyData == Keys.R) { ReplayBtn_Click(this, EventArgs.Empty); return true; }
            if (keyData == Keys.S || keyData == Keys.Escape) { StopBtn_Click(this, EventArgs.Empty); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SelectFileBtn_Click(object sender, EventArgs e)
        {
            //弹出文件选择框
            OpenFileDialog openFileDialog = new()
            {
                Filter = "报文文本(*.txt)|*.txt",
                Multiselect = false//关闭多选
            };


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePathLbl.Text = openFileDialog.FileName;

            }
            else
            {
                MessageBox.Show("未选择任何文件,试试其他模式吧!");
            }
        }

        private void CodingDefinitionBtn_Click(object sender, EventArgs e)
        {

            EncodingConfiguration encodingConfiguration = new(encodingType, code);
            var result = encodingConfiguration.ShowDialog();
            if (result == DialogResult.OK)
            {
                code = encodingConfiguration.Code;
                player.UpdateEncoding(code);
                //记录下新的编码方式
                encodingType = encodingConfiguration.EncodingType;
                player?.UpdateEncoding(code);
            }



        }

        private void SpeedBox_ValueChanged(object sender, EventArgs e)
        {
            player.UpdateConfig(MorseConfig.Create(Convert.ToInt32(speedBox.Value)));
        }

        private void Player_Load(object sender, EventArgs e)
        {
            playerWave.Init(player);

            waveList.SelectedIndex = 0;

        }

        // 开关和 SNR 共用此事件入口，实时更新播放器的噪声处理器。
        private void NoiseSettingsChanged(object sender, EventArgs e)
        {
            snrBox.Enabled = noiseCheckBox.Checked;
            if (channelModeBox == null || channelStrengthBox == null)
                return;
            var mode = channelModeBox.SelectedIndex switch
            {
                1 => morse.ChannelMode.Fading,
                2 => morse.ChannelMode.Impulsive,
                3 => morse.ChannelMode.Multipath,
                _ => morse.ChannelMode.WhiteNoise
            };
            player.UpdateNoise(noiseCheckBox.Checked, (int)snrBox.Value, mode, (float)channelStrengthBox.Value / 100f);
        }

        private void ToneBox_ValueChanged(object sender, EventArgs e)
        {
            player.UpdateFrequency(Convert.ToInt32(toneBox.Value));
            // 音调改变后也要重建带通，保证载波继续处于通带中心。
            NoiseSettingsChanged(sender, e);
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            playerWave.Stop();
            player.Clean();
            playbackGeneration++;
            ResetHighlight();
        }

        private void ReplayBtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists(FilePathLbl.Text))
            {
                return;
            }
            var fileContent = File.ReadAllText(FilePathLbl.Text);
            playerWave.Stop();
            player.Clean();
            player.Mute(500);
            player.AddMorseCode("头", code);
            player.AddMorseCode(fileContent, code);
            player.AddMorseCode("尾", code);
            ResetGroupTracking(fileContent);
            playerWave.Play();
        }

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            playerWave.Play();

        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            playerWave.Pause();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists(FilePathLbl.Text))
            {
                MessageBox.Show("文本文件不存在，无法播放！", "文件错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var fileContent = File.ReadAllText(FilePathLbl.Text);
            playerWave.Stop();
            player.Clean();
            player.AddMorseCode("头", code);
            player.AddMorseCode(fileContent, code);
            player.AddMorseCode("尾", code);
            ResetGroupTracking(fileContent);
            playerWave.Play();
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists(FilePathLbl.Text))
            {
                MessageBox.Show("文本文件不存在，无法导出！", "文件错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using SaveFileDialog saveFileDialog = new()
            {
                Title = "选择音频保存位置",
                Filter = "音频文件(*.mp3)|*.mp3",
                FileName = DateTime.Now.ToUniversalTime().Ticks + ".mp3"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFolderPath = saveFileDialog.FileName;
                // null 表示导出干净音频；启用时传入当前 SNR 和音调，与播放使用相同算法。
                MorseToMp3.ToMp3ByLine(File.ReadAllText(FilePathLbl.Text), code, MorseConfig.Create(Convert.ToInt32(speedBox.Value)), selectedFolderPath, player.Dit_buff!, player.Dah_buff!, true, true,
                    noiseCheckBox.Checked ? (int)snrBox.Value : null, (int)toneBox.Value);
            }
        }

        private void ResetGroupTracking(string fileContent)
        {
            // 作废上一轮排入 UI 消息队列的回调；开始和重播均刷新正文。
            playbackGeneration++;
            ResetHighlight();
            ContentTxb.Text = fileContent.ToLower();
            // RichTextBox 会归一化换行，因此必须使用控件中的实际文本计算位置。
            contentGroups = System.Text.RegularExpressions.Regex.Matches(ContentTxb.Text, @"\S+");
            currentGroupIndex = -1;
            lastHighlightStart = -1;
            lastHighlightLength = 0;
        }

        private void OnGroupPlay(string group)
        {
            int generation = System.Threading.Volatile.Read(ref playbackGeneration);
            if (IsDisposed || Disposing || !IsHandleCreated) return;

            try
            {
                BeginInvoke((Action)(() =>
                {
                    if (generation != playbackGeneration || IsDisposed || Disposing) return;
                    // 计数和高亮状态都由 UI 线程维护，避免停止/重播与音频回调竞争。
                    currentGroupIndex++;
                    ResetHighlight();
                    // 第 0 组是报头；正文之后的报尾只清除最后一处高亮。
                    int contentIndex = currentGroupIndex - 1;
                    if (contentIndex < 0 || contentIndex >= contentGroups.Count) return;

                    var match = contentGroups[contentIndex];
                    ContentTxb.Select(match.Index, match.Length);
                    ContentTxb.SelectionBackColor = Color.Yellow;
                    lastHighlightStart = match.Index;
                    lastHighlightLength = match.Length;
                    ContentTxb.ScrollToCaret();
                }));
            }
            catch (InvalidOperationException) when (IsDisposed || Disposing || !IsHandleCreated)
            {
                // 关闭窗口时句柄可能在检查后销毁，丢弃最后一个音频通知即可。
            }
        }

        private void ResetHighlight()
        {
            if (lastHighlightStart >= 0 && lastHighlightLength > 0)
            {
                ContentTxb.Select(lastHighlightStart, lastHighlightLength);
                ContentTxb.SelectionBackColor = Color.White;
            }
        }

        private void Player_FormClosing(object sender, FormClosingEventArgs e)
        {

            playbackGeneration++;
            player.OnGroupPlay -= OnGroupPlay;
            playerWave?.Stop();
            player?.Clean();
        }

        private void waveList_SelectedIndexChanged(object sender, EventArgs e)
        {
            player.UpdateConfig(MorseConfig.Create(Convert.ToInt32(speedBox.Value),waveList.Text));
        }
    }
}
