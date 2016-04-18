using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using JDD.Log;

namespace JDD.Task.Log
{
    partial class LogService : ServiceBase
    {
        private System.Timers.Timer jobInitTime;

       
        public LogService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            #region 记录日志Log 1分钟执行一次

            this.jobInitTime = new System.Timers.Timer();
            this.jobInitTime.Interval = int.Parse(ConfigurationManager.AppSettings["logTimer"]);
            this.jobInitTime.Elapsed += new System.Timers.ElapsedEventHandler(RunJobLogFileData);
            this.jobInitTime.AutoReset = true;
            this.jobInitTime.Enabled = true;

            #endregion
        }


        /// <summary>
        /// 基础数据Init刷新
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void RunJobLogFileData(object source, System.Timers.ElapsedEventArgs e)
        {
            this.jobInitTime.Enabled = false;
            DateTime startTime = DateTime.Now;

            //记录文件
            (new InitMain()).GetQueueInfo();

            this.jobInitTime.Enabled = true;

        }


        

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
