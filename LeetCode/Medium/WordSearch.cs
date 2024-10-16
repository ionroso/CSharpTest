
namespace LeetCode.Medium
{
    public class WordSearch
    { 
        public void Test()
        {
            char[][] board = new char[][]
            {
                new char[] {'A', 'B', 'C', 'E'},
                new char[] {'S', 'F', 'C', 'S'},
                new char[] {'A', 'D', 'E', 'E'}
            };

            new Solution().Exist(board, "ABCCED");
        }

        public class Solution
        {
            private readonly int[] ROW = new int[] { 0, 0, -1, 1 };
            private readonly int[] COL = new int[] { -1, 1, 0, 0 };

            public bool Exist(char[][] board, string word)
            {
                int n = board.Length;
                int m = board[0].Length;

                bool[][] visited = new bool[n][];
                for (int i = 0; i < n; i++)
                {
                    visited[i] = new bool[m];
                }

                    for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if(board[i][j] == word[0])
                        {
                            bool rez = DFS(board, n, m, i, j, word, 0, visited);
                            if(rez) return true;

                            visited[i][j] = false;
                        }
                    }
                }

                return false;
            }

           

            private bool DFS(char[][] board, int n, int m, int r, int c, string word, int index, bool[][] visited)
            {
                if(r < 0 || r >= n || c < 0 || c >= m || visited[r][c])
                {
                    return false;
                }

                if (index == word.Length - 1)
                {
                    return board[r][c] == word[index] ;
                }

                if (word[index] != board[r][c])
                {
                    return false;
                }

                visited[r][c] = true;

                var rez = false;
                for (int x = 0; x<4; x++)
                {
                    rez = DFS(board, n, m, r + ROW[x], c + COL[x], word, index + 1, visited);
                    if (rez)
                    {
                        break;
                    }
                }

                visited[r][c] = false;
                return rez;
            }
        }
    }
}
