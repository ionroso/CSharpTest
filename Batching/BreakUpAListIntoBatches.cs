using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection
{
    internal class BreakUpAListIntoBatches
    {
        public void Test()
        {
            Chunk ch = new Chunk();
            List<int> fullList = new List<int>() { 43, 65, 23, 56, 76, 454, 76, 54 };
            var chunked = ch.BuildChunks(fullList, 3).ToList();

            Assert.AreEqual(3, chunked.Count);
            CollectionAssert.AreEqual(new List<int> { 43, 65, 23 }, chunked[0].ToList());
            CollectionAssert.AreEqual(new List<int> { 56, 76, 454 }, chunked[1].ToList());
            CollectionAssert.AreEqual(new List<int> { 76, 54 }, chunked[2].ToList());
        }


        public class Chunk
        {
            public IEnumerable<IEnumerable<T>> BuildChunks<T>(List<T> fullList, int batchSize)
            {
                //return BuildChunksWithRange(fullList, batchSize);
                //return BuildChunksWithIteration(fullList, batchSize);
                //return BuildChunksWithLinq(fullList, batchSize);
                //return BuildChunksWithLinqAndYield(fullList, batchSize);
                //return SplitList(fullList, batchSize);
                //return SplitListGeneric(fullList, batchSize);
                return Batch(fullList, batchSize);
            }

            // Skip is unifficient dont use it
            private IEnumerable<IEnumerable<T>> BuildChunksWithLinq<T>(List<T> fullList, int batchSize)
            {
                int total = 0;
                var chunkedList = new List<List<T>>();
                while (total < fullList.Count)
                {
                    var chunk = fullList.Skip(total).Take(batchSize);
                    chunkedList.Add(chunk.ToList());
                    total += batchSize;
                }

                return chunkedList;
            }


            IEnumerable<IEnumerable<T>> Batch1<T>(List<T> items, int batchSize)
            {
                if (items == null)
                    throw new ArgumentNullException(nameof(items));

                if (batchSize <= 0)
                    throw new ArgumentException("Batch size must be greater than zero.", nameof(batchSize));

                IEnumerable<IGrouping<int, Result<T>>> groups =
                    items.Select((item, index) => new Result<T>(item, index))
                         .GroupBy(x => x.Index / batchSize);

                return groups.Select(group => group.Select(result => result.Item));
            }

            public static IEnumerable<IEnumerable<T>> Batch<T>(List<T> items, int batchSize)
            {
                return items.Select((item, index) => new { item, index })
                            .GroupBy(x => x.index / batchSize)
                            .Select(g => g.Select(x => x.item));
            }


            public static List<List<T>> Batch2<T>(List<T> items, int batchSize)
            {
                if (items == null)
                    throw new ArgumentNullException(nameof(items));

                if (batchSize <= 0)
                    throw new ArgumentException("Batch size must be greater than zero.", nameof(batchSize));

                return items
                    .Select((item, index) => new { item, index })
                    .GroupBy(x => x.index / batchSize)
                    .Select(g => g.Select(x => x.item).ToList())
                    .ToList();
            }


            public static IEnumerable<IEnumerable<T>> SplitList<T>(List<T> locations, int batchSize)
            {
                var list = new List<List<T>>();

                for (int i = 0; i < locations.Count; i += batchSize)
                {
                    list.Add(locations.GetRange(i, Math.Min(batchSize, locations.Count - i)));
                }

                return list;
            }
            
            public static IEnumerable<IEnumerable<T>> SplitListGeneric<T>(List<T> locations, int batchSize)
            {

                for (int i = 0; i < locations.Count; i += batchSize)
                {
                   yield return locations.GetRange(i, Math.Min(batchSize, locations.Count - i));
                }
            }

            private IEnumerable<IEnumerable<T>> BuildChunksWithLinqAndYield<T>(List<T> fullList, int batchSize)
            {
                int total = 0;
                while (total < fullList.Count)
                {
                    yield return fullList.Skip(total).Take(batchSize);
                    total += batchSize;
                }
            }

            private IEnumerable<IEnumerable<T>> BuildChunksWithIteration<T>(List<T> fullList, int batchSize)
            {
                var chunkedList = new List<List<T>>();
                var temporary = new List<T>();
                for (int i = 0; i < fullList.Count; i++)
                {
                    var e = fullList[i];
                    if (temporary.Count < batchSize)
                    {
                        temporary.Add(e);
                    }
                    else
                    {
                        chunkedList.Add(temporary);
                        temporary = new List<T>() { e };
                    }

                    if (i == fullList.Count - 1)
                    {
                        chunkedList.Add(temporary);
                    }
                }

                return chunkedList;
            }

            private IEnumerable<IEnumerable<T>> BuildChunksWithRange<T>(List<T> fullList, int batchSize)
            {
                List<List<T>> chunkedList = new List<List<T>>();
                int index = 0;

                while (index < fullList.Count)
                {
                    int rest = fullList.Count - index;
                    chunkedList.Add(fullList.GetRange(index, Math.Min(rest, batchSize)));
                    //if (rest >= batchSize)
                    //{
                    //    chunkedList.Add(fullList.GetRange(index, batchSize));
                    //}
                    //else
                    //{
                    //    chunkedList.Add(fullList.GetRange(index, rest));
                    //}
                    index += batchSize;
                }

                return chunkedList;
            }
        }




        [TestMethod()]
        public void BuildChunksTests()
        {
            Chunk ch = new Chunk();
            List<int> fullList = new List<int>() { 43, 65, 23, 56, 76, 454, 76, 54, 987 };
            var chunked = ch.BuildChunks(fullList, 3).ToList();

            Assert.AreEqual(3, chunked.Count);
            CollectionAssert.AreEqual(new List<int> { 43, 65, 23 }, chunked[0].ToList());
            CollectionAssert.AreEqual(new List<int> { 56, 76, 454 }, chunked[1].ToList());
            CollectionAssert.AreEqual(new List<int> { 76, 54, 987 }, chunked[2].ToList());

            chunked = ch.BuildChunks(fullList, 2).ToList();
            Assert.AreEqual(5, chunked.Count);
            CollectionAssert.AreEqual(new List<int> { 43, 65 }, chunked[0].ToList());
            CollectionAssert.AreEqual(new List<int> { 23, 56 }, chunked[1].ToList());
            CollectionAssert.AreEqual(new List<int> { 76, 454 }, chunked[2].ToList());
            CollectionAssert.AreEqual(new List<int> { 76, 54 }, chunked[3].ToList());
            CollectionAssert.AreEqual(new List<int> { 987 }, chunked[4].ToList());

            chunked = ch.BuildChunks(fullList, 5).ToList();
            Assert.AreEqual(2, chunked.Count);
            CollectionAssert.AreEqual(new List<int> { 43, 65, 23, 56, 76 }, chunked[0].ToList());
            CollectionAssert.AreEqual(new List<int> { 454, 76, 54, 987 }, chunked[1].ToList());

            chunked = ch.BuildChunks(fullList, 10).ToList();
            Assert.AreEqual(1, chunked.Count);
            CollectionAssert.AreEqual(new List<int> { 43, 65, 23, 56, 76, 454, 76, 54, 987 }, chunked[0].ToList());

            chunked = ch.BuildChunks(fullList, 1).ToList();
            Assert.AreEqual(9, chunked.Count);
            CollectionAssert.AreEqual(new List<int> { 43 }, chunked[0].ToList());
            CollectionAssert.AreEqual(new List<int> { 65 }, chunked[1].ToList());
            CollectionAssert.AreEqual(new List<int> { 23 }, chunked[2].ToList());
            CollectionAssert.AreEqual(new List<int> { 56 }, chunked[3].ToList());
            CollectionAssert.AreEqual(new List<int> { 76 }, chunked[4].ToList());
            CollectionAssert.AreEqual(new List<int> { 454 }, chunked[5].ToList());
            CollectionAssert.AreEqual(new List<int> { 76 }, chunked[6].ToList());
            CollectionAssert.AreEqual(new List<int> { 54 }, chunked[7].ToList());
            CollectionAssert.AreEqual(new List<int> { 987 }, chunked[8].ToList());
        }
    }

    internal class Result<T>
    {
        public T Item { get; }
        public int Index { get; }

        public Result(T item, int index)
        {
            Item = item;
            Index = index;
        }

        public override bool Equals(object? obj)
        {
            return obj is Result<T> other &&
                   EqualityComparer<T>.Default.Equals(Item, other.Item) &&
                   Index == other.Index;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Item, Index);
        }
    }
}
