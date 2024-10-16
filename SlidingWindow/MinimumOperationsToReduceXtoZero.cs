using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SlidingWindow
{
    internal class MinimumOperationsToReduceXtoZero
    {
        public static void Test()
        {
            Console.WriteLine(new Solution().MinOperations([1, 1, 4, 2, 3], 5));
        }

        class Solution
        {
            public int MinOperations(int[] nums, int x)
            {
                int n = nums.Length;
                int totalSum = 0;
                Dictionary<int, int> mymap = new();   //<sum,pos>
                for (int i = 0; i < n; ++i)
                {
                    totalSum += nums[i];
                    mymap[totalSum] = i;
                }

                mymap[0] = 0;

                int longest = 0;
                int restSum = totalSum - x;
                int val = 0;
                for (int i = 0; i < n; ++i)
                {
                    val += nums[i];
                    if (mymap.ContainsKey(val - restSum) && i != mymap[val - restSum])
                    {
                          longest = Math.Max(longest, i - mymap[val - restSum]);
                    }
                }
                return longest == 0 ? (restSum == 0 ? n : -1) : n - longest;
            }
        }

        class Solution1
        {
            public int MinOperations(int[] nums, int x)
            {
                int n = nums.Length;


                int total = nums.Sum();
                int maxi = -1;
                int current = 0;


                for (int left = 0, right = 0; right < n; right++)
                {
                    // sum([left ,..., right]) = total - x
                    current += nums[right];
                    // if larger, move `left` to left
                    while (current > total - x && left <= right)
                    {
                        current -= nums[left];
                        left++;
                    }
                    // check if equal
                    if (current == total - x)
                    {
                        maxi = Math.Max(maxi, right + 1 - left);
                    }
                }

                return maxi != -1 ? n - maxi : -1;
            }
        }

    }
}
