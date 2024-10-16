using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Intervals
{
    internal class RemoveInterval
    {
        public void Test()
        {

        }

        public class Solution
        {
            public IList<IList<int>> RemoveInterval(int[][] intervals, int[] toBeRemoved)
            {
                IList<IList<int>> result = new List<IList<int>>();
                foreach (var cur in intervals)
                {
                    // If there are no overlaps, add the curr to the list as is.
                    if (cur[0] > toBeRemoved[1] || cur[1] < toBeRemoved[0])
                    {
                        result.Add(new List<int>() { cur[0], cur[1] });
                        continue;
                    }

                    // There's an overlap
                    // Is there a right interval we need to keep?
                    if (cur[0] < toBeRemoved[0])
                    {
                        result.Add(new List<int>() { cur[0], toBeRemoved[0] });
                    }

                    // Is there a right curr we need to keep?
                    if (cur[1] > toBeRemoved[1])
                    {
                        result.Add(new List<int>() { toBeRemoved[1], cur[1] });
                    }
                }
                return result;
            }
        }
    }
}
