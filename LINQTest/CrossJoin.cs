using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTest
{
    /*
    When combining two data sources (or you can say two collections) using Cross Join, each element in the first data source (i.e., first collection) will be mapped with each and every element in the second data source (i.e., second collection).
    So, in simple words, we can say that the cross-join produces the Cartesian Products of the collections or data sources involved in the join.
    This is also known as a Cartesian product, where if the first collection has m elements and the second has n elements, the result will have m * n elements.

     
     Generating Combinations: Cross joins are perfect for scenarios where you need to generate all possible combinations of elements from two collections. For example, pairing every color with every size when listing all product variants.
Data Analysis and Reporting: Cross-joins can be used to combine data from different dimensions. For instance, combining every sales region with every product analyzes potential market opportunities.
Scheduling and Planning: For scheduling applications, a Cross Join can help generate all possible combinations of times and events, like pairing every meeting room with every time slot to explore scheduling options.
Educational Purposes and Problem-Solving: Cross Joins can be used for educational purposes in programming and data science, helping to illustrate concepts of combinations and permutations or to solve complex problems requiring an exploration of all possible pairings.
Scenario Analysis: Cross Joins can assist in scenario analysis in business or research, where you might need to pair every potential investment option with different market conditions.
Matrix Operations: Cross Joins can simulate certain types of matrix or grid operations, where you need to pair elements of two arrays or lists in a grid-like fashion.
     
     */
    internal class CrossJoin
    {
        public class Student
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public static List<Student> GetAllStudents()
            {
                return new List<Student>()
            {
                new Student { ID = 1, Name = "Preety"},
                new Student { ID = 2, Name = "Priyanka"},
                new Student { ID = 3, Name = "Anurag"},
                new Student { ID = 4, Name = "Pranaya"},
                new Student { ID = 5, Name = "Hina"}
            };
            }
        }

        public class Subject
        {
            public int ID { get; set; }
            public string SubjectName { get; set; }
            public static List<Subject> GetAllSubjects()
            {
                return new List<Subject>()
            {
                new Subject { ID = 1, SubjectName = "ASP.NET"},
                new Subject { ID = 2, SubjectName = "SQL Server" },
                new Subject { ID = 5, SubjectName = "Linq"}
            };
            }
        }

        public void QuerySyntax()
        {
            //Cross Join using Query Syntax
            var CrossJoinResult = from student in Student.GetAllStudents() //First Data Source
                                  from subject in Subject.GetAllSubjects() //Cross Join with Second Data Source
                                  select new //Projecting the Result to Anonymous Type
                                  {
                                      StudentName = student.Name,
                                      SubjectName = subject.SubjectName
                                  };
            //Accessing the Elements using For Each Loop
            foreach (var item in CrossJoinResult)
            {
                Console.WriteLine($"Name : {item.StudentName}, Subject: {item.SubjectName}");
            }
            Console.ReadLine();
        }


        public void MethodSyntax()
        {
            //Cross Join using SelectMany Method
            var CrossJoinResult = Student.GetAllStudents()
                        .SelectMany(sub => Subject.GetAllSubjects(),
                         (std, sub) => new
                         {
                             StudentName = std.Name,
                             SubjectName = sub.SubjectName
                         });

            //Cross Join using Join Method
            var CrossJoinResult2 = Student.GetAllStudents()
                        .Join(Subject.GetAllSubjects(),
                            std => true,
                            sub => true,
                            (std, sub) => new
                            {
                                StudentName = std.Name,
                                SubjectName = sub.SubjectName
                            }
                         );

            foreach (var item in CrossJoinResult2)
            {
                Console.WriteLine($"Name : {item.StudentName}, Subject: {item.SubjectName}");
            }
            Console.ReadLine();
        }

    }
}
