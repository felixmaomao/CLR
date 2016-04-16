//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace CLR读书笔记
//{
//    class 事件详解
//    {
//        public static void Main(string[] args)
//        {
//            MailManager mailManager = new MailManager();
//            Fax fax = new Fax();
//            Pager pager = new Pager();
//            mailManager.NewMail += fax.Fax_onNewMail;
//            mailManager.NewMail += pager.Pager_onNewMail;
//            mailManager.NewMail(mailManager,"哈哈哈你好");
//            Console.ReadKey();
//        }       
//    }

//    public delegate void delegateNewMail(object sender,object e);
//    class MailManager
//    {
//        public delegateNewMail NewMail;
//    }
//    class Fax
//    {
//        public void Fax_onNewMail(object sender,object e)
//        {
//            Console.WriteLine("this is from "+sender.GetType().ToString());
//            Console.WriteLine(e.ToString());
//        }
//    }
//    class Pager
//    {
//        public void Pager_onNewMail(object sender,object e)
//        {
//            Console.WriteLine("this is from "+sender.GetType().ToString());
//            Console.WriteLine(e.ToString());
//        }
//    }    
//}
