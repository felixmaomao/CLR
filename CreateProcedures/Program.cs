using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;

namespace CreateDataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];

            SqlConnection conn = new SqlConnection(ConnectionString);

            string SQLScript1 = "select object_id as ProceCode,name proceName from sys.procedures order by Name ";

            string SQLScript2 = @"select syscolumns.id as proceCode, syscolumns.name as ParamName, 
                                  systypes.name as ParamType, syscolumns.length as ParamSize,syscolumns.isoutparam as Direction
                                  from syscolumns left join systypes on syscolumns.xusertype=systypes.xusertype
                                  where id in (select object_id from sys.procedures) Order by ID";

            conn.Open();
            DataTable dt1 = new DataTable();
            SqlDataReader reader1 = ExecuteReader(conn, SQLScript1,null);
            dt1.Load(reader1);
            reader1.Close();

            DataTable dt2 = new DataTable();
            SqlDataReader reader2 = ExecuteReader(conn, SQLScript2, null);
            dt2.Load(reader2);
            reader2.Close();

            conn.Close();

            CreateXmlFile(ConnectionString,dt1, dt2);
        }

        static SqlDataReader ExecuteReader(SqlConnection connection, string cmdText,params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

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

        private static void CreateXmlFile(string ConnectionString, DataTable dt1, DataTable dt2)
        {
            StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<frame>");
            sb.AppendFormat("<database name=\"BaseFrame\" connectionString=\"{0}\">", ConnectionString);

            foreach (DataRow row1 in dt1.Rows)
            {
                sb.Append(string.Format("<procedures name=\"{0}\" code=\"{1}\">", row1["proceName"].ToString(), row1["proceName"].ToString()));
                foreach (DataRow row2 in dt2.Select(" proceCode =" + row1["proceCode"].ToString()))
                {
                    sb.Append(string.Format("<parameters name=\"{0}\" code=\"{1}\" type=\"{2}\" size=\"{3}\" direction=\"{4}\"></parameters>", row2["ParamName"].ToString(), row2["ParamName"].ToString().Replace("@", ""), row2["ParamType"].ToString(), row2["ParamSize"].ToString(), row2["Direction"].ToString()));
                }
                sb.Append("</procedures>");
            }
            sb.AppendFormat("</database>");
            sb.Append("</frame>");

            string file = System.Configuration.ConfigurationManager.AppSettings["DataObjectFile"];

            StreamWriter sw = File.CreateText(file);
            sw.WriteLine(sb.ToString());
            sw.Close();
            sw.Dispose();
        }    
    }
}
