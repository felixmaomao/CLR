using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
namespace CLR读书笔记二
{
    class autofac测试三
    {
        public static void Main(string[] args)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<sward>()
                .AsSelf()
                .As<IWeapon>()
                .As<INoise>();
            IContainer container = builder.Build();
            var weapon = container.Resolve<sward>();
            weapon.MakeNoise();
            weapon.Attack();

            var weapon2 = container.Resolve<INoise>();
            weapon2.MakeNoise();

            Console.ReadKey();
        }
    }

    public interface IWeapon
    {
        void Attack();
    }

    public interface INoise
    {
        void MakeNoise();
    }
    public class sward : IWeapon, INoise
    {
        public void Attack()
        {
            Console.Write("Attacking");
        }

        public void MakeNoise()
        {
            Console.WriteLine("didididididididi");
        }
    }

}
