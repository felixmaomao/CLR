/******************************************************************
 * Description   : 存储过程封装操作类
 * CreateDate    : 2012-7-3
 * Creater       : 李书喜
 * LastChangeDate: 2015-10-26 11：53
 * LastChanger   : 焦杰
 * ******************************************************************/

using System.Data;
using System.Data.SqlClient;

namespace JDD.DAL.Utility
{

    public class Instance
    {
        /// <summary>
        /// 存储过程名称
        /// </summary>
        private string _proceCode;
        private readonly string _proceName;
        private readonly object[] _proceParams;
        private readonly SqlParameter[] _cmdParameters;

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="proceCode">存储过程Code</param>
        /// <param name="proceName">存储过程名称</param>
        /// <param name="cmdParameters">command参数</param>
        /// <param name="proceParams">存储过程参数</param>
        public Instance(string proceCode, string proceName, SqlParameter[] cmdParameters, params object[] proceParams)
        {
            _proceCode = proceCode;
            _proceName = proceName;
            _proceParams = proceParams;
            _cmdParameters = cmdParameters;
            InitParameters();
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="connectionString">连接信息</param>
        /// <returns></returns>
        public int ExecuteNoQuery(string connectionString)
        {
            return SQLHelper.ExecuteNonQuery(connectionString, _proceName, _cmdParameters);
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="connectionString">连接信息</param>
        /// <param name="rtnXml">返回结果xml</param>
        /// <returns></returns>
        public int ExecuteToReturn(string connectionString, ref string rtnXml)
        {
            return SQLHelper.ExecuteNonQuery(connectionString, _proceName, _cmdParameters, ref rtnXml);
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="connectionString">连接信息</param>
        /// <param name="rltXml">返回结果xml</param>
        /// <returns></returns>
        public int ExecuteToResult(string connectionString, ref string rltXml)
        {
            const int val = 0;
            rltXml = SQLHelper.ExecuteReader(connectionString, _proceName, _cmdParameters);
            return val;
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="connectionString">连接信息</param>
        /// <param name="rltXml">返回结果xml</param>
        /// <param name="rtnXml">返回结果xml</param>
        /// <returns></returns>
        public int ExecuteToRltRtn(string connectionString, ref string rltXml, ref string rtnXml)
        {
            const int val = 0;
            rltXml = SQLHelper.ExecuteReader(connectionString, _proceName, _cmdParameters, ref rtnXml);
            return val;
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="connectionString">连接信息</param>
        /// <returns>返回DataTable</returns>
        public DataTable ExecuteToDataTable(string connectionString)
        {
            return SQLHelper.ExecuteDataTable(connectionString, _proceName, _cmdParameters);
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="connectionString">连接信息</param>
        /// <param name="rtnXml">返回结果xml</param>
        /// <returns>Datatable</returns>
        public DataTable ExecuteToDataTable(string connectionString, ref string rtnXml)
        {
            return SQLHelper.ExecuteDataTable(connectionString, _proceName, _cmdParameters, ref rtnXml);
        }

        /// <summary>
        /// 初始化参数列表
        /// </summary>
        private void InitParameters()
        {
            for (int ii = 0; ii < _proceParams.Length; ii++)
            {
                _cmdParameters[ii].Value = _proceParams[ii];
            }
        }
    }
}
