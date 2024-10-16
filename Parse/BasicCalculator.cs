using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Parse
{
    internal class BasicCalculator
    {
        public void Test()
        {
            //Console.WriteLine(new Solution().Calculate("-2147483648"));
            //Console.WriteLine(new Solution().Calculate("55")); //55
            Console.WriteLine(new Solution().Calculate("1-(-2)")); //3
            //Console.WriteLine(new Solution().Calculate("(5-3)-(2+1)")); // -1
            //Console.WriteLine(new Solution().Calculate("20-1+2")); //21
        }


        // Learn this for interview
        class Solution // Basic Calculator |
        {
            public int Calculate(string s)
            {
                Stack<int> stack = new Stack<int>();
                int operand = 0;
                int sign = 1;
                int result = 0;

                for (int i = 0; i < s.Length; i++)
                {
                    char c = s[i];
                    if (c == ' ') continue;

                    if (char.IsDigit(c))
                    {
                        operand = operand * 10 + (c - '0');
                    }
                    else if (c == '+')
                    {
                        result += sign * operand;

                        operand = 0;
                        sign = 1;
                    }
                    else if (c == '-')
                    {
                        result += sign * operand;

                        operand = 0;
                        sign = -1;
                    }
                    else if (c == '(')
                    {
                        stack.Push(result);
                        stack.Push(sign);

                        result = 0;

                        operand = 0;
                        sign = 1;
                    }
                    else if (c == ')')
                    {
                        result += sign * operand;
                        result *= stack.Pop();
                        result += stack.Pop();

                        operand = 0;
                        sign = 1;
                    }
                }

                return result + sign * operand;
            }
        }

        // Learn this for interview

        class Solution2 // Basic Calculator ||
        {
            public int Calculate(string s)
            {

                if (s == null || s.Length == 0) return 0;
                int len = s.Length;
                Stack<int> stack = new Stack<int>();
                int currentNumber = 0;
                char operation = '+';
                for (int i = 0; i < len; i++)
                {
                    char currentChar = s[i];

                    if (Char.IsWhiteSpace(currentChar)) continue;

                    if (Char.IsDigit(currentChar))
                    {
                        currentNumber = (currentNumber * 10) + (currentChar - '0');
                    }

                    if (!Char.IsDigit(currentChar) || i == len - 1)
                    {
                        if (operation == '+')
                        {
                            stack.Push(currentNumber);
                        }
                        else if(operation == '-')
                        {
                            stack.Push(-currentNumber);
                        }
                        else if (operation == '*')
                        {
                            stack.Push(stack.Pop() * currentNumber);
                        }
                        else if (operation == '/')
                        {
                            stack.Push(stack.Pop() / currentNumber);
                        }
                        operation = currentChar;
                        currentNumber = 0;
                    }
                }

                int result = 0;
                while (stack.Count() > 0)
                {
                    result += stack.Pop();
                }
                return result;
            }
        }

        // Learn this for interview
        class Solution3
        {
            class Node
            {
                public Node(string value, Node? left = null, Node? right = null)
                {
                    Value = value;
                    Left = left;
                    Right = right;
                }

                public string Value;
                public Node? Left;
                public Node? Right;
            }

            public int Calculate(string s)
            {
                var values = new Stack<Node>();
                var ops = new Stack<char>();

                int i = 0;
                while (i < s.Length)
                {
                    char c = s[i];

                    if (c == ' ')
                    {
                        i++;
                        continue;
                    }

                    if (char.IsDigit(c))
                    {
                        int j = i;
                        while (j < s.Length && char.IsDigit(s[j]))
                        {
                            j++;
                        }

                        values.Push(new Node(s[i..j]));
                        i = j;
                        continue;
                    }

                    if (c == '(')
                    {
                        ops.Push(c);
                        i++;
                        continue;
                    }

                    if (c == ')')
                    {
                        ReduceUntilOpenParen(values, ops);

                        ops.Pop(); // remove '('
                        i++;
                        continue;
                    }

                    if (c == '+' || c == '-')
                    {
                        // unary minus → push 0 first
                        if (c == '-' && IsUnary(s, i))
                        {
                            values.Push(new Node("0"));
                        }

                        ReduceUntilOpenParen(values, ops);

                        ops.Push(c);
                        i++;
                        continue;
                    }

                    i++;
                }

                while (ops.Count > 0)
                {
                    Reduce(values, ops);
                }

                var root = values.Pop();
                return Eval(root);
            }

            private void ReduceUntilOpenParen(Stack<Node> values, Stack<char> ops)
            {
                while (ops.Count > 0 && ops.Peek() != '(')
                {
                    Reduce(values, ops);
                }
            }

            private void Reduce(Stack<Node> values, Stack<char> ops)
            {
                char op = ops.Pop();
                Node right = values.Pop();
                Node left = values.Pop();

                values.Push(new Node(op.ToString(), left, right));
            }

            private bool IsUnary(string s, int i)
            {
                int j = i - 1;
                while (j >= 0 && s[j] == ' ') j--;

                return j < 0 || s[j] == '(';
            }

            private int Eval(Node node)
            {
                if (node.Left == null && node.Right == null)
                {
                    return (int)long.Parse(node.Value);
                }

                int left = Eval(node.Left!);
                int right = Eval(node.Right!);

                if (node.Value == "+") return left + right;
                return left - right;
            }
        }


        public class Solution4 //Basic Calculator III
        {
        public int Calculate(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return 0;

            Stack<int> values = new Stack<int>();
            Stack<char> ops = new Stack<char>();

            int i = 0;
            while (i < s.Length)
            {
                char c = s[i];

                if (c == ' ')
                {
                    i++;
                    continue;
                }

                if (char.IsDigit(c))
                {
                    int num = 0;
                    while (i < s.Length && char.IsDigit(s[i]))
                    {
                        num = num * 10 + (s[i] - '0');
                        i++;
                    }

                    values.Push(num);
                    continue;
                }

                if (c == '(')
                {
                    ops.Push(c);
                    i++;
                    continue;
                }

                if (c == ')')
                {
                    while (ops.Count > 0 && ops.Peek() != '(')
                    {
                        Reduce(values, ops);
                    }

                    ops.Pop(); // remove '('
                    i++;
                    continue;
                }

                // operator: +, -, *, /
                if ((c == '+' || c == '-') && IsUnary(s, i))
                {
                    values.Push(0);
                }

                while (ops.Count > 0 &&
                       ops.Peek() != '(' &&
                       Precedence(ops.Peek()) >= Precedence(c))
                {
                    Reduce(values, ops);
                }

                ops.Push(c);
                i++;
            }

            while (ops.Count > 0)
            {
                Reduce(values, ops);
            }

            return values.Pop();
        }

        private int Precedence(char op)
        {
            if (op == '+' || op == '-') return 1;
            if (op == '*' || op == '/') return 2;
            return 0;
        }

        private void Reduce(Stack<int> values, Stack<char> ops)
        {
            int right = values.Pop();
            int left = values.Pop();
            char op = ops.Pop();

            int result = 0;

            if (op == '+') result = left + right;
            else if (op == '-') result = left - right;
            else if (op == '*') result = left * right;
            else if (op == '/') result = left / right;

            values.Push(result);
        }

        private bool IsUnary(string s, int i)
        {
            int j = i - 1;

            while (j >= 0 && s[j] == ' ')
            {
                j--;
            }

            return j < 0 || s[j] == '(';
        }
    }


    // not needed for interview from HERE below
    class Solution123 // Basic Calculator |
        {
            public int Calculate(string s)
            {
                Stack<string> stack = new Stack<string>();

                int i = 0;
                while (i < s.Length)
                {
                    char c = s[i];
                    if (c == ' ' || c == '+')
                    {
                        i++;
                        continue;
                    }

                    if (char.IsDigit(c))
                    {
                        int j = i;
                        while (j < s.Length && char.IsDigit(s[j]))
                        {
                            j++;
                        }

                        string myNum = s[i..j];
                        stack.Push(myNum);

                        i = j ;
                        continue;
                    }

                    if (c == ')')
                    {
                        long accum = 0;
                        while (stack.Count > 0 && stack.TryPop(out var top) && top != "(")
                        {
                            if (long.TryParse(top, out long value))
                            {
                                if (stack.Count > 0 && stack.TryPeek(out var next) && next == "-")
                                {
                                    accum = accum - value;
                                    stack.Pop();
                                    continue;
                                }

                                accum += value;
                                continue;
                            }
                        }

                        stack.Push(accum.ToString());
                        i++;
                        continue;
                    }

                    stack.Push(c.ToString());

                    i++;
                }

                long result = 0;
                while (stack.Count > 0 && stack.TryPop(out var top))
                {
                    if (long.TryParse(top, out long value))
                    {
                        if (stack.Count > 0 && stack.TryPeek(out var next) && next == "-")
                        {
                            result = result - value;
                            stack.Pop();
                            continue;
                        }
                        result += value;
                    }
                }

                return (int)result;
            }
        }

        class Solution7 // Basic Calculator |
        {
            public int Calculate(string s)
            {
                Stack<string> stack = new Stack<string>();
                for (int i = s.Length-1; i >= 0; i--)
                {
                    char c = s[i];
                    if (c == ' ')
                    {
                        continue;
                    }

                    if (c == '+')
                    {
                        continue;
                    }

                    string toPush = "";

                    if (c == '-')
                    {
                        string last = stack.Pop();
                        if (last.StartsWith("-"))
                        {
                            toPush = last[1..];
                        } else
                        {
                            toPush = "-"+last;
                        }
                        continue;
                    }


                    if (char.IsDigit(c))
                    {
                        if(i< s.Length-1 && char.IsDigit(s[i + 1]))
                        {
                           var last = stack.Pop();
                            toPush = c.ToString()+last;
                        }  
                    } 

                    if(c == '(')
                    {
                        int accumulator = 0;
                        while (stack.TryPeek(out string top) && top != ")")
                        {
                            string last = stack.Pop();
                            
                            if (int.TryParse(last, out int value))
                            {
                                accumulator += value;
                            }
                        }

                        stack.Pop();
                        toPush = accumulator.ToString();
                    }

                    stack.Push(toPush);
                }

                int result = 0;

                while (stack.Count > 0)
                {
                    string last = stack.Pop();
                    if (int.TryParse(last, out int value))
                    {
                        result += value;
                    }
                }

                return result;
            }
        }

        class Solution5
        {
            private string Evaluate(char operation, string first, string second)
            {
                int x = int.Parse(first);
                int y = int.Parse(second);
                int res = 0;

                if (operation == '+') {
                    res = x;
                } else if (operation == '-') {
                    res = -x;
                } else if (operation == '*') {
                    res = x * y;
                } else
                {
                    res = x / y;
                }

                return res.ToString();
            }

            public int Calculate(string s)
            {
                Stack<string> stack = new();
                string curr = "";
                char previousOperator = '+';
                s += "@";

                HashSet<string> operators = new(new List<string>() { "+", "-", "*", "/" });

                foreach (char c in s)
                {
                    if (Char.IsDigit(c))
                    {
                        curr += c; // single number term
                        continue;
                    }

                    if (c == '(')
                    {
                        stack.Push(previousOperator.ToString()); // convert char to string before pushing
                        previousOperator = '+';
                        continue;
                    }

                    if (previousOperator == '*' || previousOperator == '/')
                    {
                        stack.Push(Evaluate(previousOperator, stack.Pop(), curr)); // puth the term
                    }
                    else // else + or -
                    {
                        stack.Push(Evaluate(previousOperator, curr, "0")); // puth the term
                    }

                    curr = string.Empty;
                    previousOperator = c;
                    if (c == ')')
                    {
                        int currentTerm = 0;
                        //while (Char.IsDigit(c) && !operators.Contains(c))
                        //{
                        //    currentTerm += int.Parse(stack.Pop());
                        //}

                        curr = currentTerm.ToString(); 
                        previousOperator = stack.Pop()[0]; // convert string from stack back to char
                    }
                }

                int ans = 0;
                foreach (string num in stack)
                {
                    ans += int.Parse(num);
                }

                return ans;
            }
        }

        class Solution423 // Basic Calculator ||
        {
            private int Evaluate(char operation, int x, int y)
            {
                if (operation == '+') {
                    return x;
                } else if (operation == '-') {
                    return -x;
                } else if (operation == '*') {
                    return x * y;
                }

                return x / y;
            }

            public int Calculate(string s)
            {
                Stack<int> stack = new();
                int curr = 0;
                char previousOperator = '+';
                s += "@";

                foreach (char c in s)
                {
                    if (c == ' ')
                    {
                        continue;
                    }

                    if (Char.IsNumber(c))
                    {
                        curr = curr * 10 + (int)(c - '0');
                        continue;
                    }

                    if (previousOperator == '*' || previousOperator == '/')
                    {
                        stack.Push(Evaluate(previousOperator, stack.Pop(), curr));
                    }
                    else
                    {
                        stack.Push(Evaluate(previousOperator, curr, 0));
                    }

                    curr = 0;
                    previousOperator = c;
                }

                int ans = 0;
                foreach (int num in stack)
                {
                    ans += num;
                }

                return ans;
            }
        }




        

        class Solution212
        {
            public int Calculate(string s)
            {
                Stack<char> stack = new Stack<char>();
                if(s == null) return 0;

                foreach(char c in s)
                {
                    if (c == ' ') continue;
                    if(c != ')')
                    {
                        stack.Push(c);
                        continue;
                    }
                    
                    string cur = "";
                    while (stack.Count > 0 && stack.Peek() != '(')
                    {
                        cur = stack.Pop() + cur;
                    }
                    stack.Pop();

                    int curInt = Cal(cur);
                    if (stack.Count > 0 && stack.Peek() == '-' && curInt < 0)
                    {
                        stack.Pop();
                        stack.Push('+');
                        curInt = -curInt;
                    }

                    string curString = curInt + "";
                    for (int i = 0; i < curString.Length; i++)
                    {
                        stack.Push(curString[i]);
                    }
                }

                string last = "";
                while (stack.Count > 0) last = stack.Pop() + last;

                return Cal(last);
            }

            public int Cal(string s)
            {
                int total = 0;
                if (s == null) return 0;

                int last = 0;
                int power = 0;

                for (int i = s.Length-1; i >= 0; i--)
                {
                    char cur = s[i];
                    if ( cur == ' ') continue;

                    if(Char.IsNumber(cur))
                    {
                        last += (cur - '0') * (int)Math.Pow(10, power);
                        power++;

                        continue;
                    }

                    if (cur == '+')
                    {
                        total += last;
                    } else if(cur == '-') {
                        total -= last;
                    }

                    power = 0;
                    last = 0;
                }

                total += last;
                return total;
            }
        }
    }
}
