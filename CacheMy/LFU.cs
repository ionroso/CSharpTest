using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static CacheMy.LRU;

namespace CacheMy
{
    internal class LFU
    {

        public void Test()
        {
            LFUCache lfuCache = new LFUCache(2);
            lfuCache.Refer(1);
            lfuCache.Refer(2);
            lfuCache.Refer(1);
            lfuCache.Refer(3);
            lfuCache.Refer(2);
            lfuCache.Refer(4);
            lfuCache.Refer(5);
        }

        class Pair
        {
            public int value
            {
                get;
                set;
            }
            public int frequency
            {
                get;
                set;
            }

            public Pair(int value, int frequency)
            {
                this.value = value;
                this.frequency = frequency;
            }
        }

        class LFUCache
        {
            public int cacheSize
            {
                get;
                set;
            }
            public Dictionary<int, Pair> cache
            {
                get;
                set;
            }

            public LFUCache(int cacheSize)
            {
                this.cacheSize = cacheSize;
                this.cache = new Dictionary<int, Pair>();
            }

            // Self made heap to Rearranges
            // the nodes in order to maintain the heap property
            public void Increment(int value)
            {
                if (cache.ContainsKey(value))
                {
                    cache[value].frequency += 1;
                }
            }

            // Function to Insert a new node in the heap
            public void Insert(int value)
            {
                if (cache.Count == cacheSize)
                {
                    // remove least frequently used
                    int lfuKey = findLFU();
                    Console.WriteLine("Cache block " + lfuKey
                                    + " removed.");
                    cache.Remove(lfuKey);
                }

                Pair newPair = new Pair(value, 1);
                cache.Add(value, newPair);
                Console.WriteLine("Cache block " + value
                                + " inserted.");
            }

            // Function to refer to the block value in the cache
            public void Refer(int value)
            {
                if (!cache.ContainsKey(value))
                {
                    Insert(value);
                }
                else
                {
                    Increment(value);
                }
            }

            // Function to find the LFU block
            public int findLFU()
            {
                int lfuKey = 0;
                int minFrequency = Int32.MaxValue;
                foreach (KeyValuePair<int, Pair> entry in cache)
                {
                    if (entry.Value.frequency < minFrequency)
                    {
                        minFrequency = entry.Value.frequency;
                        lfuKey = entry.Key;
                    }
                }
                return lfuKey;
            }
        }
    }
}
