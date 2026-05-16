using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Interview.Microsoft_TelemetyAggregator_brute_force;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml;
using System.Numerics;

namespace Interview
{
    public class Microsoft_TelemetyAggregator_brute_force
    {
        public void Test()
        {
            TelemetyAggregatorChronologicallyBinarySearch.TelemetryAggregator aggregator = new TelemetyAggregatorChronologicallyBinarySearch.TelemetryAggregator();
            int index = 0;

            for (int i = 0; i < 10; i++)
            {
                aggregator.InsertEvent(new TelemetryEvent() { EventName = $"EV{index}", Timestamp = index, Data = new Dictionary<string, string> { { $"user", $"user{index}" } } }); //0
                index = new Random().Next(1, 10);
            }

            Console.WriteLine(string.Join(" ", aggregator.Events.Select(e => e.Timestamp)));

            Console.WriteLine("Searching for (2,5)");
            Console.WriteLine(aggregator.GetUniqueUsersCount(2, 5));
        }


        public class TelemetryEvent
        {
            public long Timestamp { get; set; }
            public string EventName { get; set; }
            public Dictionary<string, string> Data { get; set; }
        }

        // sol 3 best one so far
        public class TelemetyAggregatorBatches
        {
            public class TelemetryAggregator
            {
                private List<TelemetryEvent> events;
                public const int OneMillion = 1_000_000;
                public const int BatchSize = 1_000;

                public TelemetryAggregator()
                {
                    events = new List<TelemetryEvent>();
                }

                public List<TelemetryEvent> Events { get => events; private set => events = value; }

                public void InsertEvent(TelemetryEvent telemetryEvent)
                {
                        //int batchIdx = telemetryEvent .Timestamp / BatchSize;

                        //if (!batches.TryGetValue(batchIdx, out var values))
                        //{
                        //    values = new List<int>();
                        //}

                        //values.Add(i);
                        //batches[batchIdx] = values;
                }

                public int GetEventCount(string eventName, long startTime, long endTime)
                {
                    (bool, int, int) indexes = FindNearestIndexes(events, startTime);
                    int eventStartIdx = indexes.Item1 ? indexes.Item3 : indexes.Item2;

                    indexes = FindNearestIndexes(events, endTime);
                    int eventEndIdx = indexes.Item3;

                    int count = events.GetRange(eventStartIdx, eventEndIdx).Where(e => e.EventName.Equals(eventName)).Count();

                    return count;
                }

                static (bool, int, int) FindNearestIndexes(List<TelemetryEvent> sortedList, long target)
                {
                    int left = 0;
                    int right = sortedList.Count - 1;
                    int nearestSmaller = int.MinValue;
                    int nearestBigger = int.MaxValue;

                    while (left <= right)
                    {
                        int mid = left + (right - left) / 2;

                        if (sortedList[mid].Timestamp < target)
                        {
                            nearestSmaller = mid;
                            left = mid + 1;
                        }
                        else
                        {
                            nearestBigger = mid;
                            right = mid - 1;
                        }
                    }

                    return (sortedList[nearestBigger].Timestamp == target, nearestSmaller, nearestBigger);
                }

                public int GetUniqueUsersCount(long startTime, long endTime)
                {
                    (bool found, int nearestSmaller, int nearestBiggerOrFound) indexes = FindNearestIndexes(events, startTime);
                    int eventStartIdx = indexes.nearestBiggerOrFound;

                    indexes = FindNearestIndexes(events.GetRange(eventStartIdx, events.Count - eventStartIdx), endTime);
                    int eventEndIdx = eventStartIdx + (indexes.found ? indexes.nearestBiggerOrFound : indexes.nearestSmaller);


                    int count = events.GetRange(eventStartIdx, eventEndIdx - eventStartIdx + 1).GroupBy(e => e.Data["user"]).Count();
                    Console.WriteLine(count);
                    return count;
                }
            }
        }

        // sol 2 best one so far
        public class TelemetyAggregatorChronologicallyBinarySearch
        {
            public class TelemetryAggregator
            {
                private List<TelemetryEvent> events;
                private IComparer<TelemetryEvent> comparer;

                public TelemetryAggregator()
                {
                    events = new List<TelemetryEvent>();
                    comparer = Comparer<TelemetryEvent>.Create((t1, t2) => t1.Timestamp.CompareTo(t2.Timestamp));
                }

                public List<TelemetryEvent> Events { get => events; private set => events = value; }

