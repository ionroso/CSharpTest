using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class FreshRottedOranges
    {
        public class Solution
        {
            public int OrangesRotting(int[][] grid)
            {
                //int freshCount = grid.Sum(row => row.Count(cell => cell == 1));
                // count total oranges

                int n = grid.Length;
                int m = grid[0].Length;

                var allRotted = new Queue<(int r, int c)>();
                int totalOranges = 0;
                for (int r = 0; r < grid.Length; r++)
                {
                    for (int c = 0; c < grid[0].Length; c++)
                    {
                        if (grid[r][c] != 0)
                        {
                            totalOranges++;
                        }

                        if (grid[r][c] == 2)
                        {
                            allRotted.Enqueue((r, c));
                        }
                    }
                }

                if (totalOranges == 0)
                {
                    return 0;
                }

                (int totalRotted, int minutes) = Bfs(grid, allRotted);

                return totalRotted != totalOranges ? -1 : minutes;
            }

            private (int totalRotted, int minutes) Bfs(int[][] grid, Queue<(int r, int c)> allRotted)
            {
                int n = grid.Length;
                int m = grid[0].Length;

                var aux = new Queue<(int r, int c)>();

                (int, int)[] dirs = new[] { (1, 0), (-1, 0), (0, 1), (0, -1) };

                var totalRotted = allRotted.Count;
                int minutes = 0;

                while (allRotted.Count > 0)
                {
                    while (allRotted.Count > 0)
                    {
                        var curr = allRotted.Dequeue();
                        foreach (var dir in dirs)
                        {
                            int nextR = curr.r + dir.Item1;
                            int nextC = curr.c + dir.Item2;
                            if (nextR < 0 || nextR >= n || nextC < 0 || nextC >= m)
                            {
                                continue;
                            }
                            if (grid[nextR][nextC] == 1)
                            {
                                aux.Enqueue((nextR, nextC));
                                grid[nextR][nextC] = 3;
                                totalRotted++;
                            }
                        }
                    }

                    minutes++;
                    allRotted = aux;
                    aux = new Queue<(int r, int c)>();
                }

                return (totalRotted, minutes - 1);
            }
        }
    }
}
