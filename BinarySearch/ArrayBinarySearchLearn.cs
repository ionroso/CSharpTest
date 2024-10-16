using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearch
{
    internal class ArrayBinarySearchLearn
    {
        public void Test()
        {
            // Sorted array
            int[] numbers = { 1, 3, 5, 7, 9, 11, 13 };

            // Value to search for
            int target = 7;

            // Perform binary search
            int index = Array.BinarySearch(numbers, target);

            if (index >= 0)
            {
                Console.WriteLine($"Element {target} found at index {index}.");
            }
            else
            {
                Console.WriteLine($"Element {target} not found. Closest index: {~index}.");
            }
        }
    }
}
