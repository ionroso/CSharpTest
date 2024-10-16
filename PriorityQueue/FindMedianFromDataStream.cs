using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue
{
    internal class FindMedianFromDataStream
    {
        public void Test()
        {
            MedianFinder medianFinder = new MedianFinder();
            medianFinder.AddNum(1);    // arr = [1]
            medianFinder.AddNum(2);    // arr = [1, 2]
            medianFinder.FindMedian(); // return 1.5 (i.e., (1 + 2) / 2)
            medianFinder.AddNum(3);    // arr[1, 2, 3]
            medianFinder.FindMedian(); // return 2.0
        }

        public class MedianFinder
        {
            private PriorityQueue<int, int> rightPQ;
            private PriorityQueue<int, int> leftPQ;

            public MedianFinder()
            {
                rightPQ = new PriorityQueue<int, int>();
                leftPQ = new PriorityQueue<int, int>(Comparer<int>.Create((o1,o2) => o2 - o1));
            }

            public void AddNum(int num)
            {
                if (leftPQ.Count == 0)
                {
                    leftPQ.Enqueue(num,num);
                    return;
                }

                if (leftPQ.Count == 1 && rightPQ.Count == 0)
                {
                    if(num > leftPQ.Peek())
                    {
                        rightPQ.Enqueue(num, num);
                    } else
                    {
                        MoveOneItem(from: leftPQ, to: rightPQ);
                        leftPQ.Enqueue(num, num);
                    }
                    return;
                }

                if ( num > leftPQ.Peek() )
                {
                    rightPQ.Enqueue(num, num);
                }
                else
                {
                    leftPQ.Enqueue(num, num);
                }

                Balance();
            }

            private void Balance()
            {
                if(rightPQ.Count - leftPQ.Count > 1)
                {
                    MoveOneItem(from: rightPQ, to: leftPQ);
                }

                if (leftPQ.Count - rightPQ.Count > 1)
                {
                    MoveOneItem(from: leftPQ, to: rightPQ);
                }


            }

            private void MoveOneItem(PriorityQueue<int, int> from, PriorityQueue<int, int> to)
            {
                to.Enqueue(from.Peek(), from.Dequeue());
            }

            public double FindMedian()
            {
                if (rightPQ.Count - leftPQ.Count == 0)
                {
                    return (double)(rightPQ.Peek() + leftPQ.Peek() ) / 2;
                }

                var temp = rightPQ.Count > leftPQ.Count ? rightPQ : leftPQ;

                return temp.Peek();
            }
        }
    }
}
