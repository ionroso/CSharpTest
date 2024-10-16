using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class KClosestPointsToOrigin
    {
        public void Test()
        {
            var solution = new Solution();
            int[][] points =
            [
                [1, 3],
                [-2, 2]
            ];
            int k = 1;
            var result = solution.KClosest(points, k);
        }

        class Solution
        {
            public int[][] KClosest(int[][] points, int k)
            {
                PriorityQueue<(int x, int y), double> queue = new(k);

                foreach (var point in points)
                {
                    queue.Enqueue((point[0], point[1]), Math.Sqrt(point[0] * point[0] + point[1] * point[1]));
                }

                var output = new int[k][];
                int i = 0;
                while (queue.Count > 0 && i < k)
                {
                    (int x, int y) = queue.Dequeue();
                    output[i] = [x, y];
                    i++;
                }

                return output;
            }
        }
    }
}
