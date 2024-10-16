namespace LeetCode.Medium
{

    class LongestConsecutiveSequence
    {
        public void Test()
        {
            //int[] nums = new int[] { -1,-1, 0, 1, 3, 4, 5, 6, 7, 8,9 };
            int[] nums = new int[]   { -8, -4, 9, 9, 4, 6, 1, -4, -1, 6, 8 };

            //Console.WriteLine(new Solution().LongestConsecutive(new int[] { 0, 3, 7, 2, 5, 8, 4, 6, 0, 1 }));

            //Console.WriteLine(new SolutionBinarySearch().BSDups(nums, nums.Length, 1));
            Console.WriteLine(new SolutionBinarySearch().LongestConsecutive(nums));
        }


        public class SolutionBinarySearch
        {
            public int LongestConsecutive(int[] nums)
            {
                int n = nums.Length;
                if (n == 0) return 0;
                if (n == 1) return 1;

                Array.Sort(nums);
                nums = nums.Distinct().ToArray();
                n = nums.Length;

                for (int i = 0; i < n; i++)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
                Console.WriteLine(string.Join(',', nums));

                int newCurrIdx = 0;
                int max = 1;
                while (newCurrIdx < n)
                {
                    int lastIdx = BS(nums, n, newCurrIdx);
                    int dist = lastIdx - newCurrIdx;
                    max = Math.Max(max, dist + 1);
                    newCurrIdx = lastIdx + 1;
                }

                return max;
            }

            public int BS(int[] nums, int n, int currIdx)
            {
                int l = currIdx;
                int r = n - 1;

                while (l <= r && l < n)
                {
                    int mid = l + (r - l) / 2;
                    if (nums[mid] <= nums[currIdx] + (mid - currIdx))
                    {
                        l = mid + 1;
                    } else
                    {
                        r = mid - 1;
                    }
                }

                return l-1;
            }
        }

        public class Solution
        {
            public int LongestConsecutive(int[] nums)
            {
                if (nums == null || nums.Length == 0) return 0;
                if (nums.Length == 1) return 1;

                Array.Sort(nums);
                Console.Write(string.Join(',', nums));
                
                int count = 1, max = 0;
                for (int i = 1; i < nums.Length; i++)
                {
                    if (nums[i] == nums[i - 1] + 1) continue;

                    if (nums[i] == nums[i - 1] + 1)
                    {
                        count++;
                    }
                    else
                    {
                        count = 1;
                    }

                    max = Math.Max(max, count);
                }

                return max;
            }
        }

    }
}
