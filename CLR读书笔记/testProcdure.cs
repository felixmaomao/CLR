//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data;
//using System.Data.SqlClient;
//namespace CLR读书笔记二
//{
//    class testProcdure
//    {

//        //妈个蛋啊，关于ado.net的东西竟然都忘光了
//        public static void Main(string[] args)
//        {
//            string connstr = "Data Source = .;Initial Catalog = Northwind;User Id = sa;Password = 11111;";            
//            using(SqlConnection conn=new SqlConnection(connstr))
//            {
//                conn.Open();
//                Console.WriteLine("connection OK");
//                SqlCommand cmd = new SqlCommand("simpleProc",conn);
//                cmd.CommandType = CommandType.StoredProcedure;               
//                cmd.Parameters.Add("productName",SqlDbType.NVarChar,50);
//                cmd.Parameters.Add("result",SqlDbType.Int);
//                cmd.Parameters.Add("description",SqlDbType.NVarChar,100);
//                cmd.Parameters["productName"].Value = "Chai";           
//                cmd.Parameters["result"].Direction = ParameterDirection.Output;                
//                cmd.Parameters["description"].Direction = ParameterDirection.Output;
//                cmd.ExecuteNonQuery();

//                string result = cmd.Parameters["result"].Value.ToString();
//                string description = cmd.Parameters["description"].Value.ToString();

//                Console.WriteLine(result);
//                Console.WriteLine(description);                

//                Console.ReadKey();

//            }
//        }
//    }
//}
