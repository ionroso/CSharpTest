using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTest
{
    internal class GroupJoin
    {
        public class Employee
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int DepartmentId { get; set; }
            public static List<Employee> GetAllEmployees()
            {
                return new List<Employee>()
            {
                new Employee { ID = 1, Name = "Preety", DepartmentId = 10},
                new Employee { ID = 2, Name = "Priyanka", DepartmentId =20},
                new Employee { ID = 3, Name = "Anurag", DepartmentId = 30},
                new Employee { ID = 4, Name = "Pranaya", DepartmentId = 30},
                new Employee { ID = 5, Name = "Hina", DepartmentId = 20},
                new Employee { ID = 6, Name = "Sambit", DepartmentId = 10},
                new Employee { ID = 7, Name = "Happy", DepartmentId = 10},
                new Employee { ID = 8, Name = "Tarun", DepartmentId = 0},
                new Employee { ID = 9, Name = "Santosh", DepartmentId = 10},
                new Employee { ID = 10, Name = "Raja", DepartmentId = 20},
                new Employee { ID = 11, Name = "Ramesh", DepartmentId = 30}
            };
            }
        }
        public class Department
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public static List<Department> GetAllDepartments()
            {
                return new List<Department>()
            {
                new Department { ID = 10, Name = "IT"},
                new Department { ID = 20, Name = "HR"},
                new Department { ID = 30, Name = "Sales"  },
            };
            }
        }

        public void MethodSyntax()
        {
            //Group Employees by Department using Method Syntax
            var GroupJoinMS = Department.GetAllDepartments(). //Outer Data Source i.e. Departments
                GroupJoin( //Performing Group Join with Inner Data Source
                    Employee.GetAllEmployees(), //Inner Data Source
                    dept => dept.ID, //Outer Key Selector  i.e. the Common Property
                    emp => emp.DepartmentId, //Inner Key Selector  i.e. the Common Property
                    (dept, emp) => new { dept, emp } //Projecting the Result to an Anonymous Type
                );
            //Printing the Result set
            //Outer Foreach is for Each department
            foreach (var item in GroupJoinMS)
            {
                Console.WriteLine("Department :" + item.dept.Name);
                //Inner Foreach loop for each employee of a Particular department
                foreach (var employee in item.emp)
                {
                    Console.WriteLine("  EmployeeID : " + employee.ID + " , Name : " + employee.Name);
                }
            }
            Console.ReadLine();
        }
    
        public void QuerySyntax()
        {
            //Group Employees by Department using Query Syntax
            var GroupJoinQS = from dept in Department.GetAllDepartments() //Outer Data Source i.e. Departments
                              join emp in Employee.GetAllEmployees() //Joining with Inner Data Source i.e. Employees
                              on dept.ID equals emp.DepartmentId //Joining Condition
                              into EmployeeGroups //Projecting the Joining Result into EmployeeGroups
                              //Final Result include each department and the corresponding employees
                              select new { dept, EmployeeGroups };
            //Printing the Result set
            //Outer Foreach is for Each department
            foreach (var item in GroupJoinQS)
            {
                Console.WriteLine("Department :" + item.dept.Name);
                //Inner Foreach loop for each employee of a Particular department
                foreach (var employee in item.EmployeeGroups)
                {
                    Console.WriteLine("  EmployeeID : " + employee.ID + " , Name : " + employee.Name);
                }
            }
            
            Console.ReadLine();
        }    }
}
