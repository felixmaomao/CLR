//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Threading;
//namespace CLR读书笔记二
//{
//    class Async测试
//    {
//        public static  void Main(string[] args)
//        {
//            Console.WriteLine("MainThread : {0}", Thread.CurrentThread.ManagedThreadId);
//            JobAll();           
//            Console.ReadKey();
//        }
//        public static async Task<string> JobAAsync()
//        {
//            return await Task.Run(() => {
//                Console.WriteLine("JobA CurrentThread : {0}", Thread.CurrentThread.ManagedThreadId);
//                Thread.Sleep(3000);
//                return "JobA"; });
           
//        }
//        public static async Task<string> JobBAsync()
//        {            
//           return await Task.Run(() => {
//               Console.WriteLine("JobB CurrentThread : {0}", Thread.CurrentThread.ManagedThreadId);
//               Thread.Sleep(3000);
//               return "JobB";});            
//        }
//        public static async Task<string> JobCAsync()
//        {
//            return await Task.Run(() =>
//            {
//                Console.WriteLine("JobB CurrentThread : {0}", Thread.CurrentThread.ManagedThreadId);
//                Thread.Sleep(3000);
//                return "JobC";
//            }); 
//        }
//        public static async void JobAll()
//        {
//            Console.WriteLine("JobAll : {0}", Thread.CurrentThread.ManagedThreadId);
//            var taskA=JobAAsync();
//            var taskB=JobBAsync();
//            var taskC=JobCAsync();
//            Console.WriteLine("jobA result {0}",await taskA);
//            Console.WriteLine("jobB result {0}",await taskB);
//            Console.WriteLine("jobC result {0}",await taskC);
//        }

//    }
//}
