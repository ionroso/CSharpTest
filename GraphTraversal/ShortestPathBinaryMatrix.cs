using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class ShortestPath
    {
        public class Solution
        {
            public int ShortestPathBinaryMatrix(int[][] grid)
            {
                int n = grid.Length;
                int m = grid[0].Length;

                if (grid[0][0] == 1 || grid[n - 1][m - 1] == 1) return -1;
                if (n == 1 && m == 1) return 1;

                int steps = 1;
                int countPerLevel = 1;

                var queue = new Queue<(int Row, int Col)>();
                queue.Enqueue((0, 0));

                while (queue.Count > 0)
                {
                    int newCountPerLevel = 0;
                    while (countPerLevel > 0)
                    {
                        var (row, col) = queue.Dequeue();
                        
                        for(int dr = -1; dr <= 1; dr++)
                            for (int dc = -1; dc <= 1; dc++)
                            {
                                int newRow = row + dr;
                                int newCol = col + dc;

                                if (newRow < 0 || newRow >= n) continue;
                                if (newCol < 0 || newCol >= m) continue;
                                if (grid[newRow][newCol] == 1) continue;

                                if(newRow == n - 1 && newCol == m - 1) return steps + 1;

                                grid[newRow][newCol] = 1;

                                queue.Enqueue((newRow, newCol));
                                newCountPerLevel++;
                        }
                        countPerLevel--;
                    }
                    countPerLevel = newCountPerLevel;
                    steps++;
                }

                return -1;
            }
        }
    }
}
