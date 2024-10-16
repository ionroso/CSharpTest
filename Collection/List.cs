using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Collection
{
    public class ListMy
    {
        public int MyProperty { get; set; }
        public void Test()
        {
            //List<int> sortedList = new List<int> { 1, 3, 5, 7, 9 };
            //sortedList.Insert(1, 2);
            ////sortedList.Last();
            //Console.WriteLine(string.Join(" ",sortedList));

            SortedList<int, string> sl = new SortedList<int, string>();

            // Adding key-value pairs
            sl.Add(3, "Three");
            sl.Add(1, "One");
            sl.Add(2, "Two");

            Console.WriteLine($"Size: {sl.Count}");
            sl.Add(4, "Four");

            Console.WriteLine($"Size again: {sl.Count}");
            
            // Displaying elements in sorted by key
            foreach (var item in sl)
            {
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }

            for (var i = 0; i < sl.Count; i++)
            {
                Console.WriteLine($"Get at index {i}: {sl.GetKeyAtIndex(i)}");
            }    

        }
    }
}
