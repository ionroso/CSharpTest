using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing
{
    public class ValidParentheses
    {
        public void Test()
        {
            Console.WriteLine(new Solution().IsValid("(())"));
        }

        public class Solution
        {
            public bool IsValid(string s)
            {

                var openings = new HashSet<char> { '(', '{', '[' };
                var pairs = new Dictionary<char, char>
                {
                    ['('] = ')',
                    ['{'] = '}',
                    ['['] = ']'
                };

                var stack = new Stack<char>();

                foreach (char c in s)
                {
                    if(openings.Contains(c))
                    {
                        stack.Push(c);
                        continue;
                    }

                    if (!stack.TryPop(out char top) || pairs[top] != c)
                    {
                        return false;
                    }
                }

                return stack.Count == 0;
            }
        }
    }
}
