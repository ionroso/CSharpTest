
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriorityQueue;

namespace PriorityQueueLearn
{
    public class Program
    {
        public class HospitalQueueComparer : IComparer<Patient>
        {
            public int Compare(Patient x, Patient y)
            {
                Console.WriteLine($"Comparing {x.Name} and {y.Name}");
                Console.WriteLine();

                if (x.Age == y.Age)
                    return 0;
                else if (x.Age > y.Age)
                    return -1;
                else
                    return 1;
            }
        }

        public class Patient
        {
            public string Name { get; set; } = string.Empty;
            public int Age { get; set; }

            public Patient(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }

        static void Main(string[] args)
        {
            //test();
            //test1();
            //test2();

            new FindMedianFromDataStream().Test();
        }

        private static void test2()
        {
            //Descending Sort, Integer
            var queue = new PriorityQueue<int, int>(Comparer<int>.Create((x, y) => y - x));

            //queue.Enqueue(2, 2);

            ////Ascending Sort, Object
            //var queue = new PriorityQueue<ObjectA, ObjectB>(Comparer<ObjectB>.Create((x, y) => x.Something.CompareTo(y.Something));

        }
        private static void test1()
        {
            var patients = new List<Patient>()
            {
                new("Sarah", 23),
                new("Joe", 50),
                new("Elizabeth", 60),
                new("Natalie", 16),
                new("Angie", 25),
            };

            var hospitalQueue = new PriorityQueue<Patient, Patient>(new HospitalQueueComparer());
            patients.ForEach(p => hospitalQueue.Enqueue(p, p));

        }
        private static void test()
        {
            var patients = new List<(Patient, int)>()
            {
                (new("Sarah", 23), 4),
                (new("Joe", 50), 2),
                (new("Elizabeth", 60), 1),
                (new("Natalie", 16), 5),
                (new("Angie", 25), 3)
            };
            var hospitalQueue = new PriorityQueue<Patient, int>(patients);
            Assert.AreEqual(5, hospitalQueue.EnsureCapacity(0));
            Assert.AreEqual(5, hospitalQueue.Count);

            var highestPriorityPatient = hospitalQueue.Dequeue();
            Assert.AreEqual(highestPriorityPatient.Age, 60);
            Assert.AreEqual(highestPriorityPatient.Name, "Elizabeth");

            var secondHighestPriorityPatient = hospitalQueue.Peek();
            Assert.AreEqual(secondHighestPriorityPatient.Age, 50);
            Assert.AreEqual(secondHighestPriorityPatient.Name, "Joe");
        }
    }
}