using AutoMapper;
using System;
using System.Net;

internal class Program
{
    public class Address
    {
        public string Name { get; set; }
    }

    public class Employee
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public Address Address { get; set; }
        public string Department { get; set; }

        public string Prop1 { get; set; } = "Prop1";

        public TimeSpan Timespan { get; set; } = TimeSpan.Zero;
    }

    public class EmployeeDTO
    {
        public string Title { get; set; }
        public int Salary { get; set; }
        public Address Address { get; set; }
        public string Department { get; set; }
        public string Prop1 { get; set; }

        public string Timespan { get; set; }
    }
    public enum HttpStatusCodeMine
    {
        OK = 1, NOT_FOUND = 2, NOT_ACCEPTABLE = 3
    }

    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                //Configuring Employee and EmployeeDTO
                cfg.CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name + "1111"))
                .ForAllMembers(x => x.Ignore());

                cfg.CreateMap<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>();
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }


    static void Main(string[] args)
        {
        //Create and Populate Employee Object i.e. Source Object
        Employee emp = new Employee
        {
            Name = "James",
            Salary = 20000,
            Address = new Address() { Name = "London" },
            Department = "IT"
        };


        Employee emp2 = new Employee
        {
            Name = "James2",
            Salary = 20000,
            Address = new Address() { Name = "London" },
            Department = "IT"
        };

        List<Employee> list = new List<Employee>();
        list.Add(emp);
        list.Add(emp2);

        //Initializing AutoMapper
        var mapper = MapperConfig.InitializeAutomapper();
        //Way1: Specify the Destination Type and to The Map Method pass the Source Object
        //Now, empDTO1 object will having the same values as emp object
        var empDTO1 = mapper.Map<EmployeeDTO>(emp);
        Console.WriteLine("Name: " + empDTO1.Title + ", Salary: " + empDTO1.Salary + ", Address: " + empDTO1.Address + ", Department: " + empDTO1.Department + ", Timespan: " + empDTO1.Timespan);

        //Way2: Specify the both Source and Destination Type 
        //and to The Map Method pass the Source Object
        //Now, empDTO2 object will having the same values as emp object
        //var empDTO2 = mapper.Map<IEnumerable<Employee>>(list);
        //Console.WriteLine("Name: " + empDTO1.Title + ", Salary: " + empDTO1.Salary + ", Address: " + empDTO1.Address + ", Department: " + empDTO1.Department);
        //Console.ReadLine();

        HttpStatusCode httpStatusCodeMine = HttpStatusCode.OK;

        var empDTO2 = mapper.Map<EmployeeDTO>(httpStatusCodeMine);
        Console.WriteLine();
    }
}