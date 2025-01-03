using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    public class AnswerTools
    {
        /// <summary>        /// 
        /// 生成答案字符串
        /// </summary>
        /// <param name="words"></param>
        /// <param name="isRepeat">同组无连续</param>
        /// <param name="isContinuous">同组无重复</param>
        /// <param name="groupNum">组数限制</param>
        /// <param name="number">每组字符个数</param>
        /// <returns></returns>
        public static string GenerateAnswer(List<string> words,bool isRepeat,bool isContinuous,int groupNum,int number)
        {


            Random random = new();
            StringBuilder answerBuilder = new();

            for (int i = 0; i < groupNum; i++)
            {
                var key = "";
                for (int j = 0; j < number; j++)
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
                    answerBuilder.Append(' ');
                }

            }
            return answerBuilder.ToString();

        }

    }
}
