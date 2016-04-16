using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using WCFDemo.Contract;
namespace WCFDemo.Service
{
    public class Calculate : ICalculate
    {
        public void Add()
        {
            Console.WriteLine("reaching the add method");
        }
    }
}