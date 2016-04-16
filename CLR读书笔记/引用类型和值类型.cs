//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace CLR读书笔记
//{
//    //CLR 定义了两种类型，ReferenceTypes引用类型 和 ValueTypes 值类型。我们定义的各种Class都是引用类型，而我们用的decimal int 之类是值类型。
//    //他们有什么区别呢？为什么 CLR要搞出两种类型出来呢？肯定是因为他们各有各的优点。我们会分别论述
//    //首先 引用类型，我们知道 创建引用类型的实例 必须通过new 这个关键字，比如 说 Person p=new Person();
//    //这个时候 会在托管堆中申请出一片空间出来，用来存放我们真正的这个对象，同时在栈中 会存放一个引用p 存放该对象在堆中的内存地址。也就是说 p中存放的是地址，我们管p叫做对对象的引用。
//    //也就是说 我们没创建一个引用类型的对象，都要通过new来申请空间，但是在堆中申请空间 比较慢而且损耗性能。所以CLR有了值类型的想法，值类型 就是把所有数据都存放在栈里面，因为栈比较快，所以性能会比较好。
//    //比如 int num=3; 就在栈 内存中 有个变量num 它里面存的就不是什么内存地址 ，而是真正的数值3。
//    //你可能会说，既然栈快，那我们干脆都放在栈里好了。这也是行不通的。我们写程序的时候会经常有赋值操作，比如
//    //我们写 Person p2=p; 也就是说我们 又定义了一个p2对象 ，指向原先的p对象。这个时候在内存中真正发生了呢，堆中并没有创建一个新的对象，而是 有个新的p2引用指向原来的对象而已。p 和p2指向的是同一个对象。
//    //而 对于 int num=3;   int num2=num;而言 就要在栈中完整复制一个空间。这样的话，如果本身对象体积比较大，经常复制的话，空间需要就太大了。而同样对于引用类型，只不过是复制了内存地址而已。
//    //所以 这两种类型都是有其存在意义的。当然，上面说的这些并不代表只有这么多优点。

//    //我们知道C#中所有对象 都继承自 system.object。这一点不要有半点怀疑。那什么时候 分支出了值类型和引用类型两种呢？
//    //system.object有个抽象子类system.ValueType， 具体的struct Enum等都是继承自这个抽象类，但是这些具体的值类型之后 却是不可以再“遗传”的，不可以再有子类，
//    //就从这里 断了根。 而引用类型则是从System.object直接往下继承，(不经过system.ValueType),类可以有自己的子类，一直向下，绵延不绝。

//    //关于引用类型 和值类型的赋值。看下面的示例

//    //那么当我们要定义自己的类型时，如何决定我们是定义成 引用类型 class 呢，还是定义成值类型 呢
//    //有这么几条原则
//    //如果要定义成值类型，那 你的类型 一定不可以 继承其他类型，也不可以被其他类型继承，这是必须要遵守的，上面也讲过 值类型 断了根
//    //还有就是 我们很少需要对 结构体中的字段进行修改。甚至 我们写代码的时候 习惯直接设置 结构体中的字段为readonly形式的，即只读的。
//    //我个人认为 结构体就是为了 更方便的使用 一整块基本不变的数据而已。
//    //那除了列的这种情况，基本都是定义成class了。



//    class 引用类型和值类型
//    {
//        public static void Main(string[] args)
//        {
//            PersonClass p1 = new PersonClass();          
//            p1.name = "shenwei"; p1.age = 23; p1.mobile = "xxxxx";
//            PersonStruct p2 = new PersonStruct();
//            Console.WriteLine(p2.age);
//            p2.name = "zhangxiaomao"; p2.age = 23; p2.mobile = "yyyyy";

//            //重新定义两个对象
//            PersonClass p3 = p1;  //指向p1
//            PersonStruct p4 = p2;  //复制P2

//            //当我们对 新的对象做修改时，原来的对象会改变吗？
//            p3.name = "changed";
//            p4.name = "changed";

//            Console.WriteLine(p1.name);
//            Console.WriteLine(p2.name);

//            Console.ReadKey();
//        }
//    }

//    public class PersonClass
//    {
//        public int age;
//        public string name;
//        public string mobile;
//    }
//    public struct PersonStruct
//    {
//        public int age;
//        public string name;
//        public string mobile;        
//    }

    
//}
