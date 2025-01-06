using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    internal class Constant
    {
        //数字
        public static readonly Dictionary<char, string> number = new() { { '1', ".----" }, { '2', "..---" }, { '3', "...--" }, { '4', "....-" }, { '5', "....." }, { '6', "-...." }, { '7', "--..." }, { '8', "---.." }, { '9', "----." }, { '0', "-----" } };
        //数字短码(短5改)
        public static readonly Dictionary<char, string> shortNumber5 = new() { { '1', ".-" }, { '2', "..-" }, { '3', "...--" }, { '4', "....-" }, { '5', "....." }, { '6', "-...." }, { '7', "--..." }, { '8', "-.." }, { '9', "-." }, { '0', "-" } };
        //数字短码(短10改)
        public static readonly Dictionary<char, string> shortNumber10 = new() { { '1', ".-" }, { '2', "..-" }, { '3', ".--" }, { '4', "...-" }, { '5', "..." }, { '6', "-..." }, { '7', "--." }, { '8', "-.." }, { '9', "-." }, { '0', "-" } };

        //字母
        public static readonly Dictionary<char, string> alphabet = new() { { 'A', ".-" }, { 'B', "-..." }, { 'C', "-.-." }, { 'D', "-.." }, { 'E', "." }, { 'F', "..-." }, { 'G', "--." }, { 'H', "...." }, { 'I', ".." }, { 'J', ".---" }, { 'K', "-.-" }, { 'L', ".-.." }, { 'M', "--" }, { 'N', "-." }, { 'O', "---" }, { 'P', ".--." }, { 'Q', "--.-" }, { 'R', ".-." }, { 'S', "..." }, { 'T', "-" }, { 'U', "..-" }, { 'V', "...-" }, { 'W', ".--" }, { 'X', "-..-" }, { 'Y', "-.--" }, { 'Z', "--.." } };
        //符号
        public static readonly Dictionary<char, string> symbol = new() { { '.', ".-.-.-" }, { ':', "---..." }, { ',', "--..--" }, { ';', "-.-.-." }, { '?', "..--.." }, { '=', "-...-" }, { '\'', ".----." }, { '/', "-..-." }, { '!', "-.-.--" }, { '-', "-....-" }, { '_', "..--.-" }, { '\\', "..-..-." }, { '(', "-.--." }, { ')', "-.--.-" }, { '$', "...-..-" }, { '@', ".--.-." } };
        //数字和字母
        public static readonly Dictionary<char, string> numberAndAlphabet = new Dictionary<char, string>[] { alphabet, number }.SelectMany(disc => disc).ToDictionary(
                group => group.Key,
                group => group.Value // 取最后一个值（覆盖冲突键）
            );
        public static readonly Dictionary<string, char> allCode = new Dictionary<char, string>[] { alphabet, number, symbol }.SelectMany(disc => disc).ToLookup(pair => pair.Value, pair => pair.Key)
            .ToDictionary(
                group => group.Key,
                group => group.Last() // 取最后一个值（覆盖冲突键）
            );
        //新闻类型
        public static readonly Dictionary<string, string> newsType = new() { { "中国", @"https://www.cgtn.com/subscribe/rss/section/china.xml" }, { "世界", @"https://www.cgtn.com/subscribe/rss/section/world.xml" }, { "商业", "https://www.cgtn.com/subscribe/rss/section/business.xml" }, { "体育", @"https://www.cgtn.com/subscribe/rss/section/sports.xml" }, { "科学", @"https://www.cgtn.com/subscribe/rss/section/tech-sci.xml" }, { "旅行", @"https://www.cgtn.com/subscribe/rss/section/travel.xml" }, { "现场", @"https://www.cgtn.com/subscribe/rss/section/live.xml" }, { "文化", @"https://www.cgtn.com/subscribe/rss/section/culture.xml" } };
        //力大砖飞,所有的Koch教程
        public static readonly Dictionary<string, string[]> KochType = new() {
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
        //内置的文章存放路径
        public readonly static string ArticlePath = @"./text/";
        public readonly static string TempPath = @"./temp/";
        public readonly static string DbPath = @"./db/";
        //开始提示符
        public readonly static string StartString = "===\r\n";
        //结束提示符
        public readonly static string EndString = "\r\niii\r\n";
        //英文键盘布局
        public readonly static string EnglishKeyboardLayout = @"00000409";
    }
}
