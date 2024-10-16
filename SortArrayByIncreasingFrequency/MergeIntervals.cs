using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Intervals.SortArrayByIncreasingFrequency;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Intervals
{
    internal class MergeIntervals
    {
        public void Test()
        {
            new Solution().Merge([[7, 10], [2, 4]]);
        }

        public class Solution
        {
            public int[][] Merge(int[][] intervals)
            {
                Array.Sort(intervals, Comparer<int[]>.Create((a, b) => a[0] - b[0]));

                LinkedList<int[]> merged = new();
                foreach (int[] interval in intervals)
                {
                    if (merged.Count == 0 || merged.Last()[1] < interval[0])
                    {
                        merged.AddLast(interval);
                    }
                    else
                    {
                        merged.Last()[1] = Math.Max(merged.Last()[1], interval[1]);
                    }
                }
                return merged.ToArray();
            }
        }
    }
}
