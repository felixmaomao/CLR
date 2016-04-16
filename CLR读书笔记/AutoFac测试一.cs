//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Autofac;
//namespace CLR读书笔记二
//{
//    class AutoFac测试一
//    {
//        public static void Main(string[] args)
//        {
//            IContainer container=buildingDependencies();
//            var scope = container.BeginLifetimeScope();
//            DateWriter writer = new DateWriter(scope.Resolve<IOutput>());
//            writer.writeDate();
//            Console.ReadKey();
//        }

//        public static IContainer buildingDependencies()
//        {
//            var builder = new ContainerBuilder();
//            builder.RegisterType<ConsoleOutput>().As<IOutput>();
//            IContainer container = builder.Build();
//            return container;
//        }
//    }
   
  


//    public interface IOutput
//    {
//        void write(string content);
//    }

//    public class ConsoleOutput : IOutput
//    {
//        public void write(string content)
//        {
//            Console.WriteLine(content);
//        }
//    }

//    public class DateWriter
//    {
//        private IOutput output;
//        public DateWriter(IOutput output)
//        {
//            this.output = output;
//        }
//        public void writeDate()
//        {
//            this.output.write(DateTime.Today.ToShortDateString());
//        }
//    }
//}
