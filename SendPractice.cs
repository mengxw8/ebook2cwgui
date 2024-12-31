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

namespace CW
{
    public partial class SendPractice : Form
    {
        [DllImport("user32.dll")]
        static extern long LoadKeyboardLayout(string pwszKLID, uint Flags);
        public SendPractice()
        {
            InitializeComponent();
            //不允许息屏
            SystemSleep.PreventForCurrentThread();
            //输入法切换为英文
            LoadKeyboardLayout("00000409", 1);




        }

        //定义当前工作的模式，0分组数字，1分组字母，2分组字母数字，3英语文章
        WorkingMode mode = WorkingMode.None;
        //数字
        private static readonly Dictionary<string, string> number = new Dictionary<string, string> { { "1", ".----" }, { "2", "..---" }, { "3", "...--" }, { "4", "....-" }, { "5", "....." }, { "6", "-...." }, { "7", "--..." }, { "8", "---.." }, { "9", "----." }, { "0", "-----" } };
        //字母
        private static readonly Dictionary<string, string> alphabet = new Dictionary<string, string> { { "A", ".-" }, { "B", "-..." }, { "C", "-.-." }, { "D", "-.." }, { "E", "." }, { "F", "..-." }, { "G", "--." }, { "H", "...." }, { "I", ".." }, { "J", ".---" }, { "K", "-.-" }, { "L", ".-.." }, { "M", "--" }, { "N", "-." }, { "O", "---" }, { "P", ".--." }, { "Q", "--.-" }, { "R", ".-." }, { "S", "..." }, { "T", "-" }, { "U", "..-" }, { "V", "...-" }, { "W", ".--" }, { "X", "-..-" }, { "Y", "-.--" }, { "Z", "--.." } };
        //符号
        private static readonly Dictionary<string, string> symbol = new Dictionary<string, string> { { ".", ".-.-.-" }, { ":", "---..." }, { ",", "--..--" }, { ";", "-.-.-." }, { "?", "..--.." }, { "=", "-...-" }, { "'", ".----." }, { "/", "-..-." }, { "!", "-.-.--" }, { "-", "-....-" }, { "_", "..--.-" }, { "\"", "..-..-." }, { "(", "-.--." }, { ")", "-.--.-" }, { "$", "...-..-" }, { "@", ".--.-." } };
        private static readonly Dictionary<string, string> allCode = new Dictionary<string, string>[] { alphabet, number, symbol }.SelectMany(disc => disc).ToLookup(pair => pair.Value, pair => pair.Key)
            .ToDictionary(
                group => group.Key,
                group => group.Last() // 取最后一个值（覆盖冲突键）
            );
        //新闻类型
        Dictionary<string, string> newsType = new Dictionary<string, string> { { "中国", "https://www.cgtn.com/subscribe/rss/section/china.xml" }, { "世界", "https://www.cgtn.com/subscribe/rss/section/world.xml" }, { "商业", "https://www.cgtn.com/subscribe/rss/section/business.xml" }, { "体育", "https://www.cgtn.com/subscribe/rss/section/sports.xml" }, { "科学", "https://www.cgtn.com/subscribe/rss/section/tech-sci.xml" }, { "旅行", "https://www.cgtn.com/subscribe/rss/section/travel.xml" }, { "现场", "https://www.cgtn.com/subscribe/rss/section/live.xml" }, { "文化", "https://www.cgtn.com/subscribe/rss/section/culture.xml" } };

