using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProj
{
    internal class Create
    {


        static void Main(string[] args)
        {
            // Create a string with a line of text.
            string text = "First line" + Environment.NewLine;

            // Set a variable to the Documents path.
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the text to a new file named "WriteFile.txt".
            File.WriteAllText(Path.Combine(docPath, "WriteFile.txt"), text);

            // Create a string array with the additional lines of text
            string[] lines = { "New line 1", "New line 2" };

            // Append new lines of text to the file
            File.AppendAllLines(Path.Combine(docPath, "WriteFile.txt"), lines);
        }

        static void WriteLine(string[] args)
        {

            // Create a string array with the lines of text
            string[] lines = { "First line", "Second line", "Third line" };

            // Set a variable to the Documents path.
            string docPath =
              Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteLines.txt")))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }


        static async Task AppendAsync()
        {
            // Set a variable to the Documents path.
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the specified text asynchronously to a new file named "WriteTextAsync.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteTextAsync.txt")))
            {
                await outputFile.WriteAsync("This is a sentence.");
            }
        }

        static void Append(string[] args)
        {

            // Set a variable to the Documents path.
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Append text to an existing file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteLines.txt"), true))
            {
                outputFile.WriteLine("Fourth Line");
            }
        }
    // The example adds the following line to the contents of "WriteLines.txt":
    // Fourth Line

    class MyStream
            {
                private const string FILE_NAME = "Test.data";

                public static void Main()
                {
                    if (File.Exists(FILE_NAME))
                    {
                        Console.WriteLine($"{FILE_NAME} already exists!");
                        return;
                    }

                    using (FileStream fs = new FileStream(FILE_NAME, FileMode.CreateNew))
                    {
                        using (BinaryWriter w = new BinaryWriter(fs))
                        {
                            for (int i = 0; i < 11; i++)
                            {
                                w.Write(i);
                            }
                        }
                    }

                    using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader r = new BinaryReader(fs))
                        {
                            for (int i = 0; i < 11; i++)
                            {
                                Console.WriteLine(r.ReadInt32());
                            }
                        }
                    }
                }
            }


            // The example creates a file named "Test.data" and writes the integers 0 through 10 to it in binary format.
            // It then writes the contents of Test.data to the console with each integer on a separate line.
        }
}
