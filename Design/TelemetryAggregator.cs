using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design
{
    using System;
    using System.Collections.Generic;

    namespace Design
    {
        public class TelemetryEvent
        {
            public DateTime Timestamp { get; set; }
            public string EventName { get; set; } = "";
            public string UserId { get; set; } = "";

            // Optional extra metadata
            public Dictionary<string, string> Data { get; set; } = new();
        }

        public class TelemetryAggregatorBS
        {
            // Global index: all events sorted by time
            private readonly List<TelemetryEvent> _allEventsByTime = new();

            // Per-event-name index: each event name has its own list sorted by time
            private readonly Dictionary<string, List<TelemetryEvent>> _eventsByName = new();

            public void InsertEvent(TelemetryEvent telemetryEvent)
            {
                if (telemetryEvent == null)
                    throw new ArgumentNullException(nameof(telemetryEvent));

                AddToGlobalTimeIndex(telemetryEvent);
                AddToEventNameIndex(telemetryEvent);
            }

            public int GetEventCount(string eventName, DateTime startTime, DateTime endTime)
            {
                if (string.IsNullOrWhiteSpace(eventName))
                    throw new ArgumentException("Event name cannot be empty.", nameof(eventName));

                if (startTime > endTime)
                    throw new ArgumentException("startTime must be less than or equal to endTime.");

                if (!_eventsByName.TryGetValue(eventName, out var eventsForName))
                    return 0;

                int startIndex = LowerBound(eventsForName, startTime);
                int endIndex = UpperBound(eventsForName, endTime);

                return endIndex - startIndex;
            }

            public int GetUniqueUsersCount(DateTime startTime, DateTime endTime)
            {
                if (startTime > endTime)
                    throw new ArgumentException("startTime must be less than or equal to endTime.");

                int startIndex = LowerBound(_allEventsByTime, startTime);
                int endIndex = UpperBound(_allEventsByTime, endTime);

                HashSet<string> uniqueUsers = new();

                for (int i = startIndex; i < endIndex; i++)
                {
                    string userId = _allEventsByTime[i].UserId;

                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        uniqueUsers.Add(userId);
                    }
                }

                return uniqueUsers.Count;
            }

            private void AddToGlobalTimeIndex(TelemetryEvent telemetryEvent)
            {
                int insertIndex = LowerBound(_allEventsByTime, telemetryEvent.Timestamp);
                _allEventsByTime.Insert(insertIndex, telemetryEvent);
            }

            private void AddToEventNameIndex(TelemetryEvent telemetryEvent)
            {
                if (!_eventsByName.TryGetValue(telemetryEvent.EventName, out var eventsForName))
                {
                    eventsForName = new List<TelemetryEvent>();
                    _eventsByName[telemetryEvent.EventName] = eventsForName;
                }

                int insertIndex = LowerBound(eventsForName, telemetryEvent.Timestamp);
                eventsForName.Insert(insertIndex, telemetryEvent);
            }

            // First index where event.Timestamp >= target
            private int LowerBound(List<TelemetryEvent> events, DateTime target)
            {
                int left = 0;
                int right = events.Count;

                while (left < right)
                {
                    int mid = left + (right - left) / 2;

                    if (events[mid].Timestamp < target)
                        left = mid + 1;
                    else
                        right = mid;
                }

                return left;
            }

            // First index where event.Timestamp > target
            private int UpperBound(List<TelemetryEvent> events, DateTime target)
            {
                int left = 0;
                int right = events.Count;

                while (left < right)
                {
                    int mid = left + (right - left) / 2;

                    if (events[mid].Timestamp <= target)
                        left = mid + 1;
                    else
                        right = mid;
                }

                return left;
            }
        }

        public class TimeNode
        {
            public DateTime Timestamp { get; set; }

            // Optional: keep raw events too
            public List<TelemetryEvent> Events { get; set; } = new();

            // Aggregate count of event names at this timestamp
            public Dictionary<string, int> EventNameCounts { get; set; } = new();

            // Unique users at this timestamp
            public HashSet<string> UserIds { get; set; } = new();

            public TimeNode? Left { get; set; }
            public TimeNode? Right { get; set; }

            public TimeNode(TelemetryEvent telemetryEvent)
            {
                Timestamp = telemetryEvent.Timestamp;
                AddEvent(telemetryEvent);
            }

            public void AddEvent(TelemetryEvent telemetryEvent)
            {
                Events.Add(telemetryEvent);

                if (!EventNameCounts.ContainsKey(telemetryEvent.EventName))
                    EventNameCounts[telemetryEvent.EventName] = 0;

                EventNameCounts[telemetryEvent.EventName]++;

                if (!string.IsNullOrWhiteSpace(telemetryEvent.UserId))
                    UserIds.Add(telemetryEvent.UserId);
            }
        }

        public class TelemetryAggregatorBst
        {
            private TimeNode? _root;

            public void InsertEvent(TelemetryEvent telemetryEvent)
            {
                if (telemetryEvent == null)
                    throw new ArgumentNullException(nameof(telemetryEvent));

                _root = Insert(_root, telemetryEvent);
            }

            private TimeNode Insert(TimeNode? node, TelemetryEvent telemetryEvent)
            {
                if (node == null)
                    return new TimeNode(telemetryEvent);

                if (telemetryEvent.Timestamp < node.Timestamp)
                {
                    node.Left = Insert(node.Left, telemetryEvent);
                }
                else if (telemetryEvent.Timestamp > node.Timestamp)
                {
                    node.Right = Insert(node.Right, telemetryEvent);
                }
                else
                {
                    node.AddEvent(telemetryEvent);
                }

                return node;
            }

            public int GetEventCount(string eventName, DateTime startTime, DateTime endTime)
            {
                if (string.IsNullOrWhiteSpace(eventName))
                    throw new ArgumentException("Event name cannot be empty.", nameof(eventName));

                if (startTime > endTime)
                    throw new ArgumentException("startTime must be <= endTime");

                return GetEventCountInRange(_root, eventName, startTime, endTime);
            }

            private int GetEventCountInRange(TimeNode? node, string eventName, DateTime start, DateTime end)
            {
                if (node == null)
                    return 0;

                int count = 0;

                if (node.Timestamp > start)
                    count += GetEventCountInRange(node.Left, eventName, start, end);

                if (node.Timestamp >= start && node.Timestamp <= end)
                {
                    if (node.EventNameCounts.TryGetValue(eventName, out int value))
                        count += value;
                }

                if (node.Timestamp < end)
                    count += GetEventCountInRange(node.Right, eventName, start, end);

                return count;
            }

            public int GetUniqueUsersCount(DateTime startTime, DateTime endTime)
            {
                if (startTime > endTime)
                    throw new ArgumentException("startTime must be <= endTime");

                HashSet<string> uniqueUsers = new();
                CollectUniqueUsersInRange(_root, startTime, endTime, uniqueUsers);

                return uniqueUsers.Count;
            }

            private void CollectUniqueUsersInRange(
                TimeNode? node,
                DateTime start,
                DateTime end,
                HashSet<string> uniqueUsers)
            {
                if (node == null)
                    return;

                if (node.Timestamp > start)
                    CollectUniqueUsersInRange(node.Left, start, end, uniqueUsers);

                if (node.Timestamp >= start && node.Timestamp <= end)
                {
                    foreach (var userId in node.UserIds)
                    {
                        uniqueUsers.Add(userId);
                    }
                }

                if (node.Timestamp < end)
                    CollectUniqueUsersInRange(node.Right, start, end, uniqueUsers);
            }
        }
    }

}
