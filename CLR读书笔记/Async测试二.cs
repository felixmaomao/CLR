//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Threading;
//namespace CLR读书笔记二
//{
//    class Async测试二
//    {
//        public static void Main(string[] args)
//        {
//            dosomething();
//            Console.ReadKey();
//        }

//        public static async Task<string> JobA()
//        {
//            return await Task.Run(() => { Thread.Sleep(3000);return "JobA"; });
//        }
//        public static async  Task<string> JobB(string name)
//        {
//            return await Task.Run(() => { Thread.Sleep(3000); return name+"JobB"; });
//        }
//        public static Task<string> JobC()
//        {
//            return Task.Run(() => { Thread.Sleep(3000); return "JobC"; });
//        }

//        public static async Task dosomething()
//        {
//            var taska = JobA();
//            var taskb = JobB(await taska);
//            var taskc = JobC();
//            string a = await taska;
//            string b = await taskb;
//            string c = await taskc;
//            Console.WriteLine(a+" "+b+" "+c);
//        }
//    }
//}
