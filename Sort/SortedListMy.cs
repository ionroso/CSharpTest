using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{

    public class YearThenWeight
    {
        public int year;
        public int wieght;
    }

    public class Weight
    {
        public int wieght;
    }

    public class CustomComparer : IComparer<YearThenWeight>
    {
        public int Compare(YearThenWeight x, YearThenWeight y)
        {
            // Custom comparison logic
            if (x.year == y.year) return x.wieght.CompareTo(y.wieght);

            return x.year.CompareTo(y.year);
        }
    }

    internal class SortedListMy
    {
        public void Test1()
        {
            SortedList<int, int > sortedList = new SortedList<int, int>();
            sortedList.Add(1, 1);
            sortedList.Add(2, 2);
            sortedList.Add(1, 3);
            sortedList.Add(4, 5);
        }
        public void Test()
        {
            SortedList<YearThenWeight, YearThenWeight> sortedList = new SortedList<YearThenWeight, YearThenWeight>(new CustomComparer());
            var y1 = new YearThenWeight() {  year = 2024, wieght = 2 };
            var y2 = new YearThenWeight() {  year = 2024, wieght = 1 };
            var y3 = new YearThenWeight() {  year = 2024, wieght = 6 };
            var y4 = new YearThenWeight() {  year = 2024, wieght = 2 };
            var y5 = new YearThenWeight() {  year = 2024, wieght = 8 };


            sortedList.Add(y1, y1);
            sortedList.Add(y2, y2);
            sortedList.Add(y3, y3);
            sortedList.Add(y4, y4);
            sortedList.Add(y5, y5);

            foreach (var x in sortedList)
            {
                Console.WriteLine($"{x.Key.year} {x.Key.year}" );
            }

        }
    }
}
