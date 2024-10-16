using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class TriePractice
    {
        /*
        For interview:

        public class Trie
        {
            private class TrieNode
            {
                public bool IsWord;
                public Dictionary<char, TrieNode> Children = new();
            }

            private readonly TrieNode root;

            public Trie()
            {
                root = new TrieNode();
            }

            public void Insert(string word)
            {
                TrieNode node = root;

                foreach (char c in word)
                {
                    if (!node.Children.ContainsKey(c))
                    {
                        node.Children[c] = new TrieNode();
                    }

                    node = node.Children[c];
                }

                node.IsWord = true;
            }

            public bool Search(string word)
            {
                TrieNode node = root;

                foreach (char c in word)
                {
                    if (!node.Children.TryGetValue(c, out node))
                        return false;
                }

                return node.IsWord;
            }

            public bool StartsWith(string prefix)
            {
                TrieNode node = root;

                foreach (char c in prefix)
                {
                    if (!node.Children.TryGetValue(c, out node))
                        return false;
                }

                return true;
            }
        }



         */

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
    }
}
