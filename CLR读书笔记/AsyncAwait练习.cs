//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Threading;
//namespace CLR读书笔记二
//{
//    class AsyncAwait练习
//    {
//        public static void Main(string[] args)
//        {
//            Task task = new Task(doingB);
//            task.Start();
//            Console.ReadKey();
//        }

//        public static string doingA()
//        {
//            Console.WriteLine("doing A");
//            Thread.Sleep(10000);
//            return "doingA";
//        }
//        public static async void doingB()
//        {
//            Console.WriteLine("B starting");
//            Task<string> task = new Task<string>(doingA);
//            task.Start();
//            string result = await task;
//            Console.WriteLine("doing other things");
//            Console.WriteLine("Job A is done");
//            Console.WriteLine(result);
//        }

//    }
//}
