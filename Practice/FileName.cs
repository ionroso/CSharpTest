using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class FileName
    {
    }

    class Solution
    {
        public int[] MaxSubsequence(int[] nums, int k)
        {

            PriorityQueue<(int n, int index), int> pq = new();

            for (int i = 0; i < nums.Length; i++)
            {
                int n = nums[i];
                pq.Enqueue((n, i), n);
                if (pq.Count > k)
                {
                    pq.Dequeue();
                }
            }

            (int n, int index)[] rez = new (int n, int index)[k];
            int i = 0;
            while (pq.Count > 0)
            {
                rez[i++] = pq.Dequeue();
            }

            Array.Sort((a, b) => a.index.CompareTo(b.index));

            int[] output = new int[k];
            for (int i = 0; i < k; i++)
            {
                output[i] = rez[i].n;
            }

            return output;
        }
    }
}
