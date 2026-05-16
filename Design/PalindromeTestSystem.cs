using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    class Solution
    {

        // abba - polindrome string 
        // approach 1 - use two pointers - use while or for loop - iterative version
        // recursion
        // test1 abba
        // lPtr=2 rPtr=1 - success
        // test 2 aba 
        // lPtr=1 rPtr=1 - success
        // test 3 abc - success


        // Desing test system
        // in parallel - optional
        // actual to expected
        // 
        record Result(bool Sucess, string? Error);

        class Input
        {
            public string Data { get; }
            public bool Expected { get; }
            public Input(string str, bool expected)
            {
                this.Data = str;
                this.Expected = expected;
            }

        }

        class TestSystem
        {
            private Polindrome _testObject;

            public TestSystem()
            {
                _testObject = new Polindrome();
            }

            public Result Test(Input testThis)
            {
                var actual = _testObject.CheckIfPolindrom(testThis.Data);

                Result result = actual == testThis.Expected ? new(true, null) : new(false, "Test has failed");

                Console.WriteLine($"Test result of input ( data:{testThis.Data} expected: {testThis.Expected}, actual {result}");

                return result;
            }
        }

        static void Main1(String[] args)
        {
            string str = Convert.ToString(Console.ReadLine());

            var testSrv = new TestSystem();

            // positive cases
            testSrv.Test(new Input("abba", true));
            testSrv.Test(new Input("aba", true));
            testSrv.Test(new Input("abc", false));

            //negaitve cases
            testSrv.Test(new Input("abba", false));
        }
        // didnt asks emptry sting is is polindrome?
        // didn't ask if a chanracter is polindrome?
        // why i did to char array? - to avoid string indexing and use char array indexing which is faster
        class Polindrome
        {
            public bool CheckIfPolindrom(string str)
            {
                if (string.IsNullOrEmpty(str))
                {
                    return false;
                }

                if (str.Length == 1)
                {
                    return false;
                }

                int n = str.Length;
                char[] chars = str.ToCharArray();
                int lPtr = 0, rPtr = n - 1;

                while (lPtr < rPtr)
                {
                    if (chars[lPtr++] != chars[rPtr--])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }

}
