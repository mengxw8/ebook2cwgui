using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW
{
    public partial class AnswerBoard : Form
    {

        string? answer = "";
        DataGridView dgv = null;


        public AnswerBoard(string answer, DataGridView dgv)
        {
            InitializeComponent();
            this.answer = answer;
            this.dgv = dgv;
        }

        private void AnswerBoard_Load(object sender, EventArgs e)
        {


            //对答案掐头去尾
            answer = answer.Replace("=== ", "");
            answer = answer.Replace(" iii ", "");
            //分解答案
            var answerList = answer.Split(' ').Select(p => p.Trim()).ToList();
            int answerFlag = 0;
            answerBox.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            //插入20个空行
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 49; j++)
                {
                    answerBox.AppendText(" ");
                }
                answerBox.AppendText(Environment.NewLine);
            }
         

            //错误
            int error = 0;
            //漏掉
            int miss = 0;
            

            foreach (DataGridViewRow row in dgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ReadOnly)
                    {
                        break;
                    }
                    var content =System.Convert.ToString(cell.Value) ;
                    //空的就直接算作错误
                    if (content == "" || content == null)
                    {
                        error += 4;
                        //插入正确答案
                        answerBox.SelectionStart = 3 * (answerFlag / 10) * 50 + (answerFlag % 10)*5 ;
                        answerBox.SelectionLength = 4;
                        answerBox.SelectedText = answerList[answerFlag];
                        //插入回答
                        answerBox.SelectionStart = (3 * (answerFlag / 10) + 1) * 50 + (answerFlag % 10) * 5;
                        answerBox.SelectionLength = 4;
                        answerBox.SelectedText = "____";
                        answerBox.SelectionStart = (3 * (answerFlag / 10) + 1) * 50 + (answerFlag % 10) * 5;
                        answerBox.SelectionLength = 4;
                        answerBox.SelectionColor = Color.Red;
                        answerFlag++;
                        continue;
                    }
                    //划线的就直接算作漏掉
                    if (content == "----" || content == "____")
                    {
                        miss += 4;
                        //插入正确答案
                        answerBox.SelectionStart = 3 * (answerFlag / 10) * 50 + (answerFlag % 10) * 5;
                        answerBox.SelectionLength = 4;
                        answerBox.SelectedText = answerList[answerFlag];
                        //插入回答
                        answerBox.SelectionStart = (3 * (answerFlag / 10) + 1) * 50 + (answerFlag % 10) * 5;
                        answerBox.SelectionLength = 4;
                        answerBox.SelectedText = "____";
                        answerBox.SelectionStart = (3 * (answerFlag / 10) + 1) * 50 + (answerFlag % 10) * 5;
                        answerBox.SelectionLength = 4;
                        answerBox.SelectionColor = Color.Red;
                        answerFlag++;
                        continue;
                    }

                    var key = answerList[answerFlag];
                    //插入正确答案
                    answerBox.SelectionStart = 3 * (answerFlag / 10) * 50 + (answerFlag % 10) * 5;
                    answerBox.SelectionLength = 4;
                    answerBox.SelectedText = answerList[answerFlag];
                    for (int i = 0; i < key.Length; i++)
                    {


                        //如果内容还没有答案长，后面的都是按照掉了的处理
                        if (content.Length - 1 < i)
                        {
                            miss += 1;
                            answerBox.SelectionStart = (3 * (answerFlag / 10) + 1) * 50 + (answerFlag % 10) * 5+i;
                            answerBox.SelectionLength = 1;
                            answerBox.SelectedText = "_";
                            answerBox.SelectionStart = (3 * (answerFlag / 10) + 1) * 50 + (answerFlag % 10) * 5+i;
                            answerBox.SelectionLength = 1;
                            answerBox.SelectionColor = Color.Red;
                            continue;
                        }

                        //统计漏掉的
                        if (content[i] == '-' || content[i] == '_')
                        {
                            answerBox.SelectionStart = (3 * (answerFlag / 10) + 1) * 50 + (answerFlag % 10) * 5 + i;
                            answerBox.SelectionLength = 1;
                            answerBox.SelectedText = "_";
                            answerBox.SelectionStart = (3 * (answerFlag / 10) + 1) * 50 + (answerFlag % 10) * 5 + i;
                            answerBox.SelectionLength = 1;
                            answerBox.SelectionColor = Color.Red;
                            miss += 1;
                            continue;
                        }
                        answerBox.SelectionStart = (3 * (answerFlag / 10) + 1) * 50 + (answerFlag % 10) * 5 + i;
                        answerBox.SelectionLength = 1;
                        answerBox.SelectedText = System.Convert.ToString(content[i]);

                        //统计错误的
                        if (content[i] != key[i])
                        {

                            answerBox.SelectionStart = (3 * (answerFlag / 10) + 1) * 50 + (answerFlag % 10) * 5 + i;
                            answerBox.SelectionLength = 1;
                            answerBox.SelectionColor = Color.Red;
                            error += 1;
                            continue;
                            
                        }
                    }
                    //下一个答案
                    answerFlag++;

                }
            }
            //展示漏掉的和错误的
            answerBox.AppendText(Environment.NewLine+"错：" + error + " 漏:" + miss);
            //answerBox.Select(0, 1);
            //answerBox.SelectionColor = Color.Red;




        }
    }
}
