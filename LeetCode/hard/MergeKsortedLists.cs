namespace LeetCode.hard
{
    public class MergeKsortedLists
    {
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        public void Test()
        {
        }

        public class Solution
        {
            public ListNode MergeKLists(ListNode[] lists)
            {
                ListNode head = new();
                var curr = head;

                SortedDictionary<int, int> counts = new();
                for (int i = 0; i < lists.Length; i++)
                {
                    ListNode node = lists[i];
                    while (node != null)
                    {
                        var count = counts.GetValueOrDefault(node.val, 0);
                        counts[node.val] = count + 1;
                        node = node.next;
                    }
                }

                foreach (var key in counts.Keys)
                {
                    for (int i = 0; i < counts[key]; i++)
                    {
                        var node = new ListNode(key);
                        curr.next = node;
                        curr = curr.next;

                    }
                }

                return head.next;
            }
        }

        public class SolutionTwoPointers
        {
            public ListNode MergeKLists(ListNode[] lists)
            {
                ListNode head = new();
                var curr = head;

                SortedDictionary<int, int> counts = new();
                for (int i = 0; i < lists.Length; i++)
                {
                    ListNode node = lists[i];
                    while (node != null)
                    {
                        var count = counts.GetValueOrDefault(node.val,0);
                        counts[node.val] = count + 1;
                        node = node.next;
                    }
                }

                foreach (var key in counts.Keys)
                {
                    for(int i = 0; i < counts[key]; i++)
                    {
                        var node = new ListNode(key);
                        curr.next = node;
                        curr = curr.next;

                    }
                }

                return head.next;
            }
        }

        public class SolutionHeap
        {
            public ListNode MergeKLists(ListNode[] lists)
            {
                PriorityQueue<int, int> heap = new();
                for (int i = 0; i < lists.Length; i++)
                {
                    ListNode node = lists[i];
                    while (node != null)
                    {
                        heap.Enqueue(node.val, node.val);
                        node = node.next;
                    }
                }
                ListNode head = null, curr = null;
                while (heap.Count > 0)
                {
                    var node = new ListNode();
                    node.val = heap.Dequeue();
                    if(head == null)
                    {
                        curr = node;
                        head = curr;
                        continue;
                    } 

                    curr.next = node;
                    curr = curr.next;
                }

                return head;
            }
        }
    }
}
