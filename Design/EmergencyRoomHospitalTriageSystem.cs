using System;
using System.Collections.Generic;

namespace Design
{
    public record Patient(int Id, string Name, int Severity, long SequenceNumber);

    public class TriageSystem
    {
        private readonly PriorityQueue<Patient, (int Severity, long SequenceNumber)> _queue;
        private long _nextSequenceNumber = 0;

        public TriageSystem()
        {
            _queue = new PriorityQueue<Patient, (int Severity, long SequenceNumber)>(
                Comparer<(int Severity, long SequenceNumber)>.Create((x, y) =>
                {
                    // Higher severity should come first
                    if (x.Severity != y.Severity)
                        return y.Severity.CompareTo(x.Severity);

                    // Earlier arrival should come first
                    return x.SequenceNumber.CompareTo(y.SequenceNumber);
                }));
        }

        public void AddPatient(int id, string name, int severity)
        {
            var patient = new Patient(id, name, severity, _nextSequenceNumber++);
            _queue.Enqueue(patient, (patient.Severity, patient.SequenceNumber));
        }

        public Patient PeekNextPatient()
        {
            if (_queue.Count == 0)
                throw new InvalidOperationException("No patients waiting.");

            return _queue.Peek();
        }

        public Patient TreatNextPatient()
        {
            if (_queue.Count == 0)
                throw new InvalidOperationException("No patients waiting.");

            return _queue.Dequeue();
        }

        public bool HasWaitingPatients()
        {
            return _queue.Count > 0;
        }

        public int GetWaitingCount()
        {
            return _queue.Count;
        }
    }

    public class Program2
    {
        public static void Test()
        {
            var triage = new TriageSystem();

            triage.AddPatient(1, "Alice", 2);
            triage.AddPatient(2, "Bob", 5);
            triage.AddPatient(3, "Charlie", 3);
            triage.AddPatient(4, "Diana", 5);

            Console.WriteLine("Next patient:");
            Console.WriteLine(triage.PeekNextPatient());

            Console.WriteLine();
            Console.WriteLine("Treating patients:");

            while (triage.HasWaitingPatients())
            {
                var patient = triage.TreatNextPatient();
                Console.WriteLine(patient);
            }
        }
    }
}