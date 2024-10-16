using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTest
{
    /*
    Full Outer Join in SQL
A Full Outer Join in SQL is a type of join that returns all rows from both tables involved in the join, with matching rows from both sides where available. 
    If there is no match, the result set will include NULL on the side of the join where the match is missing. This type of join is useful
    when you want to find all records from two tables and see how they relate to each other, including records that don’t have a corresponding match in the other table.
    The syntax for a Full Outer Join in SQL is as follows:


    Here’s a breakdown of the syntax:

SELECT columns: This part specifies the columns that you want to retrieve. You can select columns from either table involved in the join or from both. You can also use * to select all columns.
FROM table1: This specifies the first table in the join.
FULL OUTER JOIN table2: This clause performs the Full Outer Join on the second table. You specify the second table immediately after the FULL OUTER JOIN keyword.
ON table1.column_name = table2.column_name: The ON clause specifies the condition for the join, typically a match between columns from both tables.
     */
    internal class OuterJoin
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
                new Employee { ID = 3, Name = "Anurag", DepartmentId = 0},
                new Employee { ID = 4, Name = "Pranaya", DepartmentId = 0},
                new Employee { ID = 5, Name = "Hina", DepartmentId = 10},
                new Employee { ID = 6, Name = "Sambit", DepartmentId = 30},
                new Employee { ID = 7, Name = "Mahesh", DepartmentId = 30}
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
                new Department { ID = 10, Name = "IT"       },
                new Department { ID = 20, Name = "HR"       },
                new Department { ID = 30, Name = "Payroll"  },
                new Department { ID = 40, Name = "Admin"    },
                new Department { ID = 40, Name = "Sales"    }
            };
            }
        }

        public void MethodSyntax()
        {
            //Performing Left Outer Join using LINQ using Method Syntax
            var MSLeftOuterJOIN = Employee.GetAllEmployees() //Left Data Source
                              .GroupJoin( //Performing Group join with Right Data Source
                                    Department.GetAllDepartments(), //Right Data Source
                                    employee => employee.DepartmentId, //Outer Key Selector, i.e. Left Data Source Common Property
                                    department => department.ID, //Inner Key Selector, i.e. Right Data Source Common Property
                                    (employee, department) => new { employee, department } //Projecting the Result
                              )
                              .SelectMany(
                                    x => x.department.DefaultIfEmpty(), //Performing Left Outer Join 
                                                                        //Final Result Set
                                    (employee, department) => new
                                    {
                                        EmployeeId = employee?.employee?.ID,
                                        EmployeeName = employee?.employee?.Name,
                                        DepartmentName = department?.Name
                                    }
                               );
            //Performing Right Outer Join using LINQ using Method Syntax
            var MSRightOuterJOIN = Department.GetAllDepartments() //Left Data Source
                              .GroupJoin( //Performing Group join with Right Data Source
                                    Employee.GetAllEmployees(), //Right Data Source
                                    department => department.ID, //Outer Key Selector, i.e. Left Data Source Common Property
                                    employee => employee.DepartmentId, //Inner Key Selector, i.e. Right Data Source Common Property
                                    (department, employee) => new { department, employee } //Projecting the Result
                              )
                              .SelectMany(
                                    x => x.employee.DefaultIfEmpty(), //Performing Left Outer Join 
                                                                      //Final Result Set
                                    (department, employee) => new
                                    {
                                        EmployeeId = employee?.ID,
                                        EmployeeName = employee?.Name,
                                        DepartmentName = department?.department?.Name
                                    }
                               );
            var FullOuterJoin = MSLeftOuterJOIN.Union(MSRightOuterJOIN);
            //Accessing the Elements using For Each Loop
            foreach (var emp in FullOuterJoin)
            {
                Console.WriteLine($"EmployeeId: {emp.EmployeeId}, Name: {emp.EmployeeName}, Department: {emp.DepartmentName}");
            }
        }

        public void QuerySyntax()
        {
            //Full Outer Join = Left Outer Join UNION Right Outer Join
            //Performinng Left Outer Join
            var LeftOuterJoin = from emp in Employee.GetAllEmployees()
                                join dept in Department.GetAllDepartments()
                                on emp.DepartmentId equals dept.ID into EmployeeDepartmentGroup
                                from department in EmployeeDepartmentGroup.DefaultIfEmpty()
                                select new
                                {
                                    //To Avoid Runtime Null Reference Exception, check NULL 
                                    EmployeeId = emp?.ID,
                                    EmployeeName = emp?.Name,
                                    DepartmentName = department?.Name
                                };
            var RightOuterJoin = from dept in Department.GetAllDepartments()
                                 join emp in Employee.GetAllEmployees()
                                 on dept.ID equals emp.DepartmentId into EmployeeDepartmentGroup
                                 from employee in EmployeeDepartmentGroup.DefaultIfEmpty()
                                 select new
                                 {
                                     //To Avoid Runtime Null Reference Exception, check NULL 
                                     EmployeeId = employee?.ID,
                                     EmployeeName = employee?.Name,
                                     DepartmentName = dept?.Name
                                 };


            foreach (var emp in LeftOuterJoin)
            {
                Console.WriteLine($"EmployeeId: {emp.EmployeeId}, Name: {emp.EmployeeName}, Department: {emp.DepartmentName}");
            }

            Console.WriteLine();
            Console.WriteLine();
            
            foreach (var emp in RightOuterJoin)
            {
                Console.WriteLine($"EmployeeId: {emp.EmployeeId}, Name: {emp.EmployeeName}, Department: {emp.DepartmentName}");
            }

            Console.WriteLine();
            Console.WriteLine();


            var FullOuterJoin = LeftOuterJoin.Union(RightOuterJoin);
            foreach (var emp in FullOuterJoin)
            {
                Console.WriteLine($"EmployeeId: {emp.EmployeeId}, Name: {emp.EmployeeName}, Department: {emp.DepartmentName}");
            }
            Console.ReadLine();
        }
    }
}
