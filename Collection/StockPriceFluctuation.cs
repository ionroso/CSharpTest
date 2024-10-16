using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection
{
    internal class StockPriceFluctuation
    {

        class Stock
        {
           public int Timestamp { get; set; }
           public int Value { get; set; }


        }

        class StockPrice
        {
            int latestTime;
            // Store price of each stock at each timestamp.
            Dictionary<int, int> timestampPriceMap;
            // Store stock prices in increasing order to get min and max price.
            SortedDictionary<int, int> priceFrequency;

            public StockPrice()
            {
                latestTime = 0;
                timestampPriceMap = new();
                priceFrequency = new();
            }

            public void Update(int timestamp, int price)
            {
                // Update latestTime to latest timestamp.
                latestTime = Math.Max(latestTime, timestamp);

                // If same timestamp occurs again, previous price was wrong. 
                if (timestampPriceMap.ContainsKey(timestamp))
                {
                    // Remove previous price.
                    int oldPrice = timestampPriceMap[timestamp];
                    priceFrequency[oldPrice] = priceFrequency[oldPrice] - 1;

                    // Remove the entry from the map.
                    if (priceFrequency[oldPrice] == 0)
                    {
                        priceFrequency.Remove(oldPrice);
                    }
                }

                // Add latest price for timestamp.
                timestampPriceMap[timestamp] = price;
                priceFrequency[price] = priceFrequency.GetValueOrDefault(price, 0) + 1;
            }

            public int Current()
            {
                // Return latest price of the stock.
                return timestampPriceMap[latestTime];
            }

            public int Maximum()
            {
                // Return the maximum price stored at the end of sorted-map.
                return priceFrequency.Last().Key;
            }

            public int Minimum()
            {
                // Return the maximum price stored at the front of sorted-map.
                return priceFrequency.First().Key;
            }
        }

        public class StockPrice1
        {
            private class Stock
            {
                public int Timestamp { get; set; }
                public int Price { get; set; }
            }
            private int latestTime;
            private Dictionary<int, Stock> timeToStockValue;
            private SortedDictionary<Stock, int> valueToTime;

            public StockPrice1()
            {
                timeToStockValue = new();
                valueToTime = new(Comparer<Stock>.Create((o1, o2) => o1.Price - o2.Price));
            }

            public void Update(int timestamp, int price)
            {
                // Update latestTime to latest timestamp.
                latestTime = Math.Max(latestTime, timestamp);

                var current = new Stock() { Timestamp = timestamp, Price = price };
                if (timeToStockValue.TryGetValue(timestamp, out var replacement)) {
                    valueToTime.Remove(replacement);
                }

                timeToStockValue[current.Timestamp] = current;
                valueToTime[current] = current.Timestamp;


            }

            public int Current()
            {
                var rez = timeToStockValue?[latestTime] ?? throw new ArgumentNullException();
                return rez.Price;
            }

            public int Maximum()
            {
                var rez = valueToTime?.Last() ?? throw new ArgumentNullException();
                return rez.Key.Price;
            }

            public int Minimum()
            {
                var rez = valueToTime?.First() ?? throw new ArgumentNullException();
                return rez.Key.Price;
            }
        }

        public void Test()
        {
            StockPrice stockPrice = new StockPrice();
            stockPrice.Update(1, 10); // Timestamps are [1] with corresponding prices [10].
            stockPrice.Update(2, 5);  // Timestamps are [1,2] with corresponding prices [10,5].
            stockPrice.Current();     // return 5, the latest timestamp is 2 with the price being 5.
            stockPrice.Maximum();     // return 10, the maximum price is 10 at timestamp 1.
            stockPrice.Update(1, 3);  // The previous timestamp 1 had the wrong price, so it is updated to 3.
                                      // Timestamps are [1,2] with corresponding prices [3,5].
            stockPrice.Maximum();     // return 5, the maximum price is 5 after the correction.
            stockPrice.Update(4, 2);  // Timestamps are [1,2,4] with corresponding prices [3,5,2].
            stockPrice.Minimum();     // return 2, the minimum price is 2 at timestamp 4.
        }

            /**
             * Your StockPrice object will be instantiated and called as such:
             * StockPrice obj = new StockPrice();
             * obj.Update(timestamp,price);
             * int param_2 = obj.Current();
             * int param_3 = obj.Maximum();
             * int param_4 = obj.Minimum();
             */



            public void Test1()
        {

            Dictionary<int, Stock> timeToStockValue= new();
            SortedDictionary<Stock, int> valueToTime = new(Comparer<Stock>.Create((o1, o2) => o1.Value - o2.Value));

            var temp = new Stock() { Timestamp = 1, Value = 4 };
            timeToStockValue.Add(1, new Stock() { Timestamp = 1, Value = 4 });
            valueToTime.Add(temp, 1);


            var temp1 = new Stock() { Timestamp = 2, Value = 7 };
            timeToStockValue.Add(2, temp1);
            valueToTime.Add(temp1, 2);

            var temp2 = new Stock() { Timestamp = 8, Value = 10 };
            timeToStockValue.Add(3, temp2);
            valueToTime.Add(temp2, 3);

            foreach (var keyValuePair in valueToTime)
            {
                Console.WriteLine($"value:{keyValuePair.Key.Value} time: {keyValuePair.Value}");
            }

            // update

            Console.WriteLine();


            var newStockTimeTwo = new Stock() { Timestamp = 2, Value = 15 };
            var replacement = timeToStockValue[newStockTimeTwo.Timestamp];
            timeToStockValue[newStockTimeTwo.Timestamp] = newStockTimeTwo;

            valueToTime.Remove(replacement);
            valueToTime.Add(newStockTimeTwo, newStockTimeTwo.Timestamp);
            
            foreach (var keyValuePair in valueToTime)
            {
                Console.WriteLine($"value:{keyValuePair.Key.Value} time: {keyValuePair.Value}");
            }

            Console.WriteLine($"First: {valueToTime.First().Key.Value} and last: {valueToTime.Last().Key.Value}") ;

        }
    }
}
