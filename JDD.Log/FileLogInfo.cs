/******************************************************************
 * Description   : 文件日志类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-27 14:35
 * LastChanger   : 焦杰
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JDD.Log
{
    /// <summary>
    /// 文件日志类
    /// </summary>
    [Serializable]
    public class FileLogInfo
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
        /// 日志警告等级
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

        public FileLogInfo()
        {
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="logFilePath">日志文件路径</param>
        /// <param name="siteID">站点ID</param>
        /// <param name="site">站点</param>
        /// <param name="url">URL</param>
        /// <param name="logLevel">日志警告等级</param>
        /// <param name="logIP">来源IP</param>
        /// <param name="subject">日志主题</param>
        /// <param name="logContent">日志内容</param>
        /// <param name="dateTime">日期</param>
        public FileLogInfo(string logFilePath, string siteID, string site, string url, string logLevel, string logIP, string subject, string logContent, DateTime dateTime)
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
    }
}
