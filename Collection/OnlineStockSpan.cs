using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Collection
{
    internal class OnlineStockSpan
    {
        public void Test()
        {
            StockSpanner stockSpanner = new StockSpanner();
            stockSpanner.Next(31); // return 1
            stockSpanner.Next(41);  // return 1
            stockSpanner.Next(48);  // return 1
            stockSpanner.Next(59);  // return 2
            stockSpanner.Next(79);  // return 1
        }
        public class StockSpanner
        {
            class Span
            {
                public int Price { get; set; }
                public int FirstGreater { get; set; }
                public int Index { get; set; }
                public int SpanCounter { get; set; }
            }

            List<Span> history;

            public StockSpanner()
            {
                history = new();
            }

            public int Next(int price)
            {
                if (history.Count == 0)
                {
                    history.Add(new Span() { Price = price, FirstGreater = -1, Index = -1, SpanCounter = 1 });

                    return 1;
                }

                int i = history.Count-1;
                int span = 1;
                for (; i>=0; )
                {
                    var curr = history[i];
                    if (curr.Price > price)
                    {
                        break;
                    }

                    i = curr.Index;
                    span += curr.SpanCounter;
                }

                var tuple = new Span() { Price = price } ;

                if(i == -1)
                {
                    tuple.SpanCounter = span;
                    tuple.FirstGreater = -1;
                    tuple.Index = -1;
                } else if(i == 0)
                {
                    var temp = history[i];

                    if (price > temp.Price)
                    {
                        tuple.SpanCounter++;
                        tuple.FirstGreater = -1;
                        tuple.Index = -1;
                    } else
                    {
                        tuple.SpanCounter = span;
                        tuple.FirstGreater = temp.Price;
                        tuple.Index = 0;
                    }

                } else
                {
                    var temp = history[i];

                    tuple.SpanCounter = span;
                    tuple.FirstGreater = temp.Price;
                    tuple.Index = i;
                }

                history.Add(tuple);

                return tuple.SpanCounter;
            }
        }
    }
}
