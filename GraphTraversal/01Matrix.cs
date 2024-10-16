using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class _01Matrix
    {
        public class Solution
        {
            public int[][] UpdateMatrix(int[][] mat)
            {
                int n = mat.Length; 
                int m = mat[0].Length;

                (int, int)[] dirs = new[] { (1, 0), (-1, 0), (0, 1), (0, -1) };

                var allZeros = new Queue<(int Row, int Col)>();
                for (int r = 0; r < n; r++)
                {
                    for (int c = 0; c < m; c++)
                    {
                        if (mat[r][c] != 0) continue;

                        foreach (var (dr, dc) in dirs)
                        {
                            if(r + dr >= 0 && r + dr < n && c + dc >= 0 && c + dc < m && mat[r + dr][c + dc] == 1)
                            {
                                allZeros.Enqueue((r, c));
                                continue;
                            }
                        }
                    }
                }

                bool[,] visited = new bool[n, m];

                int distance = 1;
                int countPerLevel = allZeros.Count;

                while (allZeros.Count > 0)
                {
                    while(countPerLevel>0)
                    {
                        var ( row, col) = allZeros.Dequeue();
                        foreach (var (r, c) in dirs)
                        {
                            int newRow = row + r;
                            int newCol = col + c;
                            if (newRow < 0 || newRow >= n) continue;
                            if (newCol < 0 || newCol >= m) continue;
                            if (visited[newRow, newCol]) continue;
                            if (mat[newRow][newCol] == 0) continue;


                            visited[newRow, newCol] = true;
                            mat[newRow][newCol] = distance;
                            allZeros.Enqueue((newRow, newCol));
                        }
                        countPerLevel--;
                    }
                    countPerLevel = allZeros.Count;
                    distance++;
                }

                return mat;
            }
        }
    }
}
