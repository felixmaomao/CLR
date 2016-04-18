using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JDD.Cache.Redis;
using MVCForCLR.DAL;
namespace MVCForCLR.Controllers
{
    public class ProductController : ApiController
    {

        //[HttpGet]
        //public string ProductList()
        //{
        //    string CacheKey = "testcache";

        //    string CacheResult = JDD.Cache.Redis.RedisManager.Get<string>(CacheKey);
        //    if (string.IsNullOrEmpty(CacheResult) || CacheResult == "{}")
        //    {
        //        CacheResult = new productHandler().ProductList(); 
        //        RedisManager.Set<string>(CacheKey, CacheResult, 7 * 24 * 60);
        //    }
        //    return CacheResult;
          
        //}

        [HttpGet]
        public string EmployeeList()
        {
            string CacheKey = "employeeCache";
            Jason.Cache.Redis.RedisManager.Set<string>("hello","hello world");
            //string CacheResult = JDD.Cache.Redis.RedisManager.Get<string>(CacheKey);
            string CacheResult=Jason.Cache.Redis.RedisManager.Get<string>(CacheKey);
            if (string.IsNullOrEmpty(CacheResult) || CacheResult == "{}")
            {
                CacheResult = new productHandler().EmployeeList();
                Jason.Cache.Redis.RedisManager.Set<string>(CacheKey, CacheResult);
            }
            return CacheResult;
        }
    }
}
