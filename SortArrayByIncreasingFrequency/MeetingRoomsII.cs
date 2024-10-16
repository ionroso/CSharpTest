using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Intervals
{
    internal class MeetingRoomsII
    {
        public class Solution
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
