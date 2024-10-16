namespace LeetCode.Medium
{
    public class NonOverlappingIntervals
    {
        public void Test()
        {
            int[][] input = new int[][] { new int[] { 1, 3 }, new int[] { 2, 9 } };
            var rez = new Solution().EraseOverlapIntervals(input);
            Console.WriteLine(rez);
        }


        public class Solution //Optimal
        {
            public int EraseOverlapIntervals(int[][] intervals)
            {
                Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
                int n = intervals.Length;
                int count = 0;
                int[] last = null;
                foreach (int[] curr in intervals)
                {
                    if (last == null || last[1] <= curr[0])
                    {
                        last = curr;
                        continue;
                    }

                    if (last[1] > curr[1])
                    {
                        last = curr;
                    }

                    count++;
                }

                return count;
            }
        }
        public class SolutionNotOptimal
        {
            public int EraseOverlapIntervals(int[][] intervals)
            {
                Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
                int n = intervals.Length;
                int count = 0;
                List<int[]> output = new();
                foreach (int[] curr in intervals)
                {
                    if (output.Count == 0 || output.Last()[1] <= curr[0])
                    {
                        output.Add(curr);
                        continue;
                    }

                    if (output.Last()[1] > curr[1])
                    {
                        output.Remove(output.Last());
                        output.Add(curr);
                    }

                    count++;
                }

                return count;
            }
        }
    }
}
