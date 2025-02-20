using AngleSharp.Dom;
using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CW
{
    public class NewsPapersTools
    {
        /// <summary>
        /// 取网上下载一篇新闻
        /// </summary>
        /// <param name="words">首字母合集</param>
        /// <param name="groupNum">有多少组</param>
        /// <param name="needSymbols"> 需要符号</param>
        /// <returns></returns>
        public static string GetNewsPapers(List<string> words, int groupNum, bool needSymbols)
        {

            Random random = new();
            var type = random.Next(0, words.Count);
            var resp = newspapers.HttpRequestUtil.GetWebRequest(Constant.newsType[words[type]]);
            XmlDocument doc = new();
            doc.LoadXml(resp);
            var content = "";
            var item = doc.SelectNodes("/rss/channel/item");
            if (item is not null)
            {
                var index = random.Next(0, item.Count);
                var newsPaper = item[index];
                var titleXml = newsPaper!.SelectSingleNode("title");
                if (titleXml is not null)
                {
                    var title = titleXml.InnerText;
                }
                XmlNamespaceManager nsm1 = new(doc.NameTable);
                nsm1.AddNamespace("dc", @"http://purl.org/dc/elements/1.1/");
                var date = newsPaper.SelectSingleNode("//dc:date", nsm1)!.InnerText;
                XmlNamespaceManager nsm2 = new(doc.NameTable);
                nsm2.AddNamespace("content", @"http://purl.org/rss/1.0/modules/content/");
                var contentHtml = newsPaper.SelectSingleNode("//content:encoded", nsm2)!.InnerText;
                //处理超文本
                IDocument document = BrowsingContext.New(Configuration.Default).OpenAsync(req => req.Content(contentHtml)).Result;
                if (document.Body is not null)
                {
                    content = document.Body.TextContent;
                }

                //处理时间
                content = DateTime.Parse(date).ToString("yyyy-MM-dd HH:mm:ss") + " " + content;

            }
                 
            var list = StringTools.CleanCharacters(content, needSymbols).Split(' ').Take(groupNum).ToList();
            return System.String.Join(" ", list);
        }
    }
}
