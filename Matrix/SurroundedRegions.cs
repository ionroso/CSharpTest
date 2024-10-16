using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public class SurroundedRegions
    {
        public void Test()
        {
        }

        public class Solution
        {
            private static readonly int[][] Directions = [ [1, 0],[-1, 0],[0, 1],[0, -1] ];

            public void Solve(char[][] board)
            {
                if (board == null || board.Length == 0 || board[0].Length == 0)
                {
                    return;
                }

                int n = board.Length;
                int m = board[0].Length;

                for (int r = 0; r < n; r++)
                {
                    Dfs(board, r, 0);
                    Dfs(board, r, m - 1);
                }

                for (int c = 0; c < m; c++)
                {
                    Dfs(board, 0, c);
                    Dfs(board, n - 1, c);
                }

                for (int r = 0; r < n; r++)
                {
                    for (int c = 0; c < m; c++)
                    {
                        if (board[r][c] == 'O')
                        {
                            board[r][c] = 'X';
                        }
                        else if (board[r][c] == 'Y')
                        {
                            board[r][c] = 'O';
                        }
                    }
                }
            }

            private void Dfs(char[][] board, int r, int c)
            {
                if (r < 0 || r >= board.Length || c < 0 || c >= board[0].Length)
                {
                    return;
                }

                if (board[r][c] != 'O')
                {
                    return;
                }

                board[r][c] = 'Y';

                foreach (var dir in Directions)
                {
                    int nr = r + dir[0];
                    int nc = c + dir[1];

                    Dfs(board, nr, nc);
                }
            }
        }
    }
}
