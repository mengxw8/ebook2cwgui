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
        private Dictionary<char, string> code = Constant.allCharCode;
        private readonly MorsePlayer player = new(600, MorseConfig.Create(20));
        // 创建 WaveOutEvent 对象来播放音频
        private readonly WaveOutEvent playerWave = new();
        public Player()
        {
            InitializeComponent();

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
            EncodingConfiguration encodingConfiguration = new();
            var result = encodingConfiguration.ShowDialog();
            if (result == DialogResult.OK)
            {
                code = encodingConfiguration.Code;
                player.UpdateEncoding(code);
            }

        }

        private void SpeedBox_ValueChanged(object sender, EventArgs e)
        {
            player.UpdateConfig(MorseConfig.Create(Convert.ToInt32(speedBox.Value)));
        }

        private void Player_Load(object sender, EventArgs e)
        {
            playerWave.Init(player);
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
            playerWave.Stop();
            player.Clean();
            player.AddMorseCode(File.ReadAllText(FilePathLbl.Text), code);
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
            playerWave.Stop();
            player.Clean();
            player.AddMorseCode(File.ReadAllText(FilePathLbl.Text), code);
            ContentTxb.Text = File.ReadAllText(FilePathLbl.Text);
            playerWave.Play();
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists(FilePathLbl.Text))
            {
                MessageBox.Show("文本文件不存在，无法导出！", "文件错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using  SaveFileDialog saveFileDialog = new()
            {
                Title = "选择音频保存位置",
                Filter = "音频文件(*.mp3)|*.mp3",
                FileName = DateTime.Now.ToUniversalTime().Ticks + ".mp3"
            };
            saveFileDialog.ShowDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                string selectedFolderPath = saveFileDialog.FileName;
                MorseToMp3.ToMp3(File.ReadAllText(FilePathLbl.Text),code,MorseConfig.Create(Convert.ToInt32(speedBox.Value)), selectedFolderPath, player.Dit_buff!,player.Dah_buff!);
            }

        }
    }
}
