using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearch
{
    internal class FindFirstAndLastPositionOfElementInSortedArray
    {
        public void Test()
        {
            //int[] rez = SearchRange([5, 7, 7, 8, 8, 10], 8);
            //int[] rez = SearchRange([1], 1);
            int[] rez = SearchRange([1, 4], 4);

            Console.WriteLine(rez[0] + ' ' + rez[1]);
        }

        public int[] SearchRange(int[] nums, int target)
        {
            int index = BinarySearch(nums, target);
            if(index == -1)
            {
                return new int[] { -1, -1 };
            }

            return new int[] { BinarySearchLeft(nums, index), BinarySearchRight(nums, index) };
        }

        public int BinarySearchLeft(int[] nums, int index)
        {
            int n = nums.Length;
            int l = 0, r = index, target = nums[index];

            if (l == r)
            {
                return 0;
            }


            while (l < r)
            {
                var mid = l + (r - l) / 2;
                if (nums[mid] == target)
                {
                    r = mid;
                }
                else
                {
                    l = mid;
                }

                if(l+1 == r)
                {
                    break;
                }
            }

            return nums[l]==target ? l : r;
        }

        public int BinarySearchRight(int[] nums, int index) // 5, 7, 7, 8, 8, 10
        {
            int n = nums.Length;
            int l = index, r = n-1, target = nums[index];


            if (l == r)
            {
                return index;
            }

            while (l < r)
            {
                var mid = l + (r - l) / 2;
                if (nums[mid] == target)
                {
                    l = mid;
                }
                else
                {
                    r = mid;
                }

                if (l + 1 == r)
                {
                    break;
                }
            }

            return nums[r] == target ? r : l;
        }

        public int BinarySearch(int[] nums, int target)
        {
            int n = nums.Length;
            int l = 0, r = n - 1;

            if(n == 1)
            {
                if (nums[0] == target) return 0;

                return -1;
            }

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

            return -1;
        }
    }
}
