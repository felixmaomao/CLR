/******************************************************************
 * Description   : Redis Key基础类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-27 16:10
 * LastChanger   : 焦杰
 * ******************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JDD.Cache.Redis
{
    public class RedisKeyTypeBase
    {
        /// <summary>
        /// Key的分隔符
        /// </summary>
        public const string MergeRedisKeySpiltChar = "_";

        /// <summary>
        /// 组合成Key
        /// </summary>
        public static string MergeToRedisKey(string prefixRedisKey, string[] keyIds)
        {
            if (keyIds == null || keyIds.Length == 0)
            {
                return null;
            }
            return prefixRedisKey + String.Join(MergeRedisKeySpiltChar, keyIds);
        }
    }
}
