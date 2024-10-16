using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class WordSearchII
    {


        public class Solution
        {
            private class Trie
            {
                public class TrieNode
                {
                    public char Value { get; internal set; }
                    public string Word { get; internal set; }
                    public bool IsWord { get; internal set; }
                    public Dictionary<char, TrieNode> Children { get; internal set; }

                    public TrieNode(char value)
                    {
                        Value = value;
                        IsWord = false;
                        Children = new Dictionary<char, TrieNode>();
                    }
                }

                public TrieNode Root { get; }

                public Trie()
                {
                    Root = new TrieNode(' ');
                }

                public void Insert(string word)
                {
                    Insert(Root, word, 0);
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
                        trieNode.Word = word;
                        trieNode.IsWord = true;
                        return;
                    }

                    Insert(trieNode, word, index + 1);
                }
            }

            private readonly int[] row = [-1, 0, 1, 0];
            private readonly int[] col = [0, 1, 0, -1];

            public IList<string> FindWords(char[][] board, string[] words)
            {

                Trie trie = new Trie();
                foreach (string word in words) trie.Insert(word);

                bool[][] visited = new bool[board.Length][];
                for (int i = 0; i < board.Length; i++)
                {
                    visited[i] = new bool[board[i].Length];
                }

                var foundSoFar = new HashSet<string>();

                string curr = "";
                for (int r = 0; r < board.Length; r++)
                    for (int c = 0; c < board[r].Length; c++)
                            Dfs(board, r, c, visited, trie.Root, foundSoFar);

                return foundSoFar.ToList();
            }

            private void Dfs(
                char[][] board,
                int r, int c,
                bool[][] visited,
                Trie.TrieNode node,
                HashSet<string> found)
            {
                if (r < 0 || r >= board.Length || c < 0 || c >= board[r].Length) return;

                if (visited[r][c]) return;

                if (!node.Children.TryGetValue(board[r][c], out Trie.TrieNode trieNode)) return;

                if (trieNode.IsWord)
                {
                    found.Add(trieNode.Word);
                }

                visited[r][c] = true;

                for (int i = 0; i < 4; i++)
                        Dfs(board, r + row[i], c + col[i], visited, trieNode, found);

                visited[r][c] = false;
            }
        }
    }
}
