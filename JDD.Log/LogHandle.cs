/******************************************************************
 * Description   : 日志管理类
 * CreateDate    : 2014-7-31
 * Creater       : 宋贵生
 * LastChangeDate: 2015-10-27 14:40
 * LastChanger   : 焦杰
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace JDD.Log
{
    /// <summary>
    /// 日志管理类
    /// 
    /// Author:宋贵生
    /// CreateDate:2014-7-31
    /// </summary>
    public class LogHandle
    {
        private LogLevel _logLevel;
        private string _logType;
        private string _content;
        private string _subject;
        private int _siteID;
        private string _appName;
        private string _site;
        private string _url;
        private string _logToType;
        private string _ip;


        private static DateTime _lastWcfErrTime = DateTime.Now.AddHours(-1); //最后一次Wcf连接异常时间，每10秒钟请求一次
        private static DateTime _lastToLocalErrTime = DateTime.Now.AddHours(-1); //最后一次Wcf连接异常记录到本地磁盘的时间，每5分钟请求一次

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="logType">日志类型</param>
        /// <param name="content">内容</param>
        /// <param name="subject">主题</param>
        /// <param name="logToType">日志写入的位置类型</param>
        private LogHandle(LogLevel logLevel, string logType, string content, string subject = "", LogToType logToType = LogToType.remote)
        {
            _logLevel = logLevel;
            _logType = logType;
            _content = content;
            _subject = subject;
            _ip = LogHelper.GetIP();
            _siteID = Setting.siteID;
            _appName = Setting.appName;
            _logToType = Setting.logToType;
            _site = LogHelper.GetDomain();
            _url = LogHelper.GetUrl();
        }


        /// <summary>
        /// Debug级别的日志的写方法
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="content">日志的内容</param>
        /// <param name="subject">日志主题</param>
        public static void Debug(string logType, string content, string subject = "", LogToType logToType = LogToType.remote)
        {
            Write(LogLevel.Debug, logType, content, subject, logToType);
        }


        /// <summary>
        /// Info级别的日志的写方法
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="content">日志的内容</param>
        /// <param name="subject">日志主题</param>
        public static void Info(string logType, string content, string subject = "", LogToType logToType = LogToType.remote)
        {
            Write(LogLevel.Info, logType, content, subject, logToType);
        }

        /// <summary>
        /// Warn级别的日志的写方法
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="content">日志的内容</param>
        /// <param name="subject">日志主题</param>

        public static void Warn(string logType, string content, string subject = "", LogToType logToType = LogToType.remote)
        {
            Write(LogLevel.Warn, logType, content, subject, logToType);
        }

        /// <summary>
        /// Error级别的日志的写方法
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="content">日志的内容</param>
        /// <param name="subject">日志主题</param>
        public static void Error(string logType, string content, string subject = "", LogToType logToType = LogToType.remote)
        {
            Write(LogLevel.Error, logType, content, subject, logToType);
        }

        /// <summary>
        /// Fatal级别的日志的写方法
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="content">日志的内容</param>
        /// <param name="subject">日志主题</param>
        public static void Fatal(string logType, string content, string subject = "", LogToType logToType = LogToType.remote)
        {
            Write(LogLevel.Fatal, logType, content, subject);
        }

        /// <summary>
        /// 日志的写方法
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="alertLevel">日志的等级</param>
        /// <param name="content">日志的内容</param>
        /// <param name="subject">日志主题</param>
        /// <param name="LogToType">日志写到的位置类型，默认为日志服务器</param>
        public static void Write(LogLevel alertLevel, string logType, string content, string subject = "", LogToType logToType = LogToType.remote)
        {
            LogHandle lh = new LogHandle(alertLevel, logType, content, subject);
            string _logToType = Setting.logToType;

            //if (_logToType == "remote" || _logToType == "all")
            //{
            //    Thread tr = new Thread(new ThreadStart(lh.WriteToWcf));
            //    tr.Start();
            //}
            //if (_logToType == "local" || _logToType == "all")
            //{
            //    Thread tr = new Thread(new ThreadStart(lh.WriteToLocal));
            //    tr.Start();
            //}

            if (_logToType == "all")
            {
                Parallel.Invoke(
                () => lh.WriteToWcf(),
                () => lh.WriteToLocal());
            }
            else if (_logToType == "remote")
            {
                Parallel.Invoke(
                  () => lh.WriteToWcf());
            }
            else
            {
                Parallel.Invoke(
                  () => lh.WriteToLocal());
            }
        }

        /// <summary>
        /// 将日志写入到本地的磁盘日志文件中
        /// </summary>
        /// <param name="alertLevel"></param>
        /// <param name="logType"></param>
        /// <param name="content"></param>
        /// <param name="subject"></param>
        public static void WriteToLocal(LogLevel alertLevel, string logType, string content, string subject, LogToType logToType = LogToType.remote)
        {
            LogHandle lh = new LogHandle(alertLevel, logType, content, subject);
            Thread tr = new Thread(new ThreadStart(lh.WriteToWcf));
            tr.Start();
        }

        /// <summary>
        /// 将日志写入到远程日志服务器
        /// </summary>
        private void WriteToWcf()
        {
            if (_lastWcfErrTime.AddSeconds(10) > DateTime.Now)
                return;

            try
            {
                string ip = _ip;
                LogService.ExchangeClient lc = new LogService.ExchangeClient();
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add(LogItem.DateTime.ToString(), DateTime.Now.ToString());
                dict.Add(LogItem.LogType.ToString(), _logType.ToString());
                dict.Add(LogItem.LogLevel.ToString(), ((byte)_logLevel).ToString());
                dict.Add(LogItem.SiteID.ToString(), _siteID.ToString());
                dict.Add(LogItem.Site.ToString(), _site);
                dict.Add(LogItem.AppName.ToString(), _appName);
                dict.Add(LogItem.Url.ToString(), _url);
                dict.Add(LogItem.Subject.ToString(), _subject);
                dict.Add(LogItem.Content.ToString(), _content);
                dict.Add(LogItem.IP.ToString(), _ip);
                lc.Add(dict);

                lc.Close();
            }
            catch (Exception e)
            {
                _lastWcfErrTime = DateTime.Now;

                if (_lastToLocalErrTime.AddMinutes(5) > DateTime.Now)
                    return;

                StringBuilder sb = new StringBuilder();
                sb.Append("日志写入wcf出现异常:");
                sb.AppendFormat("LogType：{0}，Subject：{1}，Content：{2}，错误：{3}",_logType,_subject, _content, e.Message + e.StackTrace);
                _content = sb.ToString();
                _logType = LogType.Log;
                _logLevel = LogLevel.Fatal;
                _subject = "WriteToWcfError";
                WriteToLocal();
                _lastToLocalErrTime = DateTime.Now;//设置最后一次异常记录到本地磁盘的时间
            }
        }

        /// <summary>
        /// 将日志写入到本地日志文件
        /// </summary>
        private void WriteToLocal()
        {
            try
            {
                LogLocalFile.Write(_logType, Setting.siteID, _site, _url, _logLevel, _ip, _subject, _content);
            }
            catch
            {;}
        }


    }
}