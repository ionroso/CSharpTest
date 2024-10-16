using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Create and populate the SortedDictionary
        SortedDictionary<int, string> sortedDict = new SortedDictionary<int, string>
        {
            { 1, "One" },
            { 2, "Two" },
            { 3, "Three" },
            { 4, "Four" },
            { 5, "Five" }
        };

        // Define the range of keys
        int startKey = 2;
        int endKey = 4;

        //// Get the range of keys
        //var range = sortedDict.(startKey, endKey);

        //// Iterate and print the range
        //foreach (var kvp in range)
        //{
        //    Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        //}
    }
}
