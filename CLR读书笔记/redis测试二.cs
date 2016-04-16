//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ServiceStack.Redis;
//namespace CLR读书笔记二
//{
//    class redis测试二
//    {
//        public static void Main()
//        {
//            IRedisClient client = new RedisClient("127.0.0.1",6379);

//            Student student = new Student { ID=1,Name="shenwei",Sex="male"};
//            Student student_2 = new Student { ID=2,Name="zhangxiaomao",Sex="female"};
//            client.Set<Student>(RedisKey.student_key+student.ID,student);
//            client.Set<Student>(RedisKey.student_key+student_2.ID,student_2);
//            Console.ReadKey();

//        }
//    }

//    public class Student
//    {
//        public int ID { get; set; }
//        public string Name { get; set; }
//        public string Sex { get; set; }
//    }

//    public class RedisKey
//    {
//        public static string student_key = "student100:";
//    }


//}
