/******************************************************************
 * Description   : Redis Config配置类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-27 16:10
 * LastChanger   : 焦杰
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace JDD.Cache.Redis
{
    public class RedisConfig : ConfigurationSection
    {
        /// <summary>
        /// Servers节点
        /// </summary>
        [ConfigurationProperty("servers")]
        public ModuleMappingCollection ModuleMappings
        {
            get { return this["servers"] as ModuleMappingCollection; }
        }

        /// <summary>
        /// SocketPool节点
        /// </summary>
        [ConfigurationProperty("socketPool")]
        public SocketPool SocketPool
        {
            get
            {
                return (SocketPool)this["socketPool"]; 
            }
            set
            {
                this["socketPool"] = value; 
            }
        }
    }

    public class ModuleMappingCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ModuleMapping();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ModuleMapping)element).Address;
        }

        //写一个索引器，方便的访问该集合中的元素。
        //如果不写，则只有foreach来访问。
        public ModuleMapping this[int index]
        {
            get
            {
                return this.BaseGet(index) as ModuleMapping;
            }
        }

    }

    public class SocketPool : ConfigurationElement 
    {
        public SocketPool()
        {
        }

        /// <summary>
        /// 超时设置节点
        /// </summary>
        [ConfigurationProperty("poolTimeOutSeconds")]
        public int PoolTimeOutSeconds
        {
            get
            {
                return int.Parse(this["poolTimeOutSeconds"].ToString());
            }
        }

        /// <summary>
        /// 最大写入大小
        /// </summary>
        [ConfigurationProperty("maxWritePoolSize")]
        public int MaxWritePoolSize
        {
            get
            {
                return int.Parse(this["maxWritePoolSize"].ToString());
            }
        }

        /// <summary>
        /// 最大读取大小
        /// </summary>
        [ConfigurationProperty("maxReadPoolSize")]
        public int MaxReadPoolSize
        {
            get
            {
                return int.Parse(this["maxReadPoolSize"].ToString());
            }
        }

        [ConfigurationProperty("DefaultDb")]
        public int DefaultDb
        {
            get
            {
                return int.Parse(this["DefaultDb"].ToString());
            }
        }

    }


    public class ModuleMapping : ConfigurationElement
    {
        /// <summary>
        /// 地址
        /// </summary>
        [ConfigurationProperty("address")]
        public string Address
        {
            get
            {
                //return (string)this["address"];
                return "127.0.0.1";
            }
        }

        /// <summary>
        /// 端口号
        /// </summary>
        [ConfigurationProperty("port")]
        public int Port
        {
            get
            {
                //return int.Parse(this["port"].ToString());
                return int.Parse("6379");
            }
        }
    }

}
