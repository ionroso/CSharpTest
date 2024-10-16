using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQTest
{
    internal class TestClass
    {
        public void Test()
        {
            var numbers = Enumerable.Range(1, 20);
            var parallelQuery = numbers.AsParallel().Where(n => n % 2 == 0).ToList();
            foreach (var num in parallelQuery)
            {
                Console.Write($"{num} ");
            }
            Console.ReadKey();
        }

        public void Test2()
        {
            static void Main(string[] args)
            {
                var numbers = Enumerable.Range(-5, 10); // This will include negative numbers, which will cause an exception when calculating the square root.
                try
                {
                    var query = numbers.AsParallel()
                                       .Select(number =>
                                       {
                                           if (number < 0)
                                               throw new ArgumentException($"Invalid number for square root: {number}");
                                           return Math.Sqrt(number);
                                       });
                    foreach (var result in query)
                    {
                        Console.WriteLine(result);
                    }
                }
                catch (AggregateException ae)
                {
                    ae.Flatten().Handle(ex => //!!!
                    {
                        if (ex is ArgumentException)
                        {
                            Console.WriteLine(ex.Message);
                            return true; // This indicates that the exception was handled.
                        }
                        return false; // This indicates that the exception was not handled here and should be re-thrown.
                    });
                }
                Console.ReadKey();
            }
        }

        static void ThreadSafeParallesation(string[] args)
        {
            // Example list of numbers
            var numbers = Enumerable.Range(1, 10000);
            // Thread-safe collection to store results
            var safeCollection = new ConcurrentBag<int>();
            try
            {
                // Parallel query to find squares of all even numbers
                numbers.AsParallel().Where(n => n % 2 == 0).ForAll(n =>
                {
                    var square = n * n;
                    safeCollection.Add(square); // Thread-safe operation
                });
                Console.WriteLine("Squares of even numbers have been calculated and stored successfully.");
            }
            catch (AggregateException ae)
            {
                // Handling exceptions gracefully
                Console.WriteLine("One or more exceptions occurred:");
                foreach (var ex in ae.InnerExceptions)
                {
                    Console.WriteLine($"   {ex.Message}");
                }
            }
            // Optional: Displaying a few results
            Console.WriteLine("Displaying first 10 results:");
            safeCollection.Take(10).ToList().ForEach(s => Console.WriteLine(s));
            Console.ReadKey();
        }

        void AutomaticPartitioning(string[] args)
        {
            // Generate a list of numbers
            var numbers = Enumerable.Range(1, 1000000);
            // Start stopwatch for timing
            Stopwatch stopwatch = Stopwatch.StartNew();
            // Use PLINQ with AsParallel for automatic partitioning
            var primeNumbers = numbers.AsParallel()
                                      .Where(IsPrime)
                                      .ToList();
            stopwatch.Stop();
            Console.WriteLine($"Total prime numbers: {primeNumbers.Count}");
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");
            // Optional: print a few prime numbers
            primeNumbers.Take(10).ToList().ForEach(n => Console.WriteLine(n));
            Console.ReadKey();
        }

        // Method to check if a number is prime
        bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            var boundary = (int)Math.Floor(Math.Sqrt(number));
            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            // Example list of sales records
            List<SaleRecord> sales = new List<SaleRecord>
            {
                new SaleRecord { Category = "Electronics", Amount = 1000 },
                new SaleRecord { Category = "Books", Amount = 500 },
                new SaleRecord { Category = "Electronics", Amount = 1500 },
                new SaleRecord { Category = "Clothing", Amount = 700 },
                // Add more records as needed
            };
            // Perform the aggregation
            var categoryTotals = sales
                .AsParallel()
                .Aggregate(
                    // Seed factory function to initialize the local total for each thread
                    () => new Dictionary<string, decimal>(),
                    // Accumulator function to sum amounts by category locally on each thread
                    (localTotals, sale) =>
                    {
                        if (!localTotals.ContainsKey(sale.Category))
                            localTotals[sale.Category] = 0;
                        localTotals[sale.Category] += sale.Amount;
                        return localTotals;
                    },
                    // Combiner function to merge local totals from each thread
                    (mainTotals, localTotals) =>
                    {
                        foreach (var localTotal in localTotals)
                        {
                            if (!mainTotals.ContainsKey(localTotal.Key))
                                mainTotals[localTotal.Key] = 0;
                            mainTotals[localTotal.Key] += localTotal.Value;
                        }
                        return mainTotals;
                    },
                    // Final result selector, could be used to transform the result but just returns it here
                    finalTotals => finalTotals
                );
            // Display the results
            foreach (var categoryTotal in categoryTotals)
            {
                Console.WriteLine($"Category: {categoryTotal.Key}, Total Sales: {categoryTotal.Value}");
            }
            Console.ReadKey();
        }
    }
    public class SaleRecord
    {
        public string Category { get; set; }
        public decimal Amount { get; set; }
    }
}
