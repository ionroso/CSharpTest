
namespace Sort
{
    using System.Collections;
    using System.Drawing;


    class GFG
    {

        // Main Method
        static public void Main()
        {

            // Creating a sortedlist
            // Using SortedList class
            SortedList my_slist1 = new SortedList();

            // Adding key/value pairs in 
            // SortedList using Add() method
            my_slist1.Add(1.02, "This");
            my_slist1.Add(1.07, "Is");
            my_slist1.Add(1.04, "SortedList");
            my_slist1.Add(1.01, "Tutorial");

            foreach (DictionaryEntry pair in my_slist1)
            {
                Console.WriteLine("{0} and {1}",
                        pair.Key, pair.Value);
            }
            Console.WriteLine();

            // Creating another SortedList
            // using Object Initializer Syntax
            // to initialize sortedlist
            SortedList my_slist2 = new SortedList() {
                                { "b.09", 234 },
                                { "b.11", 395 },
                                { "b.01", 405 },
                                { "b.67", 100 }};

            foreach (DictionaryEntry pair in my_slist2)
            {
                Console.WriteLine("{0} and {1}",
                        pair.Key, pair.Value);
            }
        }
    }

internal class SortArrayByIncreasingFrequency
    {
        public void Test()
        {
            int[] output = new SortArrayByIncreasingFrequency()
                .FrequencySort1(new int[] { 1, 1, 2, 2, 3 });

            for (int j = 0; j < output.Length; j++)
            {
                Console.Write(output[j] + " ");
            }
        }

        public int[] FrequencySort1(int[] nums)
        {
            Dictionary<int, int> freq = new Dictionary<int, int>();

            foreach (int num in nums)
            {
                if (!freq.ContainsKey(num))
                {
                    freq[num] = 0;
                }

                freq[num]++;
            }

            Array.Sort(
                nums,
                Comparer<int>.Create((a, b) =>
                {
                    if (freq[a] == freq[b])
                    {
                        return b.CompareTo(a);   // same frequency -> larger number first
                    }

                    return freq[a].CompareTo(freq[b]); // smaller frequency first
                }));



            var freq1 = nums
            .GroupBy(x => x)
            .ToDictionary(g => g.Key, g => g.Count());

            nums.OrderBy(x => freq[x]).ThenBy(x => x);

            return nums;
        }
    }


}
