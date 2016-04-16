//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ServiceStack.Redis;
//namespace CLR读书笔记二
//{
//    class redisConsole
//    {
//        static RedisClient redisClient = new RedisClient("127.0.0.1", 6379);
//        public static void Main(string[] args)
//        {
//            var city = redisClient.Get<string>("city");
//            var place = redisClient.Get<string>("place");
//            Console.WriteLine(city);
//            Console.WriteLine(place);
//            Console.ReadKey();
//        }
//    }
//}
