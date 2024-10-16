using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class WordSearch
    {
        public void Test()
        {
            var wordSearch = new Solution();
            char[][] board =
            [
                    ['a', 'b', 'c', 'e'],
                    ['s', 'f', 'c', 's'],
                    ['a', 'd', 'e', 'e']
            ];
            string word = "abcced";
            var result = wordSearch.Exist(board, word);
        }

        class Solution {

            private readonly int[] row = [-1, 0, 1, 0];
            private readonly int[] col = [0, 1, 0, -1];

            public bool Exist(char[][] board, string word)
            {
                bool[][] visited = new bool[board.Length][];
                for (int i = 0; i < board.Length; i++)
                {
                    visited[i] = new bool[board[i].Length];
                }

                for (int r = 0; r < board.Length; r++)
                    for (int c = 0; c < board[r].Length; c++)
                        if (word[0] == board[r][c] && Dfs(board, word, r, c, visited, 0)) return true;



                return false;
            }

            private bool Dfs(char[][] board, string word, int r, int c, bool[][] visited, int index)
            {
                if (index >= word.Length) return true;
                if (r< 0 || r >= board.Length || c < 0 || c >= board[r].Length) return false;
                if (visited[r][c]) return false;

                if(board[r][c] != word[index]) return false;
                
                visited[r][c] = true;

                for (int i = 0; i < 4; i++)
                {
                    if (Dfs(board, word, r + row[i], c + col[i], visited, index + 1)) return true;
                }

                visited[r][c] = false;

                return false;
            }
        }
    }
}
