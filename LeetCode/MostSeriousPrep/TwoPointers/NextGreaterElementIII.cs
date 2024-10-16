
namespace LeetCode.MostSeriousPrep.TwoPointers
{
    public class NextGreaterElementIII
    {
        internal void Test()
        {
            Console.WriteLine(new Solution().NextGreaterElement(12));
            ;
        }

        public class Solution
        {
            public int NextGreaterElement(int n)
            {
                var array = n.ToString().ToArray();

                int len = array.Length;

                int i = len - 2;
                while (i >= 0 && array[i + 1] <= array[i])
                {
                    i--;
                }

                if (i < 0) return -1;

                Array.Sort(array, i + 1, len - i - 1);

                int j = i + 1;
                while (j < len && array[j] <= array[i])
                {
                    j++;
                }

                char temp = array[j];
                array[j] = array[i];
                array[i] = temp;

                try
                {
                    return int.Parse(array);
                }
                catch { }

                return -1;
            }
        }
    }
}
