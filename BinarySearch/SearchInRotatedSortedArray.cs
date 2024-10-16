using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearch
{
    internal class SearchInRotatedSortedArray
    {
        public void Test()
        {
            Console.WriteLine(new Solution().Search([4, 5, 6, 7, 0, 1, 2], target: 0));
            //Console.WriteLine(Search([0, 1, 2, 3, 4], target: 1));
            //Console.WriteLine(Search([4, 0, 1, 2, 3], target: 1));
            //Console.WriteLine(Search([3, 4, 0, 1, 2], target: 1));
            //Console.WriteLine(Search([2, 3, 4, 0, 1], target: 1));
            //Console.WriteLine(Search([1, 2, 3, 4, 0], target: 1));
        }

        public class Solution
        {
            public int Search(int[] nums, int target)
            {
                int n = nums.Length;
                int left = 0, right = n - 1;
                // Find the index of the pivot element (the smallest element)
                while (left <= right)
                {
                    int mid = (left + right) / 2;
                    if (nums[mid] > nums[n - 1])
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }

                int pivot = left;
                // Binary search over elements on the pivot element's left
                int answer = BinarySearch(nums, 0, pivot - 1, target);
                if (answer != -1)
                {
                    return answer;
                }

                // Binary search over elements on the pivot element's right
                return BinarySearch(nums, pivot, n - 1, target);
            }

            // Binary search over an inclusive range [left_boundary ~ right_boundary]
            private int BinarySearch(int[] nums, int left_boundary, int right_boundary,
                                     int target)
            {
                int left = left_boundary, right = right_boundary;
                while (left <= right)
                {
                    int mid = (left + right) / 2;
                    if (nums[mid] == target)
                    {
                        return mid;
                    }
                    else if (nums[mid] > target)
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }

                return -1;
            }
        }
    }
}
