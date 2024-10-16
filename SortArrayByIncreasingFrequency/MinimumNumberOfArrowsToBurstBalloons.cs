using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intervals
{
    internal class MinimumNumberOfArrowsToBurstBalloons
    {

        public void Test()
        {
            Console.WriteLine(1.CompareTo(1));
            Console.WriteLine(1.CompareTo(2));
            Console.WriteLine(2.CompareTo(1));
            new Solution().FindMinArrowShots([[10, 16], [2, 8], [1, 6], [7, 12]]);
        }

        class Solution
        {
            public int FindMinArrowShots(int[][] points)
            {
                if (points.Length == 0) return 0;


                // sort by x_end
             Array.Sort(points, Comparer<int[]>.Create((o1, o2) => {
                    // We can't simply use the o1[1] - o2[1] trick, as this will cause an
                    // integer overflow for very large or small values.
                    if (o1[1] == o2[1]) return 0;
                    if (o1[1] < o2[1]) return -1;
                    return 1;
                }));


                int arrows = 1;
                int xStart, xEnd, firstEnd = points[0][1];
                foreach (int[] p in points)
                {
                    xStart = p[0];
                    xEnd = p[1];


                    // If the current balloon starts after the end of another one,
                    // one needs one more arrow
                    if (firstEnd < xStart)
                    {
                        arrows++;
                        firstEnd = xEnd;
                    }
                }


                return arrows;
            }
        }

    }
}
