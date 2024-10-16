using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class RotateArray
    {
        public void Test()
        {
            // 1,2
            // k = 0
            var solution = new Solution();
            int[] nums = [1, 2, 3, 4, 5, 6, 7];
            int k = 3;
            solution.Rotate(nums, k);
        }
        class Solution
        {
            // start with this
            public void Rotate(int[] nums, int k)
            {
                int n = nums.Length;
                k %= n;

                int[] copy = new int[n];

                for (int i = 0; i < n; i++)
                {
                    int newIndex = (i + k) % n;
                    copy[newIndex] = nums[i];
                }

                for (int i = 0; i < n; i++)
                    nums[i] = copy[i];
            }

            // then propose
            public void RotateNext(int[] nums, int k)
            {
                int n = nums.Length;
                k %= n;

                int moved = 0;

                for (int start = 0; moved < n; start++)
                {
                    int currentIndex = start;
                    int currentValue = nums[start];

                    do
                    {
                        int nextIndex = (currentIndex + k) % n;

                        int temp = nums[nextIndex];
                        nums[nextIndex] = currentValue;

                        currentValue = temp;
                        currentIndex = nextIndex;

                        moved++;

                    } while (currentIndex != start);
                }
            }
        }
    }  
}
