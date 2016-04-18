/******************************************************************
 * Description   : 日志写入方法类
 * CreateDate    : 2014-7-22
 * Creater       : 宋贵生
 * LastChangeDate: 2015-10-27 14:45
 * LastChanger   : 焦杰
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace JDD.Log
{
    /// <summary>
    /// 将日志写入本地的文件
    /// 
    /// Author:宋贵生
    /// Date:2014-7-22
    /// </summary>
    public static class LogLocalFile
    {
        private delegate void AsyncHandler();

        private static Queue<FileLog> _logQueue = new Queue<FileLog>();
         private static Queue<FileLog> _logListQueue = new Queue<FileLog>();
        //private static string _logWriteFile = ConfigurationManager.AppSettings["LogWriteFile"]; //日志的跟位置
        private static string _basePath = ConfigurationManager.AppSettings["LogPath"]; //日志的根位置

        private static List<FileLog> listResult = new List<FileLog>();

        /// <summary>
        /// 写日志到磁盘文件
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="siteId">站点ID</param>
        /// <param name="url">url</param>
        /// <param name="logLevel">日志等级</param>
        /// <param name="logIp">用户IP地址</param>
        /// <param name="subject">日志主题</param>
        /// <param name="logContent">日志内容</param>
        /// <param name="site">站点</param>
        public static void Write(string logType, int siteId, string site, string url, LogLevel logLevel, string logIp, string subject, string logContent)
        {
            Write(logType, siteId, site, url, (byte)logLevel, logIp, subject, logContent, DateTime.Now);
        }

        /// <summary>
        /// 写日志到磁盘文件
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="siteId">站点ID</param>
        /// <param name="url">url</param>
        /// <param name="logLevel">日志等级</param>
        /// <param name="logIp">用户IP地址</param>
        /// <param name="subject">日志主题</param>
        /// <param name="logContent">日志内容</param>
        /// <param name="dateTime">时间</param>
        /// <param name="site">站带你</param>
        public static void Write(string logType, int siteId, string site, string url, LogLevel logLevel, string logIp, string subject, string logContent, DateTime dateTime)
        {
            Write(logType, siteId, site, url, (byte)logLevel, logIp, subject, logContent, dateTime);
        }

        /// <summary>
        /// 写日志到磁盘文件
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="siteId">站点ID</param>
        /// <param name="url">url</param>
        /// <param name="logLevel">日志等级</param>
        /// <param name="logIp">用户IP地址</param>
        /// <param name="subject">日志主题</param>
        /// <param name="logContent">日志内容</param>
        /// <param name="site">站点</param>
        public static void Write(string logType, int siteId, string site, string url, byte logLevel, string logIp, string subject, string logContent)
        {
            Write(logType, siteId, site, url, (byte)logLevel, logIp, subject, logContent, DateTime.Now);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="url">url</param>
        /// <param name="logLevel">日志等级</param>
        /// <param name="subject">日志主题</param>
        /// <param name="logContent">日志内容</param>
        /// <param name="siteId">站点ID</param>
        /// <param name="site">站点</param>
        /// <param name="logIp">来源IP</param>
        /// <param name="dateTime">记录时间</param>
        public static void Write(string logType, int siteId, string site, string url, byte logLevel, string logIp, string subject, string logContent, DateTime dateTime)
        {
            lock (_logQueue)
            {
                _logQueue.Enqueue(new FileLog(GetFilePath(logType, subject), siteId.ToString(),site,url, logLevel.ToString(), logIp, subject, logContent, dateTime));
                //MsgQueue.SendMessage(new FileLogInfo(GetFilePath(logType, subject), siteID.ToString(), site,
                //    url, logLevel.ToString(), logIP, subject, logContent, dateTime));
            }

            AsyncHandler asyc = new AsyncHandler(Write);

            IAsyncResult ia = asyc.BeginInvoke(null, null);

            asyc.EndInvoke(ia);
        }

        /// <summary>
        /// 从队列中写日志到文件中
        /// </summary>
        private static void Write()
        {
            lock (_logQueue)
            {
                while(_logQueue.Count > 0)
                {
                    
                    FileLog log = _logQueue.Dequeue();
                    if (log != null)
                    {
                        //_logListQueue.Enqueue(log);

                        //单个写入日志
                        log.Write();
                    }
                    //批量写入日志
                    //GetQueueInfo();
                }
            }
        }

        /// <summary>
        /// 获得日志存放的路径
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="subject">日志主题</param>
        /// <returns></returns>
        public static string GetFilePath(string logType,string subject)
        {
            if (subject.Trim() == "")
                return logType;
            else
                return logType + @"\" + subject.ToString();
        }

        /// <summary>
        /// 文件日志类
        /// </summary>
        private class FileLog
        {
            private string _logFilePath; //日志存放的位置
            private string _siteID; //日志警告等级
            private string _site; //站点
            private string _url; //url
            private string _logLevel; //日志警告等级
            private string _logSubject; //日志主题
            private string _logContent; //日志描述
            private string _logIP; //来源IP
            private DateTime _logDateTime; //时间
            /// <summary>
            /// 日志存放的位置
            /// </summary>
            public string LogFilePath
            {
                get { return _logFilePath; }
                set { _logFilePath = value; }
            }
            /// <summary>
            /// 站点ID
            /// </summary>
            public string SiteID
            {
                get { return _siteID; }
                set { _siteID = value; }
            }
            /// <summary>
            /// 站点
            /// </summary>
            public string Site
            {
                get { return _site; }
                set { _site = value; }
            }
            /// <summary>
            /// URL
            /// </summary>
            public string Url
            {
                get { return _url; }
                set { _url = value; }
            }
            /// <summary>
            /// 日志等级
            /// </summary>
            public string LogLevel
            {
                get { return _logLevel; }
                set { _logLevel = value; }
            }
            /// <summary>
            /// 日志主题
            /// </summary>
            public string LogSubject
            {
                get { return _logSubject; }
                set { _logSubject = value; }
            }
            /// <summary>
            /// 日志内容
            /// </summary>
            public string LogContent
            {
                get { return _logContent; }
                set { _logContent = value; }
            }
            /// <summary>
            /// 来源IP
            /// </summary>
            public string LogIP
            {
                get { return _logIP; }
                set { _logIP = value; }
            }
            /// <summary>
            /// 日志记录时间
            /// </summary>
            public DateTime LogDateTime
            {
                get { return _logDateTime; }
                set { _logDateTime = value; }
            }

            public FileLog(string logFilePath, string siteID, string site, string url, string logLevel, string logIP, string subject, string logContent, DateTime dateTime)
            {
                this._logFilePath = logFilePath;
                this._siteID = siteID;
                this._site = site;
                this._url = url;
                this._logLevel = logLevel;
                this._logSubject = subject;
                this._logContent = logContent;
                this._logIP = logIP;
                this._logDateTime = dateTime;
            }

            /// <summary>
            /// 日志写入文件
            /// </summary>
            public void Write()
            {


                String FileName = _logDateTime.ToString("yyyy-MM-dd HH") + ".log";
                if (String.IsNullOrEmpty(_basePath))
                    _basePath = System.AppDomain.CurrentDomain.BaseDirectory;
                String strFolderPath = _basePath + @"\LogFiles\" + this._logFilePath;

                if (!Directory.Exists(strFolderPath))
                    Directory.CreateDirectory(strFolderPath);

                string strPath = strFolderPath + @"\" + FileName;
                FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);

                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine("--------------------------");
                m_streamWriter.WriteLine("D:" + _logDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "; L：" + _logLevel + "; S:" + _site + ";url="+ _url +"; Subject:" + _logSubject + "; IP: " + _logIP);
                m_streamWriter.WriteLine("C:" + _logContent);
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs.Close();
                //fs.Dispose();
            }

        }

        /// <summary>
        /// 根据日志记录路径批量写入日志
        /// </summary>
        private static void GetQueueInfo()
        {
            FileLog log = _logListQueue.Peek();
            TimeSpan ts = DateTime.Now - log.LogDateTime;
            //每间隔五秒写入一次
            if (ts.Seconds >= 5)
            {
                //取出Queue里面的所有内容
                var list = _logListQueue.ToArray();
                //清空Queue+
                _logListQueue.Clear();

                //取得路径列表
                List<string> listPath = list.Select(x =>
                {
                    return (string) x.LogFilePath;
                }).Distinct().ToList();

                //根据路径批量插入日志
                foreach (var item in listPath)
                {
                    var listInfo = list.Where(p => p.LogFilePath == item).ToList();
                    WriteBatchData(listInfo);
                }

                //Thread.Sleep(2000);//睡眠2秒
            }
        }

        /// <summary>
        /// 批量写入日志
        /// </summary>
        /// <param name="list"></param>
        private static void WriteBatchData(List<FileLog> list)
        {
            String FileName = DateTime.Now.ToString("yyyy-MM-dd HH") + ".log";
            if (String.IsNullOrEmpty(_basePath))
                _basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            String strFolderPath = _basePath + @"\LogFiles\" + list[0].LogFilePath;

            if (!Directory.Exists(strFolderPath))
                Directory.CreateDirectory(strFolderPath);

            string strPath = strFolderPath + @"\" + FileName;
            FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            foreach (var item in list)
            {
                m_streamWriter.WriteLine("--------------------------");
                m_streamWriter.WriteLine("D:" + item.LogDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "; L：" + item.LogLevel + "; S:" + item.Site + ";url=" + item.Url + "; Subject:" + item.LogSubject + "; IP: " + item.LogIP);
                m_streamWriter.WriteLine("C:" + item.LogContent);
            }
            m_streamWriter.Flush();
            m_streamWriter.Close();
            fs.Close();
           
        }

    }

}
