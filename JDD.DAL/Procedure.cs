/******************************************************************
 * Description   : 存储过程封装操作类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-28 10：40
 * LastChanger   : 焦杰
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Xml;
using System.Xml.Linq;
using JDD.DAL.Utility;

namespace JDD.DAL
{
    public class Procedure
    {
        private static DataBases main;
        static Procedure()
        {
            main = DataObject.GetInstance();
        }

        private Procedure()
        {
        }

        public static string[] GetParamCodes(string proceCode)
        {
            return main.GetProceParams(proceCode);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="proceCode">存储过程Code</param>
        /// <param name="paramList">参数</param>
        /// <returns></returns>
        public static string Select(string proceCode, params object[] paramList)
        {
            try
            {
                string rltxml = string.Empty;

                string LogGuid = Guid.NewGuid().ToString();
                //DateTime StartTime = DateTime.Now;
                //JDD.Log.LogHandle.Info(JDD.Log.LogType.DataBase, "开始:" + LogGuid + ",[" + string.Join(":", paramList) + "]", proceCode);

                main.ExecuteResult(proceCode, ref rltxml, paramList);

                //DateTime EndTime = DateTime.Now;
                //JDD.Log.LogHandle.Info(JDD.Log.LogType.DataBase, "结束:" + LogGuid + ",耗时:" + (EndTime - StartTime).TotalSeconds, proceCode);

                return rltxml;
            }
            catch (Exception ex)
            {
                JDD.Log.LogHandle.Error(JDD.Log.LogType.DataBase, "存储过程执行异常，[" + string.Join(":", paramList) + "]，"+ex.Message+ex.StackTrace, proceCode);
                return "";
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="proceCode">存储过程Code</param>
        /// <param name="rtnxml">执行结果</param>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        public static string Select(string proceCode, ref string rtnxml, params object[] paramList)
        {
            try
            {
                string rltxml = string.Empty;

                string LogGuid = Guid.NewGuid().ToString();
                //DateTime StartTime = DateTime.Now;
                //JDD.Log.LogHandle.Info(JDD.Log.LogType.DataBase, "开始:" + LogGuid + ",[" + string.Join(":", paramList) + "]", proceCode);

                main.ExecuteRltRtn(proceCode, ref rltxml, ref rtnxml, paramList);

                //DateTime EndTime = DateTime.Now;
                //JDD.Log.LogHandle.Info(JDD.Log.LogType.DataBase, "结束:" + LogGuid + ",耗时:" + (EndTime - StartTime).TotalSeconds, proceCode);

                return rltxml;
            }
            catch (Exception ex)
            {
                JDD.Log.LogHandle.Error(JDD.Log.LogType.DataBase, "存储过程执行异常，[" + string.Join(":", paramList) + "]，" + ex.Message + ex.StackTrace, proceCode);
                return "";
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="proceCode">存储过程Code</param>
        /// <param name="paramList">参数</param>
        /// <returns></returns>
        public static DataTable SelectToDataTable(string proceCode, params object[] paramList)
        {
            try
            {
                DataTable dt = null;

                string LogGuid = Guid.NewGuid().ToString();
                //DateTime StartTime = DateTime.Now;
                //JDD.Log.LogHandle.Info(JDD.Log.LogType.DataBase, "开始:" + LogGuid + ",[" + string.Join(":", paramList) + "]", proceCode);

                dt=main.ExecuteToDataTable(proceCode, paramList);

                //DateTime EndTime = DateTime.Now;
                //JDD.Log.LogHandle.Info(JDD.Log.LogType.DataBase, "Count:" + (dt == null ? 0 : dt.Rows.Count) + ",结束:" + LogGuid + ",耗时:" + (EndTime - StartTime).TotalSeconds, proceCode);

                return dt;
            }
            catch (Exception ex)
            {
                JDD.Log.LogHandle.Error(JDD.Log.LogType.DataBase, "存储过程执行异常，[" + string.Join(":", paramList) + "]，" + ex.Message + ex.StackTrace, proceCode);
                return null;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="proceCode">存储过程</param>
        /// <param name="rtnxml">执行结果</param>
        /// <param name="paramList">参数</param>
        /// <returns></returns>
        public static DataTable SelectToDataTable(string proceCode, ref string rtnxml, params object[] paramList)
        {
            try
            {
                DataTable dt = null;

                string LogGuid = Guid.NewGuid().ToString();
                //DateTime StartTime = DateTime.Now;
                //JDD.Log.LogHandle.Info(JDD.Log.LogType.DataBase, "开始:" + LogGuid + ",[" + string.Join(":", paramList) + "]", proceCode);

                dt = main.ExecuteToDataTable(proceCode,ref rtnxml, paramList);

                //DateTime EndTime = DateTime.Now;
                //JDD.Log.LogHandle.Info(JDD.Log.LogType.DataBase, "Count:" + (dt == null ? 0 : dt.Rows.Count) + ",结束:" + LogGuid + ",耗时:" + (EndTime - StartTime).TotalSeconds, proceCode);

                return dt;
            }
            catch (Exception ex)
            {
                JDD.Log.LogHandle.Error(JDD.Log.LogType.DataBase, "存储过程执行异常，[" + string.Join(":", paramList) + "]，" + ex.Message + ex.StackTrace, proceCode);
                return null;
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="proceCode">存储过程</param>
        /// <param name="xmlDoc">返回xml</param>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        public static DataTable ExcuteToDataTable(string proceCode, ref XmlDocument xmlDoc, params object[] paramList)
        {
            try
            {
                string rtnxml = string.Empty;

                DataTable dt = null;

                string LogGuid = Guid.NewGuid().ToString();
                //DateTime StartTime = DateTime.Now;
                //JDD.Log.LogHandle.Info(JDD.Log.LogType.DataBase, "开始:" + LogGuid + ",[" + string.Join(":", paramList) + "]", proceCode);

                dt = main.ExecuteToDataTable(proceCode, ref rtnxml, paramList);

                //DateTime EndTime = DateTime.Now;
                //JDD.Log.LogHandle.Info(JDD.Log.LogType.DataBase, "Count:" + (dt == null ? 0 : dt.Rows.Count) + ",结束:" + LogGuid + ",耗时:" + (EndTime - StartTime).TotalSeconds, proceCode);
                
                if (!string.IsNullOrEmpty(rtnxml))
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(rtnxml);
                    if (!xmlDoc.DocumentElement.HasChildNodes)
                    {
                        xmlDoc = null;
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                JDD.Log.LogHandle.Error(JDD.Log.LogType.DataBase, "存储过程执行异常，[" + string.Join(":", paramList) + "]，" + ex.Message + ex.StackTrace, proceCode);
                return null;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="proceCode">存储过程</param>
        /// <param name="paramList">参数</param>
        /// <returns></returns>
        public static string Execute(string proceCode, params object[] paramList)
        {
            try
            {
                string rtnxml = string.Empty;

                string LogGuid = Guid.NewGuid().ToString();
                //DateTime StartTime = DateTime.Now;
                //JDD.Log.LogHandle.Info(JDD.Log.LogType.DataBase, "开始:" + LogGuid + ",[" + string.Join(":", paramList) + "]", proceCode);

                int val = main.ExecuteReturn(proceCode, ref rtnxml, paramList);

                //DateTime EndTime = DateTime.Now;
                //JDD.Log.LogHandle.Info(JDD.Log.LogType.DataBase, "结束:" + LogGuid + ",耗时:" + (EndTime - StartTime).TotalSeconds, proceCode);

                return rtnxml;
            }
            catch (Exception ex)
            {
                JDD.Log.LogHandle.Error(JDD.Log.LogType.DataBase, "存储过程执行异常，[" + string.Join(":", paramList) + "]，" + ex.Message + ex.StackTrace, proceCode);
                return "";
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="proceCode">存储过程</param>
        /// <param name="paramList">参数</param>
        /// <returns>xml</returns>
        public static XmlDocument ExecuteReturnXML(string proceCode, params object[] paramList)
        {
            try
            {
                string rtnxml = string.Empty;

                string LogGuid = Guid.NewGuid().ToString();
                //DateTime StartTime = DateTime.Now;
                //JDD.Log.LogHandle.Info(JDD.Log.LogType.DataBase, "开始:" + LogGuid + ",[" + string.Join(":", paramList) + "]", proceCode);

                int val = main.ExecuteReturn(proceCode, ref rtnxml, paramList);

                //DateTime EndTime = DateTime.Now;
                //JDD.Log.LogHandle.Info(JDD.Log.LogType.DataBase, "结束:" + LogGuid + ",耗时:" + (EndTime - StartTime).TotalSeconds, proceCode);

                if (string.IsNullOrEmpty(rtnxml))
                    return null;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(rtnxml);
                if (!doc.DocumentElement.HasChildNodes)
                {
                    return null;
                }
                return doc;
            }
            catch (Exception ex)
            {
                JDD.Log.LogHandle.Error(JDD.Log.LogType.DataBase, "存储过程执行异常，[" + string.Join(":", paramList) + "]，" + ex.Message + ex.StackTrace, proceCode);
                return null;
            }
        }

    }
}
