using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingWindow
{
    internal class distinctNumbersInEachSubarray
    {
        public class Solution
        {
            public int[] DistinctNumbers(int[] nums, int k)
            {
                int n = nums.Length;
                int[] rez = new int[n - k + 1];

                int index = 0;
                Dictionary<int, int> numCounts = new();
                for (int i = 0; i < n; i++)
                {
                    numCounts[nums[i]] = numCounts.GetValueOrDefault(nums[i], 0) + 1;

                    if (i < k)
                    {
                        rez[index] = numCounts.Count;
                        continue;
                    }

                    int numToRemove = nums[i - k];
                    int numToRemoveCount = numCounts[numToRemove];
                    if (numToRemoveCount == 1)
                    {
                        numCounts.Remove(numToRemove);
                    }
                    else
                    {
                        numCounts[numToRemove] = numToRemoveCount - 1;
                    }
                    rez[++index] = numCounts.Count;
                }
                return rez;
            }
        }
    }
}
