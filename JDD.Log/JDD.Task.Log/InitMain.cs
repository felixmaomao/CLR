using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using JDD.Log;

namespace JDD.Task.Log
{
    public class InitMain
    {
        private static string _basePath = ConfigurationManager.AppSettings["LogPath"]; //日志的根位置

        /// <summary>
        /// 根据日志记录路径批量写入日志
        /// </summary>
        public void GetQueueInfo()
        {
            //取队列里的信息
            try
            {
                var list = MsgQueue.GetAllMessage();
                if (list != null && list.Count > 0)
                {
                    //取得路径列表
                    List<string> listPath = list.Select(x =>
                    {
                        return (string) x.LogFilePath;
                    }).Distinct().ToList();

                    //根据路径批量插入日志
                    foreach (var item in listPath)
                    {
                        var listInfo = list.Where(p => p.LogFilePath == item).ToList();
                        WriteBatchData(listInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                JDD.Log.LogHandle.Info(JDD.Log.LogType.RedisJob, "取消息到队列：" + ex.ToString(), "GetQueue");
            }
            //Thread.Sleep(2000);//睡眠2秒
        }


        /// <summary>
        /// 批量写入日志
        /// </summary>
        /// <param name="list"></param>
        private void WriteBatchData(List<FileLogInfo> list)
        {
            String FileName = DateTime.Now.ToString("yyyy-MM-dd HH") + ".log";
            if (String.IsNullOrEmpty(_basePath))
                _basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            String strFolderPath = _basePath + @"\LogFiles\" + list[0].LogFilePath;

            if (!Directory.Exists(strFolderPath))
                Directory.CreateDirectory(strFolderPath);

            string strPath = strFolderPath + @"\" + FileName;
            FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            foreach (var item in list)
            {
                m_streamWriter.WriteLine("--------------------------");
                m_streamWriter.WriteLine("D:" + item.LogDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "; L：" + item.LogLevel + "; S:" + item.Site + ";url=" + item.Url + "; Subject:" + item.LogSubject + "; IP: " + item.LogIP);
                m_streamWriter.WriteLine("C:" + item.LogContent);
            }
            m_streamWriter.Flush();
            m_streamWriter.Close();
            fs.Close();

        }
    }
}
