using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue
{
    internal class KthLargestElementInAnArray
    {
        public void Test()
        {
            KthLargest kthLargest = new KthLargest();
            Console.WriteLine(kthLargest.FindKthLargest([4, 5, 8, 2],2));
        }

        public class KthLargest
        {

            public int FindKthLargest(int[] nums, int k)
            {
                PriorityQueue<int, int> pq = new();
                foreach (int item in nums)
                {
                    pq.Enqueue(item, item);
                    if (pq.Count > k)
                    {
                        _ = pq.Dequeue();
                    }
                }

                return pq.Peek();
            }
        }
    }
}