        private static string ArticlePath = @"./text/";
        //用来装答案的表格
        DataTable dataTable = null;
        //答案
        string answer = "";
        //上一次播放的音频文件路径
        string lastMusicPath = "";
        string lastCheckMusicPath = "";
        //用来装生成的图形
        private static ConcurrentQueue<Bitmap> bitmapQueue = new ConcurrentQueue<Bitmap>(); // 双缓冲队列
        //用来装敲过的字符
        private static ConcurrentQueue<char> charQueue = new ConcurrentQueue<char>();
        //当前帧
        private static Bitmap? bitmap;
        //是否严格解析
        private static bool isStrict = false;


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.Number;

            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;

            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (var k in number.Keys)
            {
                eqBox.Items.Add(k);
                neBox.Items.Add(k);
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.Alphabet;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;


            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (var k in alphabet.Keys)
            {
                eqBox.Items.Add(k);
                neBox.Items.Add(k);
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.AlphabetAndNumber;

            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;


            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (var k in number.Keys)
            {
                eqBox.Items.Add(k);
                neBox.Items.Add(k);
            }
            foreach (var k in alphabet.Keys)
            {
                eqBox.Items.Add(k);
                neBox.Items.Add(k);
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.Symbol;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;


            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (var k in symbol.Keys)
            {
                eqBox.Items.Add(k);
                neBox.Items.Add(k);
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.Article;
            //英文文章
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;


            //加载文章列表
            // 确保路径是目录并且存在
            if (!Directory.Exists(ArticlePath))
            {
                MessageBox.Show("没有可供的选择文章!");
                return;
            }

            List<string> files = new List<string>(Directory.GetFiles(ArticlePath, "*.txt", SearchOption.TopDirectoryOnly));
            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (string file in files)
            {
                string fileNmae = file.Replace(ArticlePath, "");
                eqBox.Items.Add(fileNmae);
                neBox.Items.Add(fileNmae);
            }

        }
        //新闻
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.News;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;

            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (string type in newsType.Keys)
            {
                eqBox.Items.Add(type);
                neBox.Items.Add(type);
            }

        }
        //随机单词
        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2_CheckedChanged(sender, e);
            mode = WorkingMode.Word;
        }











        private string generateAnswer(List<string> words)
        {

            //随机字符
            //同组无连续
            var isRepeat = repeatRbtn.Checked;
            //同组无重复
            var isContinuous = continuousRbtn.Checked;
            //组数限制
            var groupNum = groupNumBox.Value;

            Random random = new Random();

            for (int i = 0; i < groupNum; i++)
            {
                var key = "";
                for (int j = 0; j < EachGroup.Value; j++)
                {
                    //注意：Random.Next(minValue, maxValue)方法生成的随机数范围是从minValue（包括）到maxValue（不包括）之间的随机整数。
                    var s = words[random.Next(0, words.Count)];
                    if (isContinuous && key.Contains(s))
                    {
                        //重新生成
                        j--;
                        continue;
                    }

                    if (isRepeat && key.Length > 0 && key.Contains(s))
                    {
                        j--;
                        continue;
                    }
                    key += s;

                }
                answer += key;
                if (i + 1 < groupNum)
                {
                    answer += " ";
                }

            }
            return answer;

        }
        /// <summary>
        /// 生成单词串
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        private string generateWord(List<string> words)
        {
            //组数限制
            var groupNum = groupNumBox.Value;

            string answer = "";

            Dictionary<string, string> book = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(@".\word\Level8.json"));
            if (book == null)
            {
                return answer;
            }
            Random random = new Random();
            while (groupNum > 0)
            {
                string word = book[random.Next(1, 12198).ToString()];
                //允许指定开头字母
                if (words.Contains(word.Substring(0, 1).ToUpper()))
                {
                    answer += word;
                    groupNum--;
                    if (groupNum > 0)
                    {
                        answer += " ";
                    }
                }
            }
            return answer;
        }

