//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Threading;
//namespace CLR读书笔记二
//{
//    class Task写法
//    {
//        public static void Main(string[] args)
//        {
//            dosomething();
//            Console.ReadKey();
//        }
//        public static void dosomething()
//        {
//            Task.Run(() => { Thread.Sleep(3000) ;Console.WriteLine("job 1"); });
//            Task.Run(() => { Thread.Sleep(3000); Console.WriteLine("job 2"); });
//            Task.Run(() => { Thread.Sleep(3000); Console.WriteLine("job 3"); });
//            Task.Run(() => { Thread.Sleep(3000); Console.WriteLine("job 4"); });
//            Task.Run(() => { Thread.Sleep(3000); Console.WriteLine("job 5"); });
//            Task.Run(() => { Thread.Sleep(3000); Console.WriteLine("job 6"); });
//            Task.Run(() => { Thread.Sleep(3000); Console.WriteLine("job 7"); });
//        }
//    }
//}
