using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intervals
{
    internal class InsertInterval
    {
        public void Test()
        {

        }

        class Solution
        {
            public int[][] Insert(int[][] intervals, int[] newInterval)
            {
                int n = intervals.Length;
                LinkedList<int[]> result = new();
                result.AddLast(newInterval);

                foreach (var curr in intervals)
                {
                    var last = result.Last();

                    int lo = Math.Max(curr[0], last[0]);
                    int hi = Math.Min(curr[1], last[1]);
                    if (lo <= hi)
                    {
                        result.RemoveLast();
                        result.AddLast(new int[]{ Math.Min(curr[0], last[0]), Math.Max(curr[1], last[1])});
                        continue;
                    }

                    if (curr[1] <= last[0])
                    {
                        result.RemoveLast();
                        result.AddLast(curr);
                        result.AddLast(last);
                        continue;
                    }

                    result.AddLast(curr);
                }

                return result.ToArray();
            }
        }

        public class SolutionBS
        {
            public int[][] Insert(int[][] intervals, int[] newInterval)
            {
                int n = intervals.Length;
                var result = new List<int[]>();

                int left = FirstEndGreaterOrEqual(intervals, newInterval[0]);
                int right = FirstStartGreater(intervals, newInterval[1]);

                // Add all intervals completely before newInterval
                for (int i = 0; i < left; i++)
                {
                    result.Add(intervals[i]);
                }

                // Merge overlapping intervals
                if (left < right)
                {
                    int mergedStart = Math.Min(newInterval[0], intervals[left][0]);
                    int mergedEnd = Math.Max(newInterval[1], intervals[right - 1][1]);
                    result.Add(new int[] { mergedStart, mergedEnd });
                }
                else
                {
                    // No overlap, just insert newInterval in the correct place
                    result.Add(newInterval);
                }

                // Add all intervals completely after newInterval
                for (int i = right; i < n; i++)
                {
                    result.Add(intervals[i]);
                }

                return result.ToArray();
            }

            // First interval with end >= target
            private int FirstEndGreaterOrEqual(int[][] intervals, int target)
            {
                int left = 0, right = intervals.Length;

                while (left < right)
                {
                    int mid = left + (right - left) / 2;
                    if (intervals[mid][1] < target)
                        left = mid + 1;
                    else
                        right = mid;
                }

                return left;
            }

            // First interval with start > target
            private int FirstStartGreater(int[][] intervals, int target)
            {
                int left = 0, right = intervals.Length;

                while (left < right)
                {
                    int mid = left + (right - left) / 2;
                    if (intervals[mid][0] <= target)
                        left = mid + 1;
                    else
                        right = mid;
                }

                return left;
            }
        }
    }
}