        /// <summary>
        /// 随机一篇文章
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        private string getArticle(List<string> words)
        {
            string answer = "";
            //组数限制
            int groupNum = System.Convert.ToInt32(groupNumBox.Value);
            //是否不要符号
            var flag = symbolsChb.Checked;
            Random random = new Random();
            var index = random.Next(0, words.Count);

            if (!File.Exists(ArticlePath + words[index]))
            {
                return answer;
            }

            var article = File.ReadAllText(ArticlePath + words[index]);
            if (!flag)
            {
                foreach (string s in symbol.Keys)
                {
                    article = article.Replace(s, "");
                }
                article.Trim();
            }
            var list = article.Split(' ').Take(groupNum).ToList();




            return System.String.Join(" ", list);

        }
        /// <summary>
        /// 取网上下载一篇新闻
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        private string getNewsPapers(List<string> words)
        {
            //组数限制
            int groupNum = System.Convert.ToInt32(groupNumBox.Value);
            //是否不要符号
            var flag = symbolsChb.Checked;
            Random random = new Random();
            var type = random.Next(0, words.Count);
            var resp = newspapers.HttpRequestUtil.GetWebRequest(newsType[words[type]]);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(resp);
            var item = doc.SelectNodes("/rss/channel/item");
            var index = random.Next(0, item.Count);
            var newsPaper = item[index];
            var title = newsPaper.SelectSingleNode("title").InnerText;
            XmlNamespaceManager nsm1 = new XmlNamespaceManager(doc.NameTable);
            nsm1.AddNamespace("dc", @"http://purl.org/dc/elements/1.1/");
            var date = newsPaper.SelectSingleNode("//dc:date", nsm1).InnerText;
            XmlNamespaceManager nsm2 = new XmlNamespaceManager(doc.NameTable);
            nsm2.AddNamespace("content", @"http://purl.org/rss/1.0/modules/content/");
            var contentHtml = newsPaper.SelectSingleNode("//content:encoded", nsm2).InnerText;
            //处理超文本
            NSoup.Nodes.Document html = NSoup.NSoupClient.Parse(contentHtml);
            var content = html.Text();
            //处理时间
            content = DateTime.Parse(date).ToString("yyyy-MM-dd HH:mm:ss") + " " + content;
            if (!flag)
            {
                foreach (string s in symbol.Keys)
                {
                    content = content.Replace(s, "");
                }
                content.Trim();
            }


            var list = content.Split(' ').Take(groupNum).ToList();


            return System.String.Join(" ", list);
        }

        //生成报文并播放
        private void startBtn_Click(object sender, EventArgs e)
        {
            //生成测试数据
            List<string> words = getWords();
            if ((words.Count == 0 || words == null) && mode != WorkingMode.Customize)
            {
                return;
            }
            StringBuilder answerBuilder = new StringBuilder();
            answerBuilder.Append("===\r\n");
            if (mode == WorkingMode.Number || mode == WorkingMode.Alphabet || mode == WorkingMode.AlphabetAndNumber || mode == WorkingMode.Symbol)
            {
                answerBuilder.Append(generateAnswer(words));
            }
            else if (mode == WorkingMode.Article)
            {
                answerBuilder.Append(getArticle(words));
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
                    answerBuilder.Append(getNewsPapers(words));
                }
                catch
                {
                    MessageBox.Show("当前网络不通畅，请试试其他模式吧！");
                    return;
                }


            }
            else if (mode == WorkingMode.Word)
            {
                answerBuilder.Append(generateWord(words));


            }
            else if (mode == WorkingMode.Customize)
            {

            }

            answerBuilder.Append("\r\niii\r\n");
            if (mode != WorkingMode.Customize)
            {
                answer = answerBuilder.ToString();
                answer = answer.ToLower();
            }


            var fileName = DateTime.Now.ToUniversalTime().Ticks;
            var filePath = "./temp/" + fileName + ".txt";
            if (!Path.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            //写入临时文件
            File.WriteAllText(filePath, answer);
            //生成音频
            var audioFileName = GenerateAudio(fileName.ToString(), filePath, speetBox.Value.ToString());
            //重命名音频文件名称
            RenameMusic("./temp/" + audioFileName, filePath.Replace("txt", "mp3"));
            audioFileName = filePath.Replace("txt", "mp3");


            Mp3Player.Stop();




            //播放音频
            lastMusicPath = audioFileName;
            Mp3Player.Play(audioFileName);

            //解除封禁
            pauseBtn.Enabled = true;
            rePlayBtn.Enabled = true;




        }
        /// <summary>
        /// 生成音频文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="speed"></param>
        /// <returns></returns>

