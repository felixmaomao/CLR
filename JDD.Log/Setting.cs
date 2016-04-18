/******************************************************************
 * Description   : 设置基础类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-27 14:50
 * LastChanger   : 焦杰
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace JDD.Log
{
    internal struct Setting
    {
        /// <summary>
        /// 站点ID
        /// </summary>
        public static int siteID;

        /// <summary>
        /// 日志的唯一来源标示
        /// </summary>
        public static string appName;

        /// <summary>
        /// 日志写入位置的类型
        /// </summary>
        public static string logToType;

        /// <summary>
        /// 设置
        /// </summary>
        static Setting()
        {
            bool result=int.TryParse(ConfigurationManager.AppSettings["siteID"],out siteID);
            if (result == false)
                siteID = 0;

            appName = ConfigurationManager.AppSettings["appName"] == null ? "" : ConfigurationManager.AppSettings["appName"].ToString();

            logToType = ConfigurationManager.AppSettings["logToType"];

            if (string.IsNullOrEmpty(logToType))
                logToType = "all";

        }

    

    }
}
