using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoPointers
{
    internal class SortColors
    {
        public static void Test()
        {
            new Solution().sortColors([1,2,0,0,1,1]);
        }

        class Solution
        {
            public void sortColors(int[] nums)
            {
                int left = 0, right = nums.Length - 1, curr = 0;

                while (curr <= right)
                {
                    if (nums[curr] == 0)
                    {
                        (nums[curr], nums[left]) = (nums[left], nums[curr]);
                        left++;
                        curr++;
                        continue;
                    }

                    if (nums[curr] == 2)
                    {
                        (nums[curr], nums[right]) = (nums[right], nums[curr]);
                        right--;
                        continue;
                    }

                    curr++;
                }
            }
        }

    }
}
