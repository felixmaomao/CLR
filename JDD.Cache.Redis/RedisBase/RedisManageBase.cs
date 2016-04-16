//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ServiceStack.Redis;
//using ServiceStack.Text;

//namespace JDD.Cache.Redis
//{
//    public class RedisManager
//    {
//        //主redis去GetClient，从redis去GetReadOnlyClient

//        public static RedisConfig redisSection = ConfigurationManager.GetSection("redis") as RedisConfig;
//        public static string[] readWriteHosts = GetHosts();
//        public static string[] readOnlyHosts = GetHosts();
//        public static int poolTimeOutSeconds = redisSection.SocketPool.PoolTimeOutSeconds;
//        public static int maxWritePoolSize = redisSection.SocketPool.MaxWritePoolSize;
//        public static int maxReadPoolSize = redisSection.SocketPool.MaxReadPoolSize;

//        public static PooledRedisClientManager redisPoolManager = new PooledRedisClientManager(readWriteHosts, readOnlyHosts,
//               new RedisClientManagerConfig()
//               {
//                   MaxWritePoolSize = redisSection.SocketPool.MaxWritePoolSize,
//                   MaxReadPoolSize = redisSection.SocketPool.MaxReadPoolSize,
//                   AutoStart = true
//               }) { PoolTimeout = redisSection.SocketPool.PoolTimeOutSeconds };
        
//        public RedisManager(RedisModuleType moduleType)
//        {
//            //RedisConfig redisSection = ConfigurationManager.GetSection("redis") as RedisConfig;

//            //redisPoolManager = new PooledRedisClientManager(module.ReadWriteHosts.Split(','), module.ReadOnlyHosts.Split(','),
//            //    new RedisClientManagerConfig()
//            //    {
//            //        MaxWritePoolSize = module.MaxWritePoolSize,
//            //        MaxReadPoolSize = module.MaxReadPoolSize,
//            //        AutoStart = true
//            //    }) { PoolTimeout = module.PoolTimeOutSeconds };
//        }

//        /// <summary>
//        /// 获得对应的配置项
//        /// </summary>
//        /// <param name="moduleType"></param>
//        /// <returns></returns>
//        private static string[] GetHosts()
//        {
//            RedisConfig redisSection = ConfigurationManager.GetSection("redis") as RedisConfig;

//            string hosts = "";
            

//            for (int i = 0; i < redisSection.ModuleMappings.Count; i++)
//            {
//                hosts = hosts + redisSection.ModuleMappings[i].Address + ":" + redisSection.ModuleMappings[i].Port;
//                if (i < (redisSection.ModuleMappings.Count-1))
//                    hosts = hosts + ",";
//            }

//            return hosts.Split(',');
//        }

//        /// <summary>
//        /// 添加或者更新数据
//        /// expMinute 超时分钟数
//        /// </summary>
//        public static bool Set<T>(string key, T val, int expMinute = 0)
//        {
//            if (string.IsNullOrWhiteSpace(key) || val == null)
//            {
//                return false;
//            }

//            using (IRedisClient rds = redisPoolManager.GetClient())
//            {
//                if (expMinute <= 0)
//                {
//                    return rds.Set<T>(key, val);
//                }
//                else
//                {
//                    //设置超时时间
//                    TimeSpan ts = new TimeSpan(0, expMinute, 0);
//                    return rds.Set<T>(key, val, ts);
//                }
//            }

//        }

//        /// <summary>
//        /// 读取数据
//        /// </summary>
//        public static T Get<T>(string key)
//        {
//            if (string.IsNullOrWhiteSpace(key))
//            {
//                return default(T);
//            }

//            using (IRedisClient rds = redisPoolManager.GetReadOnlyClient())
//            {
//                return rds.Get<T>(key);
//            }

//        }

//        /// <summary>
//        /// 删除数据
//        /// </summary>
//        public static bool Remove(string key)
//        {
//            if (string.IsNullOrWhiteSpace(key))
//            {
//                return false;
//            }

//            using (IRedisClient rds = redisPoolManager.GetClient())
//            {
//                return rds.Remove(key);
//            }
//        }

//        /// <summary>
//        /// 添加set元素
//        /// </summary>
//        public static bool AddSet<T>(string key, T element)
//        {
//            if (string.IsNullOrWhiteSpace(key) || element == null)
//            {
//                return false;
//            }

//            using (IRedisClient redis = redisPoolManager.GetClient())
//            {
//                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(element);
//                redis.AddItemToSet(key, value);
//                return true;
//            }

//        }

//        /// <summary>
//        /// 是否包含set元素
//        /// </summary>
//        public static bool ContainsSet<T>(string key, T element)
//        {
//            if (string.IsNullOrWhiteSpace(key) || element == null)
//            {
//                return false;
//            }

//            using (IRedisClient redis = redisPoolManager.GetReadOnlyClient())
//            {
//                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(element);
//                return redis.SetContainsItem(key, value);
//            }

//        }

//        /// <summary>
//        /// 删除set元素
//        /// </summary>
//        public static bool RemoveSet<T>(string key, T element)
//        {
//            if (string.IsNullOrWhiteSpace(key) || element == null)
//            {
//                return false;
//            }

