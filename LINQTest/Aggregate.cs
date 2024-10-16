using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTest
{
    internal class AggregateTest
    {
        public class Employee
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Salary { get; set; }
            public string Department { get; set; }
            public static List<Employee> GetAllEmployees()
            {
                List<Employee> listStudents = new List<Employee>()
                {
                    new Employee{ID= 101,Name = "Preety", Salary = 10000, Department = "IT"},
                    new Employee{ID= 102,Name = "Priyanka", Salary = 15000, Department = "Sales"},
                    new Employee{ID= 103,Name = "James", Salary = 50000, Department = "Sales"},
                    new Employee{ID= 104,Name = "Hina", Salary = 20000, Department = "IT"},
                    new Employee{ID= 105,Name = "Anurag", Salary = 30000, Department = "IT"},

                };
                return listStudents;
            }
        }

        public void test1()
        {
            string[] skills = { "C#.NET", "MVC", "WCF", "SQL", "LINQ", "ASP.NET" };
            string result = skills.Aggregate((s1, s2) => s1 + ", " + s2);

            Console.WriteLine(result);
        }

        public void test()
        {
            string CommaSeparatedEmployeeNames = Employee.GetAllEmployees().Aggregate<Employee, string, string>(
                            "Employee Names: ",  // seed value
                            (employeeNames, employee) => employeeNames = employeeNames + employee.Name + ", ",
                            employeeNames => employeeNames.Substring(0, employeeNames.Length - 1));

            Console.WriteLine(CommaSeparatedEmployeeNames);
        }

        public void test6()
        {
            string CommaSeparatedEmployeeNames = Employee.GetAllEmployees().Aggregate<Employee, string>(
                            "Employee Names: ",  // seed value
                            (employeeNames, employee) => employeeNames = employeeNames + employee.Name + ", ");

            int LastIndex = CommaSeparatedEmployeeNames.LastIndexOf(",");
            CommaSeparatedEmployeeNames = CommaSeparatedEmployeeNames.Remove(LastIndex);
            Console.WriteLine(CommaSeparatedEmployeeNames);
        }

        public void test4()
        {
            int Salary = Employee.GetAllEmployees()
                .Aggregate<Employee, int>(0,
                (TotalSalary, emp) => TotalSalary += emp.Salary);

            Console.WriteLine(Salary);
        }
        public void test3()
        {
            int[] intNumbers = { 3, 5};
            int result = intNumbers.Aggregate(2, (n1, n2) => n1 * n2);

            Console.WriteLine(result);
        }
        public void test2()
        {
            int[] intNumbers = { 3, 5, 7, 9 };
            int result = intNumbers.Aggregate((n1, n2) => n1 * n2);

            Console.WriteLine(result);
        }
    }
}
