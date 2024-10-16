
namespace LeetCode.hard
{
    internal class FindMedianFromDataStream
    {
        public void Test()
        {
            int[][] input = new int[][] { new int[] { 1, 3 }, new int[] { 2, 9 } };
            var rez = new MedianFinder();
            Console.WriteLine(rez);
        }

        public class MedianFinder
        {
            private PriorityQueue<int, int> rightPQ;
            private PriorityQueue<int, int> leftPQ;

            public MedianFinder()
            {
                rightPQ = new PriorityQueue<int, int>(Comparer<int>.Create((o1, o2) => o1 - o2));
                leftPQ = new PriorityQueue<int, int>(Comparer<int>.Create((o1, o2) => o2 - o1));
            }

            public void AddNum(int num)
            {
                leftPQ.Enqueue(num, num);

                if (rightPQ.Count - leftPQ.Count > 1)
                {
                    var top = rightPQ.Dequeue();
                    leftPQ.Enqueue(top, top);
                }
            }

            public double FindMedian()
            {
                if (rightPQ.Count - leftPQ.Count == 0)
                {
                    return (double)(rightPQ.Peek() + leftPQ.Peek()) / 2;
                }

                var temp = rightPQ.Count > leftPQ.Count ? rightPQ : leftPQ;

                return temp.Peek();
            }
        }
    }
}
