using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearch
{
    internal class FindMinimumInRotatedSortedArray
    {
        public void Test()
        {
            Console.WriteLine(FindMin([0, 1, 2, 3, 4]));
            Console.WriteLine(FindMin([4, 0, 1, 2, 3]));
            Console.WriteLine(FindMin([3, 4, 0, 1, 2]));
            Console.WriteLine(FindMin([2, 3, 4, 0, 1]));
            Console.WriteLine(FindMin([1, 2, 3, 4, 0]));
        }

        private int FindMin(int[] nums)
        {
            int n = nums.Length;
            int l = 0, r = n - 1;

            if (nums[l] < nums[r])
            {
                return nums[0];
            }

            while (l < r)
            {
                int mid = l + (r - l) / 2;

                if (nums[l] > nums[r] && nums[mid] < nums[r])
                {
                    r = mid;
                }
                else if (nums[l] > nums[r] && nums[mid] > nums[l])
                {
                    l = mid;
                }

                if (l + 1 == r) break;
            }

            var minIdx = nums[r] < nums[l] ? r : l;
            return nums[minIdx];
        }
    }
}
