using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection
{
    internal class Dictionary
    {
        // Driver code 
        public static void Main()
        {

            // Create a new dictionary 
            // of strings, with string keys. 
            Dictionary<string, string> myDict =
            new Dictionary<string, string>();

            // Adding key/value pairs in myDict 
            myDict.Add("Australia", "Canberra");
            myDict.Add("Belgium", "Brussels");
            myDict.Add("Netherlands", "Amsterdam");
            myDict.Add("China", "Beijing");
            myDict.Add("Russia", "Moscow");
            myDict.Add("India", "New Delhi");

            // To get count of key/value pairs in myDict 
            Console.WriteLine("Total key/value pairs" +
                " in myDict are : " + myDict.Count);

            // To get the values alone, 
            // use the Values property. 
            Dictionary<string, string>.ValueCollection valueColl =
                                                    myDict.Values;

            // The elements of the ValueCollection 
            // are strongly typed with the type 
            // that was specified for dictionary values. 
            foreach (string s in valueColl)
            {
                Console.WriteLine("Value = {0}", s);
            }

            // The elements of the ValueCollection 
            // are strongly typed with the type 
            // that was specified for dictionary values. 
            foreach (var entry in myDict)
            {
                Console.WriteLine("Key = {0} Value={1}", entry.Key, entry.Value);
            }
        }
    }
}
