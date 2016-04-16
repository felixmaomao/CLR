//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace MVCForCLR.Controllers
//{
//    public class testcookieController : Controller
//    {
//        //
//        // GET: /testcookie/

//        public void Index()
//        {
//            //request先从客户端get cookie
//            HttpCookie cookiefromclient= this.HttpContext.Request.Cookies.Get("JaneCookie");
//            Response.Write(string.Format("cookie name ：{0}  cookie value ：{1}",cookiefromclient.Name,cookiefromclient.Value));



//            //response向客户端写入cookie
//            HttpCookie cookie = new HttpCookie("JaneCookie","yeah this a cookie by myself");
//            HttpCookie cookie2 = new HttpCookie("JasonCookie","this is jason cookie");
//            HttpCookie cookie3 = new HttpCookie("LinaCookie","this is lina cookie");
//            HttpCookie cookie4 = new HttpCookie("LinaCookie","this is another lina cookie");
//            this.HttpContext.Response.Cookies.Add(cookie);
//            this.HttpContext.Response.Cookies.Add(cookie2);
//            this.HttpContext.Response.Cookies.Add(cookie3);
//            this.HttpContext.Response.Cookies.Add(cookie4);
//        }

//    }
//}
