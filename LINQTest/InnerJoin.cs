﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTest
{
    internal class InnerJoin
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
                new Employee { ID = 1, Name = "Preety", AddressId = 1 },
                new Employee { ID = 2, Name = "Priyanka", AddressId = 2 },
                new Employee { ID = 3, Name = "Anurag", AddressId = 3 },
                new Employee { ID = 4, Name = "Pranaya", AddressId = 4 },
                new Employee { ID = 5, Name = "Hina", AddressId = 5 },
                new Employee { ID = 6, Name = "Sambit", AddressId = 6 },
                new Employee { ID = 7, Name = "Happy", AddressId = 7},
                new Employee { ID = 8, Name = "Tarun", AddressId = 8 },
                new Employee { ID = 9, Name = "Santosh", AddressId = 9 },
                new Employee { ID = 10, Name = "Raja", AddressId = 10},
                new Employee { ID = 11, Name = "Sudhanshu", AddressId = 11}
            };
            }
        }

        public class Address
        {
            public int ID { get; set; }
            public string AddressLine { get; set; }
            public static List<Address> GetAllAddresses()
            {
                return new List<Address>()
            {
                new Address { ID = 1, AddressLine = "AddressLine1"},
                new Address { ID = 2, AddressLine = "AddressLine2"},
                new Address { ID = 3, AddressLine = "AddressLine3"},
                new Address { ID = 4, AddressLine = "AddressLine4"},
                new Address { ID = 5, AddressLine = "AddressLine5"},
                new Address { ID = 9, AddressLine = "AddressLine9"},
                new Address { ID = 10, AddressLine = "AddressLine10"},
                new Address { ID = 11, AddressLine = "AddressLine11"},
            };
            }
        }

        class EmployeeAddress
        {
            public string EmployeeName { get; set; }
            public string AddressLine { get; set; }
        }

        public void MethodSyntax()
        {
            //Performing Inner Between Employees and Addresses Data Sources
            var JoinUsingMS = Address.GetAllAddresses()  //Outer Data Source
                           .Join( //Performing LINQ Inner Join
                           Employee.GetAllEmployees(),  //Inner Data Source
                           address => address.ID,   //Outer Key Selector
                           employee => employee.AddressId, //Inner Key selector
                           (address, employee) => new EmployeeAddress //Projecting the data to named type i.e. EmployeeAddress
                           {
                               EmployeeName = employee.Name,
                               AddressLine = address.AddressLine
                           }).ToList();
            //Accessing the Result using For Each Loop
            foreach (var employee in JoinUsingMS)
            {
                Console.WriteLine($"Name :{employee.EmployeeName}, Address : {employee.AddressLine}");
            }
            Console.ReadLine();
        }

        public void MethodSyntax1()
        {
            //Performing Inner Join Between Employees and Addresses Collections
            var optimizedJoin = Employee.GetAllEmployees().Join(Address.GetAllAddresses(),
                                      employee => employee.AddressId,
                                      address => address.ID,
                                      (employee, address) => new { employee.ID, employee.Name, address.AddressLine });
            //Accessing the Elements using Foreach Loop
            foreach (var empAddress in optimizedJoin)
            {
                Console.WriteLine($"Id: {empAddress.ID}, Name :{empAddress.Name}, Address : {empAddress.AddressLine}");
            }
            Console.ReadLine();
        }

        public void QuerySyntax()
        {
            //Performing Inner Join Between Employees and Addresses Collections
            var JoinUsingQS = (from emp in Employee.GetAllEmployees() //Outer Data Source
                               join address in Address.GetAllAddresses() //Joining with Inner Data Source
                               on emp.AddressId equals address.ID //Joining Condition
                               select new //Projecting the Result to an Anonymous Type
                               {
                                   EmployeeName = emp.Name,
                                   AddressLine = address.AddressLine
                               });
            //Accessing the Elements using Foreach Loop
            foreach (var employee in JoinUsingQS)
            {
                Console.WriteLine($"Name :{employee.EmployeeName}, Address : {employee.AddressLine}");
            }
            Console.ReadLine();
        }
    }
}
