//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CLR读书笔记二
//{
//    //依赖注入思想，还是没能自然的掌握。依赖就是一个对象内部依赖于另一个对象。我觉得这更多的是一种模块上的依赖。为了更轻松的模块替换才这样？

//    class 依赖注入深度体验一
//    {

//    }

//    class Product
//    {

//    }


//    public interface IProductfactory
//    {
//        IEnumerable<Product> GetAll();
//    }
//    class ProductFactory:IProductfactory
//    {     
//        IEnumerable<Product> IProductfactory.GetAll()
//        {
//            //using ef
//            throw new NotImplementedException();
//        }
//    }
//    class ProductFactory_MySql:IProductfactory
//    {
//        //create by hands
//        public IEnumerable<Product> GetAll()
//        {
//           throw new NotImplementedException();
//        }
//    }

//    class Work_Line
//    {
//        private IProductfactory _repo;
//        public IProductfactory Repo
//        {
//            get { return _repo; }
//            private set;
//        }
//        public Work_Line() { }
//        public Work_Line(IProductfactory repo)
//        {
//            _repo = repo;
//        }
//    }


//}
