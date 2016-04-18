/******************************************************************
 * Description   : Log帮助类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-27 14:40
 * LastChanger   : 焦杰
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace JDD.Log
{
    /// <summary>
    /// Log帮助类
    /// </summary>
    public class LogHelper
    {
        private const string paramsSymbol1 = ";";
        private const string paramsSymbol1_replace = ";";
        private const string paramsSymbol2 = ":";
        private const string paramsSymbol2_replace = ":";

        /// <summary>
        /// 判断IP地址是否正确
        /// </summary>
        /// <param name="str">IP地址</param>
        /// <returns></returns>
        public static bool IsIPAddress(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length < 7 || str.Length > 15)
                return false;

            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}{1}";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);

            return regex.IsMatch(str);
        }


        /// <summary>
        /// 获得客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string str = string.Empty;
            if (HttpContext.Current == null)
                return GetIPAddressLocal();

            HttpRequest request = HttpContext.Current.Request;

            //if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
            //{
            //    str = request.ServerVariables["REMOTE_ADDR"];
            //}
            //else
            //{
            //    str = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //}

            //if (request.ServerVariables["HTTP_VIA"] != null)
            //{
            //    str = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            //}
            //else
            //{
            //    str = request.ServerVariables["REMOTE_ADDR"].ToString();
            //}

            //if ((str != null) && !(str == ""))
            //{
            //    return str;
            //}
            //return request.UserHostAddress;

            //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
            if (request.ServerVariables.AllKeys.Contains("HTTP_X_FORWARDED_FOR") && 
                request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                str = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            //否则直接读取REMOTE_ADDR获取客户端IP地址
            if (string.IsNullOrEmpty(str) && 
                request.ServerVariables.AllKeys.Contains("REMOTE_ADDR"))
            {
                str = request.ServerVariables["REMOTE_ADDR"];
            }
            //前两者均失败，则利用Request.UserHostAddress属性获取IP地址，但此时无法确定该IP是客户端IP还是代理IP
            if (string.IsNullOrEmpty(str))
            {
                str = request.UserHostAddress;
            }

            //最后判断获取是否成功
            if (string.IsNullOrEmpty(str))
            {
                return "0.0.0.0";
            }

            if (str == "::1")
            {
                return "127.0.0.1";
            }

            return str;
        }

        /// <summary>
        /// 获得域名
        /// </summary>
        /// <returns></returns>
        public static string GetDomain()
        {
            if (HttpContext.Current == null)
                return "";

            string host = HttpContext.Current.Request.Url.Host;
            int port = HttpContext.Current.Request.Url.Port;

            if (port == 80)
                return host;
            else
                return host + ":" + port.ToString();
        }

        /// <summary>
        /// 获得页面的完整URL
        /// </summary>
        /// <returns></returns>
        public static string GetUrl()
        {
            if (HttpContext.Current == null)
                return "";
            return HttpContext.Current.Request.Url.ToString();
        }

        /// <summary>
        /// 通过DNS获得本机IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddressLocal()
        {
            string hostname;
            System.Net.IPHostEntry localhost;
            hostname = System.Net.Dns.GetHostName();
            localhost = System.Net.Dns.GetHostEntry(hostname);
            string ip = localhost.AddressList[0].ToString();
            int i = 1;
            while (ip.Contains(":"))
            {
                if (i == localhost.AddressList.Length)
                {
                    ip = "";
                    break;
                }
                ip = localhost.AddressList[i].ToString();
                if (ip.Contains(":"))
                {
                    i++;
                }
                else
                    break;
            }
            return ip;
        }

        /// <summary>
        /// 获得参数列表里的value
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string ParamGet(string parameters, string key)
        {
            if (string.IsNullOrWhiteSpace(parameters))
                return "";

            string[] pl = Regex.Split(parameters, paramsSymbol1);
            key = key.ToLower();
            string result = String.Empty;

            foreach (string str in pl)
            {
                if (str.ToLower().StartsWith(key + paramsSymbol2))
                {
                    result = Regex.Split(str, paramsSymbol2)[1];
                    break;
                }
            }


            return result;

        }


        /// <summary>
        /// 移除一个键值
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string ParamRemove(string parameters, string key)
        {
            if (string.IsNullOrWhiteSpace(parameters))
                return "";

            int place = ParamIndexOf(parameters, key);
            if (place == -1)
                return parameters;

            string newParams = "";

            string[] pl = Regex.Split(parameters, paramsSymbol1);

            for (int i = 0; i < pl.Length; i++)
            {
                if (i == place)
                    continue;
                newParams += pl[i];
            }

            // newParams = newParams.Substring(0,newParams.Length-paramsSymbol2.Length);

            return newParams;
        }

        /// <summary>
        /// 将一个value值添加或者修改到参数列表中
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <returns></returns>
        public static string ParamSet(string parameters, string key, string value)
        {
            if (string.IsNullOrWhiteSpace(parameters))
                return "";

            int place = ParamIndexOf(parameters, key);
            if (place == -1)
            {
                parameters += key + paramsSymbol2 + value;
                return parameters;
            }

            string newParams = "";
            string[] pl = Regex.Split(parameters, paramsSymbol1);

            for (int i = 0; i < pl.Length; i++)
            {
                if (i == place)
                    newParams += key + paramsSymbol2 + value;

                newParams += pl[i];
            }

            //newParams = newParams.TrimEnd(paramsSymbol1.ToArray());

            return newParams;
        }

        /// <summary>
        /// 判断key项在参数列表中是否存在
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <param name="key">key</param>
        /// <returns>-1：没有在列表中发现该项</returns>
        public static int ParamIndexOf(string parameters, string key)
        {
            int place = -1;
            if (string.IsNullOrWhiteSpace(parameters))
                return place;

            string[] pl = Regex.Split(parameters, paramsSymbol1);
            key = key.ToLower();

            for (int i = 0; i < pl.Length; i++)
            {
                if (pl[i].ToLower().StartsWith(key + paramsSymbol2))
                {
                    place = i;
                    break;
                }
            }

            return place;
        }

        /// <summary>
        /// 将参数里的关键字替换
        /// </summary>
        /// <param name="value">value</param>
        /// <returns></returns>
        public static string ParamSymbolEncode(string value)
        {
            value = value.Replace(paramsSymbol1, "；").Replace(paramsSymbol2, "：");
            return value;
        }

        /// <summary>
        /// 将参数里的关键字替换
        /// </summary>
        /// <param name="value">value</param>
        /// <returns></returns>
        public static string ParamSymbolDecode(string value)
        {
            value = value.Replace(paramsSymbol1, "；").Replace(paramsSymbol2, "：");
            return value;
        }

        /// <summary>
        /// 获得日志等级的名称
        /// </summary>
        /// <param name="logLevel">等级</param>
        /// <returns></returns>
        public static string GetLogLevel(int logLevel)
        {
            string str = "";
            switch (logLevel)
            {
                case 1:
                    str = "Debug";
                    break;
                case 2:
                    str = "Info";
                    break;
                case 3:
                    str = "Warn";
                    break;
                case 4:
                    str = "Error";
                    break;
                case 5:
                    str = "Fatal";
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }
    }
}
