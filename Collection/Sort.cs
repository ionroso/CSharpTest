using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection
{
    internal class Sort
    {
        // Main Method
        public static void Main()
        {

            // declaring and initializing the array
            int[] arr = new int[] { 1, 9, 6, 7, 5, 9 };

            // Sort the arr from last to first.
            // compare every element to each other
            Array.Sort<int>(arr, new Comparison<int>((i1, i2) => i2.CompareTo(i1)));

            // print all element of array
            foreach (int value in arr)
            {
                Console.Write(value + " ");
            }
        }

        public static void Main1()
        {

            // declaring and initializing the 
            // array with 6 positive number
            int[] arr = new int[] { 1, 9, 6, 7, 5, 9 };

            // Sort the arr in decreasing order
            // and return a array
            arr = arr.OrderByDescending(c => c).ToArray();

            // print all element of array
            foreach (int value in arr)
            {
                Console.Write(value + " ");
            }
        }


        public class Teacher
        {
            public required string First { get; init; }
            public required string Last { get; init; }
            public required int ID { get; init; }
            public required string City { get; init; }
        }

        public static void Main2()
        {
            List<Teacher> teachers = new List<Teacher>();
            IEnumerable<string> query = teachers
                .OrderByDescending(teacher => teacher.Last)
                .Select(teacher => teacher.Last);

            foreach (string str in query)
            {
                Console.WriteLine(str);
            }
        }

        public static void Main3()
        {
            // List creation
            List<string> Geek = new List<string>();

            // List elements
            Geek.Add("ABCD");
            Geek.Add("QRST");
            Geek.Add("XYZ");
            Geek.Add("IJKL");


            Console.WriteLine("The Original List is:");
            foreach (string g in Geek)
            {

                // prints original List
                Console.WriteLine(g);

            }

            Console.WriteLine("\nThe List in Sorted form");

            // sort the List
            Geek.Sort();
        }
    }
}
