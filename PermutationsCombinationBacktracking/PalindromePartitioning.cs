using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermutationsCombinationBacktracking
{
    internal class PalindromePartitioning
    {
        public void Test()
        {
            var rez = new Solution().Partition("aab");
        }

        private class Solution
        {
            public IList<IList<string>> Partition(string str)
            {
                var ans = new List<IList<string>>();
                Dfs(0, new List<string>(), str, ans);
                return ans;
            }

            private void Dfs(
                int start,
                List<string> currentList,
                string str,
                List<IList<string>> result)
            {
                if (start >= str.Length)
                    result.Add(new List<string>(currentList));
                else
                {
                    for (int end = start; end < str.Length; end++)
                    {
                        if (IsPalindrome(str, start, end))
                        {
                            currentList.Add(str.Substring(start, end - start + 1));
                            Dfs(end + 1, currentList, str, result);
                            currentList.RemoveAt(currentList.Count - 1);
                        }
                    }
                }
            }

            bool IsPalindrome(string str, int low, int high)
            {
                while (low < high)
                    if (str[low++] != str[high--])
                        return false;
                return true;
            }
        }
    }
}