        private string GenerateAudio(string fileName, string filePath, string speed)
        {
            var param = "";


            //生成音频
            param += " -q 1 -o " + "./temp/" + fileName + " -w " + speed + " -f " + toneBox.Value + " " + filePath;
            ProcessStartInfo startInfo = new ProcessStartInfo("ebook2cw.exe", param);

            startInfo.UseShellExecute = false;    //是否使用操作系统的shell启动
            //startInfo.RedirectStandardInput = true;      //接受来自调用程序的输入     
            startInfo.RedirectStandardOutput = true;     //由调用程序获取输出信息
            startInfo.CreateNoWindow = true;             //不显示调用程序的窗口 
                                                         //创建进程对象   
            try
            {
                //调用EXE
                using var process = Process.Start(startInfo);

                using var reader = process.StandardOutput;
                // 获取exe的输出结果
                string result = reader.ReadToEnd();
                if (result != "")
                {
                    string[] lines = result.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    if (lines.Length >= 2)
                    {
                        int startIndex = lines.Length - 3;
                        result = string.Join(Environment.NewLine, lines.Skip(startIndex));
                    }

                    if (result.IndexOf("Error:") >= 0)
                    {
                        MessageBox.Show("配置错误，转换失败，请检查！");
                    }
                    else
                    {
                        var data = lines[lines.Length - 3].Split(":");
                        if (data.Length == 3)
                        {
                            //MessageBox.Show("转换完成，共计用时" + data[2] + "！");

                        }
                        return fileName + "0000.mp3";
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("程序错误，请重新下载！");
            }

            return "";

        }

        private void RenameMusic(string oldFileName, string newFileName)
        {
            try
            {
                // 确保目标文件名不存在，因为Move会替换目标文件
                if (File.Exists(newFileName))
                {
                    File.Delete(newFileName);
                }

                // 重命名文件
                File.Move(oldFileName, newFileName);

            }
            catch (Exception ex)
            {

            }
        }
        private List<string> getWords()
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
                    case WorkingMode.Number: words.AddRange(number.Keys); break;
                    case WorkingMode.Alphabet: words.AddRange(alphabet.Keys); break;
                    case WorkingMode.AlphabetAndNumber: words.AddRange(number.Keys); words.AddRange(alphabet.Keys); break;
                    case WorkingMode.Symbol: words.AddRange(symbol.Keys); break;
                    case WorkingMode.Article: words.AddRange(new List<string>(Directory.GetFiles(ArticlePath, "*.txt", SearchOption.TopDirectoryOnly)).Select(n => n.Replace(ArticlePath, "")).ToList()); break;
                    case WorkingMode.News: words.AddRange(newsType.Keys); break;
                    case WorkingMode.Customize: words.AddRange(alphabet.Keys); break;

                }
            }

            return words;
        }



