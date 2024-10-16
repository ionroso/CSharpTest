using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTest
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

    internal class FullOuterJoin
    {
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
            var FullOuterJoin = LeftOuterJoin.Union(RightOuterJoin);
            foreach (var emp in FullOuterJoin)
            {
                Console.WriteLine($"EmployeeId: {emp.EmployeeId}, Name: {emp.EmployeeName}, Department: {emp.DepartmentName}");
            }
            Console.ReadLine();
        }
    }
}
