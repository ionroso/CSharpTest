using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProj
{
    internal class ReadToEnd
    {
        public void Test()
        {
            try
            {
                // Open the text file using a stream reader.
                using StreamReader reader = new StreamReader("TestFile.txt");
                // Read the stream as a string.
                string text = reader.ReadToEnd();
                // Write the text to the console.
                Console.WriteLine(text);
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }
        
        public async Task Test1()
        {
            try
            {
                // Open the text file using a stream reader.
                using StreamReader reader = new("TestFile.txt");

                // Read the stream as a string.
                string text = await reader.ReadToEndAsync();

                // Write the text to the console.
                Console.WriteLine(text);
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
