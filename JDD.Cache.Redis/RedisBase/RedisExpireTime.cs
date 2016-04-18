/******************************************************************
 * Description   : Redis 超时基础类
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JDD.Cache.Redis
{
    public class RedisExpireTime
    {
        /// <summary>
        /// 1天
        /// </summary>
        public const int DayOne = 24 * 60;

        /// <summary>
        /// 7天
        /// </summary>
        public const int DaySeven = 7 * 24 * 60;

        /// <summary>
        /// 1小时
        /// </summary>
        public const int HourOne = 60;

        /// <summary>
        /// 半小时
        /// </summary>
        public const int HourHalf = 30;
    }
}
