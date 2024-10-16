using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingWindow
{
    internal class FindKLengthSubstringsWithNoRepeatedCharacters
    {
        public static void Test()
        {
            //new Solution().CompareVersion("1.2", "1.10");
        }

        class Solution
        {
            public int numKLenSubstrNoRepeats(string s, int k)
            {
                int n = s.Length;
                int count = 0;
                HashSet<char> set = new ();

                for (int l = 0, r = 0; r < n; r++)
                {
                    if (!set.Contains(s[r]))
                    {
                        set.Add(s[r]);
                    }
                    else
                    {
                        while (s[l] != s[r])
                        {
                            set.Remove(s[l]);
                            l++;
                        }
                        l++;
                    }


                    if (r - l + 1 == k)
                    {
                        count++;

                        set.Remove(s[l]);
                        l++;
                    }
                }

                return count;
            }
        }

    }
}
