/******************************************************************
 * Description   : Redis操作方法类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-27 16:15
 * LastChanger   : 焦杰
 * ******************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using ServiceStack.Text;

namespace JDD.Cache.Redis
{
    public class RedisManager
    {
        //主redis去GetClient，从redis去GetReadOnlyClient

        public static RedisConfig redisSection = ConfigurationManager.GetSection("redis") as RedisConfig;
        public static string[] readWriteHosts = GetHosts();
        public static string[] readOnlyHosts = GetHosts();
        public static int poolTimeOutSeconds = redisSection.SocketPool.PoolTimeOutSeconds;
        public static int maxWritePoolSize = redisSection.SocketPool.MaxWritePoolSize;
        public static int maxReadPoolSize = redisSection.SocketPool.MaxReadPoolSize;

        public static PooledRedisClientManager redisPoolManager = new PooledRedisClientManager(readWriteHosts, readOnlyHosts,
               new RedisClientManagerConfig()
               {
                   MaxWritePoolSize = redisSection.SocketPool.MaxWritePoolSize,
                   MaxReadPoolSize = redisSection.SocketPool.MaxReadPoolSize,
                   AutoStart = true,
                   DefaultDb = redisSection.SocketPool.DefaultDb
               })
        { PoolTimeout = redisSection.SocketPool.PoolTimeOutSeconds };


        public RedisManager()
        {

        }

        /// <summary>
        /// 获得对应的配置项
        /// </summary>
        /// <returns></returns>
        private static string[] GetHosts()
        {
            RedisConfig redisSection = ConfigurationManager.GetSection("redis") as RedisConfig;

            string hosts = "";


            for (int i = 0; i < redisSection.ModuleMappings.Count; i++)
            {
                hosts = hosts + redisSection.ModuleMappings[i].Address + ":" + redisSection.ModuleMappings[i].Port;
                if (i < (redisSection.ModuleMappings.Count - 1))
                    hosts = hosts + ",";
            }

            return hosts.Split(',');
        }



        #region 存储对象的操作方法

        /// <summary>
        /// 添加或者更新数据
        /// expMinute 超时分钟数
        /// </summary>
        public static bool Set<T>(string key, T val, int expMinute = 0)
        {
            if (string.IsNullOrWhiteSpace(key) || val == null)
            {
                return false;
            }

            using (IRedisClient rds = redisPoolManager.GetClient())
            {
                if (expMinute <= 0)
                {
                    return rds.Set<T>(key, val);
                }
                else
                {
                    //设置超时时间
                    TimeSpan ts = new TimeSpan(0, expMinute, 0);
                    return rds.Set<T>(key, val, ts);
                }
            }

        }

        /// <summary>
        /// 添加或者更新数据
        /// expSecond 超时秒数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="expSecond"></param>
        /// <returns></returns>
        public static bool SetBySecond<T>(string key, T val, int expSecond = 0)
        {
            if (string.IsNullOrWhiteSpace(key) || val == null)
            {
                return false;
            }

            using (IRedisClient rds = redisPoolManager.GetClient())
            {
                if (expSecond <= 0)
                {
                    return rds.Set<T>(key, val);
                }
                else
                {
                    //设置超时时间
                    TimeSpan ts = new TimeSpan(0, 0, expSecond);
                    return rds.Set<T>(key, val, ts);
                }
            }

        }

        /// <summary>
        /// 读取数据
        /// </summary>
        public static T Get<T>(string key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    return default(T);
                }

                using (IRedisClient rds = redisPoolManager.GetReadOnlyClient())
                {
                    return rds.Get<T>(key);
                }
            }
            catch (Exception ex)
            {
                JDD.Log.LogHandle.Error(JDD.Log.LogType.Redis,
                    string.Format("key:{0},读取redis异常，ex:{1}", key, ex), "RedisGet");
                return default(T);
            }

        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public static bool Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            using (IRedisClient rds = redisPoolManager.GetClient())
            {
                return rds.Remove(key);
            }
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="keys">所有的keys值的集合</param>
        /// <returns></returns>
        public static bool RemoveBatch(List<string> keys)
        {
            bool result = true;
            foreach (string key in keys)
            {
                result = Remove(key);
            }
            return result;
        }

        /// <summary>
        /// 批量删除符合keysReg条件的所有key
        /// 例如：user_201:*
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="element">泛型对象</param>
        /// <returns></returns>
        /// <summary>
        /// 添加set元素
        /// </summary>
        public static bool AddSet<T>(string key, T element)
        {
            if (string.IsNullOrWhiteSpace(key) || element == null)
            {
                return false;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(element);
                redis.AddItemToSet(key, value);
                return true;
            }

        }

        /// <summary>
        /// 是否包含set元素
        /// </summary>
        public static bool ContainsSet<T>(string key, T element)
        {
            if (string.IsNullOrWhiteSpace(key) || element == null)
            {
                return false;
            }

            using (IRedisClient redis = redisPoolManager.GetReadOnlyClient())
            {
                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(element);
                return redis.SetContainsItem(key, value);
            }

        }

        /// <summary>
        /// 删除set元素
        /// </summary>
        public static bool RemoveSet<T>(string key, T element)
        {
            if (string.IsNullOrWhiteSpace(key) || element == null)
            {
                return false;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(element);
                redis.RemoveItemFromSet(key, value);
                return true;
            }

        }

        /// <summary>
        /// 批量set元素
        /// </summary>
        public static bool PipelinesSet<T>(List<string> keys, List<T> list, int expMinute = 0)
        {
            if (keys == null || keys.Count == 0 || list == null || list.Count == 0 || keys.Count != list.Count)
            {
                return false;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                using (var pipeline = redis.CreatePipeline())
                {
                    var len = keys.Count;
                    for (int i = 0; i < len; i++)
                    {
                        String key = keys[i];
                        T val = list[i];
                        if (string.IsNullOrWhiteSpace(key) || val == null)
                        {
                            continue;
                        }

                        if (expMinute <= 0)
                        {
                            pipeline.QueueCommand(r => r.Set<T>(key, val));
                        }
                        else
                        {
                            //设置超时时间
                            TimeSpan ts = new TimeSpan(0, expMinute, 0);
                            pipeline.QueueCommand(r => r.Set<T>(key, val));
                        }
                    }
                    pipeline.Flush();
                    return true;
                }
            }
        }

        /// <summary>
        /// 批量get元素
        /// </summary>
        public static Dictionary<string, T> PipelinesGet<T>(List<string> keys)
        {
            if (keys == null || keys.Count == 0)
            {
                return null;
            }

            using (IRedisClient redis = redisPoolManager.GetReadOnlyClient())
            {
                using (var pipeline = redis.CreatePipeline())
                {
                    Dictionary<string, string> strResult = new Dictionary<string, string>();
                    Dictionary<string, T> map = new Dictionary<string, T>();
                    foreach (string key in keys)
                    {
                        if (string.IsNullOrWhiteSpace(key))
                        {
                            continue;
                        }
                        pipeline.QueueCommand(r => r.Get<String>(key), y => strResult[key] = y);
                    }
                    pipeline.Flush();
                    foreach (var item in strResult)
                    {
                        T val = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(item.Value);
                        map[item.Key] = val;
                    }
                    return map;
                }
            }

        }
        #endregion

        #region List类型的操作方法
        /// <summary>
        /// 添加list元素
        /// </summary>
        public static bool AddList<T>(string key, T element)
        {
            if (string.IsNullOrWhiteSpace(key) || element == null)
            {
                return false;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(element);
                redis.AddItemToList(key, value);
                return true;
            }

        }

        /// <summary>
        /// 批量添加list元素
        /// </summary>
        public static bool AddRangeToList(string key, List<string> list)
        {
            if (string.IsNullOrWhiteSpace(key) || list == null || list.Count == 0)
            {
                return false;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                redis.AddRangeToList(key, list);
                return true;
            }

        }

        /// <summary>
        /// 获取list元素
        /// </summary>
        public static T GetItemFromList<T>(string key, int index)
        {
            if (string.IsNullOrWhiteSpace(key) || index < 0)
            {
                return default(T);
            }

            using (IRedisClient redis = redisPoolManager.GetReadOnlyClient())
            {
                string value = redis.GetItemFromList(key, index);
                T item = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(value);
                return item;
            }

        }

        /// <summary>
        /// 获取list
        /// </summary>
        public static List<T> GetAllList<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return new List<T>();
            }

            using (IRedisClient redis = redisPoolManager.GetReadOnlyClient())
            {
                List<string> values = redis.GetAllItemsFromList(key);

                if (values == null || values.Count == 0)
                {
                    return new List<T>();
                }
                List<T> items = new List<T>(values.Count);
                foreach (string value in values)
                {
                    T item = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(value);
                    items.Add(item);
                }
                return items;
            }

        }
        //redis.RemoveItemFromList()

        /// <summary>
        /// 删除list元素
        /// </summary>
        public static bool RemoveItemFromList<T>(string key, T item)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(item);
                redis.RemoveItemFromList(key, value);
                return true;
            }

        }


        /// <summary>
        /// 删除list
        /// </summary>
        public static bool RemoveAllFromList(string listId)
        {
            if (string.IsNullOrWhiteSpace(listId))
            {
                return false;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                redis.RemoveAllFromList(listId);
                return true;
            }

        }

        /// <summary>
        /// 根据索引设置list元素
        /// </summary>
        public static bool SetItemInList<T>(string key, T item, int index)
        {
            if (string.IsNullOrWhiteSpace(key) || index < 0)
            {
                return false;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(item);
                redis.SetItemInList(key, index, value);
                return true;
            }

        }

        /// <summary>
        /// 弹出最后进入的元素
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="key">key值</param>
        /// <returns></returns>
        public static T PopItemFromList<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return default(T);
            }
            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                return JsonSerializer.DeserializeFromString<T>(redis.PopItemFromList(key));
            }
        }

        /// <summary>
        /// 获取list长度
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static long GetListCount(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return 0;
            }
            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                return redis.GetListCount(key);
            }
        }


        /// <summary>
        /// 根据索引位置批量获取List
        /// </summary>
        /// <param name="key"></param>
        /// <param name="startIndex">开始下标，从0开始</param>
        /// <param name="endIndex">截止下标，startIndex=0，endIndex=1，取出索引为0和1两条数据</param>
        /// <returns></returns>
        public static List<string> GetRangeFromList(string key, int startIndex, int endIndex)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }
            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                return redis.GetRangeFromList(key, startIndex, endIndex);
            }
        }

        #endregion

        #region Key值操作
        /// <summary>
        /// 获取keys
        /// </summary>
        //public static List<string> GetKeys(string keyPartern)
        //{
        //    if (string.IsNullOrWhiteSpace(keyPartern))
        //    {
        //        return null;
        //    }

        //    using (IRedisClient redis = redisPoolManager.GetReadOnlyClient())
        //    {
        //        List<string> keys = redis.SearchKeys(keyPartern);
        //        return keys;
        //    }

        //}


        /// <summary>
        /// 判断Key在本数据库内是否已被使用(包括各种类型、内置集合等等)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ContainsKey(string key)
        {
            using (IRedisClient redis = redisPoolManager.GetReadOnlyClient())
            {
                return redis.ContainsKey(key);
            }
        }

        /// <summary>
        /// 从数据库中查找名称相等的Keys的集合，特殊模式如h[ae]llo，仅英文有效。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<string> SearchKeys(string key)
        {
            using (IRedisClient redis = redisPoolManager.GetReadOnlyClient())
            {
                return redis.SearchKeys(key);
            }
        }

        #endregion

        #region Hash表操作

        /// <summary>
        /// 获取hash实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IRedisHash<string, T> GetHash<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                IRedisHash<string, T> redisHash = redisTypeClient.GetHash<string>(key);
                return redisHash;
            }
        }

        /// <summary>
        /// 获取hash里面所有键值对(同redis命令hgetall)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Dictionary<string, T> GetAllHash<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return new Dictionary<string, T>();
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisHash<string, T> hash = GetHash<T>(key);
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                Dictionary<string, T> hashItems = redisTypeClient.GetAllEntriesFromHash(hash);
                return hashItems;
            }
        }

        /// <summary>
        /// 获取hash的长度(同redis命令hlen)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long GetHashCount<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return 0;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisHash<string, T> hash = GetHash<T>(key);
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                return redisTypeClient.GetHashCount<string>(hash);
            }
        }

        /// <summary>
        /// 获取hash里所有字段集合(同redis命令hkeys)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<string> GetAllFields<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return new List<string>();
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisHash<string, T> hash = GetHash<T>(key);
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                return redisTypeClient.GetHashKeys<string>(hash);
            }
        }

        /// <summary>
        /// 获取hash里所有内容集合(同redis命令hvals)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T> GetAllValues<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return new List<T>();
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisHash<string, T> hash = GetHash<T>(key);
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                return redisTypeClient.GetHashValues<string>(hash);
            }
        }

        /// <summary>
        /// 获取指定字段的内容(同redis命令hget)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static T GetValueFromHash<T>(string key, string field)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return default(T);
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisHash<string, T> hash = GetHash<T>(key);
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                return redisTypeClient.GetValueFromHash<string>(hash, field);
            }
        }

        /// <summary>
        /// hash里是否含有指定字段(同redis命令hexists)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool ContainsField<T>(string key, string field)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisHash<string, T> hash = GetHash<T>(key);
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                return redisTypeClient.HashContainsEntry<string>(hash, field);
            }
        }

        /// <summary>
        /// 移除指定字段(同redis命令hdel)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool RemoveField<T>(string key, string field)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisHash<string, T> hash = GetHash<T>(key);
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                return redisTypeClient.RemoveEntryFromHash<string>(hash, field);
            }
        }

        /// <summary>
        /// 修改指定字段的内容，如字段不存在，则直接创建(同redis命令hset)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetHash<T>(string key, string field, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisHash<string, T> hash = GetHash<T>(key);
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                return redisTypeClient.SetEntryInHash<string>(hash, field, value);
            }
        }

        /// <summary>
        /// 增加新内容，如字段不存在，则直接创建(同redis命令hsetnx)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AddHash<T>(string key, string field, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisHash<string, T> hash = GetHash<T>(key);
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                return redisTypeClient.SetEntryInHashIfNotExists<string>(hash, field, value);
            }
        }

        /// <summary>
        /// 批量增加新内容(同redis命令hmset)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="keyValuePairs"></param>
        public static void SetHashs<T>(string key, List<KeyValuePair<string, T>> keyValuePairs)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }

            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisHash<string, T> hash = GetHash<T>(key);
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                redisTypeClient.SetRangeInHash<string>(hash, keyValuePairs);
            }
        }


        #endregion
        #region 存储表格对象操作

        /// <summary>
        /// 存储虚拟表单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public static void TableStore<T>(T entity)
        {
            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                redisTypeClient.Store(entity);
            }
        }

        /// <summary>
        /// 存储虚拟表的对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        public static void TableStoreAll<T>(IEnumerable<T> entities)
        {
            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                redisTypeClient.StoreAll(entities);
            }
        }

        /// <summary>
        /// 取得虚拟表的对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> TableGetALL<T>()
        {
            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                return redisTypeClient.GetAll();
            }
        }

        /// <summary>
        /// 删除虚拟表的某个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public static void TableDelete<T>(T entity)
        {
            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                redisTypeClient.Delete(entity);
            }
        }

        /// <summary>
        /// 根据Id删除虚拟表中的某个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public static void TableDeleteById<T>(object id)
        {
            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                redisTypeClient.DeleteById(id);
            }
        }

        /// <summary>
        /// 根据Ids删除虚拟表中的一组对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        public static void TableDeleteByIds<T>(IEnumerable ids)
        {
            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                redisTypeClient.DeleteByIds(ids);
            }
        }

        /// <summary>
        /// 删除虚拟表的所有对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public static void TableDeleteAll<T>()
        {
            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                IRedisTypedClient<T> redisTypeClient = redis.As<T>();
                redisTypeClient.DeleteAll();
            }
        }

        #endregion


        /// <summary>
        /// 根据指定的Key，将值加1(仅整型有效)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long IncrementValue(string key)
        {
            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                return redis.IncrementValue(key);
            }
        }


        /// <summary>
        /// 根据指定的Key，将值加上指定值(仅整型有效)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long IncrementValueBy(string key, int count)
        {
            using (IRedisClient redis = redisPoolManager.GetClient())
            {
                return redis.IncrementValueBy(key, count);
            }
        }

        //public static void AddItemToList()
        //{


        //    using (IRedisClient redis = redisPoolManager.GetClient())
        //    {
        //        IRedisTypedClient<Person> IRPerson = redis.As<Person>();
        //        IRPerson.DeleteAll();


        //        Person p1 = new Person() { ID = 1, Name = "刘备" };
        //        Person p2 = new Person() { ID = 2, Name = "关羽" };
        //        Person p3 = new Person() { ID = 3, Name = "张飞" };
        //        Person p4 = new Person() { ID = 4, Name = "曹操" };
        //        Person p5 = new Person() { ID = 5, Name = "典韦" };
        //        Person p6 = new Person() { ID = 6, Name = "郭嘉" };
        //        List<Person> ListPerson = new List<Person>() { p2, p3, p4, p5, p6 };

        //        //添加单条数据
        //        IRPerson.Store(p1);
        //        //添加多条数据
        //        IRPerson.StoreAll(ListPerson);

        //        //Linq支持
        //        List<Person> list = IRPerson.GetAll().ToList();
        //        string name = IRPerson.GetAll().Where(m => m.ID == 1).First().Name;       //刘备
        //        IRPerson.Delete(p1);    //删除 刘备
        //        int count1 = IRPerson.GetAll().Count();
        //        IRPerson.DeleteById(2); //删除 关羽
        //        int count2 = IRPerson.GetAll().Count();

        //        //redis.AddItemToList("ktest_1", "111");
        //        //redis.AddItemToList("ktest_1", "222");
        //        //redis.AddItemToList("ktest_1", "333");
        //        //redis.AddItemToList("ktest_1", "444");
        //        //redis.AddItemToList("ktest_1", "555");
        //        //List<string> list = redis.GetAllItemsFromList("ktest_1");
        //        ////string result=redis.GetItemFromList("ktest_1", 2);
        //        ////string result2=redis.PopItemFromList("ktest_1");
        //        //redis.AddItemToSet("ktest_2", "111");
        //        //redis.AddItemToSet("ktest_2", "222");
        //        //redis.AddItemToSet("ktest_2", "333");
        //        //redis.AddItemToSet("ktest_2", "444");
        //        //redis.AddItemToSet("ktest_2", "555");
        //        //redis.AddItemToSet("ktest_2", "111");
        //        //redis.AddItemToSet("ktest_2", "222");
        //        //HashSet<string> list2 = redis.GetAllItemsFromSet("ktest_2");

        //        //int count=list2.Count();
        //    }

        //}

    }

    //public class Person {
    //    public int ID{get;set;}
    //    public string Name{get;set;}
    //}


}
