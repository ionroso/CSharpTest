using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class FirstNonRepeatingCharInString
    {
        public void Test()
        {
        }

        class Solution
        {
            public char? FirstNonRepeatingChar(string str)
            {
                Dictionary<char, int> counts = new();
                Queue<char> queue = new();

                foreach (char c in str)
                {
                    int count = counts.GetValueOrDefault(c, 0);
                    counts[c] = count + 1;
                    queue.Enqueue(c);

                    while(queue.Count != 0 && counts[queue.Peek()] > 1) queue.Dequeue();
                }

                return queue.Count != 0 ? queue.Peek() : default;
            }

            public char? FirstNonRepeatingChar1(string str)
            {
                Dictionary<char, int> counts = new();

                foreach (char c in str)
                {
                    int count = counts.GetValueOrDefault(c, 0);
                    counts[c] = count+1;
                }

                foreach (char c in str)
                {
                    int count = counts[c];
                    if(count == 1)
                    {
                        return c;
                    }
                }

                return default;
            }
        }
    }
}
