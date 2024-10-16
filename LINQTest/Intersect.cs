using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTest
{
    internal class Intersect
    {
        public class Student2 : IEquatable<Student2>
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public bool Equals(Student2 other)
            {
                return this.ID.Equals(other.ID) && this.Name.Equals(other.Name);
            }
            public override int GetHashCode()
            {
                int IDHashCode = this.ID.GetHashCode();
                int NameHashCode = this.Name == null ? 0 : this.Name.GetHashCode();
                return IDHashCode ^ NameHashCode;
            }
        }

        public class Student1
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public override bool Equals(object obj)
            {
                //As the obj parameter type is object, so we need to
                //cast it to Student Type
                return this.ID == ((Student1)obj).ID && this.Name == ((Student1)obj).Name;
            }
            public override int GetHashCode()
            {
                //Get the ID hash code value
                int IDHashCode = this.ID.GetHashCode();
                //Get the string HashCode Value
                //Check for null refernece exception
                int NameHashCode = this.Name == null ? 0 : this.Name.GetHashCode();
                return IDHashCode ^ NameHashCode;
            }
        }


        public class StudentComparer : IEqualityComparer<Student>
        {
            public bool Equals(Student x, Student y)
            {
                return x.ID == y.ID && x.Name == y.Name;
            }
            public int GetHashCode(Student obj)
            {
                return obj.ID.GetHashCode() ^ obj.Name.GetHashCode();
            }
        }



        public class Student
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public void MethodSyntax()
        {
            string[] dataSource1 = { "India", "USA", "UK", "Canada", "Srilanka" };
            string[] dataSource2 = { "India", "uk", "Canada", "France", "Japan" };
            //Method Syntax
            var MS = dataSource1.Intersect(dataSource2, StringComparer.OrdinalIgnoreCase).ToList();

            foreach (var item in MS)
            {
                Console.WriteLine(item);
            }

        }

        public void QuerySyntax()
        {
            List<int> dataSource1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            List<int> dataSource2 = new List<int>() { 1, 3, 5, 8, 9, 10 };
            //Method Syntax
            var MS = dataSource1.Intersect(dataSource2).ToList();
            //Query Syntax
            var QS = (from num in dataSource1
                      select num)
                      .Intersect(dataSource2).ToList();
            foreach (var item in MS)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }

        public void test()
        {
            List<Student> StudentCollection1 = new List<Student>()
            {
                new Student {ID = 101, Name = "Preety" },
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 105, Name = "Hina"},
                new Student {ID = 106, Name = "Anurag"},
            };
            List<Student> StudentCollection2 = new List<Student>()
            {
                new Student {ID = 105, Name = "Hina"},
                new Student {ID = 106, Name = "Anurag"},
                new Student {ID = 107, Name = "Pranaya"},
                new Student {ID = 108, Name = "Santosh"},
            };

            //Method Syntax
            var MS = StudentCollection1.Select(x => x.Name)
                     .Intersect(StudentCollection2.Select(y => y.Name)).ToList();
            //Query Syntax
            var QS = (from std in StudentCollection1
                      select std.Name)
                      .Intersect(StudentCollection2.Select(y => y.Name)).ToList();
            foreach (var name in MS)
            {
                Console.WriteLine(name);
            }
        }

        public void test1()
        {
            List<Student> StudentCollection1 = new List<Student>()
            {
                new Student {ID = 101, Name = "Preety" },
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 105, Name = "Hina"},
                new Student {ID = 106, Name = "Anurag"},
            };
            List<Student> StudentCollection2 = new List<Student>()
            {
                new Student {ID = 105, Name = "Hina"},
                new Student {ID = 106, Name = "Anurag"},
                new Student {ID = 107, Name = "Pranaya"},
                new Student {ID = 108, Name = "Santosh"},
            };
            StudentComparer studentComparer = new StudentComparer();
            //Method Syntax
            var MS = StudentCollection1
                     .Intersect(StudentCollection2, studentComparer).ToList();
            //Query Syntax
            var QS = (from std in StudentCollection1
                      select std)
                      .Intersect(StudentCollection2, studentComparer).ToList();
            foreach (var student in QS)
            {
                Console.WriteLine($" ID : {student.ID} Name : {student.Name}");
            }

        }

        void test2()
        {
            List<Student1> StudentCollection1 = new List<Student1>()
            {
                new Student1 {ID = 101, Name = "Preety" },
                new Student1 {ID = 102, Name = "Sambit" },
                new Student1 {ID = 105, Name = "Hina"},
                new Student1 {ID = 106, Name = "Anurag"},
            };
            List<Student1> StudentCollection2 = new List<Student1>()
            {
                new Student1 {ID = 105, Name = "Hina"},
                new Student1 {ID = 106, Name = "Anurag"},
                new Student1 {ID = 107, Name = "Pranaya"},
                new Student1 {ID = 108, Name = "Santosh"},
            };
            //Method Syntax
            var MS = StudentCollection1.Intersect(StudentCollection2).ToList();
            //Query Syntax
            var QS = (from std in StudentCollection1
                      select std).Intersect(StudentCollection2).ToList();
            foreach (var student in MS)
            {
                Console.WriteLine($" ID : {student.ID} Name : {student.Name}");
            }

            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            List<Student2> StudentCollection1 = new List<Student2>()
            {
                new Student2 {ID = 101, Name = "Preety" },
                new Student2 {ID = 102, Name = "Sambit" },
                new Student2 {ID = 105, Name = "Hina"},
                new Student2 {ID = 106, Name = "Anurag"},
            };
            List<Student2> StudentCollection2 = new List<Student2>()
            {
                new Student2 {ID = 105, Name = "Hina"},
                new Student2 {ID = 106, Name = "Anurag"},
                new Student2 {ID = 107, Name = "Pranaya"},
                new Student2 {ID = 108, Name = "Santosh"},
            };
            //Method Syntax
            var MS = StudentCollection1.Intersect(StudentCollection2).ToList();
            //Query Syntax
            var QS = (from std in StudentCollection1
                      select std).Intersect(StudentCollection2).ToList();
            foreach (var student in MS)
            {
                Console.WriteLine($" ID : {student.ID} Name : {student.Name}");
            }

            Console.ReadKey();
        }

    }
}
