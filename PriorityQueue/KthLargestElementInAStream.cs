using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue
{
    internal class KthLargestElementInAStream
    {
        public void Test()
        {
            KthLargest kthLargest = new KthLargest(2, [4, 5, 8, 2]);
            Console.WriteLine(kthLargest.Add(3)); // return 4
            Console.WriteLine(kthLargest.Add(3)); // return 4
            Console.WriteLine(kthLargest.Add(5)); // return 5
            Console.WriteLine(kthLargest.Add(10)); // return 5
            Console.WriteLine(kthLargest.Add(9)); // return 8
            Console.WriteLine(kthLargest.Add(4)); // return 8
        }

        public class KthLargest
        {
            private PriorityQueue<int, int> pq;
            private int capacity;
            public KthLargest(int k, int[] nums)
            {
                capacity = k;

                pq = new();
                pq.EnsureCapacity(k);

                while (pq.TryDequeue(out int val, out int priority)) {
                    Console.Write($" {val} {priority}");
                }

                Console.WriteLine();
                foreach (var item in nums)
                {
                    pq.Enqueue(item, item);
                }

                while(pq.Count > capacity)
                {
                    _ = pq.Dequeue();
                }

                while (pq.TryDequeue(out int val, out int priority))
                {
                    Console.Write($" {val} {priority}");
                }

                Console.WriteLine();

            }

            public int Add(int val)
            {

                pq.Enqueue(val, val);


                while (pq.Count > capacity)
                {
                    _ = pq.Dequeue();
                }

                return pq.Peek();
            }
        }
    }
}
