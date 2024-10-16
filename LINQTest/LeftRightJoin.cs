using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTest
{
    /*
     * The RIGHT OUTER JOIN retrieves all the matching rows from both the data sources involved in the join and non-matching rows from the right-side data source. In this case, the non-matching data will take the default values. However, Right Outer Joins are not supported with LINQ. LINQ only supports left outer joins.

Exchange the data sources to perform the right outer join. In our previous examples, we used Employees as the Left Data Source and Addresses as the Right Data Source. In the example below, we are just changing the data sources. Now, we are making addresses the Left Data Source and Employees the Right Data Source.
     */
    internal class LeftRightJoin
    {
        public class Employee
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int AddressId { get; set; }
            public static List<Employee> GetAllEmployees()
            {
                return new List<Employee>()
            {
                new Employee { ID = 1, Name = "Preety", AddressId = 1},
                new Employee { ID = 2, Name = "Priyanka", AddressId =2},
                new Employee { ID = 3, Name = "Anurag", AddressId = 0},
                new Employee { ID = 4, Name = "Pranaya", AddressId = 0},
                new Employee { ID = 5, Name = "Hina", AddressId = 5},
                new Employee { ID = 6, Name = "Sambit", AddressId = 6}
            };
            }
        }

        class EmployeeResult
        {
            public Employee Employee { get; set; }
            public Address Address { get; set; }
        }

        public class Address
        {
            public int ID { get; set; }
            public string AddressLine { get; set; }
            public static List<Address> GetAddress()
            {
                return new List<Address>()
            {
                new Address { ID = 1, AddressLine = "AddressLine1"},
                new Address { ID = 2, AddressLine = "AddressLine2"},
                new Address { ID = 5, AddressLine = "AddressLine5"},
                new Address { ID = 6, AddressLine = "AddressLine6"},
            };
            }
        }

        public void MethodSyntax()
        {
            //Performing Left Outer Join using LINQ using Query Syntax
            //Left Data Source: Employees
            //Right Data Source: Addresses
            //Note: Left and Right Data Source Matters
            var QSOuterJoin = from emp in Employee.GetAllEmployees() //Left Data Source
                              join add in Address.GetAddress() //Right Data Source
                              on emp.AddressId equals add.ID //Inner Join Condition
                              into EmployeeAddressGroup //Performing LINQ Group Join
                              from address in EmployeeAddressGroup.DefaultIfEmpty() //Performing Left Outer Join

                                  //Projecting the Result to Named Type
                              select new EmployeeResult
                              {
                                  Employee = emp,
                                  Address = address
                              };
            //Accessing the Elements using For Each Loop
            foreach (var item in QSOuterJoin)
            {
                //Before Accessing the AddressLine, please check null else you will get Null Reference Exception
                Console.WriteLine($"Name : {item.Employee.Name}, Address : {item.Address?.AddressLine} ");
            }
            Console.ReadLine();
        }

        public void QuerySyntax()
        {
            //Performing Left Outer Join using LINQ using Method Syntax
            //Left Data Source: Employees
            //Right Data Source: Addresses
            //Note: Left and Right Data Source Matters
            var MSOuterJOIN = Employee.GetAllEmployees() //Left Data Source
                              .GroupJoin(//Performing Group join with Right Data Source
                                    Address.GetAddress(), //Right Data Source
                                    employee => employee.AddressId, //Outer Key Selector, i.e. Left Data Source Common Property
                                    address => address.ID, //Inner Key Selector, i.e. Right Data Source Common Property
                                    (employee, address) => new { employee, address } //Projecting the Result
                              )
                              .SelectMany(
                                    x => x.address.DefaultIfEmpty(), //Performing Left Outer Join 
                                    (employee, address) => new { employee, address } //Final Result Set
                               );
            //Accessing the Elements using For Each Loop
            foreach (var item in MSOuterJOIN)
            {
                Console.WriteLine($"Name : {item.employee.employee.Name}, Address : {item.address?.AddressLine} ");
            }
            Console.ReadLine();
        }
    }
}
