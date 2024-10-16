using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearch
{
    internal class SearchInsertPosition
    {
        public void Test()
        {
            Console.WriteLine(SearchInsert([1, 3, 5, 6], target: 7));
        }
        public int SearchInsert(int[] nums, int target)
        {
            int n = nums.Length;
            int l = 0, r = n - 1;

            while (l <= r)
            {
                var mid = l + (r - l) / 2;
                if (nums[mid] == target)
                {
                    return mid;
                }
                else if (nums[mid] > target)
                {
                    r = mid - 1;
                }
                else
                {
                    l = mid + 1;
                }
            }
            
            return l;
        }
    }
}
