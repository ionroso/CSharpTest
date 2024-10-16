using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheMy
{
    internal class LRU
    {
        public void Test()
        {
            LRUCache lRUCache = new LRUCache(2);
            lRUCache.Get(2);
            lRUCache.Put(2, 6);
            lRUCache.Get(1);
            lRUCache.Put(1, 5);
            lRUCache.Put(1, 2);
            lRUCache.Get(1);
            lRUCache.Get(2);
        }

        public class LRUCache
        {
            private class Node
            {
                public int key;
                public int value;
                public Node next;
                public Node prev;

                public Node(int key, int value)
                {
                    this.key = key;
                    this.value = value;
                }
            }

            private int capacity;
            private int size;

            private Node head;
            private Node tail;

            Dictionary<int, Node> valueToKey;

            public LRUCache(int capacity)
            {
                this.capacity = capacity;
                this.valueToKey = new();

                this.tail = new Node(-1, -1);
                this.head = new Node(-1, -1);

                tail.next = head;
                head.prev = tail;
            }

            public int Get(int key)
            {
                if (valueToKey.TryGetValue(key, out var node))
                {
                    MoveToHead(node);
                    return node.value;
                }
                return -1;
            }

            public void Put(int key, int value)
            {
                if (valueToKey.TryGetValue(key, out var node))
                {
                    RemoveNode(node);
                }

                AddToHead(new Node(key, value));

                if (IsOverCapacity())
                {
                    RemoveNode(tail.next);
                }
            }

            private bool IsOverCapacity() => size > capacity;

            private void MoveToHead(Node node)
            {
                RemoveNode(node);
                AddToHead(new Node(node.key, node.value));
            }

            private void AddToHead(Node node)
            {
                head.prev.next = node;
                node.prev = head.prev;
                node.next = head;
                head.prev = node;
                
                valueToKey[node.key] = node;

                size++;
            }

            private void RemoveNode(Node node)
            {
                node.next.prev = node.prev;
                node.prev.next = node.next;

                node.next = null;
                node.prev = null;

                valueToKey.Remove(node.key);

                size --;
            }
        }

    }
}
