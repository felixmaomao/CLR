/******************************************************************
 * Description   : 数据库连接类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-26 11:50
 * LastChanger   : 焦杰
 * ******************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;

namespace JDD.DAL.Utility
{
    public class DbBase
    {
        private static readonly string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];

        /// <summary>
        /// 读取数据库
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteReader(string sql)
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader reader = ExecuteReader(conn, sql, null);
            dt.Load(reader);
            reader.Close();
            return dt;
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>影响结果</returns>
        public static int ExecuteNonQuery(string sql)
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            int result = ExecuteNonQuery(ConnectionString, sql, null);
            conn.Close();
            return result;
        }

        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdText">commandText</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>影响结果</returns>
        public static int ExecuteNonQuery(string connectionString, string cmdText, SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="connection">连接信息</param>
        /// <param name="cmdText">command text</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回结果</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw new Exception("异常信息：" + ex);
            }
        }

        /// <summary>
        /// 准备命令
        /// </summary>
        /// <param name="cmd">command</param>
        /// <param name="connection">连接信息</param>
        /// <param name="cmdText">command text</param>
        /// <param name="commandParameters">参数</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection connection, string cmdText, params SqlParameter[] commandParameters)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            cmd.Connection = connection;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;

            if (commandParameters != null)
            {
                foreach (SqlParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
        }
    }
}
