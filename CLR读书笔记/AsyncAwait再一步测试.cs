//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Threading;
//namespace CLR读书笔记二
//{
//    class AsyncAwait再一步测试
//    {
//        public static void Main(string[] args)
//        {
           
//            Console.WriteLine("Main Thread ID:{0}",Thread.CurrentThread.ManagedThreadId);

//            //OurSyncJob();
//            OurAsyncJobTask();

//            //JustAWrapperMethod();
//            //WrapperMethodWithAsync().Wait();

//            Console.WriteLine("---------------");
//            Console.ReadKey();
//        }      
        
//        //sync method sample
//        public static void DownLoadWebPage()
//        {
//            //TODO  cost 5s            
//            Console.WriteLine("DownLoadWebPage on Thread:{0}",Thread.CurrentThread.ManagedThreadId);
//            Thread.Sleep(5000);
//            Console.WriteLine("End downloading the page..");
//        }

//        public static void LoadDatafromDB()
//        {
//            //TODO  cost 5s            
//            Console.WriteLine("LoadDataFromDB on Thread:{0}",Thread.CurrentThread.ManagedThreadId);
//            Thread.Sleep(5000);
//            Console.WriteLine("End loading Data..");
//        }

//        public static void OurSyncJob()
//        {
//            Console.WriteLine("start doing things sync");
//            DownLoadWebPage();
//            LoadDatafromDB();
//            //do some other things
//            Console.WriteLine("do some other things");            
//        }

//        public static async Task OurAsyncJobTask()
//        {
//            Console.WriteLine("start doing things async");
//            var taskA=Task.Run(() => { DownLoadWebPage(); });
//            var taskB=Task.Run(() => { LoadDatafromDB(); });
//            await Task.WhenAll(taskA,taskB);                  //这才是合理的写法，同时做俩任务，然后等俩都做完。
//            Console.WriteLine("do some other things");
//        }
        
//        // 经过这个测试 我们可以发现 单线程sync的话 耗时很长 超过10s
//        // 而 用task 开启多个线程并行执行的话 只耗时5s多 ，性能大大提升
//        // 后来出现的async await并不是用来开启新线程的。他们从来不会开启新线程，开启新线程的 是Task.Run .net自带的一些比如
//        // getStringAsync 这些方法 也是在内部用task开启的新线程
//        // await只是 为了让线程可能更方便的进行等待。 被await的 task必须要执行完之后才能向下执行。其实这里面的实现机制是
//        // 通过将await语句后面的语句包装成委托，等待执行完当前task之后 再执行被包装的委托
//        //所以下面的这种写法 就失去意义了，虽然新开启了线程，但要等执行完之后再开启另一个线程，相当于虽然有很多人
//        //但你是一个做完再换另一个，根本不会节约时间。 应该让许多人同时做
//        //public static async void OurAsyncJobTask()
//        //{
//        //    Console.WriteLine("start doing things async");
//        //    await Task.Run(() => { DownLoadWebPage(); });
//        //    await Task.Run(() => { LoadDatafromDB(); });
//        //    Console.WriteLine("do some other things");
//        //}

//        public static void JustAWrapperMethod()
//        {
//            OurAsyncJobTask();               //直接这么调用异步方法的话，因为新开启线程的原因，会导致该方法后面的 代码继续执行，就容易混乱了
//        }

//        public static async Task WrapperMethodWithAsync()
//        {
//            await OurAsyncJobTask();          //所以 异步方法 都会一层一层async  await 这样子向外包装。 俗称感染。
//                                              //也就是 如果你的方法要调用异步方法，那你这个方法也最好写成异步的去匹配。
//                                              //然后调用的时候 都用上await 保证当前方法的内容都执行完，而不管你开了多少线程，这样才能保证不出现混乱。                                             
//        }
        
//        //也就是说 异步的使用场景在于 需要多个（耗时）任务同时进行，要不然就做一件事，你新开线程不是等于没开么。
//    }
//}
