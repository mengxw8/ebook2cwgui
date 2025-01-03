using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    public class WordsTools
    {
        /// <summary>
        /// 生成单词串
        /// </summary>
        /// <param name="words"></param>
        /// <param name="groupNum">组数</param>
        /// <returns></returns>
        public static string GenerateWord(List<string> words,int groupNum)
        {
            string answer = "";
            Dictionary<string, string> book = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(@".\word\Level8.json")) ?? [];
            if (book == null)
            {
                return answer;
            }
            Random random = new();
            while (groupNum > 0)
            {
                string word = book[random.Next(1, 12198).ToString()];
                //允许指定开头字母
                if (words.Contains(word[..1].ToUpper()))
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

    }
}
