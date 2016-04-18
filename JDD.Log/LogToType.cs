/******************************************************************
 * Description   : 日志写到的位置类型枚举类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-27 14:45
 * LastChanger   : 焦杰
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JDD.Log
{
    /// <summary>
    /// 日志写到的位置类型
    /// </summary>
    public enum LogToType
    {
        /// <summary>
        /// 写日志到本地磁盘文件，而不同步到日志服务器
        /// </summary>
        local,
        /// <summary>
        /// 写日志到日志服务器，而不写入本地磁盘文件
        /// </summary>
        remote,
        /// <summary>
        /// 既将日志写到本地磁盘文件，也将日志写到日志服务器
        /// </summary>
        all
    }
}
