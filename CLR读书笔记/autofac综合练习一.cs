//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Autofac;
//namespace CLR读书笔记二
//{

//    //学习这种东西，最忌讳东一榔头西一棒子的了。这样只会一样都学不会，样样都听过。
//    class autofac综合练习一
//    {
//        public static void Main(string[] args)
//        {
//            ContainerBuilder builder = new ContainerBuilder();
//            //如果需要改变注入，那只需要改变下面的注入组件即可。其他地方的应用代码根本无需改动。
//            builder.RegisterType<FakeProductRepo>().As<IProductRepo>();
//            builder.RegisterType<ConsoleOutput>().As<IOutput>();
//            builder.RegisterType<productService>();
//            IContainer container = builder.Build();
//            //------------------
//            productService service = container.Resolve<productService>();
//            service.ListAllProducts();
//            service.FindProduct(2);
//            Console.ReadKey();           
//        }      
//    }

//    public class Product
//    {
//        public int ProductID { get; set; }
//        public string ProductName { get; set; }
//        public string ProductPrice { get; set; }       
//    }
//    public interface IProductRepo
//    {
//        IEnumerable<Product> GetAllProducts();
//        void InsertProduct(Product product);
//        Product FindProduct(int ID);
//    }
//    public class FakeProductRepo : IProductRepo
//    {
//        public List<Product> Products = new List<Product> {
//        new Product{ProductName="computer",ProductID=1,ProductPrice="100"},
//        new Product{ProductName="filco",ProductID=2,ProductPrice="100"},
//        new Product{ProductName="hhkb",ProductID=3,ProductPrice="100"},
//        new Product{ProductName="screen",ProductID=4,ProductPrice="100"},
//        new Product{ProductName="desk",ProductID=5,ProductPrice="100"}        
//        };

//        public IEnumerable<Product> GetAllProducts()
//        {
//            return Products;
//        }

//        public void InsertProduct(Product product)
//        {
//            Products.Add(product);
//        }

//        public Product FindProduct(int ID)
//        {
//            var productTOfind = Products.FirstOrDefault(x=>x.ProductID==ID);
//            return productTOfind;
//        }
//    }
//    public class EFProductRepo : IProductRepo
//    {

//        public IEnumerable<Product> GetAllProducts()
//        {
//            throw new NotImplementedException();
//        }

//        public void InsertProduct(Product product)
//        {
//            throw new NotImplementedException();
//        }

//        public Product FindProduct(int ID)
//        {
//            throw new NotImplementedException();
//        }
//    }
//    public class productService
//    {
//        private IProductRepo _repo;
//        private IOutput _output;
//        public productService(IProductRepo repo,IOutput output)
//        {
//            this._repo = repo;
//            this._output = output;
//        }
//        public void ListAllProducts()
//        {
//            _output.printProducts(_repo.GetAllProducts());
//        }
//        public void FindProduct(int ID)
//        {
//            Product product = _repo.FindProduct(ID);
//            this._output.printResults(product.ProductName);
//        }
        
//    }

//    public interface IOutput
//    {
//        void printResults(string result);
//        void printProducts(IEnumerable<Product> products);
//    }
//    public class ConsoleOutput : IOutput
//    {

//        public void printResults(string result)
//        {
//            Console.WriteLine(result);
//        }

//        public void printProducts(IEnumerable<Product> products)
//        {
//            foreach(var item in products)
//            {
//                Console.WriteLine(item.ProductID+"  "+item.ProductName+"  "+item.ProductPrice);
//            }
//        }
//    }
    


//}
