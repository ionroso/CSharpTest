using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.Collections;

namespace PermutationsCombinationBacktracking
{
    internal class CombinationSumII
    {
        public void Test()
        {
            new Solution().CombinationSum2(new int[] { 1, 2, 2, 2, 5 }, 5);
        }

        class Solution
        {

            public IList<IList<int>> CombinationSum2(int[] candidates, int target)
            {
                List<IList<int>> list = new ();
                Array.Sort(candidates);
                backtrack(list, new Stack<int>(), candidates, target, 0);
                return list;
            }

            private void backtrack(
                List<IList<int>> answer,
                Stack<int> tempList,
                int[] candidates,
                int totalLeft,
                int index
            )
            {
                if (totalLeft < 0)
                {
                    return;
                }

                if (totalLeft == 0)
                { // Add to the answer array, if the sum is equal to target.
                    answer.Add(tempList.ToList());
                    return;
                }

                for (int i = index; i < candidates.Length && totalLeft >= candidates[i]; i++)
                {
                    if (i > index && candidates[i] == candidates[i - 1]) continue;
                    // Add it to tempList.
                    tempList.Push(candidates[i]);
                    // Check for all possible scenarios.
                    backtrack(answer, tempList, candidates, totalLeft - candidates[i], i + 1);
                    // Backtrack the tempList.
                    tempList.Pop();
                }
            }
        }
    }
}
