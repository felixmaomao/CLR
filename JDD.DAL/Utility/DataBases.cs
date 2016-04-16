/******************************************************************
 * Description   : 数据源操作类
 * CreateDate    : 2012-07-02
 * Creater       : 李书喜
 * LastChangeDate: 2015-10-26 11:40
 * LastChanger   : 焦杰
 * ******************************************************************/
using System;
using System.Collections.Generic;
using System.Data;

namespace JDD.DAL.Utility
{
    /// <summary>
    /// 功能：数据源操作类
    /// 作者：李书喜
    /// 日期：2012-7-2
    /// </summary>
    public class DataBases
    {
        /// <summary>
        /// 数据源名称
        /// </summary>
        private readonly string _name;
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// 数据源连接池
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// 数据源存储过程列表
        /// </summary>
        private readonly List<StorageProcedures> _procelist;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">数据源名称</param>
        /// <param name="connectionString">连接字符串</param>
        public DataBases(string name, string connectionString)
        {
            _name = name;
            _connectionString = connectionString;
            _procelist = new List<StorageProcedures>();
        }

        /// <summary>
        /// 添加存储过程
        /// </summary>
        /// <param name="proce">存储过程名称</param>
        public void AddProcedure(StorageProcedures proce)
        {
            if (!_procelist.Exists(Proce => Proce.Code == proce.Code))
            {
                _procelist.Add(proce);
            }
        }

        /// <summary>
        /// 根据存储过程Code获取实例
        /// </summary>
        /// <param name="proceCode">存储过程Code</param>
        /// <param name="paramList">参数</param>
        /// <returns></returns>
        private Instance GetProceInstance(string proceCode, params object[] paramList)
        {
            StorageProcedures proc = _procelist.Find(ii => ii.Code == proceCode);

            if (proc == null)
            {
                throw new Exception("找不到存储过程：" + null);
            }

            Instance instance = new Instance(proc.Code, proc.Name, proc.GetParameters(), paramList);
            return instance;
        }

        /// <summary>
        /// 执行存储过程无返回值、无结果集
        /// </summary>
        /// <param name="proceCode">存储过程Code</param>
        /// <param name="paramList">参数列表</param>
        public int ExecuteNoQuery(string proceCode, params object[] paramList)
        {
            int val;
            Instance instance = GetProceInstance(proceCode, paramList);
            if (instance != null)
            {
                val = instance.ExecuteNoQuery(_connectionString);
            }
            else
                val = -1;

            return val;
        }

        /// <summary>
        /// 执行存储过程有返回值、无结果集
        /// </summary>
        /// <param name="proceCode">存储过程Code</param>
        /// <param name="rtnxml">返回结果xml</param>
        /// <param name="paramList">参数列表</param>
        public int ExecuteReturn(string proceCode, ref string rtnxml, params object[] paramList)
        {
            int val = 0;
            Instance instance = GetProceInstance(proceCode, paramList);
            if (instance != null)
            {
                instance.ExecuteToReturn(_connectionString, ref rtnxml);
            }
            else
                val = -1;

            return val;
        }

        /// <summary>
        /// 执行存储过程无返回值、有结果集
        /// </summary>
        /// <param name="proceCode">存储过程Code</param>
        /// <param name="rltxml">返回结果xml</param>
        /// <param name="paramList">参数列表</param>
        public int ExecuteResult(string proceCode, ref string rltxml, params object[] paramList)
        {
            int val;
            Instance instance = GetProceInstance(proceCode, paramList);
            if (instance != null)
            {
                val = instance.ExecuteToResult(_connectionString, ref rltxml);
            }
            else
                val = -1;

            return val;
        }

        /// <summary>
        /// 执行存储过程有返回值、有结果集
        /// </summary>
        /// <param name="proceCode">存储过程Code</param>
        /// <param name="rtnxml">返回结果xml</param>
        /// <param name="paramList">参数列表</param>
        /// <param name="rltxml">返回结果xml</param>
        public int ExecuteRltRtn(string proceCode, ref string rltxml, ref string rtnxml, params object[] paramList)
        {
            int val;
            Instance instance = GetProceInstance(proceCode, paramList);
            if (instance != null)
            {
                val = instance.ExecuteToRltRtn(_connectionString, ref rltxml, ref rtnxml);
            }
            else
                val = -1;

            return val;
        }

        /// <summary>
        /// 执行存储过程有结果集
        /// </summary>
        /// <param name="proceCode">存储过程Code</param>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        public DataTable ExecuteToDataTable(string proceCode, params object[] paramList)
        {
            DataTable dt=null;

            Instance instance = GetProceInstance(proceCode, paramList);
            if (instance != null)
            {
                dt = instance.ExecuteToDataTable(_connectionString);
            }

            return dt;
        }

        /// <summary>
        /// 执行存储过程有返回值、有结果集
        /// </summary>
        /// <param name="proceCode">存储过程Code</param>
        /// <param name="rtnxml">返回结果xml</param>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        public DataTable ExecuteToDataTable(string proceCode, ref string rtnxml, params object[] paramList)
        {
            DataTable dt = null;

            Instance instance = GetProceInstance(proceCode, paramList);
            if (instance != null)
            {
                dt = instance.ExecuteToDataTable(_connectionString, ref rtnxml);
            }

            return dt;
        }

        /// <summary>
        /// 根据存储过程Code获取存储过程
        /// </summary>
        /// <param name="proceCode">存储过程Code</param>
        /// <returns></returns>
        public string[] GetProceParams(string proceCode)
        {
            StorageProcedures proce = _procelist.Find(ii => ii.Code == proceCode);
            return proce.GetParamCodes();
        }
    }
}
