// ============================================================
// COMPARER vs EQUALITY vs SEQUENCE — QUICK CHEAT SHEET
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;

// Sample model
public class TelemetryEvent
{
    public DateTime Timestamp { get; set; }
    public string Id { get; set; }
}



// Custom equality comparer
public class TelemetryEventEqualityComparer : IEqualityComparer<TelemetryEvent>
{
    public bool Equals(TelemetryEvent x, TelemetryEvent y)
    {
        return x.Timestamp == y.Timestamp && x.Id == y.Id;
    }

    public int GetHashCode(TelemetryEvent obj)
    {
        return HashCode.Combine(obj.Timestamp, obj.Id);
    }
}


class Test
{
    public static void Main1()
    {
        // ============================================================
        // 1. ORDERING → Comparer<T>
        // Use when you need sorting or priority queues
        // ============================================================

        var comparer = Comparer<TelemetryEvent>.Create(
            (a, b) => a.Timestamp.CompareTo(b.Timestamp)
        );

        // Example: sorting
        var events = new List<TelemetryEvent>
{
    new TelemetryEvent { Timestamp = DateTime.Now.AddMinutes(5), Id = "A" },
    new TelemetryEvent { Timestamp = DateTime.Now, Id = "B" }
};

        events.Sort(comparer); // sorted by Timestamp


        // ============================================================
        // 2. EQUALITY → EqualityComparer<T>
        // Use when you want to check if two objects are equal
        // ============================================================

        var e1 = new TelemetryEvent { Timestamp = DateTime.Now, Id = "A" };
        var e2 = new TelemetryEvent { Timestamp = e1.Timestamp, Id = "A" };

        // Default equality (reference for objects → usually false)
        bool defaultEqual = EqualityComparer<TelemetryEvent>.Default.Equals(e1, e2);



        // ============================================================
        // 3. ARRAYS / LISTS → SequenceEqual
        // Use when comparing CONTENTS of collections
        // ============================================================

        var list1 = new List<int> { 1, 2, 3 };
        var list2 = new List<int> { 1, 2, 3 };

        // WRONG (reference compare → false)
        bool wrong = EqualityComparer<List<int>>.Default.Equals(list1, list2);

        // CORRECT (content compare → true)
        bool correct = list1.SequenceEqual(list2);


        // ============================================================
        // 4. ARRAYS/LISTS OF OBJECTS → SequenceEqual + custom comparer
        // ============================================================

        var listA = new List<TelemetryEvent> { e1 };
        var listB = new List<TelemetryEvent> { e2 };

        bool equalObjects = listA.SequenceEqual(
            listB,
            new TelemetryEventEqualityComparer()
        );


        // ============================================================
        // 🧠 SUMMARY (MEMORIZE THIS)
        //
        // Comparer<T>        → ordering (sort, priority queue)
        // EqualityComparer<T>→ equality (same object/value?)
        // SequenceEqual      → compare collections by content
        // ============================================================
    }
}

