using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using Message = System.Messaging.Message;

namespace JDD.Log
{
    public class MsgQueue
    {
        private static readonly string QueueUrl = @"FormatName:DIRECT=tcp:10.33.97.232\private$\MyLogs";
        private const string _queueLocalUrl = ".\\private$\\MyLogs";
        private const string _queueServerUrl = ".\\private$\\MyLogs";
        /**/

        /// <summary>
        /// 通过Create方法创建使用指定路径的新消息队列
        /// </summary>
        /// <param name="queuePath"></param>
        public static void Createqueue(string queuePath)
        {

            if (!MessageQueue.Exists(queuePath))
            {
                MessageQueue.Create(queuePath);

            }
        }

        /**/

        /// <summary>
        /// 连接消息队列并发送消息到队列
        /// </summary>
        public static bool SendMessage(FileLogInfo log)
        {
            
            bool flag = false;
            try
            {
                //if (!MessageQueue.Exists(@"FormatName:DIRECT=http://10.33.96.162/msmq/Private$/test"))
                //{
                //    MessageQueue.Create(@"http://10.33.96.162\private$\myQueue");
                //}
                //连接到本地的队列

                MessageQueue myQueue = new MessageQueue(QueueUrl);

                System.Messaging.Message myMessage = new System.Messaging.Message();
                myMessage.Body = log;
                myMessage.Formatter = new XmlMessageFormatter(new Type[] {typeof (FileLogInfo)});
                //发送消息到队列中
                myQueue.Send(myMessage);
                flag = true;
                return flag;
            }
            catch(Exception ex)
            {
                Write(ex.ToString());
                return false;
            }
            
        }

        /**/

        /// <summary>
        /// 连接消息队列并从队列中接收消息
        /// </summary>
        public static string ReceiveMessage()
        {
            //连接到本地队列
            MessageQueue myQueue = new MessageQueue(QueueUrl);
            myQueue.Formatter = new XmlMessageFormatter(new Type[] {typeof (FileLogInfo)});

            //从队列中接收消息
            System.Messaging.Message myMessage = myQueue.Receive();
            FileLogInfo log = (FileLogInfo) myMessage.Body; //获取消息的内容
            return log.ToString();

            return null;
        }

        /**/

        /// <summary>
        /// 连接队列并获取队列的全部消息
        /// </summary>
        public static List<FileLogInfo> GetAllMessage()
        {

            var listModel = new List<FileLogInfo>();
            //连接到本地队列
            MessageQueue myQueue = new MessageQueue(_queueServerUrl);

            Message[] message = myQueue.GetAllMessages();
            //清空队列
            myQueue.Purge();
            XmlMessageFormatter formatter = new XmlMessageFormatter(new Type[] {typeof (FileLogInfo)});
            for (int i = 0; i < message.Length; i++)
            {
                message[i].Formatter = formatter;
                FileLogInfo log = (FileLogInfo) message[i].Body;
                listModel.Add(log);

            }

            return listModel;
        }

        /// <summary>
        /// 清空指定队列的消息
        /// </summary>
        public static void ClearMessage()
        {
            MessageQueue myQueue = new MessageQueue(QueueUrl);
            myQueue.Purge();
        }

        private static void Write(string content)
        {
            string _basePath = ConfigurationManager.AppSettings["LogPath"]; //日志的根位置
            DateTime dtNow = DateTime.Now;
            String FileName = dtNow.ToString("yyyy-MM-dd HH") + ".log";
            if (String.IsNullOrEmpty(_basePath))
                _basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            String strFolderPath = _basePath + @"\LogFiles\" + "Queue";

            if (!Directory.Exists(strFolderPath))
                Directory.CreateDirectory(strFolderPath);

            string strPath = strFolderPath + @"\" + FileName;
            FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            m_streamWriter.WriteLine("--------------------------");
            m_streamWriter.WriteLine("D:" + dtNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + "; L：" + LogLevel.Info + "; S:" + LogHelper.GetDomain() + ";url=" + LogHelper.GetUrl() + "; Subject：SendQueue; IP: " + LogHelper.GetIP());
            m_streamWriter.WriteLine("C:" + content);
            m_streamWriter.Flush();
            m_streamWriter.Close();
            fs.Close();
            //fs.Dispose();
        }
    }

    
}
