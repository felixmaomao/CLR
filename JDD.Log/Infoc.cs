/******************************************************************
 * Description   : 统计类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-27 14:40
 * LastChanger   : 焦杰
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace JDD.Log
{
    public class Infoc
    {
        string infocUrl = ConfigurationManager.AppSettings["infoc"] == null ? "" : ConfigurationManager.AppSettings["infoc"].ToString();
        Dictionary<string, string> _dic;



        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="dic">字典对象</param>
        public static void Write(Dictionary<string, string> dic)
        {
            Infoc ic = new Infoc();
            ic._dic = dic;

            Thread tr = new Thread(new ThreadStart(ic.Start));
            tr.Start();

            //Task task = new Task(() => JDD.Log.Infoc.WriteTask(dic));
            //task.Start();
            //JDD.Log.Infoc.WriteTask(dic);
        }

        private void Start()
        {
            WriteTask(_dic);
        }

        /// <summary>
        /// 写入任务
        /// </summary>
        /// <param name="dic">字典</param>
        private void WriteTask(Dictionary<string, string> dic)
        {
            try
            {
                string urlInfoc = BulidInfocUrlParams(dic);
                int sendCount = 0; //发送次数
                bool sendResult = false; //发送结果
                while (sendResult == false)
                {
                    sendResult = JDDInfocSend(urlInfoc, ref sendCount);

                    if (sendResult == false) //如果发送失败，则隔0.5秒钟再发送一次
                    {
                        if (sendCount > 10) //连续发送10次均失败，则放弃发送
                        {
                            JDD.Log.LogHandle.Error(JDD.Log.LogType.Infoc, "发送失败：" + urlInfoc, "WriteErr");
                            break;
                        }
                        else
                            Thread.Sleep(500);
                    }
                }

            }
            catch (Exception ex)
            {
                JDD.Log.LogHandle.Error(JDD.Log.LogType.Infoc, ex.ToString(), "WriteErr");
            }

        }



        /// <summary>
        /// 新增infoc到系统中
        /// </summary>
        /// <param name="dic">需要发送的参数</param>
        /// <param name="sendCount">发送的次数</param>
        /// <returns></returns>
        private bool JDDInfocSend(string urlInfoc, ref int sendCount)
        {
            sendCount++;

            HttpWebRequest request = WebRequest.Create(urlInfoc) as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string rst = readStream.ReadToEnd();
            response.Close();
            readStream.Close();

            if (string.IsNullOrEmpty(rst.Trim()))
                return false;

            return rst.Contains("result");

            //if (rst.IndexOf("result") > 0)
            //    return true;
            //else
            //    return false;

            //if(rst.IndexOf("\"Code\": 1")>0)
            //    return true;
            //else
            //    return false;


            //JObject obj = JObject.Parse(rst);
            //string result = "0";
            //try
            //{
            //    result = obj["result"].ToString();
            //}
            //catch
            //{
            //    result = "0";
            //}

            //if (result.Equals("1"))
            //    return true;
            //else
            //    return false;


        }

        /// <summary>
        /// 奖多多运营统计
        /// </summary>
        /// <param name="dic">字典对象</param>
        /// <returns></returns>
        private string BulidInfocUrlParams(Dictionary<string, string> dic)
        {
            string url = ConfigurationManager.AppSettings["infoc"];
            List<string> keyValues = new List<string>();
            foreach (var item in dic)
            {
                keyValues.Add(string.Format("{0}={1}", item.Key, System.Web.HttpUtility.UrlEncode(item.Value)));
            }
            return string.Format("{0}?{1}", url, string.Join("&", keyValues));
        }

        ///// <summary>
        ///// 获得基础参数
        ///// </summary>
        ///// <returns></returns>
        //private StringBuilder BuildQueryBase()
        //{
        //    string uri = root + "product_no=37&public_index=1&business_index=101&browser=Chrome&os=Win7&uid=%2FpQN%2BPpWBcXIinsLsDs8uw%3D%3D&frm=index_ssq&click=buy&expand=&step=1";
        //    StringBuilder url = new StringBuilder();
        //    url.Append(root);
        //    url.AppendFormat("product_no={0}&public_index={1}&business_index={2}", productNo, publicIndex, businessIndex);
        //    url.AppendFormat("&browser={0}&os={1}&uid={2}", GetBrowser(), GetOS(), GetUid());

        //    return url;
        //}

        /// <summary>
        /// 获得浏览器及版本号
        /// </summary>
        /// <returns></returns>
        private static string GetBrowser()
        {
            HttpBrowserCapabilities b = HttpContext.Current.Request.Browser;
            return b.Type;
        }

        /// <summary>
        /// 获得用户ID
        /// </summary>
        /// <param name="cookieKey">缓存key</param>
        /// <returns></returns>
        private static string GetUid()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["YYG.JDD.User"];

            if ((cookie == null) || (String.IsNullOrEmpty(cookie.Value)))
            {
                return string.Empty;
            }

            return cookie.Value;
        }




    }
}
