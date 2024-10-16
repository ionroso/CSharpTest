using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // Different types of vehicles supported by the parking lot
    public enum VehicleType
    {
        Motorcycle,
        Car,
        Truck
    }

    // Different types of parking spots
    public enum SpotType
    {
        Small,
        Compact,
        Large
    }

    // Base class for all vehicles
    public abstract class Vehicle
    {
        public string LicensePlate { get; }
        public VehicleType Type { get; }

        protected Vehicle(string licensePlate, VehicleType type)
        {
            LicensePlate = licensePlate;
            Type = type;
        }
    }

    // Concrete vehicle types
    public class Motorcycle : Vehicle
    {
        public Motorcycle(string licensePlate) : base(licensePlate, VehicleType.Motorcycle) { }
    }

    public class Car : Vehicle
    {
        public Car(string licensePlate) : base(licensePlate, VehicleType.Car) { }
    }

    public class Truck : Vehicle
    {
        public Truck(string licensePlate) : base(licensePlate, VehicleType.Truck) { }
    }

    // Represents one physical parking spot
    public class ParkingSpot
    {
        public int SpotNumber { get; }
        public SpotType SpotType { get; }

        // The vehicle currently parked in this spot, if any
        public Vehicle? ParkedVehicle { get; private set; }

        public bool IsFree => ParkedVehicle == null;

        public ParkingSpot(int spotNumber, SpotType spotType)
        {
            SpotNumber = spotNumber;
            SpotType = spotType;
        }

        // Checks whether this parking spot can fit the given vehicle
        public bool CanFitVehicle(Vehicle vehicle)
        {
            return vehicle.Type switch
            {
                VehicleType.Motorcycle => SpotType == SpotType.Small
                                       || SpotType == SpotType.Compact
                                       || SpotType == SpotType.Large,

                VehicleType.Car => SpotType == SpotType.Compact
                                || SpotType == SpotType.Large,

                VehicleType.Truck => SpotType == SpotType.Large,

                _ => false
            };
        }

        // Park the vehicle if the spot is free and compatible
        public bool ParkVehicle(Vehicle vehicle)
        {
            if (!IsFree || !CanFitVehicle(vehicle))
                return false;

            ParkedVehicle = vehicle;
            return true;
        }

        // Remove the parked vehicle from this spot
        public void RemoveVehicle()
        {
            ParkedVehicle = null;
        }
    }

    // Represents a parking ticket issued when a vehicle enters
    public class Ticket
    {
        public string TicketId { get; }
        public string LicensePlate { get; }
        public int SpotNumber { get; }
        public DateTime EntryTime { get; }

        public Ticket(string ticketId, string licensePlate, int spotNumber)
        {
            TicketId = ticketId;
            LicensePlate = licensePlate;
            SpotNumber = spotNumber;
            EntryTime = DateTime.Now;
        }
    }

    // Main class that manages parking operations
    public class ParkingLot
    {
        private readonly List<ParkingSpot> _spots = new();
        private readonly Dictionary<string, Ticket> _activeTickets = new();

        public ParkingLot(List<ParkingSpot> spots)
        {
            _spots = spots;
        }

        // Park a vehicle and return a ticket if successful
        public Ticket? ParkVehicle(Vehicle vehicle)
        {
            // Prevent the same vehicle from being parked twice
            if (_activeTickets.ContainsKey(vehicle.LicensePlate))
                return null;

            // Find the first free compatible spot
            ParkingSpot? availableSpot = _spots.FirstOrDefault(
                s => s.IsFree && s.CanFitVehicle(vehicle));

            if (availableSpot == null)
                return null;

            bool parked = availableSpot.ParkVehicle(vehicle);
            if (!parked)
                return null;

            var ticket = new Ticket(
                Guid.NewGuid().ToString(),
                vehicle.LicensePlate,
                availableSpot.SpotNumber
            );

            _activeTickets[vehicle.LicensePlate] = ticket;
            return ticket;
        }

        // Remove a vehicle from the parking lot using its license plate
        public bool RemoveVehicle(string licensePlate)
        {
            if (!_activeTickets.TryGetValue(licensePlate, out Ticket? ticket))
                return false;

            ParkingSpot? spot = _spots.FirstOrDefault(s => s.SpotNumber == ticket.SpotNumber);
            if (spot == null)
                return false;

            spot.RemoveVehicle();
            _activeTickets.Remove(licensePlate);
            return true;
        }

        // Print current parking lot status
        public void PrintStatus()
        {
            foreach (var spot in _spots)
            {
                string status = spot.IsFree
                    ? "Free"
                    : $"Occupied by {spot.ParkedVehicle!.Type} ({spot.ParkedVehicle.LicensePlate})";

                Console.WriteLine($"Spot {spot.SpotNumber} [{spot.SpotType}] - {status}");
            }
        }
    }

    // Example usage
    public class Program
    {
        public static void Test()
        {
            var spots = new List<ParkingSpot>
        {
            new ParkingSpot(1, SpotType.Small),
            new ParkingSpot(2, SpotType.Compact),
            new ParkingSpot(3, SpotType.Large),
            new ParkingSpot(4, SpotType.Large)
        };

            var parkingLot = new ParkingLot(spots);

            var bike = new Motorcycle("BIKE-101");
            var car = new Car("CAR-202");
            var truck = new Truck("TRUCK-303");

            var t1 = parkingLot.ParkVehicle(bike);
            var t2 = parkingLot.ParkVehicle(car);
            var t3 = parkingLot.ParkVehicle(truck);

            Console.WriteLine("After parking:");
            parkingLot.PrintStatus();

            Console.WriteLine();
            Console.WriteLine("Removing CAR-202...");
            parkingLot.RemoveVehicle("CAR-202");

            Console.WriteLine();
            Console.WriteLine("After removal:");
            parkingLot.PrintStatus();
        }
    }
}
