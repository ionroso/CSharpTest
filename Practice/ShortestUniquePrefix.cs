using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class ShortestUniquePrefix
    {
        public void Test()
        {
            var solution = new Solution();
            string[] words = ["zebra", "dog", "duck", "dove"];
            var result = solution.Prefixes(words);
        }


        class Solution
        {
            public string[] Prefixes(string[] words)
            {
                TrieNode root = new TrieNode(' ');

                foreach (var word in words)
                {
                    TrieNode current = root;
                    foreach (var c in word)
                    {
                        if (!current.Children.TryGetValue(c, out var trieNode))
                        {
                            trieNode = new TrieNode(c);
                            current.Children.Add(c, trieNode);
                        }
                        trieNode.Count++;
                        current = trieNode;
                    }
                }

                var output = new string[words.Length];
                var outputIndex = 0;
                foreach (var word in words)
                {
                    TrieNode current = root;
                    StringBuilder prefix = new StringBuilder();
                    int i = 0;
                    while (current.Children.TryGetValue(word[i], out var trieNode) && trieNode.Count>1)
                    {
                        prefix.Append(trieNode.Value);
                        current = trieNode;
                        i++;
                    }

                    output[outputIndex] = prefix.Append(word[i]).ToString();
                    outputIndex++;
                }

                return output;
            }

            private class TrieNode
            {
                public char Value { get; private set; }
                public Dictionary<char, TrieNode> Children { get; private set; }
                public int Count { get; set; }

                public TrieNode(char value)
                {
                    Value = value;
                    Children = new Dictionary<char, TrieNode>();

                    Count = 0;
                }
            }
        }
            
    }
}
