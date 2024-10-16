namespace LeetCode.Medium
{
    public class MeetingRoomsII
    {
        public void Test()
        {
            int[][] input = new int[][] { new int[] { 1, 3 }, new int[] { 2, 9 } };
            var rez = new Solution().MinMeetingRooms(input);
            Console.WriteLine(rez);
        }

        public class Solution
        {
            public int MinMeetingRooms(int[][] intervals)
            {
                Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

                PriorityQueue<int, int> heap = new();
                heap.Enqueue(intervals[0][1], intervals[0][1]);

                for (int i = 1; i < intervals.Length; i++)
                {
                    var item = intervals[i];

                    if (item[0] >= heap.Peek())
                    {
                        heap.Dequeue();
                    }

                    heap.Enqueue(item[1], item[1]);
                }

                return heap.Count;
            }
        }

        public class Solution1
        {
            public int MinMeetingRooms(int[][] intervals)
            {
                if (intervals.Length == 0) { return 0; }

                //minHeap
                PriorityQueue<int, int> minHeap = new(intervals.Length);
                Array.Sort(intervals, Comparer<int[]>.Create((a, b) => a[0] - b[0]));//Sort the intervals by start time

                // Add the first meeting
                minHeap.Enqueue(intervals[0][1], intervals[0][1]);

                // Iterate over remaining intervals
                for (int i = 1; i < intervals.Length; i++)
                {
                    // If the room due to free up the earliest is free, assign that room to this meeting.
                    if (intervals[i][0] >= minHeap.Peek())
                    {
                        minHeap.Dequeue();
                    }
                    // If a new room is to be assigned, then also we add to the heap,
                    // If an old room is allocated, then also we have to add to the heap with updated end time.
                    minHeap.Enqueue(intervals[i][1], intervals[i][1]);
                }
                // The size of the heap tells us the minimum rooms required for all the meetings.
                return minHeap.Count;
            }
        }
    }
}
