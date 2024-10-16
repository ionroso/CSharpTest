namespace LeetCode.Medium
{
    public class TopKfrequentElements
    {
        public void Test()
        {
            int[][] input = new int[][] { new int[] { 1, 3 }, new int[] { 2, 9 } };
            var rez = new Solution().TopKFrequent(new int[] { 1, 1, 1, 2, 2, 3 }, 2);
            Console.WriteLine(rez);
        }

    }

    public class PriorityItem
    {
        public int Value { get; set; }
        public int Count { get; set; }
    }

    public class Solution
    {
        public int[] TopKFrequent(int[] nums, int k)
        {
            Dictionary<int, int> counts = new();
            foreach (var num in nums)
            {
                var count = counts.GetValueOrDefault(num, 0);
                counts[num] = count+1;
            }

            var maxHeap = new PriorityQueue<PriorityItem, PriorityItem>(Comparer<PriorityItem>.Create((p1, p2) => p2.Count - p1.Count));
            foreach (var entity in counts)
            {
                var item = new PriorityItem() { Value = entity.Key, Count = entity.Value };
                maxHeap.Enqueue(item, item);
            }

            var output = new int[k];
            for (int i = 0; i < k; i++)
            {
                output[i] = maxHeap.Dequeue().Value;
            }

            return output;
        }
    }
}
