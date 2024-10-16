using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace Learn.ParallelProcessing
{
    class Program
    {
        delegate string ConvertMethod(string inString);

        private static string UppercaseString(string inputString)
        {
            return inputString.ToUpper();
        }


        static void Main()
        {
            //tesNullCoalescingOperators();
            //testLambda();
            testFunc();
        }


        static void test1()
        {
            // 2 million
            var limit = 4_000_000;
            var numbers = Enumerable.Range(0, limit).ToList();

            var watch = Stopwatch.StartNew();
            var primeNumbersFromForeach = GetPrimeList(numbers);
            watch.Stop();

            var watchForParallel = Stopwatch.StartNew();
            var primeNumbersFromParallelForeach = GetPrimeListWithParallel(numbers);
            watchForParallel.Stop();

            Console.WriteLine($"Classical foreach loop | Total prime numbers : {primeNumbersFromForeach.Count} | Time Taken : {watch.ElapsedMilliseconds} ms.");
            Console.WriteLine($"Parallel.ForEach loop  | Total prime numbers : {primeNumbersFromParallelForeach.Count} | Time Taken : {watchForParallel.ElapsedMilliseconds} ms.");

            Console.WriteLine("Press 'Enter' to exit.");
            Console.ReadLine();
        }

        static void testFunc()
        {
            /*
            // Declare a Func variable and assign a lambda expression to the
            // variable. The method takes a string and converts it to uppercase.
            Func<string, string> selector = str => str.ToUpper();

            // Create an array of strings.
            string[] words = { "orange", "apple", "Article", "elephant" };
            // Query the array and select strings according to the selector method.
            IEnumerable<String> aWords = words.Select(selector);

            // Output the results to the console.
            foreach (String word in aWords)
                Console.WriteLine(word);
            */

            Func<string, string> convertMethod = UppercaseString;
            string name = "Dakota";
            // Use delegate instance to call UppercaseString method
            Console.WriteLine(convertMethod(name));
            /*
            This code example produces the following output:

            ORANGE
            APPLE
            ARTICLE
            ELEPHANT

            */
        }

        static void testLambda()
        {
            Action<string> greet = name =>
            {
                string greeting = $"Hello {name}!";
                Console.WriteLine(greeting);
            };
            greet("World");

            Action line = () => Console.WriteLine();
            Console.WriteLine(line);

            Func<double, double> cube = x => x * x * x;
            Console.WriteLine(cube(2));

            Func<int, int, bool> testForEquality = (x, y) => x == y;
            Console.WriteLine(testForEquality(2, 3));

            Func<int, string, bool> isTooLong = (int x, string s) => s.Length > x;
            Console.WriteLine(isTooLong(3, "123"));

            Func<int, int, int> constant = (_, _) => 42;
            Console.WriteLine(constant(1, 2));

            var IncrementBy = (int source, int increment = 1) => source + increment;

            Console.WriteLine(IncrementBy(5)); // 6
            Console.WriteLine(IncrementBy(5, 2)); // 7

            Func<(int, int, int), (int, int, int)> doubleThem = ns => (2 * ns.Item1, 2 * ns.Item2, 2 * ns.Item3);
            var numbers = (2, 3, 4);
            var doubledNumbers = doubleThem(numbers);
            Console.WriteLine($"The set {numbers} doubled: {doubledNumbers}");
            // Output:
            // The set (2, 3, 4) doubled: (4, 6, 8)
        }


        static void tesNullCoalescingOperators()
        {
            List<int>? numbers = null;
            int? a = null;

            Console.WriteLine((numbers is null)); // expected: true
                                                  // if numbers is null, initialize it. Then, add 5 to numbers
            (numbers ??= new List<int>()).Add(5);
            Console.WriteLine(string.Join(" ", numbers));  // output: 5
            Console.WriteLine((numbers is null)); // expected: false        


            Console.WriteLine((a is null)); // expected: true
            Console.WriteLine((a ?? 3)); // expected: 3 since a is still null 
                                         // if a is null then assign 0 to a and add a to the list
            numbers.Add(a ??= 0);
            Console.WriteLine((a is null)); // expected: false        
            Console.WriteLine(string.Join(" ", numbers));  // output: 5 0
            Console.WriteLine(a);  // output: 0
        }

        /// <summary>
        /// GetPrimeList returns Prime numbers by using sequential ForEach
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        private static IList<int> GetPrimeList(IList<int> numbers) => numbers.Where(IsPrime).ToList();

        /// <summary>
        /// GetPrimeListWithParallel returns Prime numbers by using Parallel.ForEach
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private static IList<int> GetPrimeListWithParallel(IList<int> numbers)
        {
            var primeNumbers = new ConcurrentBag<int>();

            Parallel.ForEach(numbers, number =>
            {
                if (IsPrime(number))
                {
                    primeNumbers.Add(number);
                }
            });

            return primeNumbers.ToList();
        }

        /// <summary>
        /// IsPrime returns true if number is Prime, else false.(https://en.wikipedia.org/wiki/Prime_number)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }

            for (var divisor = 2; divisor <= Math.Sqrt(number); divisor++)
            {
                if (number % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }


}
