using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public class Rotate90Degree
    {
        public void Test()
        {
            int[][] grid =
            {
                [5,  1,  9,  11],
                [2,  4,  8,  10],
                [13, 3,  6,  7],
                [15, 14, 12, 16]
            };

            //int[][] grid =
            //{
            //    [1,  2],
            //    [3,  4,],
            //};


            //int[][] grid =
            //{
            //    [1,  2, 3],
            //    [4, 5, 6],
            //    [7, 8, 9],
            //};

            new Solution().Rotate(grid);

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    Console.Write(grid[i][j] + "  ");
                }
                Console.WriteLine();
            }
        }

        public class Solution
        {
            public void Rotate(int[][] matrix)
            {
                int n = matrix.Length;
                for (int ring = 0; ring < n / 2; ring++)
                {
                    RotateRing(matrix, ring);
                }
            }

            private void RotateRing(int[][] matrix, int ringNumber)
            {
                var n = matrix.Length;
                var m = matrix[0].Length;

                var sideLen = matrix.Length - 2 * ringNumber;
                
                var start = ringNumber;
                for (int i = 0; i < sideLen-1; i++)
                {
                    int temp = matrix[start][start + i];
                    matrix[start][start + i] = matrix[n - 1 - start - i][start];
                    matrix[n - 1 - start - i][start] = matrix[n - 1 - start][m - 1 - start - i];
                    matrix[n - 1 - start][m - 1 - start - i] = matrix[start + i][m - 1 - start];
                    matrix[start + i][m - 1 - start] = temp;
                }
            }
        }

        public class Solution1
        {
            public void Rotate(int[][] matrix)
            {
                int n = matrix.Length;

                for (int layer = 0; layer < n / 2; layer++)
                {
                    RotateLayer(matrix, layer);
                }
            }

            private void RotateLayer(int[][] matrix, int layer)
            {
                int n = matrix.Length;
                int first = layer;
                int last = n - 1 - layer;

                for (int offset = 0; offset < last - first; offset++)
                {
                    int temp = matrix[first][first + offset];              // top
                    matrix[first][first + offset] = matrix[last - offset][first];   // left -> top
                    matrix[last - offset][first] = matrix[last][last - offset];     // bottom -> left
                    matrix[last][last - offset] = matrix[first + offset][last];     // right -> bottom
                    matrix[first + offset][last] = temp;                             // top -> right
                }
            }
        }

        public class Solution2
        {
            public void Rotate(int[][] matrix)
            {
                int n = matrix.Length;

                // Transpose
                for (int i = 0; i < n; i++)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        (matrix[i][j], matrix[j][i]) = (matrix[j][i], matrix[i][j]); // tuple swap
                    }
                }

                // Reverse each row
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n / 2; j++)
                    {
                        (matrix[i][j], matrix[i][n - 1 - j]) = (matrix[i][n - 1 - j], matrix[i][j]);
                    }
                }
            }
        }
    }
}
