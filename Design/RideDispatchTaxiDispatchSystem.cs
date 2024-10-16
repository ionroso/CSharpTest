using System;
using System.Collections.Generic;
using System.Linq;

namespace Design
{
    public record Location(double X, double Y);

    public class Driver
    {
        public int Id { get; }
        public string Name { get; }
        public double Rating { get; }
        public bool IsAvailable { get; private set; }
        public Location CurrentLocation { get; private set; }

        public Driver(int id, string name, Location currentLocation, double rating)
        {
            Id = id;
            Name = name;
            CurrentLocation = currentLocation ?? throw new ArgumentNullException(nameof(currentLocation));
            Rating = rating;
            IsAvailable = true;
        }

        public void MarkUnavailable()
        {
            IsAvailable = false;
        }

        public void MarkAvailable(Location newLocation)
        {
            CurrentLocation = newLocation ?? throw new ArgumentNullException(nameof(newLocation));
            IsAvailable = true;
        }
    }

    public record RideRequest(int RequestId, int CustomerId, Location PickupLocation);

    public record RideAssignment(int RequestId, int CustomerId, int DriverId, string DriverName);

    public record DriverDispatchPriority(double Distance, double Rating);

    public class DispatchService
    {
        private readonly List<Driver> _drivers = new();

        public void RegisterDriver(Driver driver)
        {
            if (driver == null)
                throw new ArgumentNullException(nameof(driver));

            if (_drivers.Any(d => d.Id == driver.Id))
                throw new InvalidOperationException("Driver with same Id already exists.");

            _drivers.Add(driver);
        }

        public RideAssignment? RequestRide(RideRequest rideRequest)
        {
            if (rideRequest == null)
                throw new ArgumentNullException(nameof(rideRequest));

            var comparer = Comparer<DriverDispatchPriority>.Create((a, b) =>
            {
                // Smaller distance should come first
                int distanceCompare = a.Distance.CompareTo(b.Distance);
                if (distanceCompare != 0)
                    return distanceCompare;

                // Higher rating should come first
                return b.Rating.CompareTo(a.Rating);
            });

            var candidateQueue = new PriorityQueue<Driver, DriverDispatchPriority>(comparer);

            foreach (var driver in _drivers.Where(d => d.IsAvailable))
            {
                double distance = CalculateDistance(driver.CurrentLocation, rideRequest.PickupLocation);
                candidateQueue.Enqueue(driver, new DriverDispatchPriority(distance, driver.Rating));
            }

            if (candidateQueue.Count == 0)
                return null;

            var selectedDriver = candidateQueue.Dequeue();
            selectedDriver.MarkUnavailable();

            return new RideAssignment(
                rideRequest.RequestId,
                rideRequest.CustomerId,
                selectedDriver.Id,
                selectedDriver.Name);
        }

        public void CompleteRide(int driverId, Location newLocation)
        {
            var driver = _drivers.FirstOrDefault(d => d.Id == driverId);
            if (driver == null)
                throw new InvalidOperationException("Driver not found.");

            driver.MarkAvailable(newLocation);
        }

        private double CalculateDistance(Location a, Location b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}