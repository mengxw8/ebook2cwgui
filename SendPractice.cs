using AngleSharp.Dom;
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

namespace CW
{
    public partial class SendPractice : Form
    {
        //[DllImport("user32.dll")]
        //static extern long LoadKeyboardLayout(string pwszKLID, uint Flags);
        [LibraryImport("user32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf8)]
        private static partial IntPtr LoadKeyboardLayoutA([MarshalAs(UnmanagedType.LPStr)] string pwszKLID, uint Flags);

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
        //空闲绘制的长度,词间隔
        private static int blankWidth = 42;
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
        string lastMusicPath = "";
        //用来装生成的图形
        private readonly static ConcurrentQueue<Bitmap> bitmapQueue = new(); // 双缓冲队列
        //用来装敲过的莫尔斯电码字符
        private readonly static ConcurrentQueue<char> codeQueue = new();
        //用来装解析出来的答案
        private readonly static ConcurrentQueue<char> inputQueue = new();
        private readonly static StringBuilder inputBuilde = new ();
        //当前帧
        private static Bitmap? bitmap;
        //是否严格解析
        private static bool isStrict = false;
        //用来显示参考文本的label
        private readonly static List<System.Windows.Forms.Label> answerLableList = new(6);
        private readonly static List<System.Windows.Forms.RichTextBox> inputList = new(6);
        // 创建 WaveOutEvent 对象来播放音频
        WaveOutEvent waveOut = new();
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
            inputBuilde.Clear();
            //生成测试数据
            List<string> words = GetWords();
            if ((words.Count == 0 || words == null) && mode != WorkingMode.Customize)
            {
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
                    return;
                }
                try
                {
                    answerBuilder.Append(NewsPapersTools.GetNewsPapers(words ?? [], System.Convert.ToInt32(groupNumBox.Value), symbolsChb.Checked));
                }
                catch
                {
                    MessageBox.Show("当前网络不通畅，请试试其他模式吧！");
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
            //生成音频
            var param = " -q 1 -c - -o " + Constant.TempPath + fileName + " -w " + speetBox.Value + " -f " + toneBox.Value + " " + filePath;
            // 启动一个新任务
            Task task = Task.Run(() => { CWTools.GenerateAudio(fileName.ToString(), param); });
            var audioFileName =Constant.TempPath+ fileName + ".mp3";            
            //显示报文
            ShowAnswer();

            //播放音频
            Mp3Player.Stop();

            if (bgmCbx.Checked)
            {
                lastMusicPath = audioFileName;
                // 等待任务完成
                await task;
                Mp3Player.Play(audioFileName);
            }


            //解除封禁
            pauseBtn.Enabled = true;
            rePlayBtn.Enabled = true;



            await task;
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
                    case WorkingMode.AlphabetAndNumber: words.AddRange(Constant.numberAndAlphabet.Keys.Select(item => item.ToString()));  break;
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
            Mp3Player.Stop();
            timer1.Stop();
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                FileName = "报文" + Path.GetFileName(lastMusicPath).Replace(".mp3", "") + "-" + speetBox.Value + "wpm.zip",
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


            }

        }



        private void Timer1_Tick(object sender, EventArgs e)
        {


        }

        private void CopyingPractice_FormClosed(object sender, FormClosedEventArgs e)
        {
            Mp3Player.Stop();
            //清除缓存
            if (lastMusicPath != null && Path.Exists(Path.GetDirectoryName(lastMusicPath)))
            {
                Directory.Delete(Path.GetDirectoryName(lastMusicPath) ?? "", true);
            }
            waveOut?.Stop();
            waveOut?.Dispose();
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
            answer = answer.Replace(Constant.StartString, "").Replace(Constant.EndString, "");
            var data = answer.Split(" ");
            var index = 0;
            var lableIndex = 0;
            StringBuilder sb = new ();
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
            StringBuilder sb = new ();
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
        private static void CleanInput() {
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
            Mp3Player.Pause();
            continuePlayBtn.Enabled = true;
            pauseBtn.Enabled = false;
        }
        private void ContinuePlayBtn_Click(object sender, EventArgs e)
        {
            Mp3Player.ContinuePlay();
            continuePlayBtn.Enabled = false;
            pauseBtn.Enabled = true;

        }

        private void ResumeBtn_Click(object sender, EventArgs e)
        {
            Mp3Player.Play(lastMusicPath);
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
            // 创建 SineWaveProvider
            SineWaveProvider sineWaveProvider = new (System.Convert.ToDouble(sendToneBox.Text));
            // 将 SineWaveProvider 连接到 WaveOutEvent
            waveOut.Init(sineWaveProvider);
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



        long startTime;
        private void SendBtn_MouseDown(object sender, MouseEventArgs e)
        {
            //开始绘制
            isDraw = true;
            isThrob = false;


            // 开始播放音频
            waveOut.Play();
            //开始计时            
            QueryPerformanceCounter(out startTime);

        }


        private void SendBtn_MouseUp(object sender, MouseEventArgs e)
        {
            isDraw = false;
            //停止播放声音
            waveOut.Stop();

            //结束计时

            QueryPerformanceCounter(out long endTime);
            QueryPerformanceFrequency(out long lpFrequency);

            var t = ((endTime - startTime) / (double)lpFrequency) * 1000;
            Debug.WriteLine(t);
            //有可能出现按下时间非常短的情况，短于10ms,定时器都还来不及触发,所以画面上还没有展示出来
            if (!isThrob) {
                return;            
            }
            //暂且认为，比Da短的就是Di
            //严格被勾选则比Di长的都为Da         

            if (t >= System.Convert.ToInt16(sendDaLength.Text) || (isStrict && t > System.Convert.ToInt16(sendDiLength.Text)))
            {
                codeQueue.Enqueue('-');
                Debug.WriteLine("-");
            }
            else
            {
                codeQueue.Enqueue('.');
                Debug.WriteLine(".");
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
            var di = 60000 / (speed * 50);
            sendDiLength.Text = di.ToString();
            sendDaLength.Text = (di * 3).ToString();
            keyInterval.Text = di.ToString();
            charInterval.Text = (di * 7).ToString();
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
            //改变发报声音频率
            SineWaveProvider sineWaveProvider = new (System.Convert.ToDouble(sendToneBox.Text));
            waveOut?.Stop();
            waveOut?.Dispose();
            waveOut = new();
            // 将 SineWaveProvider 连接到 WaveOutEvent
            waveOut.Init(sineWaveProvider);

        }

        private void SendPractice_SizeChanged(object sender, EventArgs e)
        {
            bitmap?.Dispose();
            if (visualizedBox.Width > 0 && visualizedBox.Height > 0) {
                bitmap = new Bitmap(visualizedBox.Width, visualizedBox.Height);
            }
      
        }
    }
}
