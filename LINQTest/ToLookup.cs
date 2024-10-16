using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTest
{
    internal class ToLookup
    {
        public class Employee1
        {
            public string Name { get; set; }
            public string Department { get; set; }
        }

        public class Student
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public string Branch { get; set; }
            public int Age { get; set; }
            public static List<Student> GetStudents()
            {
                return new List<Student>()
                        {
                            new Student { ID = 1001, Name = "Preety", Gender = "Female", Branch = "CSE", Age = 20 },
                            new Student { ID = 1002, Name = "Snurag", Gender = "Male", Branch = "ETC", Age = 21  },
                            new Student { ID = 1003, Name = "Pranaya", Gender = "Male", Branch = "CSE", Age = 21  },
                            new Student { ID = 1004, Name = "Anurag", Gender = "Male", Branch = "CSE", Age = 20  },
                            new Student { ID = 1005, Name = "Hina", Gender = "Female", Branch = "ETC", Age = 20 },
                            new Student { ID = 1006, Name = "Priyanka", Gender = "Female", Branch = "CSE", Age = 21 },
                            new Student { ID = 1007, Name = "santosh", Gender = "Male", Branch = "CSE", Age = 22  },
                            new Student { ID = 1008, Name = "Tina", Gender = "Female", Branch = "CSE", Age = 20  },
                            new Student { ID = 1009, Name = "Celina", Gender = "Female", Branch = "ETC", Age = 22 },
                            new Student { ID = 1010, Name = "Sambit", Gender = "Male",Branch = "ETC", Age = 21 }
                        };
            }
        }

        public void test()
        {
            //Grouping the Students Based on Branch using ToLookup Method

            //Using Method Syntax
            ILookup<string, Student> GroupByMS = Student.GetStudents().ToLookup(s => s.Branch);

            //Using Query Syntax
            ILookup<string, Student> GroupByQS = (from std in Student.GetStudents() select std).ToLookup(x => x.Branch);

            //It will iterate through each group
            foreach (var group in GroupByMS)
            {
                Console.WriteLine(group.Key + " : " + group.Count());
                //Iterate through each student of a group
                foreach (var student in group)
                {
                    Console.WriteLine("  Name :" + student.Name + ", Age: " + student.Age + ", Gender :" + student.Gender);
                }
            }
            Console.Read();
        }
        public void test8()
        {
            // Example list of employees
            List<Employee1> employees = new List<Employee1>
            {
                new Employee1 { Name = "John Doe", Department = "IT" },
                new Employee1 { Name = "Jane Smith", Department = "HR" },
                new Employee1 { Name = "Jack White", Department = "IT" },
                new Employee1 { Name = "Sara Parker", Department = "Finance" },
                new Employee1 { Name = "Tom Brown", Department = "IT" }
            };
            // Creating a lookup to group employees by department
            var lookup = employees.ToLookup(emp => emp.Department);
            // Displaying the grouped employees
            foreach (IGrouping<string, Employee1> group in lookup)
            {

                IEnumerable<Employee1> test = lookup["IT"];

                Console.WriteLine($"Department: {group.Key}");
                foreach (Employee1 emp in group)
                {
                    Console.WriteLine($"\t{emp.Name}");
                }
            }
        }
    }
}
