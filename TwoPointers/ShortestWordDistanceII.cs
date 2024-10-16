using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TwoPointers.ShortestWordDistanceII;

namespace TwoPointers
{
    internal class ShortestWordDistanceII
    {
        public static void Test()
        {
            var wordDistance = new WordDistance(["a", "c", "d", "a", "b"]);
            wordDistance.Shortest("a", "d"); // return 3
        }

        public class WordDistance
        {

            private Dictionary<string, List<int>> locations;

            public WordDistance(string[] words)
            {
                this.locations = new();

                // Prepare a mapping from a word to all it's locations (indices).
                for (int i = 0; i < words.Length; i++)
                {
                    string word = words[i];
                    if (!locations.TryGetValue(word, out var indices))
                    {
                        indices = new List<int>();
                        locations[word] = indices;
                    }

                    indices.Add(i);
                }
            }

            public int Shortest(string word1, string word2)
            {
                List<int> loc1, loc2;

                // Location lists for both the words
                // the indices will be in SORTED order by default
                loc1 = this.locations[word1];
                loc2 = this.locations[word2];

                int l1 = 0, l2 = 0, minDiff = int.MaxValue; 
                while (l1 < loc1.Count && l2 < loc2.Count)
                {
                    minDiff = Math.Min(minDiff, Math.Abs(loc1[l1] - loc2[l2]));
                    if (loc1[l1] < loc2[l2])
                    {
                        l1++;
                    }
                    else
                    {
                        l2++;
                    }
                }

                return minDiff;
            }
        }

        /**
         * Your WordDistance object will be instantiated and called as such:
         * WordDistance obj = new WordDistance(wordsDict);
         * int param_1 = obj.Shortest(word1,word2);
         */
    }
}
