//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//namespace CLR读书笔记二
//{
//    public class 流
//    {
        
//        //这个例子 先对一个字符串进行编码进入一个二进制数组，其后使用流对这个二进制数组进行读取，以及位置偏移等等，其后再将其写入到另一个二进制数组，最后在反编码成为字符      
//        public static void Main(string[] args)
//        {
//            string testString = "Stream!HelloWorld";
//            byte[] buffer = null;  //用来存储这个二进制数组
//            byte[] readbuffer = null;
//            char[] readCharArray = null;
//            string readstring = null;
//            using(MemoryStream stream=new MemoryStream())
//            {
//               if(stream.CanWrite)
//               {
//                   buffer =  Encoding.Default.GetBytes(testString);
//                   //byte[] buffer2 = null;
//                   //buffer2 = Encoding.Unicode.GetBytes(testString);   经测试 不同的编码形式果然得到的二进制数组是不一样的
//                   stream.Write(buffer,0,3);
//                   Console.WriteLine("现在stream.position在第{0}位置",stream.Position+1);
//                   long newpositioninstream = stream.CanSeek ? stream.Seek(3,SeekOrigin.Current) : 0;
//                   Console.WriteLine("重新定位后 stream 的位置在{0}",stream.Position+1);
//                   Console.WriteLine("buffer 的长度是{0}",buffer.Length);
//                   stream.Write(buffer,(int)newpositioninstream,buffer.Length-(int)newpositioninstream);
//                   stream.Position = 0;
//                   readbuffer=new byte[buffer.Length];
//                   int count = stream.Read(readbuffer,0,readbuffer.Length);
//                   int charcount = Encoding.Default.GetCharCount(readbuffer,0,count);
//                   readCharArray=new char[charcount];
//                   readCharArray = Encoding.Default.GetChars(readbuffer,0,charcount);
//                   for (int i = 0; i < readCharArray.Length;i++ )
//                   {
//                       readstring += readCharArray[i];
//                   }
//                   Console.WriteLine(readstring);
//               }
//            }
//            Console.ReadKey();
//        }
//    }
//}
