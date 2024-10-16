internal class Program
{
    private static void Main(string[] args)
    {

        Dictionary<long, List<long>> batches = new();

        long total = 1000;
        long prev = 0;

        var test = 6387293471666;

        int stopAfter = 10000, count = -1;
        while (count < stopAfter) {
            DateTime utcNow = DateTime.UtcNow;
            //long milliseconds = utcNow.Ticks / TimeSpan.TicksPerMillisecond;
            long milliseconds = test++;
            //if (prev == milliseconds) {
            //    continue;
            //}

            long batchIdx = milliseconds / total;
            Console.WriteLine($"{ milliseconds} {batchIdx}");

            if (!batches.TryGetValue(batchIdx, out var values))
            {
                values = new List<long>();
            }

            values.Add(milliseconds);
            batches[batchIdx] = values;
            prev = milliseconds;
            count++;
        }


        foreach (long batchIdx in batches.Keys)
        {
            Console.WriteLine($"{batchIdx}: total:{batches[batchIdx].Count}");
        }
    }

    private static void Main1(string[] args)
    {
        Console.WriteLine("Hello, World!");

        int total = 100;
        int batchSize = 15;
        int startPoint = 0;
        int endPoint = 1_000;


        int totalBatches = total / batchSize;
        int rest = total % batchSize;

        if (rest > 0) {
            totalBatches++;
        }

        Console.WriteLine($"How many batches: {totalBatches}");
        Console.WriteLine($"Rest items: {rest}");

        Dictionary<int, List<int>> batches = new();
        for (int i = startPoint; i < endPoint; i++)
        {
            int batchIdx = i / batchSize;

            if (!batches.TryGetValue(batchIdx, out var values))
            {
                values = new List<int>();
            }

            values.Add(i);
            batches[batchIdx] = values;

        }

        foreach (int batchIdx in batches.Keys)
        {
            Console.WriteLine($"{batchIdx}: total:{batches[batchIdx].Count} {string.Join(",", batches[batchIdx])}");
        }

        Console.WriteLine("");
    }
}