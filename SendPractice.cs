﻿using AngleSharp.Dom;
using AngleSharp;
using Microsoft.VisualBasic.Logging;
using NAudio.SoundFont;
using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;
using NAudio.Wave.SampleProviders;
using System.Numerics;
using CW.morse;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CW
{
    public partial class SendPractice : Form
    {
        //[DllImport("user32.dll")]
        //static extern long LoadKeyboardLayout(string pwszKLID, uint Flags);
        [LibraryImport("user32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf8)]
        private static partial IntPtr LoadKeyboardLayoutA([MarshalAs(UnmanagedType.LPStr)] string pwszKLID, uint Flags);
        // 创建 WaveOutEvent 对象来播放音频
        private WaveOutEvent playerWave = new();
        private MorsePlayer player;
        //当前键类型
        private static KeyType keyType;

        //发报声音
        SineWaveProvider sineWaveProvider;
        private static WaveOutEvent transmitWave = new();
        //用来记录发报的时长
        private readonly Queue<double> audioRecordQueue = new ();


        public SendPractice()
        {
            InitializeComponent();
            //不允许息屏
            SystemSleep.PreventForCurrentThread();
            //输入法切换为英文
            LoadKeyboardLayoutA(Constant.EnglishKeyboardLayout, 1);


        }
        // 导入 timeSetEvent, timeKillEvent 和 MMRESULT 枚举
        //private const uint TIME_KILL_EVENT = 0;
        private const uint TIME_PERIODIC = 1;
        //定时器分辨率，1ms级别
        private const uint TIMER_RESOLUTION = 1;
        // 获取开始计数值
        [LibraryImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool QueryPerformanceCounter(out long lpPerformanceCount);
        // 获取性能计数器频率
        [LibraryImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool QueryPerformanceFrequency(out long lpFrequency);
        [LibraryImport("winmm.dll", SetLastError = true)]
        private static partial uint timeSetEvent(
            uint uDelay,
            uint uResolution,
            TimerCallback lpTimeFunc,
            UIntPtr dwUser,
            uint fuEvent);


        //是否在绘制中
        private static bool isDraw = false;
        //当前按下的是哪个
        private static MouseButtons pressMouseButton = MouseButtons.Left;
        private static int drawCount = 0;
        //空闲绘制的长度,词间隔
        private static int blankWidth = 42;
        private static int DiLength = 0;
        private static int DaLength = 0;
        //字间隔
        private static int keyWidth = 18;
        //是否空闲
        private static int wait = blankWidth + 1;
        //每次绘制的宽度
        private readonly static int drawWidth = 1;
        //可视化用来显示的字体
        private readonly static System.Drawing.Font font = new("Arial‌", 8, FontStyle.Regular, GraphicsUnit.Point);
        // 回调函数的委托类型
        private delegate void TimerCallback(UIntPtr uTimerID, UIntPtr uMsg, UIntPtr dwUser, UIntPtr dw1, UIntPtr dw2);
        //定义当前工作的模式，0分组数字，1分组字母，2分组字母数字，3英语文章
        private static WorkingMode mode = WorkingMode.None;
        //答案
        string answer = "";
        //上一次播放的音频文件路径
        string lastBookPath = "";
        //用来装生成的图形
        private readonly static ConcurrentQueue<Bitmap> bitmapQueue = new(); // 双缓冲队列
        //用来装敲过的莫尔斯电码字符
        private readonly static ConcurrentQueue<char> codeQueue = new();
        //用来装解析出来的答案
        private readonly static ConcurrentQueue<char> inputQueue = new();
        private readonly static StringBuilder inputBuilde = new();
        //当前帧
        private static Bitmap? bitmap;
        //是否严格解析
        private static bool isStrict = false;
        //用来显示参考文本的label
        private readonly static List<System.Windows.Forms.Label> answerLableList = new(6);
        private readonly static List<System.Windows.Forms.RichTextBox> inputList = new(6);

        //用来标记高精度定时器是否执行过
        private static volatile bool isThrob = false;


        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.Number;

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
            mode = WorkingMode.Alphabet;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;


            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (var k in Constant.alphabet.Keys)
            {
                eqBox.Items.Add(k);
                neBox.Items.Add(k);
            }
        }
        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.AlphabetAndNumber;

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
            foreach (var k in Constant.alphabet.Keys)
            {
                eqBox.Items.Add(k);
                neBox.Items.Add(k);
            }
        }

        private void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.Symbol;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;


            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (var k in Constant.symbol.Keys)
            {
                eqBox.Items.Add(k);
                neBox.Items.Add(k);
            }
        }
        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.Article;
            //英文文章
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;


            //加载文章列表
            // 确保路径是目录并且存在
            if (!Directory.Exists(Constant.ArticlePath))
            {
                MessageBox.Show("没有可供的选择文章!");
                return;
            }

            List<string> files = new(Directory.GetFiles(Constant.ArticlePath, "*.txt", SearchOption.TopDirectoryOnly));
            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (string file in files)
            {
                string fileNmae = file.Replace(Constant.ArticlePath, "");
                eqBox.Items.Add(fileNmae);
                neBox.Items.Add(fileNmae);
            }

        }
        //新闻
        private void RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.News;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;

            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (string type in Constant.newsType.Keys)
            {
                eqBox.Items.Add(type);
                neBox.Items.Add(type);
            }

        }
        //随机单词
        private void RadioButton8_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton2_CheckedChanged(sender, e);
            mode = WorkingMode.Word;
        }


        //生成报文并播放
        private async void StartBtn_Click(object sender, EventArgs e)
        {
            startBtn.Enabled = false;
            inputBuilde.Clear();
            //清空录音
            audioRecordQueue.Clear();
            //生成测试数据
            List<string> words = GetWords();
            if ((words.Count == 0 || words == null) && mode != WorkingMode.Customize)
            {
                startBtn.Enabled = true;
                return;
            }
            answer = "";
            StringBuilder answerBuilder = new();
            answerBuilder.Append(Constant.StartString);
            if (mode == WorkingMode.Number || mode == WorkingMode.Alphabet || mode == WorkingMode.AlphabetAndNumber || mode == WorkingMode.Symbol)
            {
                answerBuilder.Append(AnswerTools.GenerateAnswer(words ?? [], repeatRbtn.Checked, continuousRbtn.Checked, System.Convert.ToInt32(groupNumBox.Value), System.Convert.ToInt32(EachGroup.Value)));

            }
            else if (mode == WorkingMode.Article)
            {
                answerBuilder.Append(ArticleTools.GetArticle(words ?? [], symbolsChb.Checked, System.Convert.ToInt32(groupNumBox.Value)));
            }
            else if (mode == WorkingMode.News)
            {
                //检查网络
                if (newspapers.HttpRequestUtil.GetWebRequest("https://www.cgtn.com/subscribe/rss/section/china.xml") == "")
                {
                    MessageBox.Show("当前网络不通畅，请试试其他模式吧！");
                    startBtn.Enabled = true;
                    return;
                }
                try
                {
                    answerBuilder.Append(NewsPapersTools.GetNewsPapers(words ?? [], System.Convert.ToInt32(groupNumBox.Value), symbolsChb.Checked));
                }
                catch
                {
                    MessageBox.Show("当前网络不通畅，请试试其他模式吧！");
                    startBtn.Enabled = true;
                    return;
                }


            }
            else if (mode == WorkingMode.Word)
            {
                answerBuilder.Append(WordsTools.GenerateWord(words ?? [], System.Convert.ToInt32(groupNumBox.Value)));



            }
            else if (mode == WorkingMode.Customize)
            {

            }

            answerBuilder.Append(Constant.EndString);
            if (mode != WorkingMode.Customize)
            {
                answer = answerBuilder.ToString();
                answer = answer.ToLower();
            }


            var fileName = DateTime.Now.ToUniversalTime().Ticks;
            var filePath = Constant.TempPath + fileName + ".txt";
            if (!Path.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? "");
            }

            //写入临时文件
            File.WriteAllText(filePath, answer);
            lastBookPath = filePath;

            //显示报文
            ShowAnswer();
            player.UpdateFrequency(Convert.ToInt32(toneBox.Value));
            //停止播放并清空播放内容
            playerWave.Stop();
            player.Clean();
            if (bgmCbx.Checked)
            {
                //开始混音
                player.AddMorseCode(answer, Constant.allCharCode);
                playerWave.Play();
            }
            //解除封禁
            pauseBtn.Enabled = true;
            rePlayBtn.Enabled = true;
            startBtn.Enabled = true;
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

                }
            }

            return words;
        }



        private void StopBtn_Click(object sender, EventArgs e)
        {
            player.Clean();
            timer1.Stop();
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (lastBookPath == "")
            {
                MessageBox.Show("您还尚未生成过报文哦，请生成后重试！");
                return;
            }
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "压缩文件(*.zip)|*.*",
                Title = "保存音频文件和报文到目录",
                FileName = "拍发报文" + Path.GetFileName(lastBookPath).Replace(".txt", "") + "-" + speetBox.Value + "wpm.zip",
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
                string musicFileName = lastBookPath.Replace(".txt", ".mp3");                
                MorseToMp3.toMp3(answer, Constant.allCharCode, new MorseConfig { Speed = Convert.ToInt32(speetBox.Value) }, musicFileName, player.WaveFormat, player.dit_buff, player.dah_buff);
                archive.CreateEntryFromFile( musicFileName, Path.GetFileName(lastBookPath).Replace(".txt", ".mp3"));
                //添加报文
                string txtFileName = Path.GetFileName(lastBookPath);
                archive.CreateEntryFromFile(lastBookPath, txtFileName);
                //如果是选择了录音的，则生成自己刚刚拍发的这段内容
                if (recordingChb.Checked&&audioRecordQueue.Count>0) {
                    //生成拍发音频文件名
                    var outFileName = lastBookPath.Replace(".txt", "") + "-" + sendSpeedTxb.Text + "wpm拍发.mp3";
                    MorseToMp3.toMp3(audioRecordQueue.ToList(),  outFileName, player.WaveFormat, Convert.ToInt32(sendToneBox.Text));
                    audioRecordQueue.Clear();
                    archive.CreateEntryFromFile( outFileName, Path.GetFileName(lastBookPath).Replace(".txt", "") + "-" + sendSpeedTxb.Text + "wpm拍发.mp3");

                }


            }

        }



        private void Timer1_Tick(object sender, EventArgs e)
        {


        }

        private void CopyingPractice_FormClosed(object sender, FormClosedEventArgs e)
        {

            //清除缓存
            if (lastBookPath != null && Path.Exists(Path.GetDirectoryName(lastBookPath)))
            {
                Directory.Delete(Path.GetDirectoryName(lastBookPath) ?? "", true);
            }
            playerWave?.Stop();
            playerWave?.Dispose();
            transmitWave?.Stop();
            transmitWave?.Dispose();
        }

        //把生成的报文展示出来
        private void ShowAnswer()
        {
            //清空所有内容
            foreach (var item in answerLableList)
            {
                item.Text = "";
            }
            CleanInput();
            if (answer == "")
            {
                return;
            }
            var showAnswer = answer.Replace(Constant.StartString, "").Replace(Constant.EndString, "");
            var data = showAnswer.Split(" ");
            var index = 0;
            var lableIndex = 0;
            StringBuilder sb = new();
            while (index < data.Length)
            {
                if (lableIndex >= answerLableList.Count)
                {
                    break;
                }
                var tempLable = answerLableList[lableIndex];
                Size textSize = TextRenderer.MeasureText(sb.ToString() + data[index], tempLable.Font);
                // 检查文本是否适合 Label 的宽度和高度
                if (textSize.Width > tempLable.Width || textSize.Height > tempLable.Height)
                {
                    //显示不下，需要换行
                    tempLable.Text = sb.ToString();
                    sb.Clear();
                    lableIndex++;
                }
                else
                {
                    sb.Append(data[index]);
                    sb.Append(' ');
                    tempLable.Text = sb.ToString();
                }

                index++;
            }





        }
        //把敲出来的字符展示出来
        private static void ShowInput(string inputStr)
        {
            var data = inputStr.Split(" ");
            var index = 0;
            var lableIndex = 0;
            StringBuilder sb = new();
            while (index < data.Length)
            {
                if (lableIndex >= inputList.Count)
                {
                    break;
                }
                var temp = inputList[lableIndex];
                var tempLable = answerLableList[lableIndex];
                // 检查文本是否适合 Label 的宽度和高度
                if ((sb.Length + data[index].Length) > tempLable.Text.Length)
                {
                    //显示不下，需要换行
                    temp.Text = sb.ToString();
                    sb.Clear();
                    lableIndex++;
                }
                else
                {
                    sb.Append(data[index]);
                    sb.Append(' ');
                    temp.Text = sb.ToString();
                }

                index++;
            }

        }
        /// <summary>
        /// 清除输入框中的内容
        /// </summary>
        private static void CleanInput()
        {
            inputBuilde.Clear();
            foreach (var item in inputList)
            {
                item.Text = "";
            }

        }

        //清空答案
        private void ClearAnswer_Click(object sender, EventArgs e)
        {
            CleanInput();
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            playerWave.Pause();
            continuePlayBtn.Enabled = true;
            pauseBtn.Enabled = false;
        }
        private void ContinuePlayBtn_Click(object sender, EventArgs e)
        {
            if (playerWave != null && playerWave.PlaybackState == PlaybackState.Paused)
            {
                playerWave.Play();
            }
            continuePlayBtn.Enabled = false;
            pauseBtn.Enabled = true;

        }

        private void ResumeBtn_Click(object sender, EventArgs e)
        {
            playerWave.Stop();
            player.Clean();
            player.AddMorseCode(answer, Constant.allCharCode);
            playerWave.Play();
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

        private void CopyingPractice_Load(object sender, EventArgs e)
        {
            // 获取当前程序集的版本
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Version version = currentAssembly.GetName().Version ?? new Version(1, 0, 0, 0);
            this.Text = this.Text + " V" + version;
            //初始化画布
            bitmap = new Bitmap(visualizedBox.Width, visualizedBox.Height);
            //初始化显示标签
            answerLableList.Add(answerLbl1);
            answerLableList.Add(answerLbl2);
            answerLableList.Add(answerLbl3);
            answerLableList.Add(answerLbl4);
            answerLableList.Add(answerLbl5);
            answerLableList.Add(answerLbl6);

            //初始化输入显示标签，就是显示回答内容的那一栏
            inputList.Add(replicationBox1);
            inputList.Add(replicationBox2);
            inputList.Add(replicationBox3);
            inputList.Add(replicationBox4);
            inputList.Add(replicationBox5);
            inputList.Add(replicationBox6);
            //屏蔽输入
            replicationBox1.ReadOnly = true;
            replicationBox2.ReadOnly = true;
            replicationBox3.ReadOnly = true;
            replicationBox4.ReadOnly = true;
            replicationBox5.ReadOnly = true;
            replicationBox6.ReadOnly = true;
            //初始化声音
            //初始化播放器
            player = new MorsePlayer(Convert.ToInt32(toneBox.Value), new MorseConfig { Speed = Convert.ToInt32(speetBox.Value) });
            playerWave.Init(player);
            // 创建 SineWaveProvider
            sineWaveProvider = new(System.Convert.ToDouble(sendToneBox.Text));
            // 将 SineWaveProvider 连接到 WaveOutEvent
            transmitWave.Init(sineWaveProvider);
            //默认选中手键
            ordinaryKey.Checked = true;
            keyType = KeyType.Ordinary;
            //初始化定时器
            TimerCallback callback = TimerProc;
            UIntPtr user = UIntPtr.Zero;
            uint timerId = timeSetEvent(
                10, // 延迟 1000 毫秒（1 秒）
                TIMER_RESOLUTION, // 分辨率
                callback,
                user,
                TIME_PERIODIC // 周期性定时器
            );

        }

        private void IndividuationRbtn_CheckedChanged(object sender, EventArgs e)
        {
            if (individuationRbtn.Checked == true)
            {
                mode = WorkingMode.Customize;

                eqRbtn.Enabled = false;
                neRbtn.Enabled = false;
                //弹出文件选择框
                OpenFileDialog openImageDialog = new()
                {
                    Filter = "报文(*.txt)|*.txt",
                    Multiselect = false
                };
                if (openImageDialog.ShowDialog() == DialogResult.OK)
                {
                    answer = File.ReadAllText(openImageDialog.FileName);

                }
                else
                {
                    MessageBox.Show("未选择任何文件,试试其他模式吧!");
                    radioButton8.Checked = true;

                }
            }

        }
        /// <summary>
        /// 高精度定时器，主要处理按键按下后的图像绘制
        /// </summary>
        /// <param name="uTimerID"></param>
        /// <param name="uMsg"></param>
        /// <param name="dwUser"></param>
        /// <param name="dw1"></param>
        /// <param name="dw2"></param>
        private static void TimerProc(UIntPtr uTimerID, UIntPtr uMsg, UIntPtr dwUser, UIntPtr dw1, UIntPtr dw2)
        {
            if (isDraw || wait <= blankWidth && !isDraw)
            {
                var color = Color.Black;
                if (isDraw)
                {
                    isThrob = true;
                    wait = 0;
                    drawCount++;
                    //如果是自动键，时间到了要自己停
                    if (keyType == KeyType.Auto && pressMouseButton == MouseButtons.Left && drawCount == DiLength)
                    {
                        isDraw = false;
                        transmitWave.Stop();
                    }
                    else if (keyType == KeyType.Auto && pressMouseButton == MouseButtons.Right && drawCount == DaLength)
                    {
                        isDraw = false;
                        transmitWave.Stop();
                    }
                }
                else if (wait <= blankWidth && !isDraw)
                {
                    wait++;
                    color = SystemColors.Control;
                }

                char str = ' ';
                //解析字符
                if ((wait == keyWidth || wait > blankWidth) && !isDraw)
                {
                    var sb = new StringBuilder();
                    while (codeQueue.TryDequeue(out char c))
                    {
                        sb.Append(c);
                    }

                    while (sb.Length != 0)
                    {
                        if (Constant.allCode.TryGetValue(sb.ToString(), out str))
                        {
                            break;
                        }
                        sb.Length--;
                    }
                    if (str != ' ')
                    {
                        inputQueue.Enqueue(str);
                    }
                }
                //空闲时间够了，输入一个空格
                if (wait == blankWidth)
                {
                    inputQueue.Enqueue(' ');
                }
                Bitmap map = new(bitmap!.Width, bitmap.Height);
                using (Graphics g = Graphics.FromImage(map))
                {
                    // 将原图像绘制到新图像中，向左平移指定偏移量
                    g.DrawImage(bitmap, -drawWidth, 0);
                    //水平位置
                    var horizontalPosition = (bitmap.Height / 2) - 10;

                    using (Pen pen = new(color, 5)) // 2是线条的宽度
                    {

                        // 绘制竖线
                        // 参数分别为：x1, y1, x2, y2，表示线条的起点和终点
                        g.DrawLine(pen, bitmap.Width - drawWidth, horizontalPosition, bitmap.Width, horizontalPosition);

                    }
                    //写字
                    if (str != ' ')
                    {
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        // 设置文本要绘制的位置
                        Point position = new(bitmap.Width - 25, horizontalPosition + 15);

                        g.DrawString(str.ToString(), font, Brushes.Black, position);


                    }

                }
                bitmap.Dispose();
                bitmap = map;
                bitmapQueue.Enqueue((Bitmap)bitmap.Clone());

            }
        }



        long startTime=0;
        //按下
        private void SendBtn_MouseDown(object sender, MouseEventArgs e)
        {
            //开始绘制
            isDraw = true;
            isThrob = false;
            pressMouseButton = e.Button;
            drawCount = 0;
            // 开始播放音频
            transmitWave.Play();
    
                //记录空白时间
                if (startTime > 0&& recordingChb.Checked)
                {
                    //结束计时
                    QueryPerformanceCounter(out long endTime);
                    QueryPerformanceFrequency(out long lpFrequency);
                    var t = ((endTime - startTime) /(double)lpFrequency) * 1000;
                    audioRecordQueue.Enqueue(t);
                }
            

                //开始计时            
                QueryPerformanceCounter(out startTime);
           

        }
        //抬起

        private void SendBtn_MouseUp(object sender, MouseEventArgs e)
        {
            isDraw = false;
            //停止播放声音
            transmitWave.Stop();
            //结束计时
            QueryPerformanceCounter(out long endTime);
            QueryPerformanceFrequency(out long lpFrequency);

      
            if (keyType == KeyType.Ordinary)
            {
                var t = ((endTime - startTime) / (double)lpFrequency) * 1000;
                //记录发报时长
                if (recordingChb.Checked)
                {
                    audioRecordQueue.Enqueue(t);
                    //重新开始记录空白时间
                    QueryPerformanceCounter(out startTime);
                }
                else {
                    startTime = 0;
                }

                //有可能出现按下时间非常短的情况，短于10ms,定时器都还来不及触发,所以画面上还没有展示出来
                if (!isThrob)
                {
                    return;
                }
                //暂且认为，比Da短的就是Di
                //严格被勾选则比Di长的都为Da         

                if (t >= System.Convert.ToInt16(sendDaLength.Text) || (isStrict && t > System.Convert.ToInt16(sendDiLength.Text)))
                {
                    codeQueue.Enqueue('-');
                }
                else
                {
                    codeQueue.Enqueue('.');
                }
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                }
                codeQueue.Enqueue(e.Button == MouseButtons.Left ? '.' : '-');
            }
        }
        /// <summary>
        /// 这个定时器的作用就是把队列中的图像刷新到页面上显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer2_Tick(object sender, EventArgs e)
        {
            //刷新显示
            for (int i = 0; i < bitmapQueue.Count; i++)
            {
                if (bitmapQueue.TryDequeue(out Bitmap? sb))
                {
                    visualizedBox.Image?.Dispose();

                    visualizedBox.Image = sb;
                }
            }
            //填充答案
            //把缓存的内容全部取出来
            for (int i = 0; i < inputQueue.Count; i++)
            {
                if (inputQueue.TryDequeue(out char inputChar))
                {
                    inputBuilde.Append(inputChar);
                }
            }
            ShowInput(inputBuilde.ToString().ToLower());



        }

        private void SnedSpeedTxb_Leave(object sender, EventArgs e)
        {
            var speed = 20;
            try
            {
                speed = System.Convert.ToInt16(sendSpeedTxb.Text);
            }
            catch (Exception)
            {
            }

            if (speed > 99 || speed < 0)
            {
                sendSpeedTxb.Text = "20";
                speed = 20;
            }
            //计算剩下的值以Paris计
            var config = MorseConfig.Create(speed);
            var di = 60000 / (speed * 50);
            sendDiLength.Text = config.Di.ToString();
            sendDaLength.Text = config.Da.ToString();
            keyInterval.Text = config.KeystrokeInterval.ToString();
            charInterval.Text = config.CharInterval.ToString();
        }

        private void NumberTxb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != 8)                       //判断输入的字符是否为十进制数字,是否为退格（输入错误可删除）
            {
                e.Handled = true;                               //将事件标记为已处理，否则无效字符会继续填充进去
            }
        }

        private void CharInterval_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var value = (System.Convert.ToInt16(charInterval.Text) / 10);
                wait = value + 1;
                blankWidth = value;
            }
            catch (Exception)
            {
            }
        }
        private void KeyInterval_TextChanged(object sender, EventArgs e)
        {
            try
            {
                keyWidth = (System.Convert.ToInt16(sendDaLength.Text) / 10);
            }
            catch (Exception)
            {
            }
        }

        private void StrictCbx_CheckedChanged(object sender, EventArgs e)
        {
            isStrict = strictCbx.Checked;
        }

        private void SendToneBox_TextChanged(object sender, EventArgs e)
        {
            if (sendToneBox.Text == "") {
                sendToneBox.Text = "1";
            }
            //改变发报声音频率
            sineWaveProvider = new(System.Convert.ToDouble(sendToneBox.Text));
            transmitWave?.Stop();
            transmitWave?.Dispose();
            transmitWave = new();
            // 将 SineWaveProvider 连接到 WaveOutEvent
            transmitWave.Init(sineWaveProvider);

        }

        private void SendPractice_SizeChanged(object sender, EventArgs e)
        {
            bitmap?.Dispose();
            if (visualizedBox.Width > 0 && visualizedBox.Height > 0)
            {
                bitmap = new Bitmap(visualizedBox.Width, visualizedBox.Height);
            }

        }

        private void volumeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            player.Volume = volumeTrackBar.Value * 0.1f;
        }

        private void ordinaryKey_CheckedChanged(object sender, EventArgs e)
        {
            if (ordinaryKey.Checked)
            {
                keyType = KeyType.Ordinary;
            }
            else
            {

                keyType = KeyType.Auto;
            }
        }

        private void sendDaLength_TextChanged(object sender, EventArgs e)
        {
            DaLength = Convert.ToInt32(sendDaLength.Text.Trim())/10;
        }

        private void sendDiLength_TextChanged(object sender, EventArgs e)
        {
            DiLength = Convert.ToInt32(sendDiLength.Text.Trim())/10;
        }
    }
}
