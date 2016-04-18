/******************************************************************
 * Description   : 日志信息等级枚举类
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
    /// 日志信息等级
    /// </summary>
    public enum LogLevel
    {
        Debug = 1,
        Info = 2,
        Warn = 3,
        Error = 4,
        Fatal = 5
    }
}
