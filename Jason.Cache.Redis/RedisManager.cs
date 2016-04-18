using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
namespace Jason.Cache.Redis
{
    public class RedisManager
    {
        private static IRedisClient _client=new RedisClient("127.0.0.1",6379);
        public static IRedisClient Client
        {
            get { return _client; }
            set { _client = value; }
        }      

        public static T Get<T>(string key)
        {
            return Client.Get<T>(key);
        }

        public static void Set<T>(string key,T value)
        {
            Client.Set<T>(key,value);
        }

    }
}
