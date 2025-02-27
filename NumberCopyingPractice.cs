using AngleSharp;
using AngleSharp.Dom;
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

namespace CW
{
    public partial class NumberCopyingPractice : Form
    {
        //[DllImport("user32.dll")]
        //static extern long LoadKeyboardLayout(string pwszKLID, uint Flags);
        [LibraryImport("user32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf8)]
        private static partial IntPtr LoadKeyboardLayoutA(string pwszKLID, uint Flags);


        MorsePlayer morsePlayer;
        WaveOutEvent waveOut = new WaveOutEvent();
        MorseConfig morseConfig = new MorseConfig();
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
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddMemoryFont(fontPtr, fontData.Length);
            var myCustomFont = new Font(pfc.Families[0], 25, FontStyle.Bold);
            answerBox.Font = myCustomFont;
            //初始化播放器
            morseConfig.Speed = Convert.ToInt32(speetBox.Value);
            morsePlayer = new MorsePlayer(Convert.ToInt32(toneBox.Value), morseConfig);
            waveOut.Init(morsePlayer);




        }

        //定义当前工作的模式，0分组数字，1分组字母，2分组字母数字，3英语文章
        WorkingMode mode = WorkingMode.None;
        //答案
        string answer = "";
        //上一次播放的音频文件路径
        string lastMusicPath = "";
        string lastCheckMusicPath = "";



        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.ShortNumber5;
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
            mode = WorkingMode.ShortNumber10;
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
            if ((words.Count == 0 || words == null) && mode != WorkingMode.Customize)
            {
                startBtn.Enabled = true;
                return;
            }
            StringBuilder answerBuilder = new();
            answerBuilder.Append(Constant.StartString);
            if (mode == WorkingMode.ShortNumber5 || mode == WorkingMode.ShortNumber10)
            {
                answerBuilder.Append(AnswerTools.GenerateAnswer(words ?? [], repeatRbtn.Checked, continuousRbtn.Checked, System.Convert.ToInt32(groupNumBox.Value), System.Convert.ToInt32(EachGroup.Value)));
            }



            answerBuilder.Append(Constant.EndString);
            if (mode != WorkingMode.Customize)
            {
                answer = answerBuilder.ToString();
                answer = answer.ToLower();
            }


            var fileName = DateTime.Now.ToUniversalTime().Ticks;
            string filePath = Constant.TempPath + fileName + ".txt";
            if (!Path.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? "");
            }





            //写入临时文件
            File.WriteAllText(filePath, answer);
            //生成音频
            //var task = Task.Run(() => { CWTools.GenerateAudio(fileName.ToString(), param); });
            waveOut.Stop();
           morsePlayer.Clean();
            var task = Task.Run(() =>
            {
                var keys = mode == WorkingMode.ShortNumber5 ? Constant.shortNumber5 : Constant.shortNumber10;
                keys.TryAdd('=', "-...-");
                keys.TryAdd('i', "..");
                morsePlayer.AddMorseCode(answer, keys,Convert.ToInt32(speetBox.Value));
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

            //处理校报逻辑
            if (showAnswerChb.Checked) {
                await task;
                
                var task2 = Task.Run(() =>
                {
                    var keys = mode == WorkingMode.ShortNumber5 ? Constant.shortNumber5 : Constant.shortNumber10;
                    keys.TryAdd('=', "-...-");
                    keys.TryAdd('i', "..");
                    morsePlayer.AddMorseCode(answer, keys,Convert.ToInt32(checkAnserSpeed.Value));
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
                switch (mode)
                {
                    case WorkingMode.Number: words.AddRange(Constant.number.Keys.Select(item => item.ToString())); break;
                    case WorkingMode.Alphabet: words.AddRange(Constant.alphabet.Keys.Select(item => item.ToString())); break;
                    case WorkingMode.AlphabetAndNumber: words.AddRange(Constant.numberAndAlphabet.Keys.Select(item => item.ToString())); break;
                    case WorkingMode.Symbol: words.AddRange(Constant.symbol.Keys.Select(item => item.ToString())); break;
                    case WorkingMode.Article: words.AddRange(new List<string>(Directory.GetFiles(Constant.ArticlePath, "*.txt", SearchOption.TopDirectoryOnly)).Select(n => n.Replace(Constant.ArticlePath, "")).ToList()); break;
                    case WorkingMode.News: words.AddRange(Constant.newsType.Keys); break;
                    case WorkingMode.Word: words.AddRange(Constant.alphabet.Keys.Select(item => item.ToString())); break;
                    case WorkingMode.ShortNumber5: words.AddRange(Constant.number.Keys.Select(item => item.ToString())); break;
                    case WorkingMode.ShortNumber10: words.AddRange(Constant.number.Keys.Select(item => item.ToString())); break;

                }
            }

            return words;
        }

        private void SubmitAnswerBtn_Click(object sender, EventArgs e)
        {
            Mp3Player.Stop();
            timer1.Stop();
            if (answer == "")
            {
                MessageBox.Show("请先开始抄收！");
                return;
            }
            AnswerBoard answerBoard = new(answer, answerBox.Text);
            answerBoard.ShowDialog();

        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            waveOut.Stop();

        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (lastMusicPath == "")
            {
                MessageBox.Show("您还尚未生成过报文哦，请生成后重试！");
                return;
            }
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "压缩文件(*.zip)|*.*",
                Title = "保存音频文件和报文到目录",
                FileName = "报文" + Path.GetFileName(lastMusicPath).Replace(".mp3", "") + "-" + speetBox.Value + "wpm.zip"
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

                // 添加文件到ZIP存档
                //添加音频
                string musicFileName = Path.GetFileName(lastMusicPath);
                archive.CreateEntryFromFile(lastMusicPath, musicFileName);
                //添加报文
                string txtFileName = Path.GetFileName(lastMusicPath.Replace(".mp3", ".txt"));
                archive.CreateEntryFromFile(lastMusicPath.Replace(".mp3", ".txt"), txtFileName);
                //添加校报音频
                if (checkAnswerChb.Checked)
                {
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
                //把小写的q换成大写的Q，符合抄写习惯
                answer = answer.Replace("q", "Q");
                answerBox.Text = answer.Replace(Constant.StartString, "").Replace(Constant.EndString, "");
            }
        }



        private void NumberCopyingPractice_FormClosed(object sender, FormClosedEventArgs e)
        {
            Mp3Player.Stop();
            //清除缓存
            if (lastMusicPath != null && Path.Exists(Path.GetDirectoryName(lastMusicPath)))
            {
                Directory.Delete(Path.GetDirectoryName(lastMusicPath) ?? "", true);
            }
        }
        private void SpeetBox_ValueChanged(object sender, EventArgs e)
        {
            checkAnserSpeed.Value = speetBox.Value + 2;
            morseConfig.Speed = Convert.ToInt32(speetBox.Value);
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
                var keys = mode == WorkingMode.ShortNumber5 ? Constant.shortNumber5 : Constant.shortNumber10;
                keys.TryAdd('=', "-...-");
                keys.TryAdd('i', "..");
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
        }





        private void repeatRbtn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void toneBox_ValueChanged(object sender, EventArgs e)
        {
            morsePlayer.UpdateFrequency(Convert.ToInt32(toneBox.Value));
        }
    }
}