//            using (IRedisClient redis = redisPoolManager.GetClient())
//            {
//                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(element);
//                redis.RemoveItemFromSet(key, value);
//                return true;
//            }

//        }

//        /// <summary>
//        /// 批量set元素
//        /// </summary>
//        public static bool PipelinesSet<T>(List<string> keys, List<T> list)
//        {
//            if (keys == null || keys.Count == 0 || list == null || list.Count == 0 || keys.Count != list.Count)
//            {
//                return false;
//            }

//            using (IRedisClient redis = redisPoolManager.GetClient())
//            {
//                using (var pipeline = redis.CreatePipeline())
//                {
//                    var len = keys.Count;
//                    for (int i = 0; i < len; i++)
//                    {
//                        String key = keys[i];
//                        T val = list[i];
//                        if (string.IsNullOrWhiteSpace(key) || val == null)
//                        {
//                            continue;
//                        }
//                        pipeline.QueueCommand(r => r.Set<T>(key, val));
//                    }
//                    pipeline.Flush();
//                    return true;
//                }
//            }

//        }

//        /// <summary>
//        /// 批量get元素
//        /// </summary>
//        public static Dictionary<string, T> PipelinesGet<T>(List<string> keys)
//        {
//            if (keys == null || keys.Count == 0)
//            {
//                return null;
//            }

//            using (IRedisClient redis = redisPoolManager.GetReadOnlyClient())
//            {
//                using (var pipeline = redis.CreatePipeline())
//                {
//                    Dictionary<string, string> strResult = new Dictionary<string, string>();
//                    Dictionary<string, T> map = new Dictionary<string, T>();
//                    foreach (string key in keys)
//                    {
//                        if (string.IsNullOrWhiteSpace(key))
//                        {
//                            continue;
//                        }
//                        pipeline.QueueCommand(r => r.Get<String>(key), y => strResult[key] = y);
//                    }
//                    pipeline.Flush();
//                    foreach (var item in strResult)
//                    {
//                        T val = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(item.Value);
//                        map[item.Key] = val;
//                    }
//                    return map;
//                }
//            }

//        }

//        /// <summary>
//        /// 添加list元素
//        /// </summary>
//        public static bool AddList<T>(string key, T element)
//        {
//            if (string.IsNullOrWhiteSpace(key) || element == null)
//            {
//                return false;
//            }

//            using (IRedisClient redis = redisPoolManager.GetClient())
//            {
//                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(element);
//                redis.AddItemToList(key, value);
//                return true;
//            }

//        }

//        /// <summary>
//        /// 获取list元素
//        /// </summary>
//        public static T GetItemFromList<T>(string key, int index)
//        {
//            if (string.IsNullOrWhiteSpace(key) || index < 0)
//            {
//                return default(T);
//            }

//            using (IRedisClient redis = redisPoolManager.GetReadOnlyClient())
//            {
//                string value = redis.GetItemFromList(key, index);
//                T item = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(value);
//                return item;
//            }

//        }

//        /// <summary>
//        /// 获取list
//        /// </summary>
//        public static List<T> GetAllList<T>(string key)
//        {
//            if (string.IsNullOrWhiteSpace(key))
//            {
//                return new List<T>();
//            }

//            using (IRedisClient redis = redisPoolManager.GetReadOnlyClient())
//            {
//                List<string> values = redis.GetAllItemsFromList(key);

//                if (values == null || values.Count == 0)
//                {
//                    return new List<T>();
//                }
//                List<T> items = new List<T>(values.Count);
//                foreach (string value in values)
//                {
//                    T item = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(value);
//                    items.Add(item);
//                }
//                return items;
//            }

//        }
//        //redis.RemoveItemFromList()

//        /// <summary>
//        /// 删除list元素
//        /// </summary>
//        public static bool RemoveItemFromList<T>(string key, T item)
//        {
//            if (string.IsNullOrWhiteSpace(key))
//            {
//                return false;
//            }

//            using (IRedisClient redis = redisPoolManager.GetClient())
//            {
//                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(item);
//                redis.RemoveItemFromList(key, value);
//                return true;
//            }

//        }

//        /// <summary>
//        /// 根据索引设置list元素
//        /// </summary>
//        public static bool SetItemInList<T>(string key, T item, int index)
//        {
//            if (string.IsNullOrWhiteSpace(key) || index < 0)
//            {
//                return false;
//            }

//            using (IRedisClient redis = redisPoolManager.GetClient())
//            {
//                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(item);
//                redis.SetItemInList(key, index, value);
//                return true;
//            }

//        }

//        /// <summary>
//        /// 获取keys
//        /// </summary>
//        public static List<string> GetKeys(string keyPartern)
//        {
//            if (string.IsNullOrWhiteSpace(keyPartern))
//            {
//                return null;
//            }

//            using (IRedisClient redis = redisPoolManager.GetReadOnlyClient())
//            {
//                List<string> keys = redis.SearchKeys(keyPartern);
//                return keys;
//            }

//        }

//    }
    

//}
