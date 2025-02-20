using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    public class StringTools
    {
        /// <summary>
        /// 清洗字符串，只保留数字，字母和符号
        /// </summary>
        /// <param name="input"></param>
        /// <param name="needSymbols"></param>
        /// <returns></returns>
        public static string CleanCharacters(string input,bool needSymbols) {
            if (input == null || input == "")
            {
                return "";
            }
            StringBuilder stringBuilder = new();
            HashSet<char> charSet;
            if (needSymbols)
            {
                charSet = [.. Constant.allCode.Values.Select(item => char.ToLower(item))];
            }
            else
            {
                charSet = [.. Constant.numberAndAlphabet.Keys.Select(item => char.ToLower(item))];
            }            
            charSet.Add(' ');

            foreach (var c in input.Trim())
            {
                if (charSet.Contains(c))
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString();

        }
    }
}
