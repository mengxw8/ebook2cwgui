using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            List<string?> book;
            //
          var db=   SqliteUtil.CreateClient();
            //如果没有26个字母，那就是选择了部分字母
            if (words.Count < 26)
            {
                book = db.Queryable<Words>().Select(it => it.Word).OrderBy(string.Join(",", words.Select(k => k + " Desc").ToList())).Take(500).ToList().ToList();
            }
            else {
                book = db.Queryable<Words>().Select(it => it.Word).ToList();
            }


  
            Random random = new();
            while (groupNum > 0)
            {
                string? word = book[random.Next(1, book.Count)];
                if (word == null || word == "") {
                    continue;
                }
                answer += word;
                groupNum--;
                if (groupNum > 0)
                    {
                        answer += " ";
                    }

            }
            return answer;
        }

        /// <summary>
        /// 
        /// 统计字符串中各个字母的数量
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Words Statistics(string input) {
            Words words = new Words();
            // 将字符串转换为大写，以便统计不区分大小写
            input = input.ToUpper();
            // 使用反射获取对象的所有属性
            PropertyInfo[] properties = typeof(Words).GetProperties();
            for (int i = 'A'; i <= 'Z'; i++) {
                PropertyInfo? property = Array.Find(properties, p => p.Name == i.ToString());
                if (property != null)
                {
                    property.SetValue(properties, 0);
                }
            }

            // 遍历字符串中的每个字符
            foreach (char c in input)
            {
                // 只处理字母
                if (char.IsUpper(c))
                {
                    // 查找对应的属性
                    PropertyInfo? property = Array.Find(properties, p => p.Name == c.ToString());
                    if (property != null)
                    {
                        // 获取当前属性的值并增加计数
                       var v= property.GetValue(words);
                        int currentValue = Convert.ToInt32(v);
                        property.SetValue(words, currentValue + 1);
                    }
                }
            }
            return words;

        }
    }
}
