using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
namespace MVCForCLR
{
    //将一张图片编码成二进制数组，再通过response显示
    public static class StreamAndImage
    {
        public static byte[] LoadImage(HttpServerUtilityBase Server)
        {
            byte[] buffer=null;
            using(FileStream stream=new FileStream(Server.MapPath("~/1.png"),FileMode.Open))
            {                
                buffer = new byte[stream.Length];
                stream.Read(buffer,0,(int)(stream.Length));                
            }
            return buffer;
        }

        public static Image byteToImage(byte[] imagebytes)
        {
            MemoryStream stream = new MemoryStream(imagebytes);
            Image img = Image.FromStream(stream);
            return img;
        }
    }
}