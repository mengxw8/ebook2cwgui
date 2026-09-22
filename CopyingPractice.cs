using AngleSharp;
using AngleSharp.Dom;
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

namespace CW
{
    public partial class CopyingPractice : Form
    {
        //[DllImport("user32.dll")]
        //static extern long LoadKeyboardLayout(string pwszKLID, uint Flags);
        [LibraryImport("user32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf8)]
        private static partial IntPtr LoadKeyboardLayoutA(string pwszKLID, uint Flags);

        public CopyingPractice()
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
            answerBox.ForeColor = Color.Black;
            answerBox.BackColor = Color.White;
            answerBox.Text = "";
            answerBox.WordWrap = true;
            answerBox.ScrollBars = RichTextBoxScrollBars.Both;
            EnableSnapLayout();
        }

        private Panel? settingsPanel;
        private TableLayoutPanel? rootLayout;
        private int settingsHeight;
        private int settingsContentWidth;
        private bool applyingLayout;

        /// <summary>
        /// 上部设置保持原来的位置和大小。窗口变窄时在设置区内横向滚动，抄收结果只占下面，不能盖住输入。
        /// </summary>
        private void EnableSnapLayout()
        {
            AutoSize = false;
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;
            MinimumSize = new Size(720, 420);

            int left = groupBox1.Left;
            int top = groupBox1.Top;
            var settingsBoxes = new System.Windows.Forms.GroupBox[] { groupBox1, groupBox2, groupBox3, groupBox5 };
            int right = settingsBoxes.Max(box => box.Right);
            settingsContentWidth = right - left + 12;
            int scrollRoom = SystemInformation.HorizontalScrollBarHeight + 28;

            settingsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Margin = new Padding(0),
            };
            foreach (var box in settingsBoxes)
            {
                var location = box.Location;
                Controls.Remove(box);
                box.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                box.Location = new Point(location.X - left + 4, location.Y - top + 4);
                settingsPanel.Controls.Add(box);
            }
            int contentBottom = settingsBoxes.Max(box => box.Bottom);
            settingsPanel.Controls.Add(new Panel
            {
                Location = new Point(0, contentBottom + 4),
                Size = new Size(10, scrollRoom),
            });
            settingsHeight = contentBottom + 4 + scrollRoom + 4;

            groupBox4.Anchor = AnchorStyles.None;
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Padding = new Padding(8, 8, 8, 8);
            answerBox.Dock = DockStyle.None;
            answerBox.Anchor = AnchorStyles.None;
            groupBox4.Resize += (_, _) => LayoutAnswerBox();

            rootLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(0),
                Padding = new Padding(8),
            };
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, settingsHeight));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            rootLayout.Controls.Add(settingsPanel, 0, 0);
            rootLayout.Controls.Add(groupBox4, 0, 1);
            Controls.Add(rootLayout);

            var area = Screen.FromPoint(Cursor.Position).WorkingArea;
            ClientSize = new Size(
                Math.Min(Math.Max(right - left + 200, 1360), area.Width - 24),
                Math.Min(settingsHeight + 480, area.Height - 24));

            Resize += (_, _) => KeepAnswerVisible();
            Shown += (_, _) => KeepAnswerVisible();
        }

        private void KeepAnswerVisible()
        {
            if (applyingLayout || settingsPanel == null || ClientSize.Height < 100)
                return;

            applyingLayout = true;
            try
            {
                int answerMin = Math.Max(180, answerBox.Font.Height + 96);
                int height = Math.Min(SettingsRowHeight(), Math.Max(160, ClientSize.Height - answerMin));
                if (rootLayout != null && Math.Abs(rootLayout.RowStyles[0].Height - height) > 0.5f)
                    rootLayout.RowStyles[0].Height = height;
                LayoutAnswerBox();
            }
            finally
            {
                applyingLayout = false;
            }
        }

        private int SettingsRowHeight()
        {
            int viewWidth = settingsPanel?.ClientSize.Width ?? 0;
            if (viewWidth <= 0)
                viewWidth = Math.Max(0, ClientSize.Width - (rootLayout?.Padding.Horizontal ?? 0));
            bool needsHorizontalScroll = viewWidth < settingsContentWidth;
            int scrollRoom = needsHorizontalScroll ? SystemInformation.HorizontalScrollBarHeight + 16 : 0;
            return settingsHeight + scrollRoom;
        }

        private void LayoutAnswerBox()
        {
            if (groupBox4.ClientSize.Width < 40 || groupBox4.ClientSize.Height < 40)
                return;

            var area = groupBox4.DisplayRectangle;
            int top = Math.Max(area.Top, 28);
            int left = Math.Max(area.Left, 8);
            int width = Math.Max(40, groupBox4.ClientSize.Width - left - 8);
            int height = Math.Max(answerBox.Font.Height + 12, groupBox4.ClientSize.Height - top - 8);
            var bounds = new Rectangle(left, top, width, height);
            if (answerBox.Bounds != bounds)
                answerBox.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }

        //定义当前工作的模式，0分组数字，1分组字母，2分组字母数字，3英语文章
        WorkingMode mode = WorkingMode.None;
        //答案
        string answer = "";
        string lastBookPath = "";
        // 创建 WaveOutEvent 对象来播放音频
        private readonly MorsePlayer player = new(600, MorseConfig.Create(20));
        // 创建 WaveOutEvent 对象来播放音频
        private readonly WaveOutEvent playerWave = new();

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.Number;
            KochList.Enabled = false;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;
            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (var k in Constant.number.Keys)
            {
                eqBox.Items.Add(k.ToString());
                neBox.Items.Add(k.ToString());
            }
        }
        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.Alphabet;
            KochList.Enabled = false;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;

            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (var k in Constant.alphabet.Keys)
            {
                eqBox.Items.Add(k.ToString());
                neBox.Items.Add(k.ToString());
            }
        }
        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.AlphabetAndNumber;
            KochList.Enabled = false;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;

            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (var k in Constant.number.Keys)
            {
                eqBox.Items.Add(k.ToString());
                neBox.Items.Add(k.ToString());
            }
            foreach (var k in Constant.alphabet.Keys)
            {
                eqBox.Items.Add(k.ToString());
                neBox.Items.Add(k.ToString());
            }
        }

        private void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.Symbol;
            KochList.Enabled = false;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;

            //填充值
            eqBox.Items.Clear();
            neBox.Items.Clear();
            foreach (var k in Constant.symbol.Keys)
            {
                eqBox.Items.Add(k.ToString());
                neBox.Items.Add(k.ToString());
            }
        }
        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.Article;
            //英文文章
            KochList.Enabled = false;
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
            KochList.Enabled = false;
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
        //Koch训练法
        private void RadioButton7_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.Koch;
            KochList.Enabled = true;
            KochList.SelectedIndex = 0;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = false;
            eqRbtn.Checked = true;
        }













        //生成报文并播放
        private void StartBtn_Click(object sender, EventArgs e)
        {
            startBtn.Enabled = false;
            //生成报文数据
            List<string> words = GetWords();
            if ((words.Count == 0 || words == null) && mode != WorkingMode.Customize)
            {
                startBtn.Enabled = true;
                return;
            }
            StringBuilder answerBuilder = new();
            //answerBuilder.Append(msgStartTxb.Text);
            if (mode == WorkingMode.Number || mode == WorkingMode.Alphabet || mode == WorkingMode.AlphabetAndNumber || mode == WorkingMode.Symbol || mode == WorkingMode.Koch)
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

            //answerBuilder.Append(msgEndTxb.Text);
            if (mode != WorkingMode.Customize)
            {
                answer = answerBuilder.ToString();
                answer = answer.ToLower();
            }

            //开始播放
            player?.UpdateFrequency(Convert.ToInt32(toneBox.Value));
            player?.UpdateConfig(MorseConfig.Create(Convert.ToInt32(speetBox.Value),waveList.Text));
            //停止播放并清空播放内容
            playerWave.Stop();
            player?.Clean();
            NoiseSettingsChanged(sender, e);
            player?.AddMorseCode(msgStartTxb.Text, Constant.allCharCode);
            player?.AddMorseCode(answer, Constant.allCharCode);
            player?.AddMorseCode(msgEndTxb.Text, Constant.allCharCode);
            //如果是开启了显示答案的按钮，直接显示答案
            if (showAnswerChb.Checked)
            {
                ShowAnswer();
            }
            playerWave.Play();



            var fileName = DateTime.Now.ToUniversalTime().Ticks;
            string filePath = Constant.TempPath + fileName + ".txt";
            if (!Path.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? "");
            }
            //写入临时文件
            File.WriteAllText(filePath, answer);
            lastBookPath = filePath;


            //解除封禁
            pauseBtn.Enabled = true;
            rePlayBtn.Enabled = true;
            startBtn.Enabled = true;
            //处理校报逻辑

            if (checkAnswerChb.Checked)
            {
                //开启定时器
                timer1.Start();
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

                }
            }

            return words;
        }

        private void SubmitAnswerBtn_Click(object sender, EventArgs e)
        {
            playerWave?.Stop();
            player?.Clean();


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
            playerWave?.Stop();
            player?.Clean();
            timer1.Stop();
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (answer == "" || lastBookPath == "")
            {
                MessageBox.Show("您还尚未生成过报文哦，请生成后重试！");
                return;
            }
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "压缩文件(*.zip)|*.*",
                Title = "保存音频文件和报文到目录",
                FileName = "抄收报文" + DateTime.Now.ToUniversalTime().Ticks + "-" + speetBox.Value + "wpm.zip"
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
                // 导出使用独立的干净点划模板，匹配当前音调和波形，不修改正在播放的配置。
                var exportConfig = MorseConfig.Create((int)speetBox.Value, waveList.Text);
                var exportPlayer = new MorsePlayer((int)toneBox.Value, exportConfig);
                MorseToMp3.ToMp3(answer, Constant.allCharCode, exportConfig, musicFileName,
                    exportPlayer.Dit_buff!, exportPlayer.Dah_buff!,
                    noiseCheckBox.Checked ? (int)noiseLevel.Value : null, (int)toneBox.Value);
                archive.CreateEntryFromFile(musicFileName, Path.GetFileName(lastBookPath).Replace(".txt", ".mp3"));
                //添加报文
                string txtFileName = Path.GetFileName(lastBookPath);
                archive.CreateEntryFromFile(lastBookPath, txtFileName);



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

        private void Timer1_Tick(object sender, EventArgs e)
        {

            if (playerWave.PlaybackState == PlaybackState.Stopped)
            {
                //结束了，需要进行校报
                player?.UpdateConfig(MorseConfig.Create(Convert.ToInt32(speetBox.Value),waveList.Text));
                player?.AddMorseCode(msgStartTxb.Text, Constant.allCharCode);
                player?.AddMorseCode(answer, Constant.allCharCode);
                player?.AddMorseCode(msgEndTxb.Text, Constant.allCharCode);
                timer1.Stop();
            }
        }

        private void CopyingPractice_FormClosed(object sender, FormClosedEventArgs e)
        {
            playerWave?.Stop();
            player?.Clean();

            //清除缓存
            if (lastBookPath != null && Path.Exists(Path.GetDirectoryName(lastBookPath)))
            {
                Directory.Delete(Path.GetDirectoryName(lastBookPath) ?? "", true);
            }
        }
        private void SpeetBox_ValueChanged(object sender, EventArgs e)
        {
            checkAnserSpeed.Value = speetBox.Value + 2;
            player?.UpdateConfig(MorseConfig.Create(Convert.ToInt16(speetBox.Value),waveList.Text));
            //
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
            playerWave.Pause();
            continuePlayBtn.Enabled = true;
            pauseBtn.Enabled = false;
        }
        private void ContinuePlayBtn_Click(object sender, EventArgs e)
        {
            Mp3Player.ContinuePlay();
            playerWave.Play();
            continuePlayBtn.Enabled = false;
            pauseBtn.Enabled = true;
            if (checkAnswerChb.Checked)
            {
                timer1.Start();
            }
        }

        private void ResumeBtn_Click(object sender, EventArgs e)
        {
            playerWave.Stop();
            player?.Clean();
            //重播前先静默500ms显得没那么急
            player?.Mute(500);
            player?.UpdateConfig(MorseConfig.Create(Convert.ToInt16(speetBox.Value),waveList.Text));
            player?.AddMorseCode(msgStartTxb.Text, Constant.allCharCode);
            player?.AddMorseCode(answer, Constant.allCharCode);
            player?.AddMorseCode(msgEndTxb.Text, Constant.allCharCode);
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
            playerWave.Init(player);
            waveList.SelectedIndex = 0;
            NoiseSettingsChanged(sender, e);
        }

        private void IndividuationRbtn_CheckedChanged(object sender, EventArgs e)
        {
            if (individuationRbtn.Checked == true)
            {
                mode = WorkingMode.Customize;
                KochList.Enabled = false;

                eqRbtn.Enabled = false;
                neRbtn.Enabled = false;
                //弹出文件选择框
                OpenFileDialog openImageDialog = new()
                {
                    Filter = "报文(*.txt)|*.txt",
                    Multiselect = false//关闭多选
                };


                if (openImageDialog.ShowDialog() == DialogResult.OK)
                {
                    answer = File.ReadAllText(openImageDialog.FileName);
                    //滤除其余字符
                    answer = StringTools.CleanCharacters(answer.ToLower(), symbolsChb.Checked);
                }
                else
                {
                    MessageBox.Show("未选择任何文件,试试其他模式吧!");
                    radioButton8.Checked = true;

                }
            }

        }

        private void KochList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = KochList.SelectedItem;
            if (item == null)
            {
                return;
            }
            eqBox.Items.Clear();
            foreach (var data in Constant.KochType[item.ToString() ?? "第1课"])
            {
                eqBox.Items.Add(data, true);

            }
            ;

        }

        private void msgEndTxb_TextChanged(object sender, EventArgs e)
        {

        }

        private void extraWordSpacing_ValueChanged(object sender, EventArgs e)
        {
            //额外词间隔改变的时候
            player?.UpdateExtraInterval(extraWordSpacing.Value);

        }

        // 各种抄收模式共用 player，统一在音频读取阶段加噪，重播时沿用当前设置。
        // 关闭开关只旁路噪声处理，保留信噪比数值，方便再次启用。
        private void NoiseSettingsChanged(object sender, EventArgs e)
        {
            noiseLevel.Enabled = noiseCheckBox.Checked;
            player.UpdateNoise(noiseCheckBox.Checked, (int)noiseLevel.Value);
        }

        private void toneBox_ValueChanged(object sender, EventArgs e)
        {
            player?.UpdateFrequency(Convert.ToInt32(toneBox.Value));
            // 音调变化后重新对准带通中心，避免电码声落在阻带。
            NoiseSettingsChanged(sender, e);
        }

        private void waveList_SelectedIndexChanged(object sender, EventArgs e)
        {
            player?.UpdateConfig(MorseConfig.Create(Convert.ToInt16(speetBox.Value),waveList.Text));
        }
    }
}
