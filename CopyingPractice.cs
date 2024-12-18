﻿using NAudio.SoundFont;
using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public partial class CopyingPractice : Form
    {
        [DllImport("user32.dll")]
        static extern long LoadKeyboardLayout(string pwszKLID, uint Flags);
        public CopyingPractice()
        {
            InitializeComponent();
            //不允许息屏
            SystemSleep.PreventForCurrentThread();
            //输入法切换为英文
            LoadKeyboardLayout("00000409", 1);

            clearAnswer();


            dataGridView1.RowsDefaultCellStyle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            //表头居中
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //禁用排序
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            dataGridView1.RowHeadersVisible = true;
            dataGridView1.ColumnHeadersVisible = true;

            //dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView1_RowPostPaint);
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = e.RowIndex;

            var row = grid.Rows[rowIdx];
            var cell = row.Cells[0];

            var centerFormat = new StringFormat()
            {

                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString((rowIdx + 1).ToString(), dataGridView1.RowHeadersDefaultCellStyle.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        //定义当前工作的模式，0分组数字，1分组字母，2分组字母数字，3英语文章
        WorkingMode mode = WorkingMode.None;
        //数字
        static readonly Dictionary<string, string> number = new() { { "1", ".----" }, { "2", "..---" }, { "3", "...--" }, { "4", "....-" }, { "5", "....." }, { "6", "-...." }, { "7", "--..." }, { "8", "---.." }, { "9", "----." }, { "0", "-----" } };
        //字母
        static readonly Dictionary<string, string> alphabet = new(){ { "A", ".-" }, { "B", "-..." }, { "C", "-.-." }, { "D", "-.." }, { "E", "." }, { "F", "..-." }, { "G", "--." }, { "H", "...." }, { "I", ".." }, { "J", ".---" }, { "K", "-.-" }, { "L", ".-.." }, { "M", "--" }, { "N", "-." }, { "O", "---" }, { "P", ".--." }, { "Q", "--.-" }, { "R", ".-." }, { "S", "..." }, { "T", "-" }, { "U", "..-" }, { "V", "...-" }, { "W", ".--" }, { "X", "-..-" }, { "Y", "-.--" }, { "Z", "--.." } };
        //符号
        static readonly Dictionary<string, string> symbol = new(){ { ".", ".-.-.-" }, { ":", "---..." }, { ",", "--..--" }, { ";", "-.-.-." }, { "?", "..--.." }, { "=", "-...-" }, { "'", ".----." }, { "/", "-..-." }, { "!", "-.-.--" }, { "-", "-....-" }, { "_", "..--.-" }, { "\"", "..-..-." }, { "(", "-.--." }, { ")", "-.--.-" }, { "$", "...-..-" }, { "&", "...." }, { "@", ".--.-." } };
        //新闻类型
        static readonly Dictionary<string, string> newsType = new (){ { "中国", "https://www.cgtn.com/subscribe/rss/section/china.xml" }, { "世界", "https://www.cgtn.com/subscribe/rss/section/world.xml" }, { "商业", "https://www.cgtn.com/subscribe/rss/section/business.xml" }, { "体育", "https://www.cgtn.com/subscribe/rss/section/sports.xml" }, { "科学", "https://www.cgtn.com/subscribe/rss/section/tech-sci.xml" }, { "旅行", "https://www.cgtn.com/subscribe/rss/section/travel.xml" }, { "现场", "https://www.cgtn.com/subscribe/rss/section/live.xml" }, { "文化", "https://www.cgtn.com/subscribe/rss/section/culture.xml" } };
        //力大砖飞
        static readonly Dictionary<string, string[]> KochType = new() { 
            { "第1课", new string[] { "K", "M" } },
            { "第2课", new string[] { "K", "M", "R" } },
            { "第3课", new string[] { "K", "M", "R", "S" } },
            { "第4课", new string[] { "K", "M", "R", "S", "U" } }, 
            { "第5课", new string[] { "K", "M", "R", "S", "U", "A" } },
            { "第6课", new string[] { "K", "M", "R", "S", "U", "A", "P" } }, 
            { "第7课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T" } },
            { "第8课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L" } },
            { "第9课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O" } },
            { "第10课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W" } },
            { "第11课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" } },
            { "第12课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,"."} },
            { "第13课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N"} },
            { "第14课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J"} },
            { "第15课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E"} },
            { "第16课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F"} },
            { "第17课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O"} },
            { "第18课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y"} },
            { "第19课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",","} },
            { "第20课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V"} },
            { "第21课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G"} },
            { "第22课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5"} },
            { "第23课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/"} },
            { "第24课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q"} },
            { "第25课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9"} },
            { "第26课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9","Z"} },
            { "第27课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9","Z","H"} },
            { "第28课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9","Z","H","3"} },
            { "第29课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9","Z","H","3","8"} },
            { "第30课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9","Z","H","3","8","B"} },
            { "第31课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9","Z","H","3","8","B","?"} },
            { "第32课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9","Z","H","3","8","B","?","4"} },
            { "第33课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9","Z","H","3","8","B","?","4","2"} },
            { "第34课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9","Z","H","3","8","B","?","4","2","7"} },
            { "第35课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9","Z","H","3","8","B","?","4","2","7","C"} },
            { "第36课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9","Z","H","3","8","B","?","4","2","7","C","1"} },
            { "第37课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9","Z","H","3","8","B","?","4","2","7","C","1","D"} },
            { "第38课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9","Z","H","3","8","B","?","4","2","7","C","1","D","6"} },
            { "第39课", new string[] { "K", "M", "R", "S", "U", "A", "P", "T","L","O","W","I" ,".","N","J","E","F","O","Y",",","V","G","5","/","Q","9","Z","H","3","8","B","?","4","2","7","C","1","D","6","X"} },
        };
        private static readonly string ArticlePath = @"./text/";
        //用来装答案的表格
        DataTable dataTable = null;
        //答案
        string answer = "";
        //上一次播放的音频文件路径
        string lastMusicPath = "";
        string lastCheckMusicPath = "";


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.Number;
            KochList.Enabled = false;

            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;
            dataGridView1.Enabled = true;
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
            KochList.Enabled = false;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;
            dataGridView1.Enabled = true;

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
            KochList.Enabled = false;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;
            dataGridView1.Enabled = true;

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
            KochList.Enabled = false;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;
            dataGridView1.Enabled = true;

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
            KochList.Enabled = false;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;
            dataGridView1.Enabled = true;

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
            KochList.Enabled = false;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = true;
            dataGridView1.Enabled = true;
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
        //Koch训练法
        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            mode = WorkingMode.Koch;
            KochList.Enabled = true;
            KochList.SelectedIndex = 0;
            eqRbtn.Enabled = true;
            neRbtn.Enabled = false;
            eqRbtn.Checked = true;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 如果点击的不是行头（如果不需要可以不检查）
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // 将点击的单元格设置为编辑状态
                //dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                dataGridView1.BeginEdit(true);
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1_CellClick(sender, e);
        }

        private void groupNumBox_Leave(object sender, EventArgs e)
        {
            //更改答案提交范围
            var groupNum = groupNumBox.Value;
            for (var rowIndex = 0; rowIndex < dataGridView1.RowCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < dataGridView1.Rows[rowIndex].Cells.Count; columnIndex++)
                {

                    dataGridView1.Rows[rowIndex].Cells[columnIndex].ReadOnly = --groupNum < 0;

                }
            }

        }



        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //检测是被表示的控件还是DataGridViewTextBoxEditingControl
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridView dgv = (DataGridView)sender;

                //取得被表示的控件
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;

                //事件处理器删除
                tb.KeyPress -= new KeyPressEventHandler(dataGridViewTextBox_KeyPress);

                tb.KeyPress += new KeyPressEventHandler(dataGridViewTextBox_KeyPress);



            }

        }

        private void dataGridViewTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if ((int)e.KeyChar >= 48 & (int)e.KeyChar <= 57 | (int)e.KeyChar == 8 | (int)e.KeyChar == 46)
            //{
            //    e.Handled = false;
            //}
            //else
            //{
            //    e.Handled = true;
            //}


            //N个一组，输完直接下一组
            var dgv = sender as TextBox;
            if (dgv != null && !showAnswerChb.Checked && ((int)mode) > 3)
            {
                var data = dgv.Text;

                data += e.KeyChar;
                if (data.Length == EachGroup.Value)
                {
                    SendKeys.Send("{tab}");
                }

            }


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
            StringBuilder answerBuilder = new();

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
                answerBuilder.Append(key);

                if (i + 1 < groupNum)
                {
                    answerBuilder.Append( " ");
                }

            }
            return answerBuilder.ToString();

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
            string answer = "";
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
            if (mode == WorkingMode.Number || mode == WorkingMode.Alphabet || mode == WorkingMode.AlphabetAndNumber || mode == WorkingMode.Symbol||mode==WorkingMode.Koch)
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
            //处理校报逻辑
            if (checkAnswerChb.Checked)
            {
                //生成校验报文音频
                lastCheckMusicPath = filePath.Replace(".txt", "") + "-校报.mp3";
                //生成音频
                var checkAudioFileName = GenerateAudio(fileName.ToString(), filePath, checkAnserSpeed.Value.ToString());
                //重命名音频文件名称
                RenameMusic("./temp/" + checkAudioFileName, lastCheckMusicPath);
                //开启定时器
                timer1.Start();
            }
            //解除封禁
            pauseBtn.Enabled = true;
            rePlayBtn.Enabled = true;


            //如果是开启了显示答案的按钮，直接显示答案
            if (showAnswerChb.Checked)
            {
                showAnswer();
            }

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
            if (effectiveSpeed.Value > 0)
            {
                param += " -e ";
                param += effectiveSpeed.Value;
            }

            //生成音频
            param += " -q 1 -o " + "./temp/" + fileName + " -w " + speed + " -f " + toneBox.Value++ + " -W " + extraWordSpacing.Value + " " + filePath;
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
                    case WorkingMode.Word: words.AddRange(alphabet.Keys); break;

                }
            }

            return words;
        }

        private void submitAnswerBtn_Click(object sender, EventArgs e)
        {

            AnswerBoard answerBoard = new AnswerBoard(answer, dataGridView1);
            answerBoard.ShowDialog();

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
                        //添加校报音频
                        if (checkAnswerChb.Checked)
                        {
                            string checkFileName = Path.GetFileName(lastCheckMusicPath);
                            archive.CreateEntryFromFile(lastCheckMusicPath, checkFileName);
                        }


                    }
                }
            }

        }

        private void checkAnswerChb_CheckedChanged(object sender, EventArgs e)
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

        private void showAnswerChb_CheckedChanged(object sender, EventArgs e)
        {
            if (showAnswerChb.Checked && answer != "")
            {

                showAnswer();
            }
        }
        /// <summary>
        /// 展示答案
        /// </summary>
        private void showAnswer()
        {

            if (answer != "")
            {
                //把小写的q换成大写的Q，符合抄写习惯
                answer = answer.Replace("q", "Q");
                var s = answer.Replace("===\r\n", "").Replace("iii\r\n", "").Split(" ");
                
                for (var i = 0; i < s.Length; i++)
                {
                  var row=  dataTable.Rows[i % 10];
                    row[i / 10] = s[i];
                    //dataGridView1[i % 10, i / 10].Value = s[i];

                }
                dataGridView1.Refresh();

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (Mp3Player.Status() == PlaybackState.Stopped)
            {
                //结束了，需要进行校报
                Mp3Player.Play(lastCheckMusicPath);
                timer1.Stop();
            }
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
        private void speetBox_ValueChanged(object sender, EventArgs e)
        {
            checkAnserSpeed.Value = speetBox.Value + 2;
        }

        private void clearAnswer()
        {
            //数据区域初始化
            if (dataTable == null)
            {
                dataTable = new DataTable();
                //先来10列
                for (int i = 1; i <= 10; i++)
                {
                    dataTable.Columns.Add(i.ToString(), typeof(string));
                }
                dataGridView1.DataSource = dataTable;
            }
            dataTable.Clear();


            //再来十行
            for (int i = 1; i <= 10; i++)
            {
                var row = dataTable.NewRow();
                dataTable.Rows.Add(row);
            }

        }
        //清空答案
        private void clearAnswer_Click(object sender, EventArgs e)
        {
            clearAnswer();
            showAnswerChb.Checked = false;
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
            if (checkAnswerChb.Checked)
            {
                timer1.Start();
            }
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
        }

        private void individuationRbtn_CheckedChanged(object sender, EventArgs e)
        {
            if (individuationRbtn.Checked == true)
            {
                mode = WorkingMode.Customize;
                KochList.Enabled = false;
                dataGridView1.Enabled = false;
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

        private void KochList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = KochList.SelectedItem;
            if (item == null)
            {
                return;
            }
            var list= new List<string>();
            eqBox.Items.Clear();
            foreach (var data in KochType[item.ToString()])
            {
                eqBox.Items.Add(data,true);
                
            };

        }
    }
}
