//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CLR读书笔记二
//{
//    class 使用Async读写文件
//    {
//        public static void Main(string[] args)
//        {
//            DoJob();
//            Console.ReadKey();
//        }

//        public static async Task<string> DoSomeTimeWastingThings()
//        {
//            await Task.Delay(3000);
//            return "FileReading is Ended";
//        }

//        public static async Task DoJob()
//        {
//            Console.WriteLine("doing the job");
//            Task<string> task = DoSomeTimeWastingThings();
//            Console.WriteLine("still doing the job");
//            string result = await task;
//            Console.WriteLine(result);
//        }
//    }
//}