                public void InsertEvent(TelemetryEvent telemetryEvent)
                {
                    int index = events.BinarySearch(telemetryEvent, comparer);

                    // Check the result
                    if (index >= 0)
                    {
                        Console.WriteLine($"Element found at in the list, ignoring...");
                        return;
                    }

                    Console.WriteLine($"Element {telemetryEvent.Timestamp} not found. Nearest index: {~index}.");
                    events.Insert(~index, telemetryEvent);
                }

                public int GetEventCount(string eventName, long startTime, long endTime)
                {
                    (bool, int, int) indexes = FindNearestIndexes(events, startTime);
                    int eventStartIdx = indexes.Item1 ? indexes.Item3 : indexes.Item2;

                    indexes = FindNearestIndexes(events, endTime);
                    int eventEndIdx = indexes.Item3;

                    int count = events.GetRange(eventStartIdx, eventEndIdx).Where(e => e.EventName.Equals(eventName)).Count();

                    return count;
                }

                static (bool, int, int) FindNearestIndexes(List<TelemetryEvent> sortedList, long target)
                {
                    int left = 0;
                    int right = sortedList.Count - 1;
                    int nearestSmaller = int.MinValue;
                    int nearestBigger = int.MaxValue;

                    while (left <= right)
                    {
                        int mid = left + (right - left) / 2;

                        if (sortedList[mid].Timestamp < target)
                        {
                            nearestSmaller = mid;
                            left = mid + 1;
                        }
                        else
                        {
                            nearestBigger = mid;
                            right = mid - 1;
                        }
                    }

                    return (sortedList[nearestBigger].Timestamp == target, nearestSmaller, nearestBigger);
                }

                public int GetUniqueUsersCount(long startTime, long endTime)
                {
                    (bool found, int nearestSmaller, int nearestBiggerOrFound) indexes = FindNearestIndexes(events, startTime);
                    int eventStartIdx = indexes.nearestBiggerOrFound;

                    indexes = FindNearestIndexes(events.GetRange(eventStartIdx, events.Count - eventStartIdx), endTime);
                    int eventEndIdx = eventStartIdx + (indexes.found ? indexes.nearestBiggerOrFound : indexes.nearestSmaller);


                    int count = events.GetRange(eventStartIdx, eventEndIdx - eventStartIdx + 1).GroupBy(e => e.Data["user"]).Count();
                    Console.WriteLine(count);
                    return count;
                }
            }
        }

        // sol 1
        public class TelemetyAggregatorChronologically
        {
            public class TelemetryAggregator
            {
                private List<TelemetryEvent> events;

                public List<TelemetryEvent> Events { get => events; private set => events = value; }

                public TelemetryAggregator()
                {
                    Events = new List<TelemetryEvent>();
                }

                public void InsertEvent(TelemetryEvent telemetryEvent)
                {
                    Events.Add(telemetryEvent);
                }

                public int GetEventCount(string eventName, long startTime, long endTime)
                {
                    int eventStartIdx = FindStartIndex(startTime);
                    int eventEndIdx = FindEndIndex(endTime);

                    int count = 0;
                    for (int i = eventStartIdx; i <= eventEndIdx; i++)
                    {
                        count += (eventName.Equals(Events[i].EventName) ? 1 : 0);
                    }

                    return count;
                }

                private int FindEndIndex(long time)
                {
                    int eventEndIdx;
                    for (eventEndIdx = Events.Count - 1; eventEndIdx >= 0; eventEndIdx--)
                    {
                        var tel = Events[eventEndIdx];
                        if (time >= tel.Timestamp)
                        {
                            break;
                        }
                    }

                    return eventEndIdx;
                }

                private int FindStartIndex(long time)
                {
                    int eventStartIdx;
                    for (eventStartIdx = 0; eventStartIdx < Events.Count; eventStartIdx++)
                    {
                        var tel = Events[eventStartIdx];
                        if (time <= tel.Timestamp)
                        {
                            break;
                        }
                    }

                    return eventStartIdx;
                }

                public int GetUniqueUsersCount(long startTime, long endTime)
                {
                    if (startTime > endTime) { return 0; }

                    HashSet<string> users = new();

                    int eventStartIdx = FindStartIndex(startTime);
                    if (eventStartIdx > Events.Count) { return 0; }

                    int eventEndIdx = FindEndIndex(endTime);
                    if (eventStartIdx > Events.Count) { return 0; }

                    int count = 0;
                    for (int i = eventStartIdx; i <= eventEndIdx; i++)
                    {
                        users.Add(Events[i].Data["user"]);
                    }

                    return users.Count;
                }
            }
        }
    }
}
