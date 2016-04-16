//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CLR读书笔记二
//{
//    class linq测试一
//    {
//        public static void Main(string[] args)
//        {
//            IEnumerable<Employee> employees = new List<Employee> {
//            new Employee{Name="Jason",Salary=30000},
//            new Employee{Name="Lina",Salary=8000},
//            new Employee{Name="Jane",Salary=12000},
//            new Employee{Name="Jack",Salary=10000},
//            new Employee{Name="Miracle",Salary=6000},
//            new Employee{Name="Simon",Salary=20000},
//            new Employee{Name="Coche",Salary=4000}
//            };

//            //get items by linq
//            IEnumerable<Employee> skilledEmployees = from item in employees
//                                                     where item.Salary > 10000
//                                                     select item;
//            //get items by extentionMethods
//            IEnumerable<Employee> skilledEmployeesbyMethods = employees.Where(x => x.Salary > 10000);

//            //where应该是最常见的语句了
//            var pooeEmployees = from item in employees
//                                where item.Salary > 10000 && item.Name.Contains("a")
//                                select item;







//            Console.ReadKey();

//        }

//        public static void PrintToConsole(IEnumerable<Employee> employees)
//        {
//            foreach(Employee e in employees)
//            {
//                Console.WriteLine(e.Name+"  :  "+e.Salary);
//            }            
//        }

//    }

//    class Employee
//    {
//        public string Name { get; set; }
//        public int Salary { get; set; }
//    }
//}
