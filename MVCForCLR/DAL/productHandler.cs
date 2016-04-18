using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JDD.DAL;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MVCForCLR.DAL
{
    public class productHandler
    {
        public string ProductList()
        {
            string From = " From ProductTypes ";
            string Fields = " * ";
            string OrderBy = " Order by ID ";
            string Condition = " where 1=1 ";
            JArray arr = new JArray();

            try
            {
                DataTable dt = Procedure.SelectToDataTable("P_DBQuery", Fields, From, Condition, OrderBy);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        JObject obj = new JObject();
                        obj["ID"] = Convert.ToInt32(row["ID"].ToString());
                        obj["Name"] =row["Name"].ToString();
                        //obj["Status"] = row[""].ToString();                       
                        arr.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return arr.ToString();
        }



        public string EmployeeList()
        {
            return "hello employee. it's my own redis";
        }



    }
}