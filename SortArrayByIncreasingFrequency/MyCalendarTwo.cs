using System.Xml.Linq;

namespace Intervals
{

    internal class MyCalendarTwo
    {

        public void Test()
        {
            var cal = new MyCalendar();
            Console.WriteLine(cal.Book(10, 20));
            Console.WriteLine(cal.Book(15, 25));
            Console.WriteLine(cal.Book(20, 30));
        }

        public class MyCalendar
        {
            private readonly SortedList<int, int> _events; // !!!! works with SortedDictionary, just replce, performance must worst
            public MyCalendar()
            {
                _events = new();
            }

            public bool Book(int start, int end)
            {
                int[] bookEvent = new int[] { start, end };

                if (_events.Count == 0)
                {
                    _events.Add(start, end);
                    return true;
                }

                var keys = _events.Keys.ToList();
                int index = keys.BinarySearch(start);

                if (index >= 0)
                {
                    return false;
                }

                var insertIndex = ~index;

                var intersectPrevious = false;
                if (insertIndex - 1 >= 0)
                {
                    var key = keys[insertIndex - 1];
                    intersectPrevious = Intersect(bookEvent, new int[] { key, _events[key] });
                }

                var intersectCurrent = false;
                if (insertIndex < keys.Count)
                {
                    var key = keys[insertIndex];
                    intersectCurrent = Intersect(bookEvent, new int[] { key, _events[key] });
                }


                var intersectNext = false;
                if (insertIndex + 1 < keys.Count)
                {
                    var key = keys[insertIndex + 1];
                    intersectNext = Intersect(bookEvent, new int[] { key, _events[key] });
                }

                if (!intersectPrevious && !intersectCurrent && !intersectNext)
                {
                    _events.Add(start, end);
                    return true;
                }

                return false;
            }

            private bool Intersect(int[] event1, int[] event2)
            {
                int low = Math.Max(event1[0], event2[0]);
                int high = Math.Min(event1[1], event2[1]);

                return low < high;
            }
        }

        public class MyCalendarList
        {
            private readonly List<int[]> _events;
            public MyCalendarList()
            {
                _events = new List<int[]>();
                //_events.Add(new int[] { 1, 5 });
                //_events.Add(new int[] { 11, 20 });
                //_events.Add(new int[] { 21, 30 });
            }

            public bool Book(int start, int end)
            {
                int[] bookEvent = new int[] { start, end };

                if (_events.Count == 0)
                {
                    _events.Add(bookEvent);
                    return true;
                }

                int index = _events.BinarySearch(bookEvent, Comparer<int[]>.Create((a, b) => a[0].CompareTo(b[0])));
                if (index >= 0)
                {
                    return false;
                }

                var insertIndex = ~index;

                var intersectPrevious = false;
                if (insertIndex - 1 >= 0)
                {
                    intersectPrevious = Intersect(bookEvent, _events[insertIndex - 1]);
                }

                var intersectCurrent = false;
                if (insertIndex < _events.Count)
                {
                    intersectCurrent = Intersect(bookEvent, _events[insertIndex]);
                }


                var intersectNext = false;
                if (insertIndex + 1 < _events.Count)
                {
                    intersectNext = Intersect(bookEvent, _events[insertIndex + 1]);
                }

                if (!intersectPrevious && !intersectCurrent && !intersectNext)
                {
                    _events.Insert(insertIndex, bookEvent);
                    return true;
                }

                return false;
            }

            private bool Intersect(int[] event1, int[] event2)
            {
                int low = Math.Max(event1[0], event2[0]);
                int high = Math.Min(event1[1], event2[1]);

                return low < high;
            }
        }


        public class MyCalendarBST
        {
            private class Node
            {
                public int start, end;
                public Node? left, right;

                public Node(int start, int end)
                {
                    this.start = start;
                    this.end = end;
                    left = null;
                    right = null;
                }
            }

            private Node? root;

            public bool Book(int start, int end)
            {
                var insert = new Node(start, end);
                if (root is null)
                {
                    root = insert;
                    return true;
                }
                return Insert(root, insert);
            }

            private bool Insert(Node root, Node insert)
            {
                if (insert.end <= root.start)
                {
                    if (root.left is null)
                    {
                        root.left = insert;
                        return true;
                    }
                    else
                    {
                        return Insert(root.left, insert);
                    }
                }

                if (insert.start >= root.end)
                {
                    if (root.right is null)
                    {
                        root.right = insert;
                        return true;
                    }
                    else
                    {
                        return Insert(root.right, insert);
                    }
                }

                return false;
            }
        }
    }
}
