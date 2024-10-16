using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.MostSeriousPrep.TwoPointers
{
    public class OneEditDistance
    {
        public void Test()
        {
            Console.WriteLine(new Solution().IsOneEditDistance("abc", "adc"));
        }
    }

    public class Solution
    {
        public bool IsOneEditDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;

            if(Math.Abs(n-m)>1) return false;

            var sArray = s.ToArray();
            var tArray= t.ToArray();

            //if (n == m && TwoListExcept(sArray, tArray).Count <= 1) return true;

            //sArray.Insert(0, 'a');
            //if (TwoListExcept(sArray, tArray).Count <= 1) return true;
            //sList.RemoveAt(0);

            //tArray.Insert(0, 'a');
            //if (TwoListExcept(sList, tArray).Count <= 1) return true;

            return false;
        }

        //private int TwoListExcept(char[] s, char[] t, int i, int j)
        //{
        //    int x = i; int y = j;
        //    while (x <  && y >= 0) {
        //    }
        //}
    }
}
