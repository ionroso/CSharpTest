using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTest
{
    internal class Select
    {
        public class Student
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public List<string> Programming { get; set; }
            public static List<Student> GetStudents()
            {
                return new List<Student>()
            {
                new Student(){ID = 1, Name = "James", Email = "James@j.com", Programming = new List<string>() { "C#", "Jave", "C++"} },
                new Student(){ID = 2, Name = "Sam", Email = "Sara@j.com", Programming = new List<string>() { "WCF", "SQL Server", "C#" }},
                new Student(){ID = 3, Name = "Patrik", Email = "Patrik@j.com", Programming = new List<string>() { "MVC", "Jave", "LINQ"} },
                new Student(){ID = 4, Name = "Sara", Email = "Sara@j.com", Programming = new List<string>() { "ADO.NET", "C#", "LINQ" } }
            };
            }
        }

        public class Employee
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Salary { get; set; }
            public static List<Employee> GetEmployees()
            {
                List<Employee> employees = new List<Employee>
            {
                new Employee {ID = 101, FirstName = "Preety", LastName = "Tiwary", Salary = 60000 },
                new Employee {ID = 102, FirstName = "Priyanka", LastName = "Dewangan", Salary = 70000 },
                new Employee {ID = 103, FirstName = "Hina", LastName = "Sharma", Salary = 80000 },
                new Employee {ID = 104, FirstName = "Anurag", LastName = "Mohanty", Salary = 90000 },
                new Employee {ID = 105, FirstName = "Sambit", LastName = "Satapathy", Salary = 100000 },
                new Employee {ID = 106, FirstName = "Sushanta", LastName = "Jena", Salary = 160000 }
            };
                return employees;
            }
        }

        public void SelectTest()
        {
            //Query Syntax
            IEnumerable<Employee> selectQuery = (from emp in Employee.GetEmployees()
                                                 select new Employee()
                                                 {
                                                     FirstName = emp.FirstName,
                                                     LastName = emp.LastName,
                                                     Salary = emp.Salary
                                                 });

            foreach (var emp in selectQuery)
            {
                Console.WriteLine($" Name : {emp.FirstName} {emp.LastName} Salary : {emp.Salary} ");
            }

            //Method Syntax
            List<Employee> selectMethod = Employee.GetEmployees().
                                          Select(emp => new Employee()
                                          {
                                              FirstName = emp.FirstName,
                                              LastName = emp.LastName,
                                              Salary = emp.Salary
                                          }).ToList();
            foreach (var emp in selectMethod)
            {
                Console.WriteLine($" Name : {emp.FirstName} {emp.LastName} Salary : {emp.Salary} ");
            }
            Console.ReadKey();
        }
        public void SelectManyMethodSyntaxTest()
        {
            //Using Method Syntax
            List<string> MethodSyntax = Student.GetStudents()
                                        .SelectMany(std => std.Programming)
                                        .Distinct()
                                        .ToList();

            foreach (string program in MethodSyntax)
            {
                Console.WriteLine(program);
            }
            Console.ReadKey();
        }
        public void SelectManyQuerySyntaxTest()
        {
            List<string> nameList = new List<string>() { "Pranaya", "Kumar" };
            IEnumerable<char> methodSyntax = nameList.SelectMany(x => x);
            foreach (char c in methodSyntax)
            {
                Console.Write(c + " ");
            }
            Console.ReadKey();
        }

        public void SelectManyQuerySyntaxTest1()
        {
            {
                //Using Method Syntax
                List<string> MethodSyntax = Student.GetStudents().SelectMany(std => std.Programming).ToList();
                //Using Query Syntax
                IEnumerable<string> QuerySyntax = from std in Student.GetStudents()
                                                  from program in std.Programming
                                                  select program;
                //Printing the values
                foreach (string program in MethodSyntax)
                {
                    Console.WriteLine(program);
                }
                Console.ReadKey();
            }
        }
    }
}
