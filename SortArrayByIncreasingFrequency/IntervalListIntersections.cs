using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intervals
{
    internal class IntervalListIntersections
    {
        public void Test() {

        }
    }
    public class Solution
    {
        public int[][] IntervalIntersection(int[][] firstList, int[][] secondList)
        {
            List<int[]> ans = new();
            int i = 0, j = 0;

            while (i < firstList.Length && j < secondList.Length)
            {
                // Let's check if A[i] intersects B[j].
                // lo - the startpoint of the intersection
                // hi - the endpoint of the intersection
                int lo = Math.Max(firstList[i][0], secondList[j][0]);
                int hi = Math.Min(firstList[i][1], secondList[j][1]);
                if (lo <= hi)
                {
                    ans.Add(new int[] { lo, hi });
                }


                // Remove the interval with the smallest endpoint
                if (firstList[i][1] < secondList[j][1])
                {
                    i++;
                }
                else
                {
                    j++;
                }
            }

            return ans.ToArray();
        }
    }
}
