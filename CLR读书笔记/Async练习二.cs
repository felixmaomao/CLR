//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Threading;
//namespace CLR读书笔记二
//{
//    class Async练习二
//    {
//        public static void Main(string[] args)
//        {
//            TestAsync();

//            Console.ReadKey();
//        }
//        public static string JobThatWasteTime()
//        {
//            Console.WriteLine("Doing job that waste a lot of time");
//            Thread.Sleep(10000);
//            return "Busy Job done";
//        }

//        public static async Task<string> JobAsync()
//        {
//            Task<string> task = new Task<string>(JobThatWasteTime);
//            task.Start();
//            Console.WriteLine("doing other things");
//            string result = await task;
//            Console.WriteLine(result);
//            return result;
//        }

//        public static async Task TestAsync()
//        {
//            Task<string> task = JobAsync();
//            string a = await task;
//            Console.WriteLine(a);
//        }
//    }
//}
