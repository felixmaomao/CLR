//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.IO;
//using System.Threading.Tasks;
//namespace CLR读书笔记二
//{
//    class AsyncAwait用法
//    {

//        // async await这两个关键字是微软为程序员更方便的开发异步程序而创造的两个关键字，是在Task之后 进一步简化了
//        // 异步开发的难度。因为 高难度在生产力上是没有意义的
//        public static void Main(string[] args)
//        {
//            Console.WriteLine("the id od the main thread is {0}",Thread.CurrentThread.ManagedThreadId);
//            Console.ReadLine();
//            Task task = new Task(ProcessFileAsync);
//            task.Start();
//            task.Wait();
//            Console.ReadLine();
//        }

//        public static async void ProcessFileAsync()
//        {
//            Console.WriteLine("The thread id of the ProcessFileAsync method is {0}", Thread.CurrentThread.ManagedThreadId);
//            Task<string> task = ReadFileAsync("D:\\async.txt");
//            Console.WriteLine("Doing some other work");
//            string result = await task;
//            Console.WriteLine("The file contents are :{0}",result);
//        }
//        public static async Task<string> ReadFileAsync(string file)
//        {
//            Console.WriteLine("The thread id of the ReadFileAsync method is {0}",Thread.CurrentThread.ManagedThreadId);
//            Console.WriteLine("Begin reading file asynchronsly \n");
//            //reading the specific file
//            string DataRead = "";
//            using(StreamReader reader=new StreamReader(file))
//            {
//                string character = await reader.ReadToEndAsync();
//                DataRead += character;
//                System.Threading.Thread.Sleep(10000);
//            }
//            Console.WriteLine("Done Reading File asynchronsly..");
//            return DataRead;
//        }
//    }
//}
