using System.Linq;

namespace LeetCode.Medium
{
    public class InsertInterval
    {
        public void Test()
        {
            int[][] input =          new int[][] { new int[] { 1, 3 }, new int[] { 6, 9 } };
            var rez = new Solution().Insert(input ,new int[] { 2, 5 });
        }

        class Solution
        {
            public int[][] Insert(int[][] intervals, int[] newInterval)
            {
                int n = intervals.Length;
                LinkedList<int[]> result = new();
                result.AddLast(newInterval);

                for (int i = 0; i < n; i++)
                {
                    var last = result.Last();
                    int lo = Math.Max(intervals[i][0], last[0]);
                    int hi = Math.Min(intervals[i][1], last[1]);

                    if (lo <= hi)
                    {
                        result.RemoveLast();
                        result.AddLast(new int[]{
                    Math.Min(intervals[i][0], last[0]),
                    Math.Max(intervals[i][1], last[1])});
                        continue;
                    }

                    if (intervals[i][1] <= last[0])
                    {
                        result.RemoveLast();
                        result.AddLast(intervals[i]);
                        result.AddLast(last);
                        continue;
                    }

                    result.AddLast(intervals[i]);
                }

                return result.ToArray();
            }
        }

        private class SolutionList
        {
            public int[][] Insert(int[][] intervals, int[] newInterval)
            {
                int n = newInterval.Length;
                List<int[]> output = new();
                output.Add(newInterval);

                foreach (var curr in intervals)
                {
                    var last = output.Last();

                    // before?
                    if (curr[1] < last[0])
                    {
                        output.Remove(last);
                        output.Add(curr);
                        output.Add(last);
                        continue;
                    }

                    // overlap?
                    int low = Math.Max(last[0], curr[0]);
                    int high = Math.Min(last[1], curr[1]);
                    if (low <= high)
                    {
                        output.Remove(last);
                        var merge = new[] { Math.Min(last[0], curr[0]), Math.Max(last[1], curr[1]) };
                        output.Add(merge);
                        continue;
                    }

                    output.Add(curr);
                }

                return output.ToArray();
            }
        }
    }
}
