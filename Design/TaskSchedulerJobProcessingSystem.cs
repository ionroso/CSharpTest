using System;
using System.Collections.Generic;

namespace Design
{
    public record Job(int Id, string Name, int Priority, long SequenceNumber);

    public class JobScheduler
    {
        private readonly PriorityQueue<Job, (int Priority, long SequenceNumber)> _queue;
        private long _nextSequenceNumber = 0;

        public JobScheduler()
        {
            _queue = new PriorityQueue<Job, (int Priority, long SequenceNumber)>(
                Comparer<(int Priority, long SequenceNumber)>.Create((x, y) =>
                {
                    // Higher priority should come first
                    if (x.Priority != y.Priority)
                        return y.Priority.CompareTo(x.Priority);

                    // Older job should come first
                    return x.SequenceNumber.CompareTo(y.SequenceNumber);
                }));
        }

        public void AddJob(int id, string name, int priority)
        {
            var job = new Job(id, name, priority, _nextSequenceNumber++);
            _queue.Enqueue(job, (job.Priority, job.SequenceNumber));
        }

        public Job PeekNextJob()
        {
            if (_queue.Count == 0)
                throw new InvalidOperationException("No pending jobs.");

            return _queue.Peek();
        }

        public Job ProcessNextJob()
        {
            if (_queue.Count == 0)
                throw new InvalidOperationException("No pending jobs.");

            return _queue.Dequeue();
        }

        public bool HasPendingJobs()
        {
            return _queue.Count > 0;
        }
    }
}