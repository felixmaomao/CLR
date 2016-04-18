/******************************************************************
 * Description   : 日志记录的项枚举类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-27 14:43
 * LastChanger   : 焦杰
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JDD.Log
{
    /// <summary>
    /// 日志记录的项
    /// </summary>
    public enum LogItem
    {
        DateTime,
        LogType,
        LogLevel,
        Site,
        SiteID,
        AppName,
        Url,
        Subject,
        Content,
        IP,
    }
}
