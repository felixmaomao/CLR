//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net;
//using System.Net.Http;
//namespace CLR读书笔记二
//{
//    class 使用AsyncAwait并行发起多个web请求
//    {
//        public static void Main(string[] args)
//        {
//            CreateMultipleTaskAsync();
//            Console.ReadKey();
//        }
//        public static async Task CreateMultipleTaskAsync()
//        {
//            HttpClient client = new HttpClient();
//            Task<int> download1 = ProcessUrlAsync("http://msdn.microsoft.com", client);
//            Task<int> download2 = ProcessUrlAsync("http://msdn.microsoft.com/en-us/library/hh156528(VS.110).aspx", client);
//            Task<int> download3 = ProcessUrlAsync("http://msdn.microsoft.com/en-us/library/67w7t67f.aspx", client);
//            int length1 = await download1;
//            int length2 = await download2;
//            int length3 = await download3;
//            Console.WriteLine(length1+length2+length3);
//        }
//        public static async Task<int> ProcessUrlAsync(string url,HttpClient client)
//        {
//            var byteArray = await client.GetByteArrayAsync(url);
//            return byteArray.Length;
//        }
//    }
//}
