using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoPointers
{
    internal class ReverseWordsInAStringIII
    {
        class Solution
        {
            public String reverseWords(String str)
            {
                int lastSpaceIndex = -1;
                char[] chArray = str.ToCharArray();
                int len = chArray.Length;
                for (int i = 0; i <= len; i++)
                {
                    if (i == len || chArray[i] == ' ')
                    {
                        for (int s = lastSpaceIndex + 1, e = i - 1; s < e; s++, e--)
                        {
                            char temp = chArray[s];
                            chArray[s] = chArray[e];
                            chArray[e] = temp;
                        }
                        lastSpaceIndex = i;
                    }
                }
                return new String(chArray);
            }
        }
    }
}
