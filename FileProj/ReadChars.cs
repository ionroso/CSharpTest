using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProj
{
    internal class ReadChars
    {
        public class CharsFromStr
        {
            public static void Main()
            {
                string str = "Some number of characters";
                char[] b = new char[str.Length];

                using (StringReader sr = new StringReader(str))
                {
                    // Read 13 characters from the string into the array.
                    sr.Read(b, 0, 13);
                    Console.WriteLine(b);

                    // Read the rest of the string starting at the current string position.
                    // Put in the array starting at the 6th array member.
                    sr.Read(b, 5, str.Length - 13);
                    Console.WriteLine(b);
                }
            }
        }
        // The example has the following output:
        //
        // Some number o
        // Some f characters
    }

    /*
        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        public partial class MainWindow : Window
        {
            public MainWindow()
            {
                InitializeComponent();
            }

            private async void Window_Loaded(object sender, RoutedEventArgs e)
            {
                char[] charsRead = new char[UserInput.Text.Length];
                using (StringReader reader = new StringReader(UserInput.Text))
                {
                    await reader.ReadAsync(charsRead, 0, UserInput.Text.Length);
                }

                StringBuilder reformattedText = new StringBuilder();
                using (StringWriter writer = new StringWriter(reformattedText))
                {
                    foreach (char c in charsRead)
                    {
                        if (char.IsLetter(c) || char.IsWhiteSpace(c))
                        {
                            await writer.WriteLineAsync(char.ToLower(c));
                        }
                    }
                }
                Result.Text = reformattedText.ToString();
            }
        }
    */
}
