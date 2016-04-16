/******************************************************************
 * Description   : Sql帮助类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-26 13：00
 * LastChanger   : 焦杰
 * ******************************************************************/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;

namespace JDD.DAL.Utility
{
    public sealed class SQLHelper
    {
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 缓存参数
        /// </summary>
        /// <param name="cacheKey">缓存key</param>
        /// <param name="commandParameters">command参数</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 获取缓存参数
        /// </summary>
        /// <param name="cacheKey">缓存key</param>
        /// <returns></returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdText">Command Text</param>
        /// <param name="commandParameters">Command参数</param>
        /// <returns>返回结果</returns>
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
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdText">Command Text</param>
        /// <param name="commandParameters">Command参数</param>
        /// <param name="rtnxml">执行结果</param>
        /// <returns>返回结果</returns>
        public static int ExecuteNonQuery(string connectionString, string cmdText, SqlParameter[] commandParameters, ref string rtnxml)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                rtnxml = ConvertOutputToXml(cmd.Parameters);
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdText">Command Text</param>
        /// <param name="commandParameters">Command参数</param>
        /// <returns>返回结果</returns>
        public static string ExecuteReader(string connectionString, string cmdText, SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, cmdText, commandParameters);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                string rltxml = ConvertReaderToXml(reader);
                cmd.Parameters.Clear();
                reader.Close();
                return rltxml;
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdText">Command Text</param>
        /// <param name="commandParameters">Command参数</param>
        /// <param name="rtnxml">执行结果</param>
        /// <returns>返回结果</returns>
        public static string ExecuteReader(string connectionString, string cmdText, SqlParameter[] commandParameters, ref string rtnxml)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, cmdText, commandParameters);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                string rltxml = ConvertReaderToXml(reader);
                rtnxml = ConvertOutputToXml(cmd.Parameters);

                cmd.Parameters.Clear();
                reader.Close();
                return rltxml;
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdText">Command Text</param>
        /// <param name="commandParameters">Command参数</param>
        /// <returns>返回结果</returns>
        public static DataTable ExecuteDataTable(string connectionString, string cmdText, SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, cmdText, commandParameters);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();

                DataTable dt = new DataTable();
                dt.Load(reader);

                reader.Close();
                return dt;
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdText">Command Text</param>
        /// <param name="commandParameters">Command参数</param>
        /// <param name="rtnxml">执行结果</param>
        /// <returns>返回结果</returns>
        public static DataTable ExecuteDataTable(string connectionString, string cmdText, SqlParameter[] commandParameters, ref string rtnxml)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, cmdText, commandParameters);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                DataTable dt = new DataTable();
                dt.Load(reader);
                reader.Close();

                rtnxml = ConvertOutputToXml(cmd.Parameters);
                cmd.Parameters.Clear();
                return dt;
            }
        }

        /// <summary>
        /// 准备命令
        /// </summary>
        /// <param name="cmd">命令对象</param>
        /// <param name="connection">连接对象</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParams">Command参数</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection connection, string cmdText, SqlParameter[] cmdParams)
        {

            if (connection.State != ConnectionState.Open)
                connection.Open();

            cmd.Connection = connection;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmdParams != null)
            {
                foreach (SqlParameter parm in cmdParams)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// 输出结果转换为xml
        /// </summary>
        /// <param name="cmdParams">参数</param>
        /// <returns></returns>
        private static string ConvertOutputToXml(SqlParameterCollection cmdParams)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<Root>");
            foreach (SqlParameter param in cmdParams)
            {
                if (param.Direction == ParameterDirection.Output)
                    sb.AppendFormat("<{0}>{1}</{0}>", param.ParameterName.Replace("@", ""), param.Value);
            }
            sb.Append("</Root>");
            return sb.ToString();
        }

        /// <summary>
        /// SqlDataReader转换为xml
        /// </summary>
        /// <param name="reader">SqlDataReader</param>
        /// <returns></returns>
        private static string ConvertReaderToXml(SqlDataReader reader)
        {
            string Pattern = @"[\<\>&]";
            Regex regex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("read");
            do
            {
                while (reader.Read())
                {
                    XmlElement item = doc.CreateElement("item");
                    for (int ii = 0; ii < reader.FieldCount; ii++)
                    {
                        XmlElement node = doc.CreateElement(reader.GetName(ii));
                        string value = reader.GetValue(ii).ToString();
                        if (regex.IsMatch(value))
                        {
                            node.AppendChild(doc.CreateCDataSection(value));
                        }
                        else
                        {
                            node.InnerText = value;
                        }
                        item.AppendChild(node);
                    }
                    root.AppendChild(item);
                }
            }
            while (reader.NextResult());
            doc.AppendChild(root);
            return doc.OuterXml;
        }
    }
}