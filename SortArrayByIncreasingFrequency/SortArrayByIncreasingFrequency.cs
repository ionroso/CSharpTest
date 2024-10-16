using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Intervals
{
    internal class SortArrayByIncreasingFrequency
    {
        public void Test()
        {
            new Solution().CanAttendMeetings([[7, 10], [2, 4]]);
        }

        public class Solution
        {
            public bool CanAttendMeetings(int[][] intervals)
            {
                Array.Sort(intervals, new Comparison<int[]>((i1, i2) => i1[0] - i2[0]));

                for (int i = 1; i <= intervals.Length - 1; i++)
                {
                    if (intervals[i - 1][1] > intervals[i][0])
                    {
                        return false;
                    }
                }


                return true;
            }
        }
    }
}
