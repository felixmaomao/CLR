using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jason.Cache.Redis
{
    public class RedisExpireTime
    {
        public const int DayOne = 24 * 60;
        public const int DaySeven = 7 * 24 * 60;
        public const int OneHour = 60;
        public const int HalfHour = 30;
    }
}
