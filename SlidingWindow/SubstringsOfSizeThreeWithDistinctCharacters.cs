using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingWindow
{
    internal class SubstringsOfSizeThreeWithDistinctCharacters
    {
        class Solution
        {
            public int CountGoodSubstrings(string s)
            {
                if (s.Length < 3) return 0;


                int goodSub = 0;
                for (int i = 2; i < s.Length; i++)
                {
                    goodSub += s[i - 2] != s[i - 1]
                            && s[i - 2] != s[i]
                            && s[i - 1] != s[i] ? 1 : 0;
                }

                return goodSub;
            }
        }

    }
}
