using Interview;
using System.Numerics;

namespace Interview
{
    public class Program
    {
        private static void Main(string[] args)
        {
            new Microsoft_TelemetyAggregator_brute_force().Test();
            //BS1();
        }


        static void BS()
        {
            // Create and sort the list
            List<int> numbers = new List<int> { 0, 2, 3, 5, 6, 7, 8 };
            numbers.Sort();
            Console.WriteLine(string.Join(" ", numbers));


            // Element to search for
            int target = 12;

            // Perform binary search
            int index = numbers.BinarySearch(target);

            // Check the result
            if (index >= 0)
            {
                Console.WriteLine($"Element {target} found at index {index}.");
            }
            else
            {
                Console.WriteLine($"Element {target} not found. Nearest index: {~index}.");
                numbers.Insert(~index, target);

            }

            Console.WriteLine(string.Join(" ", numbers));

        }
        static void BS1()
        {
            List<int> numbers = new List<int> { 0, 2, 3, 5, 6, 7, 8 };
            int target = 5;
            (bool found, int nearestSmaller, int nearestBigger) pair = FindNearestSmaller(numbers, target);
            Console.WriteLine($"{pair}");
        }

        static (bool, int, int) FindNearestSmaller(List<int> sortedList, int target)
        {
            int left = 0;
            int right = sortedList.Count - 1;
            int nearestSmaller = int.MinValue;
            int nearestBigger = int.MaxValue;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (sortedList[mid] < target)
                {
                    nearestSmaller = mid;
                    left = mid + 1;
                }
                else
                {
                    nearestBigger = mid;
                    right = mid - 1;
                }
            }

            return (sortedList[nearestBigger] == target, nearestSmaller, nearestBigger);
        }
    }
}
