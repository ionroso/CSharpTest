using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class Boggle
    {
        public void Test()
        {
            var boggle = new Solution();
            char[][] board =
            [
                    ['c', 'a', 'a', 'n'],
                    ['a', 'd', 'a', 'e'],
                    ['t', 'h', 'r', 'r'],
                    ['i', 'f', 'l', 'v']
            ];
            string[] words = ["cat", "hrr", "vlf", "aahl"];
            var result = boggle.FindWords(board, words);
        }




        class Solution
        {
            class Trie
            {
                class TrieNode
                {
                    public char Value { get; set; }
                    public bool IsWord { get; set; }
                    public Dictionary<char, TrieNode> Children { get; set; }
                    public TrieNode(char value)
                    {
                        Value = value;
                        IsWord = false;
                        Children = new Dictionary<char, TrieNode>();
                    }
                }

                private TrieNode root;

                public Trie()
                {
                    root = new TrieNode(' ');
                }

                public void Insert(string word)
                {
                    Insert(root, word, 0);
                }

                public bool Search(string word) => Search(root, word, 0);

                public bool StartsWith(string prefix) => SearchPrefix(root, prefix, 0);

                private bool Search(TrieNode node, string word, int index)
                {
                    if (node is null) return false;
                    if (index >= word.Length) return false;

                    if (!node.Children.TryGetValue(word[index], out var trieNode))
                    {
                        return false;
                    }

                    if (index == word.Length - 1 && trieNode.IsWord)
                    {
                        return true;
                    }

                    return Search(trieNode, word, index + 1);
                }

                private bool SearchPrefix(TrieNode node, string prefix, int index)
                {
                    if (node is null) return false;
                    if (index >= prefix.Length) return false;

                    if (!node.Children.TryGetValue(prefix[index], out var trieNode))
                    {
                        return false;
                    }

                    if (index == prefix.Length - 1)
                    {
                        return true;
                    }

                    return SearchPrefix(trieNode, prefix, index + 1);
                }

                private void Insert(TrieNode node, string word, int index)
                {
                    if (node is null) return;
                    if (index >= word.Length) return;

                    if (!node.Children.TryGetValue(word[index], out var trieNode))
                    {
                        trieNode = new TrieNode(word[index]);
                        node.Children.Add(word[index], trieNode);
                    }

                    if (index == word.Length - 1)
                    {
                        trieNode.IsWord = true;
                        return;
                    }

                    Insert(trieNode, word, index + 1);
                }
            }

            public IList<string> FindWords(char[][] board, string[] words)
            {
                Trie trie = new Trie();

                List<string> found = new();
                foreach (string word in words) trie.Insert(word);

                bool[][] visited = new bool[board.Length][];
                for (int i = 0; i < board.Length; i++)
                {
                    visited[i] = new bool[board[i].Length];
                }

                string curr = "";
                for (int r = 0; r < board.Length; r++)
                    for (int c = 0; c < board[r].Length; c++)
                        Dfs(board, words, r, c, visited, trie, found, curr);

                return found;
            }

            private void Dfs(char[][] board, string[] words, int r, int c,
                bool[][] visited, Trie trie, List<string> found,
                string curr)
            {
                if (r < 0 || r >= board.Length || c < 0 || c >= board[0].Length) return;

                if (visited[r][c]) return;

                if (!trie.StartsWith(curr + board[r][c])) return;

                if (words.Contains(curr + board[r][c])) found.Add(curr + board[r][c]);

                visited[r][c] = true;

                for (int dr = 1; dr >= -1; dr--)
                    for (int dc = 1; dc >= -1; dc--)
                        Dfs(board, words, r + dr, c + dc, visited, trie, found, curr + board[r][c]);

                visited[r][c] = false;
            }
        }


        class Solution1
        {
            public IList<string> FindWords(char[][] board, string[] words)
            {
                HashSet<string> trie = new();
                List<string> found = new();
                foreach (string word in words)
                   for(int i=0; i<=word.Length;i++)
                        trie.Add(word[0..i]);

                bool[][] visited = new bool[board.Length][];
                for (int i = 0; i < board.Length; i++)
                {
                    visited[i] = new bool[board[i].Length];
                }

                string curr = "";
                for (int r = 0; r < board.Length; r++)
                    for (int c = 0; c < board[r].Length; c++)
                        Dfs(board, words, r, c, visited, trie, found, curr);

                 return found;
            }

            private void Dfs(char[][] board, string[] words, int r, int c,
                bool[][] visited, HashSet<string> trie, List<string> found,
                string curr)
            {
                if(r<0 || r>=board.Length || c<0 || c >= board[0].Length) return;

                if (visited[r][c]) return;

                if (!trie.Contains(curr + board[r][c])) return;

                if (words.Contains(curr+ board[r][c])) found.Add(curr+ board[r][c]);

                visited[r][c] = true;

                for(int dr = 1; dr >= -1; dr--)
                    for (int dc = 1; dc >= -1; dc--)
                        Dfs(board, words, r + dr, c + dc, visited, trie, found, curr + board[r][c]);

                visited[r][c] = false;
            }
        }
    }
}
