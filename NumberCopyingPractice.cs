using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Text;
using CW.morse;
using Microsoft.VisualBasic.Devices;
using NAudio.SoundFont;
using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace CW
{
    public partial class NumberCopyingPractice : Form
    {
        //[DllImport("user32.dll")]
        //static extern long LoadKeyboardLayout(string pwszKLID, uint Flags);
        [LibraryImport("user32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf8)]
        private static partial IntPtr LoadKeyboardLayoutA(string pwszKLID, uint Flags);
        private  Dictionary<char, string> keys = new Dictionary<char, string>[] { Constant.alphabet, Constant.shortNumber5 , Constant.symbol }.SelectMany(disc => disc).ToDictionary(
group => group.Key,
group => group.Value // 取最后一个值（覆盖冲突键）
);

        private readonly MorsePlayer morsePlayer;
        private readonly WaveOutEvent waveOut = new();
        MorseConfig morseConfig = MorseConfig.Create(20);
        public NumberCopyingPractice()
        {
            InitializeComponent();
            //不允许息屏
            SystemSleep.PreventForCurrentThread();
            //输入法切换为英文
            LoadKeyboardLayoutA(Constant.EnglishKeyboardLayout, 1);

            ClearAnswer();
            //使用自定义字体
            byte[] fontData = Properties.Resources.consola;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            PrivateFontCollection pfc = new();
            pfc.AddMemoryFont(fontPtr, fontData.Length);
            var myCustomFont = new Font(pfc.Families[0], 25, FontStyle.Bold);
            answerBox.Font = myCustomFont;
            //初始化播放器
            morseConfig.Speed = Convert.ToInt32(speetBox.Value);
            morsePlayer = new MorsePlayer(Convert.ToInt32(toneBox.Value), morseConfig);
            waveOut.Init(morsePlayer);
        }


        //答案
        string answer = "";
        //上一次播放的音频文件路径
        string lastPath = "";



        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
           keys = new Dictionary<char, string>[] { Constant.alphabet, radioButton1.Checked ? Constant.shortNumber5 : Constant.shortNumber10, Constant.symbol }.SelectMany(disc => disc).ToDictionary(
group => group.Key,
group => group.Value // 取最后一个值（覆盖冲突键）
);

            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;
            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (var k in Constant.number.Keys)
            {
                eqBox.Items.Add(k);
                neBox.Items.Add(k);
            }
        }
        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
 
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;

            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (var k in Constant.number.Keys)
            {
                eqBox.Items.Add(k);
                neBox.Items.Add(k);
            }
        }





        //生成报文并播放
        private async void StartBtn_Click(object sender, EventArgs e)
        {
            startBtn.Enabled = false;
            //生成测试数据
            List<string> words = GetWords();
            if ((words.Count == 0 || words == null) && !radioButton4.Checked)
            {
                startBtn.Enabled = true;
                return;
            }
            StringBuilder answerBuilder = new();
            answerBuilder.Append(msgStartTxb.Text.ToLower());
            if (radioButton3.Checked )
            {
                answerBuilder.Append(AnswerTools.GenerateAnswer(words ?? [], repeatRbtn.Checked, continuousRbtn.Checked, System.Convert.ToInt32(groupNumBox.Value), System.Convert.ToInt32(EachGroup.Value)));
            }

            answerBuilder.Append(msgEndTxb.Text.ToLower());
            //自定义报文
            if (radioButton3.Checked)
            {
                answer = answerBuilder.ToString();
                answer = answer.ToLower();
            }
            


            var fileName = DateTime.Now.ToUniversalTime().Ticks;
            lastPath = Constant.TempPath + fileName + ".txt";
            if (!Path.Exists(Path.GetDirectoryName(lastPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(lastPath) ?? "");
            }
            //写入临时文件
            File.WriteAllText(lastPath, answer);
            waveOut.Stop();
            morsePlayer.Clean();
            var task = Task.Run(() =>
            {

                morsePlayer.AddMorseCode(answer, keys, Convert.ToInt32(speetBox.Value));

            });

            //解除封禁
            pauseBtn.Enabled = true;
            rePlayBtn.Enabled = true;


            //如果是开启了显示答案的按钮，直接显示答案
            if (showAnswerChb.Checked)
            {
                ShowAnswer();
            }

            //var audioFileName = filePath.Replace("txt", "mp3");

            waveOut.Play();
            //Mp3Player.Play(audioFileName);

            startBtn.Enabled = true;
            await task;
            //处理校报逻辑
            if (checkAnswerChb.Checked)
            {
                var task2 = Task.Run(() =>
                {                    
                    morsePlayer.AddMorseCode(answer, keys, Convert.ToInt32(checkAnserSpeed.Value));
                });
                await task2;
            }




        }

        private List<string> GetWords()
        {
            //确定字符范围
            List<string> words = [];


            var isRepeat = repeatRbtn.Checked;
            //同组无重复
            var isContinuous = continuousRbtn.Checked;
            //校验选项是否冲突
            if (eqRbtn.Checked)
            {
                //确定允许的值范围
                var eqList = eqBox.CheckedItems.Cast<string>().ToList();
                if (eqList.Count < 4 && isContinuous)
                {
                    MessageBox.Show("指定的字符数量太少，无法做到同组无重复！");
                    return words;
                }
                if (eqList.Count < 2 && isRepeat)
                {
                    MessageBox.Show("指定的字符数量太少，无法做到同组无连续！");
                    return words;
                }
                words.AddRange(eqList);
            }
            if (neRbtn.Checked)
            {

                List<string> uncheckedItems = [];
                for (int i = 0; i < neBox.Items.Count; i++)
                {
                    if (!neBox.GetItemChecked(i))
                    {
                        uncheckedItems.Add((string)neBox.Items[i]);
                    }
                }
                if (uncheckedItems.Count < 4 && isContinuous)
                {
                    MessageBox.Show("指定的字符数量太少，无法做到同组无重复！");
                    return words;

                }

                if (uncheckedItems.Count < 2 && isRepeat)
                {
                    MessageBox.Show("指定的字符数量太少，无法做到同组无连续！");
                    return words;
                }

                words.AddRange(uncheckedItems);
            }

            if (words.Count == 0)
            {
                words.AddRange(Constant.number.Keys.Select(item => item.ToString())); 

                
            }

            return words;
        }

        private void SubmitAnswerBtn_Click(object sender, EventArgs e)
        {

            waveOut.Stop();
            morsePlayer.Clean();
            timer1.Stop();
            if (answer == "")
            {
                MessageBox.Show("请先开始抄收！");
                return;
            }
            AnswerBoard answerBoard = new(answer.Replace(msgStartTxb.Text.ToLower(),"").Replace(msgEndTxb.Text.ToLower(), ""), answerBox.Text);
            answerBoard.ShowDialog();

        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            waveOut.Stop();

        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (lastPath == "")
            {
                MessageBox.Show("您还尚未生成过报文哦，请生成后重试！");
                return;
            }
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "压缩文件(*.zip)|*.*",
                Title = "保存音频文件和报文到目录",
                FileName = "报文" + Path.GetFileName(lastPath).Replace(".txt", "") + "-" + speetBox.Value + "WPM.zip"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileDialog.FileName))
                {
                    File.Delete(saveFileDialog.FileName);
                }
                //打包文件
                using FileStream zipToOpen = new(saveFileDialog.FileName, FileMode.Create);

                // 创建ZIP存档
                using ZipArchive archive = new(zipToOpen, ZipArchiveMode.Create);
                //生成mp3
                var lastMusicPath = lastPath.Replace(".txt", "-" + speetBox.Value + "WMP.mp3");
 
                MorseToMp3.ToMp3(answer, keys, MorseConfig.Create(Convert.ToInt32(speetBox.Value)), lastMusicPath, morsePlayer.Dit_buff!, morsePlayer.Dah_buff!);
                // 添加文件到ZIP存档
                //添加音频
                string musicFileName = Path.GetFileName(lastMusicPath);
                archive.CreateEntryFromFile(lastMusicPath, musicFileName);
                //添加报文
                string txtFileName = Path.GetFileName(lastPath);
                archive.CreateEntryFromFile(lastPath.Replace(".mp3", ".txt"), txtFileName);
                //添加校报音频
                if (checkAnswerChb.Checked)
                {
                    var lastCheckMusicPath = lastPath.Replace(".txt", "-" + checkAnserSpeed.Value + "WPM-check.mp3");
                    var config = MorseConfig.Create(Convert.ToInt32(checkAnserSpeed.Value));
                    morsePlayer.UpdateConfig(config);
                    MorseToMp3.ToMp3(answer, keys, config, lastCheckMusicPath, morsePlayer.Dit_buff!, morsePlayer.Dah_buff!);
                    string checkFileName = Path.GetFileName(lastCheckMusicPath);
                    archive.CreateEntryFromFile(lastCheckMusicPath, checkFileName);
                }



            }

        }

        private void CheckAnswerChb_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAnswerChb.Checked)
            {
                checkAnserSpeed.Enabled = true;
            }
            else
            {
                checkAnserSpeed.Enabled = false;
            }
        }

        private void ShowAnswerChb_CheckedChanged(object sender, EventArgs e)
        {
            if (showAnswerChb.Checked && answer != "")
            {
                ShowAnswer();
            }
        }
        /// <summary>
        /// 展示答案
        /// </summary>
        private void ShowAnswer()
        {

            if (answer != "")
            {
                answerBox.Text = answer.Replace(msgStartTxb.Text.ToLower(),"").Replace(msgEndTxb.Text.ToLower(), "");
            }
        }



        private void NumberCopyingPractice_FormClosed(object sender, FormClosedEventArgs e)
        {
            waveOut.Stop();
            morsePlayer.Clean();
            //清除缓存
            if (Path.Exists(Path.GetDirectoryName(Constant.TempPath)))
            {
                Directory.Delete(Path.GetDirectoryName(Constant.TempPath) ?? "", true);
            }
        }
        private void SpeetBox_ValueChanged(object sender, EventArgs e)
        {
            checkAnserSpeed.Value = speetBox.Value + 2;
            morseConfig = MorseConfig.Create(Convert.ToInt32(speetBox.Value));
            morsePlayer.UpdateConfig(morseConfig);
        }

        private void ClearAnswer()
        {
            answerBox.Text = "";

        }
        //清空答案
        private void ClearAnswer_Click(object sender, EventArgs e)
        {
            ClearAnswer();
            showAnswerChb.Checked = false;
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            waveOut?.Pause();
            continuePlayBtn.Enabled = true;
            pauseBtn.Enabled = false;
        }
        private void ContinuePlayBtn_Click(object sender, EventArgs e)
        {
            waveOut?.Play();
            continuePlayBtn.Enabled = false;
            pauseBtn.Enabled = true;
            if (checkAnswerChb.Checked)
            {
                timer1.Start();
            }
        }

        private void ResumeBtn_Click(object sender, EventArgs e)
        {
            //重播，直接情况重新生成
            waveOut.Stop();
            morsePlayer.Clean();
            var task = Task.Run(() =>
            {

                morsePlayer.AddMorseCode(answer, keys);

            });
            waveOut.Play();
        }

        private void NeRbtn_CheckedChanged(object sender, EventArgs e)
        {
            neBox.Enabled = true;
            eqBox.Enabled = false;
        }

        private void EqRbtn_CheckedChanged(object sender, EventArgs e)
        {
            eqBox.Enabled = true;
            neBox.Enabled = false;
        }

        private void NumberCopyingPractice_Load(object sender, EventArgs e)
        {
            // 获取当前程序集的版本
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Version version = currentAssembly.GetName().Version ?? new Version(1, 0, 0, 0);
            this.Text = this.Text + " V" + version;
            radioButton1.Checked = true;
            radioButton3.Checked = true;
        }





        private void RepeatRbtn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ToneBox_ValueChanged(object sender, EventArgs e)
        {
            morsePlayer.UpdateFrequency(Convert.ToInt32(toneBox.Value));
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            //加载本地文件内容
            if (radioButton4.Checked)
            {
                //弹出文件选择框
                OpenFileDialog openImageDialog = new()
                {
                    Filter = "报文文本(*.txt)|*.txt",
                    Multiselect = false//关闭多选
                };


                if (openImageDialog.ShowDialog() == DialogResult.OK)
                {
                    answer =(msgStartTxb.Text+ File.ReadAllText(openImageDialog.FileName)+msgEndTxb.Text).ToLower();
                    if (showAnswerChb.Checked)
                    {
                        ShowAnswer();
                    }
                }
                else
                {
                    MessageBox.Show("未选择任何文件,试试其他模式吧!");
                    //没有选择文件就自动生成
                    radioButton3.Checked = true;
                    answer = "";

                }
            }
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
    

            }
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
           keys = new Dictionary<char, string>[] { Constant.alphabet, radioButton1.Checked ? Constant.shortNumber5 : Constant.shortNumber10, Constant.symbol }.SelectMany(disc => disc).ToDictionary(
group => group.Key,
group => group.Value // 取最后一个值（覆盖冲突键）
);
        }

        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {
           
        }
    }
}
