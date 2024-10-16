using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Collection.BreakUpAListIntoBatches;

namespace Batching
{
    /*
        Imagine that we have an integer and we want to divide it into equal groups of another integer and put any remainder to the end of the group.
        E.g. if we want to divide 100 into groups/batches of at most 15 then we’ll have the following array of integers:
        15, 15, 15, 15, 15, 10
    */
    internal class DivideAnIntegerIntoGroups
    {

        public void Test()
        {
            Console.Write(string.Join(' ', BatchInteger(10, 3)));
        }

        private IEnumerable<int> BatchInteger(int total, int batchSize)
        {
            if (batchSize == 0)
            {
                yield return 0;
            }

            if (batchSize >= total)
            {
                yield return total;
            }
            else
            {
                int rest = total % batchSize;
                int divided = total / batchSize;

                for (int i = 0; i < divided; i++)
                {
                    yield return batchSize;
                }

                if (rest > 0)
                {
                    yield return rest;
                }
            }
        }
    }
}
