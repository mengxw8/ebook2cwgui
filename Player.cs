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

        private int currentGroupIndex = -1;
        private int fileContentGroupCount = 0;
        private int lastHighlightStart = -1;
        private int lastHighlightLength = 0;

        public Player()
        {
            InitializeComponent();
            player.OnGroupPlay += OnGroupPlay;
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

        private void ToneBox_ValueChanged(object sender, EventArgs e)
        {
            player.UpdateFrequency(Convert.ToInt32(toneBox.Value));
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            playerWave.Stop();
            player.Clean();
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
            ContentTxb.Text = fileContent;
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
                MorseToMp3.ToMp3ByLine(File.ReadAllText(FilePathLbl.Text), code, MorseConfig.Create(Convert.ToInt32(speedBox.Value)), selectedFolderPath, player.Dit_buff!, player.Dah_buff!, true, true);
            }
        }

        private void ResetGroupTracking(string fileContent)
        {
            fileContentGroupCount = fileContent.Replace("\r\n", " ").Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            currentGroupIndex = -1;
            ResetHighlight();
            lastHighlightStart = -1;
            lastHighlightLength = 0;
        }

        private void OnGroupPlay(string group)
        {
            currentGroupIndex++;
            // 跳过报头(index 0)和报尾(index = fileContentGroupCount + 1)
            if (currentGroupIndex < 1 || currentGroupIndex > fileContentGroupCount)
            {
                return;
            }

            ContentTxb.BeginInvoke(() =>
            {
                ResetHighlight();

                var searchStart = lastHighlightStart + lastHighlightLength;
                if (searchStart < 0) searchStart = 0;

                var pos = ContentTxb.Find(group, searchStart, -1, RichTextBoxFinds.None);
                if (pos >= 0)
                {
                    ContentTxb.Select(pos, group.Length);
                    ContentTxb.SelectionBackColor = Color.Yellow;
                    lastHighlightStart = pos;
                    lastHighlightLength = group.Length;
                    ContentTxb.ScrollToCaret();
                }
            });
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

            playerWave?.Stop();
            player?.Clean();
        }

        private void waveList_SelectedIndexChanged(object sender, EventArgs e)
        {
            player.UpdateConfig(MorseConfig.Create(Convert.ToInt32(speedBox.Value),waveList.Text));
        }
    }
}
