using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LINQTest.Select;

namespace LINQTest
{
    /*
     Eliminating Duplicates: Union automatically removes duplicate elements. This differs from the Concat method, which concatenates two sequences without removing duplicates.
Equality Comparison: By default, this is based on the default equality comparer for the type of elements in the sequences. For example, for integers, it uses integer equality. You can provide a custom equality comparer if necessary.
Deferred Execution: The method uses deferred execution. The query is not executed until you iterate over the result.
Null Values: If either of the collections is null, Union will throw an ArgumentNullException.
Resulting Sequence: The result is a new sequence containing distinct elements from both input sequences.
Performance Considerations: The Union method internally uses a Set<T> to keep track of elements that have already been added. This helps in efficiently determining duplicates. The performance can depend on the size of the input sequences and the complexity of the equality comparison.
     */
    internal class UnionTest
    {

        public class Student
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public override bool Equals(object? obj)
            {
                //As the obj parameter type is object, so we need to
                //cast it to Student Type
                return this.ID == ((Student)obj).ID && this.Name == ((Student)obj).Name;
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
            public bool Equals(Student? x, Student? y)
            {
                return x?.ID == y?.ID && x?.Name == y?.Name;
            }
            public int GetHashCode(Student obj)
            {
                return obj.ID.GetHashCode() ^ obj.Name.GetHashCode();
            }
        }

        public class Student1 : IEquatable<Student>
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public bool Equals(Student? other)
            {
                return this.ID.Equals(other?.ID) && this.Name.Equals(other.Name);
            }
            public override int GetHashCode()
            {
                int IDHashCode = this.ID.GetHashCode();
                int NameHashCode = this.Name == null ? 0 : this.Name.GetHashCode();
                return IDHashCode ^ NameHashCode;
            }
        }

        public void Union5()
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
            var MS = StudentCollection1.Union(StudentCollection2).ToList();
            //Query Syntax
            var QS = (from std in StudentCollection1
                      select std).Union(StudentCollection2).ToList();
            foreach (var student in MS)
            {
                Console.WriteLine($" ID : {student.ID} Name : {student.Name}");
            }
        }
        public void Union4()
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
                     .Union(StudentCollection2, studentComparer).ToList();
            //Query Syntax
            var QS = (from std in StudentCollection1
                      select std)
                      .Union(StudentCollection2, studentComparer).ToList();
            foreach (var student in MS)
            {
                Console.WriteLine($" ID : {student.ID} Name : {student.Name}");
            }
        }

        public void Union3()
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
                     .Union(StudentCollection2.Select(y => y.Name)).ToList();
            //Query Syntax
            var QS = (from std in StudentCollection1
                      select std.Name)
                      .Union(StudentCollection2.Select(y => y.Name)).ToList();
            foreach (var name in MS)
            {
                Console.WriteLine(name);
            }
        }

        public void Union2()
        {
            string[] dataSource1 = { "India", "USA", "UK", "Canada", "Srilanka" };
            string[] dataSource2 = { "India", "uk", "Canada", "France", "Japan" };
            //Method Syntax
            var MS = dataSource1.Union(dataSource2, StringComparer.OrdinalIgnoreCase).ToList();
            //Query Syntax
            var QS = (from country in dataSource1
                      select country)
                      .Union(dataSource2, StringComparer.OrdinalIgnoreCase).ToList();
            foreach (var item in MS)
            {
                Console.WriteLine(item);
            }

        }
        public void Union1()
        {
            string[] dataSource1 = { "India", "USA", "UK", "Canada", "Srilanka" };
            string[] dataSource2 = { "India", "uk", "Canada", "France", "Japan" };
            //Method Syntax
            var MS = dataSource1.Union(dataSource2).ToList();
            //Query Syntax
            var QS = (from country in dataSource1
                      select country)
                      .Union(dataSource2).ToList();
            foreach (var item in MS)
            {
                Console.WriteLine(item);
            }
        }

        public void Union()
        {
            List<int> dataSource1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            List<int> dataSource2 = new List<int>() { 1, 3, 5, 8, 9, 10 };
            //Method Syntax
            var MS = dataSource1.Union(dataSource2).ToList();
            //Query Syntax
            var QS = (from num in dataSource1
                      select num)
                      .Union(dataSource2).ToList();
            foreach (var item in MS)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }
}
