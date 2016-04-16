//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ServiceStack.Redis;
//namespace CLR读书笔记二
//{
//    class redis测试一
//    {
//        public static readonly RedisClient redisClient = new RedisClient("127.0.0.1",6379);
//        public static void Main(string[] args)
//        {
//            InsertData();
//            DisplayData();
//            Console.ReadKey();
//        }
//        public static void InsertData()
//        {
//            redisClient.FlushAll();
//            var redisUsers = redisClient.As<User>();
//            var redisBlogs=redisClient.As<Blog>();
//            var redisPosts=redisClient.As<Post>();

//            var user_shenwei = new User { ID=redisUsers.GetNextSequence(),Name="JasonShenW"};
//            var blog_shenwei = new Blog { ID =redisBlogs.GetNextSequence(), UserID = user_shenwei.ID, UserName = user_shenwei.Name };
//            var post_A = new Post {ID=redisPosts.GetNextSequence(),BlogID=blog_shenwei.ID,Title="redis in action",Content="我们来学习redis" };

//            var user_zhangxiaomao = new User {ID=redisUsers.GetNextSequence(),Name="ZhangXiaoMao"};
//            var blog_zhangxiaomao = new Blog {ID=redisBlogs.GetNextSequence(),UserID=user_zhangxiaomao.ID,UserName=user_zhangxiaomao.Name };
//            var post_B = new Post { ID=redisPosts.GetNextSequence(),BlogID=blog_zhangxiaomao.ID,Title="learning NoSql",Content="NoSql也很弱智嘛！"};

//            user_shenwei.BlogIds.Add(blog_shenwei.ID);
//            blog_shenwei.BlogPostIDs.Add(post_A.ID);

//            user_zhangxiaomao.BlogIds.Add(blog_zhangxiaomao.ID);
//            blog_zhangxiaomao.BlogPostIDs.Add(post_B.ID);
//            redisUsers.StoreAll(new[]{user_shenwei,user_zhangxiaomao});
//            redisBlogs.StoreAll(new[]{blog_shenwei,blog_zhangxiaomao});
//            redisPosts.StoreAll(new[]{post_A,post_B});
//            Console.WriteLine("stored successed!!");
           
//        }

//        public static void DisplayData()
//        { 
//            var redisUsers=redisClient.As<User>();
//            var users = redisUsers.GetAll();
//            foreach(var item in users)
//            {
//                Console.WriteLine(item.ID);
//                Console.WriteLine(item.Name);
//            }
//        }
//    }

//    class User
//    {
//        public User()
//        {
//            BlogIds = new List<long>();
//        }
//        public long ID { get; set; }
//        public string Name { get; set; }
//        public List<long> BlogIds { get; set; }
//    }
//    class Blog
//    {
//        public Blog()
//        {
//            BlogPostIDs = new List<long>();
//        }
//        public long ID { get; set; }
//        public long UserID { get; set; }
//        public string UserName { get; set; }
//        public List<long> BlogPostIDs { get; set; }
//    }
//    class Post
//    {
//        public long ID { get; set; }
//        public long BlogID { get; set; }
//        public string Title { get; set; }
//        public string Content { get; set; }
//    }
//}
