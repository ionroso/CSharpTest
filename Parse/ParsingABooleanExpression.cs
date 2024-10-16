namespace Parsing
{
    internal class ParsingABooleanExpression
    {
        public void Test()
        {
            Console.WriteLine(new Solution().ParseBoolExpr("(5+(5*2))"));
        }

        class Solution
        {

            public bool ParseBoolExpr(string expression)
            {
                Stack<char> st = new ();

                // Traverse the entire expression
                foreach (char currChar in expression.ToCharArray())
                {
                    if (currChar == ')')
                    {
                        List<char> values = new ();

                        // Gather all values inside the parentheses
                        while (st.Peek() != '(')
                        {
                            values.Add(st.Pop());
                        }
                        st.Pop(); // Remove '('
                        char op = st.Pop(); // Remove the operator

                        // Evaluate the subexpression and push the result back
                        char result = evaluateSubExpr(op, values);
                        st.Push(result);
                    }
                    else if (currChar != ',')
                    {
                        st.Push(currChar); // Push non-comma characters into the stack
                    }
                }

                // Final result is on the top of the stack
                return st.Peek() == 't';
            }

            // Evaluates a subexpression based on the operator and list of values
            private char evaluateSubExpr(char op, List<char> values)
            {
                if (op == '!') return values[0] == 't' ? 'f' : 't';

                // AND: return 'f' if any value is 'f', otherwise return 't'
                if (op == '&')
                {
                    return values.Contains('f') ? 'f' : 't';
                }

                // OR: return 't' if any value is 't', otherwise return 'f'
                if (op == '|')
                {
                    return values.Contains('t') ? 'f' : 't';
                }

                return 'f'; // This point should never be reached
            }
        }
    }
}
