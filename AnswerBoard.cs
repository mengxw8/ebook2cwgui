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

        string answer = "";
        string result = "";


        public AnswerBoard(string answer, string result)
        {
            InitializeComponent();
            this.answer = answer;
            this.result = result;
        }

        private void AnswerBoard_Load(object sender, EventArgs e)
        {


            //对答案掐头去尾
            answer = answer.Replace("===\r\n", "");
            answer = answer.Replace("\r\niii", "");
            //对回答的内容进行替换
            result = result.Replace("\n", " ");
            //分解答案
            var answerList = answer.Split(' ').Select(p => p.Trim()).ToList();
            var resultList= result.Split(' ').Select(p => p.Trim()).ToList();

            answerBox.Font = new Font("Microsoft YaHei UI", 25, FontStyle.Bold);


         

            //错误
            int error = 0;
            //漏掉
            int miss = 0;

            StringBuilder answerBuff = new StringBuilder();
            StringBuilder resultBuff = new StringBuilder();
            //统计结果
            for (var j= 0;j< answerList.Count;j++)
            {

                if (j!=0&&j  % 10 == 0) { 
                    answerBox.AppendText(answerBuff.ToString());
                    answerBox.AppendText(Environment.NewLine );
                    answerBox.AppendText(resultBuff.ToString());
                    answerBox.AppendText(Environment.NewLine);
                    answerBox.AppendText(Environment.NewLine);
                    answerBuff.Clear();
                    resultBuff.Clear();
                }

                answerBuff.Append(answerList[j]);
                answerBuff.Append(" ");

                var content = "";
                if (resultList.Count-1>=j) {
                    content=resultList[j];
                }

                if (content == answerList[j]) {
                    resultBuff.Append(answerList[j]);
                    resultBuff.Append(" ");
                    continue;
                }
                    var key = answerList[j];
                for (int i = 0; i < key.Length; i++)
                {
                    //如果内容还没有答案长，后面的都是按照掉了的处理
                    if (content.Length - 1 < i)
                    {
                        miss += 1;
                        resultBuff.Append("_");
                        continue;
                    }

                    //统计漏掉的
                    if (content[i] == '-' || content[i] == '_')
                    {
                        resultBuff.Append(content[i]);
                        miss += 1;
                        continue;
                    }

                    //统计错误的
                    if (content[i] != key[i])
                    {
                        error += 1;
                        resultBuff.Append(content[i]);
                        continue;

                    }
                    resultBuff.Append(content[i]);
                }
                resultBuff.Append(" ");
            }
            //错误的内容标红处理
           var str= answerBox.Text.Trim().Split("\n") ;
            int answerIndex = 0;
            int resultIndex = 1;
            while (answerIndex < str.Length&& resultIndex < str.Length) {
                var s1= str[answerIndex].Trim().Split(" ");
                var s2= str[resultIndex].Trim().Split(" ");
                // 统计前面几行字符数
                var baseIndex = 0;
                for (int i = 0; i <=answerIndex; i++) {
                    baseIndex += str[i].Length + 1;
                }
                for (int i = 0; i < s1.Length; i++) {
                    if (s1[i] != s2[i]) {
                        for (int k=0;k< s1[i].Length;k++) {
                            if (s1[i][k] != s2[i][k]) {
                                answerBox.SelectionStart = baseIndex+ k;
                                answerBox.SelectionLength = 1;
                                answerBox.SelectionColor = Color.Red;
                            }
                            
                        
                        }

                    }
                    baseIndex += s1[i].Length + 1;
                }



                answerIndex += 3;
                resultIndex += 3;

            }


            //展示漏掉的和错误的
            answerBox.AppendText(Environment.NewLine+"错：" + error + " 漏:" + miss);
            //answerBox.Select(0, 1);
            //answerBox.SelectionColor = Color.Red;
            answerBox.ReadOnly=true;

               }
    }
}