        private void stopBtn_Click(object sender, EventArgs e)
        {
            Mp3Player.Stop();
            timer1.Stop();
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            if (lastMusicPath == "")
            {
                MessageBox.Show("您还尚未生成过报文哦，请生成后重试！");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "压缩文件(*.zip)|*.*";
            saveFileDialog.Title = "保存音频文件和报文到目录";
            saveFileDialog.FileName = "报文" + Path.GetFileName(lastMusicPath).Replace(".mp3", "") + "-" + speetBox.Value + "wpm.zip";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileDialog.FileName))
                {
                    File.Delete(saveFileDialog.FileName);
                }
                //打包文件
                using (FileStream zipToOpen = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    // 创建ZIP存档
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                    {
                        // 添加文件到ZIP存档
                        //添加音频
                        string musicFileName = Path.GetFileName(lastMusicPath);
                        archive.CreateEntryFromFile(lastMusicPath, musicFileName);
                        //添加报文
                        string txtFileName = Path.GetFileName(lastMusicPath.Replace(".mp3", ".txt"));
                        archive.CreateEntryFromFile(lastMusicPath.Replace(".mp3", ".txt"), txtFileName);



                    }
                }
            }

        }



        private void timer1_Tick(object sender, EventArgs e)
        {


        }

        private void CopyingPractice_FormClosed(object sender, FormClosedEventArgs e)
        {
            Mp3Player.Stop();
            //清除缓存
            if (lastMusicPath != null && Path.Exists(Path.GetDirectoryName(lastMusicPath)))
            {
                Directory.Delete(Path.GetDirectoryName(lastMusicPath), true);
            }
        }



        //清空答案
        private void clearAnswer_Click(object sender, EventArgs e)
        {


        }

        private void pauseBtn_Click(object sender, EventArgs e)
        {
            Mp3Player.Pause();
            continuePlayBtn.Enabled = true;
            pauseBtn.Enabled = false;
        }
        private void continuePlayBtn_Click(object sender, EventArgs e)
        {
            Mp3Player.ContinuePlay();
            continuePlayBtn.Enabled = false;
            pauseBtn.Enabled = true;

        }

        private void resumeBtn_Click(object sender, EventArgs e)
        {
            Mp3Player.Play(lastMusicPath);
        }

        private void neRbtn_CheckedChanged(object sender, EventArgs e)
        {
            neBox.Enabled = true;
            eqBox.Enabled = false;
        }

        private void eqRbtn_CheckedChanged(object sender, EventArgs e)
        {
            eqBox.Enabled = true;
            neBox.Enabled = false;
        }

        private void CopyingPractice_Load(object sender, EventArgs e)
        {
            // 获取当前程序集的版本
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Version version = currentAssembly.GetName().Version;
            this.Text = this.Text + " V" + version;
            //初始化画布
            bitmap = new Bitmap(visualizedBox.Width, visualizedBox.Height);
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

        private void individuationRbtn_CheckedChanged(object sender, EventArgs e)
        {
            if (individuationRbtn.Checked == true)
            {
                mode = WorkingMode.Customize;

                eqRbtn.Enabled = false;
                neRbtn.Enabled = false;
                //弹出文件选择框
                OpenFileDialog openImageDialog = new OpenFileDialog();

                openImageDialog.Filter = "报文(*.txt)|*.txt";
                openImageDialog.Multiselect = false;//关闭多选
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

        long start = 0;
        private void calculateInput(object sender, KeyEventArgs e)
        {
            long endTime = DateTime.Now.Ticks;
            var subTime = endTime - start;
            if (subTime < 600000)
            {
                richTextBox1.Text += ".";
            }
            else
            {
                richTextBox1.Text += "-";
            }


        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            start = DateTime.Now.Ticks;
        }

        // 导入 timeSetEvent, timeKillEvent 和 MMRESULT 枚举
        private const uint TIME_KILL_EVENT = 0;
        private const uint TIME_PERIODIC = 1;
        //定时器分辨率，1ms级别
        private const uint TIMER_RESOLUTION = 1;
        // 获取开始计数值
        [DllImport("kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
        // 获取性能计数器频率
        [DllImport("kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);
        [DllImport("winmm.dll", SetLastError = true)]
        private static extern uint timeSetEvent(
            uint uDelay,
            uint uResolution,
            TimerCallback lpTimeFunc,
            UIntPtr dwUser,
            uint fuEvent);


        //是否在绘制中
        private static bool isDraw = false;
        //空闲绘制的长度
        private static int blankWidth = 42;
        //是否空闲
        private static int wait = blankWidth + 1;
        //每次绘制的宽度
        private readonly static int drawWidth = 1;
        //可视化用来显示的字体
        private readonly static Font font = new Font("Arial‌", 8, FontStyle.Regular, GraphicsUnit.Point);


        // 回调函数的委托类型
        private delegate void TimerCallback(UIntPtr uTimerID, UIntPtr uMsg, UIntPtr dwUser, UIntPtr dw1, UIntPtr dw2);

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
                    wait = 0;
                }
                else if (wait <= blankWidth && !isDraw)
                {
                    wait++;
                    color = SystemColors.Control;
                }
                var str = "";
                if (wait > blankWidth && !isDraw)
                {
                    var sb = new StringBuilder();
                    while (charQueue.TryDequeue(out char c))
                    {
                        sb.Append(c);
                    }

                    while (sb.Length != 0)
                    {
                        if (allCode.TryGetValue(sb.ToString(), out str))
                        {
                            break;
                        }
                        sb.Length--;
                    }

                }

                Bitmap map = new Bitmap(bitmap.Width, bitmap.Height);
                using (Graphics g = Graphics.FromImage(map))
                {
                    // 将原图像绘制到新图像中，向左平移指定偏移量
                    g.DrawImage(bitmap, -drawWidth, 0);
                    //水平位置
                    var horizontalPosition = (bitmap.Height / 2) - 10;

                    using (Pen pen = new Pen(color, 5)) // 2是线条的宽度
                    {

                        // 绘制竖线
                        // 参数分别为：x1, y1, x2, y2，表示线条的起点和终点
                        g.DrawLine(pen, bitmap.Width - drawWidth, horizontalPosition, bitmap.Width, horizontalPosition);

                    }
                    //写字
                    if (str != "")
                    {
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        // 设置文本要绘制的位置
                        Point position = new Point(bitmap.Width - 10 - blankWidth, horizontalPosition + 15);

                        g.DrawString(str, font, Brushes.Black, position);


                    }

                }
                bitmap.Dispose();
                bitmap = map;
                bitmapQueue.Enqueue((Bitmap)bitmap.Clone());

            }
        }
        WaveOutEvent waveOut;
        long startTime;
        private void sendBtn_MouseDown(object sender, MouseEventArgs e)
        {
            //开始绘制
            isDraw = true;
            //播放声音

            // 创建 SineWaveProvider
            var sineWaveProvider = new SineWaveProvider(System.Convert.ToDouble(sendToneBox.Text));

            // 创建 WaveOutEvent 对象来播放音频
            waveOut = new WaveOutEvent();

            // 将 SineWaveProvider 连接到 WaveOutEvent
            waveOut.Init(sineWaveProvider);

            // 开始播放音频
            waveOut.Play();
            //开始计时            
            QueryPerformanceCounter(out startTime);

        }


        private void sendBtn_MouseUp(object sender, MouseEventArgs e)
        {
            isDraw = false;
            //停止播放声音
            waveOut.Stop();
            waveOut.Dispose();
            //结束计时

            QueryPerformanceCounter(out long endTime);
            QueryPerformanceFrequency(out long lpFrequency);

            var t = ((endTime - startTime) / (double)lpFrequency) * 1000;
            Debug.WriteLine(t);
            //暂且认为，比Da短的就是Di
            //严格被勾选则比Di长的都为Da

            if (t >= System.Convert.ToInt16(sendDaLength.Text)||(isStrict&& t > System.Convert.ToInt16(sendDiLength.Text)))
            {
                charQueue.Enqueue('-');
                Debug.WriteLine("-");
            }
            else
            {
                charQueue.Enqueue('.');
                Debug.WriteLine(".");
            }

        }
        /// <summary>
        /// 这个定时器的作用就是把队列中的图像刷新到页面上显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i < bitmapQueue.Count; i++)
            {
                if (bitmapQueue.TryDequeue(out Bitmap? sb))
                {
                    if (visualizedBox.Image != null)
                    {
                        visualizedBox.Image.Dispose();
                    }

                    visualizedBox.Image = sb;
                }
            }



        }

        private void snedSpeedTxb_Leave(object sender, EventArgs e)
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

        private void numberTxb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != 8)                       //判断输入的字符是否为十进制数字,是否为退格（输入错误可删除）
            {
                e.Handled = true;                               //将事件标记为已处理，否则无效字符会继续填充进去
            }
        }

        private void charInterval_TextChanged(object sender, EventArgs e)
        {

            try
            {
                blankWidth = (System.Convert.ToInt16(charInterval.Text) / 10);
            }
            catch (Exception)
            {
            }
        }

        private void strictCbx_CheckedChanged(object sender, EventArgs e)
        {
            isStrict = strictCbx.Checked;
        }
    }
}
