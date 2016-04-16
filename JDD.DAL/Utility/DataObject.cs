/******************************************************************
 * Description   : 读取数据库xml文件类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-26 11:40
 * LastChanger   : 焦杰
 * ******************************************************************/
using System;
using System.Xml.Linq;
using System.IO;
namespace JDD.DAL.Utility
{
    public class DataObject
    {
        private static string _path;
        private static DataBases _main;      

        #region 构造函数
        static DataObject()
        {
            _path = AppDomain.CurrentDomain.BaseDirectory + "proc.xml";
            //_path = AppDomain.CurrentDomain.BaseDirectory + System.Configuration.ConfigurationManager.AppSettings["DataObjectFile"];
            Init();
        }
        /// <summary>
        /// 不允许实例化
        /// </summary>
        private DataObject()
        {
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        private static void Init()
        {
            if (!File.Exists(_path))
            {
                Log.LogHandle.Error(Log.LogType.DataBase, "配置文件不存在:" + _path, "DB");
                return;
            }

            XDocument doc = XDocument.Load(_path);

            #region 配置初始化

            if (doc.Root != null)
            {
                XElement root = doc.Root.Element("database");
                if (root == null)
                {
                    Log.LogHandle.Error(Log.LogType.DataBase, "配置文件格式异常[database]", "DB");
                    return;
                }

                string dbName = root.Attribute("name").Value;
                string dbconnection = root.Attribute("connectionString").Value;

                if (string.IsNullOrWhiteSpace(dbName) || string.IsNullOrWhiteSpace(dbconnection))
                {
                    Log.LogHandle.Error(Log.LogType.DataBase, "配置文件格式异常[database]属性值", "DB");
                    return;
                }
                _main = new DataBases(dbName.Trim(), dbconnection.Trim());
                #endregion

                StorageProcedures proc;
                Parameters parm;

                foreach (XElement porcElement in root.Elements("procedures"))
                {
                    #region 添加存储过程

                    proc = new StorageProcedures(porcElement.Attribute("code").Value, porcElement.Attribute("name").Value);               
                    _main.AddProcedure(proc);
                
                    #endregion

                    #region 添加属性Parameter
                    foreach (XElement parmElement in porcElement.Elements("parameters"))
                    {
                        parm = new Parameters(
                            parmElement.Attribute("name").Value,
                            parmElement.Attribute("type").Value,
                            int.Parse(parmElement.Attribute("size").Value),
                            int.Parse(parmElement.Attribute("direction").Value) + 1,
                            parmElement.Attribute("code").Value
                            );
                        proc.AddParameter(parm);
                    }

                    #endregion
                }
            }
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <returns></returns>
        public static DataBases GetInstance()
        {
            return _main;
        }    
    }
}
