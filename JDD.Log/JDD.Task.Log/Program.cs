using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;

namespace JDD.Task.Log
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            JDD.Log.LogHandle.Info(JDD.Log.LogType.RedisJob, "Service启动", "InitMain");
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new LogService()
            };
            ServiceBase.Run(ServicesToRun);

            //(new InitMain()).GetQueueInfo();
        }
    }
}
