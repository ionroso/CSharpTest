using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TwoPointers
{
    internal class KDiffPairsInAnArray
    {
        public static void Test()
        {
            new Solution().FindPairs([3, 1, 4, 1, 5], 2);
        }

        public class Solution1
        {
            public int FindPairs(int[] nums, int k)
            {
                int result = 0;

                Dictionary<int, int> counter = new();
                
                foreach (int n in nums)
                {
                    counter.TryGetValue(n, out var currentCount);
                    counter[n] = currentCount + 1;

                    //counter.AddOrUpdate(n, 1, (id, count) => count + 1);

                }

                foreach (var entry in counter)
                {
                    int num = entry.Key;
                    int val = entry.Value;
                    if ((k > 0 && counter.ContainsKey(num + k)) || (k == 0 && val > 1))
                    {
                        result++;
                    }
                }
                return result;
            }
        }

        public class Solution
        {
            public int FindPairs(int[] nums, int k)
            {

                Array.Sort(nums);

                int left = 0, right = 1;
                int result = 0;

                while (left < nums.Length && right < nums.Length)
                {
                    if (left == right || nums[right] - nums[left] < k)
                    {
                        // List item 1 in the text
                        right++;
                    }
                    else if (nums[right] - nums[left] > k)
                    {
                        // List item 2 in the text
                        left++;
                    }
                    else
                    {
                        // List item 3 in the text
                        left++;
                        result++;
                        while (left < nums.Length && nums[left] == nums[left - 1])
                            left++;
                    }
                }
                return result;
            }
        }
    }
}
