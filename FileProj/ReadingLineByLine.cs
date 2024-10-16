using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProj
{
    internal class ReadingLineByLine
    {
        public ReadingLineByLine()
        {
        }

        ~ReadingLineByLine()
        {
        }


        public void Test()
        {
            using (StreamReader sr = File.OpenText(""))
            {
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    //do minimal amount of work here
                }
            }


            foreach (string line in File.ReadAllLines(""))
            {

            }
        }
    }
}
