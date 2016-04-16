//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Autofac;
//namespace CLR读书笔记二
//{
//    class Autofac测试二
//    {
//        public static void Main(string[] args)
//        {
//            var builder = new ContainerBuilder();
//            builder.RegisterType<A>().As<IA>();
//            builder.RegisterType<B>().As<IB>();
//            builder.RegisterType<C>().As<IC>();
//            var container = builder.Build();

//            using (var scope = container.BeginLifetimeScope())
//            {
//                var serviceA = scope.Resolve<IA>();
//                var serviceB = scope.Resolve<IB>();
//                var serviceC = scope.Resolve<IC>();
//                serviceA.writeA();
//                serviceB.writeB();
//                serviceC.writeC();
//            }
//            Console.ReadKey();
//        }       
//    }



//    public interface IA
//    {
//        void writeA();
//    }
//    public interface IB
//    {
//        void writeB();
//    }
//    public interface IC
//    {
//        void writeC();
//    }
//    public class A : IA
//    {

//        public void writeA()
//        {
//            Console.WriteLine(" this is A");
//        }
//    }
//    public class B : IB
//    {

//        public void writeB()
//        {
//            Console.WriteLine(" this is B");
//        }
//    }
//    public class C : IC
//    {

//        public void writeC()
//        {
//            Console.WriteLine(" this is C");
//        }
//    }
//}
