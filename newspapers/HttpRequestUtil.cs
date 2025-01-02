using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CW.newspapers
{
    internal class HttpRequestUtil
    {
        #region 公共方法
        /// <summary>
        /// Get数据接口
        /// </summary>
        /// <param name="getUrl">接口地址</param>
        /// <returns></returns>
        public static string GetWebRequest(string getUrl)
        {
            string responseContent = ""; 
            try
            {
                using var client = new HttpClient();
                using var response = client.Send(new HttpRequestMessage(HttpMethod.Get, getUrl));
                //在这里对接收到的页面内容进行处理
                responseContent = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ) {             
            }
            return responseContent;
        }
        /// <summary>
        /// Post数据接口
        /// </summary>
        /// <param name="postUrl">接口地址</param>
        /// <param name="paramData">提交json数据</param>
        /// <param name="dataEncode">编码方式(Encoding.UTF8)</param>
        /// <returns></returns>
        public static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string responseContent = string.Empty;
            try
            {
               using var client = new HttpClient();
                // 创建 HttpContent 对象
                var content = new StringContent(paramData, Encoding.UTF8, "application/x-www-form-urlencoded");
                using var response =   client.PostAsync(postUrl, content).Result;
                responseContent= response.Content.ReadAsStringAsync().Result;   
                  
            }
            catch (Exception )
            {
                
            }
            return responseContent;
        }

        #endregion
    }
}
