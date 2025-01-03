using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    public class ArticleTools
    {

        /// <summary>
        /// 随机一篇文章
        /// </summary>
        /// <param name="words">首字母合集</param>
        /// <param name="needSymbols">是否需要保留符号</param>
        /// <param name="groupNum">总共生成多少组</param>
        /// <returns></returns>
        public static string GetArticle(List<string> words, bool needSymbols, int groupNum)
        {
            string answer = "";


            Random random = new();
            var index = random.Next(0, words.Count);

            if (!File.Exists(Constant.ArticlePath + words[index]))
            {
                return answer;
            }

            var article = File.ReadAllText(Constant.ArticlePath + words[index]);
            StringBuilder stringBuilder = new ();
            HashSet<char> charSet;
            if (needSymbols)
            {
                charSet = [.. Constant.allCode.Values.Select(item => char.ToLower(item))];
            }
            else
            {
                charSet = [.. Constant.numberAndAlphabet.Keys.Select(item=>char.ToLower(item))];
            }
            charSet.Add(' ');
            //除去多余字符
            foreach (var item in article.Trim())
            {
                if (charSet.Contains(item))
                {
                    stringBuilder.Append(item);
                }
            }

            var list = stringBuilder.ToString().Split(' ').Take(groupNum).ToList();

            return System.String.Join(" ", list);

        }

    }
}
