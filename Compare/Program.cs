using System;
using System.Collections.Generic;
using System.Linq;

public static class Demo
{
    public static void Main()
    {
        // ------------------------------------------------------------
        // CASE 1: SIMPLE VALUES (int, bool, string, etc.)
        // For primitive/value-like outputs, default equality is enough.
        // ------------------------------------------------------------

        int actualInt = 5;
        int expectedInt = 5;

        bool okInt = EqualityComparer<int>.Default.Equals(actualInt, expectedInt);
        Console.WriteLine($"Int compare -> {okInt}");   // true

        string actualString = "hello";
        string expectedString = "hello";

        bool okString = EqualityComparer<string>.Default.Equals(actualString, expectedString);
        Console.WriteLine($"String compare -> {okString}"); // true


        // ------------------------------------------------------------
        // CASE 2: ARRAYS / LISTS
        // Default equality usually compares OBJECT REFERENCE, not contents.
        // So two separate arrays/lists with the same values may still be "not equal".
        // ------------------------------------------------------------

        int[] actualArray = { 1, 2, 3 };
        int[] expectedArray = { 1, 2, 3 };

        bool arrayDefaultEqual =
            EqualityComparer<int[]>.Default.Equals(actualArray, expectedArray);
        Console.WriteLine($"Array with Default.Equals -> {arrayDefaultEqual}"); // false

        // Correct way for ordered collections:
        bool arraySequenceEqual = actualArray.SequenceEqual(expectedArray);
        Console.WriteLine($"Array with SequenceEqual -> {arraySequenceEqual}"); // true


        List<int> actualList = new List<int> { 1, 2, 3 };
        List<int> expectedList = new List<int> { 1, 2, 3 };

        bool listDefaultEqual =
            EqualityComparer<List<int>>.Default.Equals(actualList, expectedList);
        Console.WriteLine($"List with Default.Equals -> {listDefaultEqual}"); // false

        // Correct way for lists too:
        bool listSequenceEqual = actualList.SequenceEqual(expectedList);
        Console.WriteLine($"List with SequenceEqual -> {listSequenceEqual}"); // true


        // ------------------------------------------------------------
        // INTERVIEW RULE:
        // - Use EqualityComparer<T>.Default.Equals(...) for scalar outputs
        //   like int, bool, string.
        // - Use SequenceEqual(...) for arrays/lists when order matters.
        // ------------------------------------------------------------

    }
}