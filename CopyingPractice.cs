using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW
{
    public partial class CopyingPractice : Form
    {
        public CopyingPractice()
        {
            InitializeComponent();
            //数据区域初始化
            DataTable dataTable = new DataTable();
            //先来10列
            for (int i = 1; i <= 10; i++)
            {
                dataTable.Columns.Add(i.ToString(), typeof(string));
            }
            //再来十行
            for (int i = 1; i <= 10; i++)
            {
                dataTable.Rows.Add(dataTable.NewRow());

            }

            dataGridView1.DataSource = dataTable;


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

            dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView1_RowPostPaint);
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
        int mode = 0;
        //数字
        Dictionary<string, string> number = new Dictionary<string, string> { { "1", ".----" }, { "2", "..---" }, { "3", "...--" }, { "4", "....-" }, { "5", "....." }, { "6", "-...." }, { "7", "--..." }, { "8", "---.." }, { "9", "----." }, { "0", "-----" } };
        //字母
        Dictionary<string, string> alphabet = new Dictionary<string, string> { { "A", ".-" }, { "B", "-..." }, { "C", "-.-." }, { "D", "-.." }, { "E", "." }, { "F", "..-." }, { "G", "--." }, { "H", "...." }, { "I", ".." }, { "J", ".---" }, { "K", "-.-" }, { "L", ".-.." }, { "M", "--" }, { "N", "-." }, { "O", "---" }, { "P", ".--." }, { "Q", "--.-" }, { "R", ".-." }, { "S", "..." }, { "T", "-" }, { "U", "..-" }, { "V", "...-" }, { "W", ".--" }, { "X", "-..-" }, { "Y", "-.--" }, { "Z", "--.." } };
        //符号
        Dictionary<string, string> symbol = new Dictionary<string, string> { { ".", ".-.-.-" }, { ":", "---..." }, { ",", "--..--" }, { ";", "-.-.-." }, { "?", "..--.." }, { "=", "-...-" }, { "'", ".----." }, { "/", "-..-." }, { "!", "-.-.--" }, { "-", "-....-" }, { "_", "..--.-" }, { "\"", "..-..-." }, { "(", "-.--." }, { ")", "-.--.-" }, { "$", "...-..-" }, { "&", "...." }, { "@", ".--.-." } };
        //答案
        string answer = "";
        //上一次播放的音频文件路径
        string lastMusicPath = "";


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mode = 0;
            eqBox.Visible = true;
            eqRbtn.Visible = true;
            neRbtn.Visible = true;
            neBox.Visible = true;
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
            mode = 1;
            eqBox.Visible = true;
            eqRbtn.Visible = true;
            neRbtn.Visible = true;
            neBox.Visible = true;
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
            mode = 2;

            eqBox.Visible = true;
            eqRbtn.Visible = true;
            neRbtn.Visible = true;
            neBox.Visible = true;
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



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 如果点击的不是行头（如果不需要可以不检查）
            123if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
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


            //四个一组，输完直接下一组
            var dgv = sender as TextBox;
            if (dgv != null&&!showAnswerChb.Checked)
            {
                var data = dgv.Text;

                data += e.KeyChar;
                if (data.Length == EachGroup.Value)
                {
                    SendKeys.Send("{tab}");
                }

            }


        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            //生成测试数据
            List<string> words = getWords();
            if (words.Count == 0 || words == null)
            {
                return;
            }
            //随机字符
            //同组无连续
            var isRepeat = repeatRbtn.Checked;
            //同组无重复
            var isContinuous = continuousRbtn.Checked;
            //组数限制
            var groupNum = groupNumBox.Value;
            answer = "===\r\n";
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
            answer += "\r\niii\r\n";
            answer = answer.ToLower();


            var fileName = DateTime.Now.ToUniversalTime().Ticks;
            var filePath = "./temp/" + fileName + ".txt";
            if (!Path.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            //写入临时文件
            File.WriteAllText(filePath, answer);
            //生成音频
            var audioFileName = GenerateAudio(fileName.ToString(), filePath);
            //重命名音频文件名称
            RenameMusic("./temp/" + audioFileName, filePath.Replace("txt", "mp3"));
            audioFileName = filePath.Replace("txt", "mp3");


            Musicplay.PauseMusic(lastMusicPath);
            //播放音频
            Musicplay.PlayMusic(audioFileName);
            lastMusicPath = audioFileName;

        }

        private string GenerateAudio(string fileName, string filePath)
        {
            //生成音频
            var param = "-q 1 -o " + "./temp/" + fileName + " -w " + speetBox.Value + " -f " + toneBox.Value + " " + filePath;
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
                    case 0: words.AddRange(number.Keys); break;
                    case 1: words.AddRange(alphabet.Keys); break;
                    case 2: words.AddRange(number.Keys); words.AddRange(alphabet.Keys); break;
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
            Musicplay.StopMusic();
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
            if (showAnswerChb.Checked && answer != "") {

                showAnswer();
            }
        }
        /// <summary>
        /// 展示答案
        /// </summary>
        private void showAnswer() {
            
            if (answer != "") {
                var s = answer.Replace("===\r\n", "").Replace("iii\r\n", "").Split(" ");
                for (var i = 0; i < s.Length; i++) {
                  dataGridView1[i /10, i % 10].Value = s[i];

                }                       
                        
            }
            dataGridView1[0, 0].Value = "dfadf";

        }
    }
}
