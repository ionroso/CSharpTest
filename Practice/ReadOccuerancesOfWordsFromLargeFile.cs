using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class ReadOccuerancesOfWordsFromLargeFile
    {
        public void Test()
        {
        }

        class Solution
        {
            public List<int> FindWordOccurances(string filePath, string word)
            {
                if (string.IsNullOrEmpty(word))
                {
                    return new();
                }

                List<int> occurrences = new();
                int lineSize = 0;
                foreach (var line in File.ReadLines(filePath))
                {
                    int index = line.IndexOf(word); // use int index = line.IndexOf(word, StringComparison.OrdinalIgnoreCase); for case insensitive search
                    while (index != -1)
                    {
                        occurrences.Add(lineSize + index);
                        index = line.IndexOf(word, index + 1); // use int index = line.IndexOf(word, StringComparison.OrdinalIgnoreCase); for case insensitive search
                    }

                    lineSize += line.Length + Environment.NewLine.Length;
                }

                return occurrences;
            }
        }
    }
}
