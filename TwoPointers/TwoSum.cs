using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TwoPointers.ShortestWordDistanceII;

namespace TwoPointers
{
    internal class TwoSum
    {
        public static void Test()
        {
            new Solution().TwoSum([2, 7, 11, 15], 9);
        }

        class Solution
        {
            public int[] TwoSum(int[] nums, int target)
            {
                Dictionary<int, int> map = new();
                for (int i = 0; i < nums.Length; i++)
                {
                    int complement = target - nums[i];
                    if (map.ContainsKey(complement))
                    {
                        return new int[] { map[complement], i };
                    }
                    map[nums[i]]= i;
                }
                // Return an empty array if no solution is found
                return new int[] { };
            }
        }

    }
}
