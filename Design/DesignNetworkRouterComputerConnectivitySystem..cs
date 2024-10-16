using System;
using System.Collections.Generic;
using System.Linq;

namespace Design
{
    public record Device(int Id, string Name, string Type);

    public class Network
    {
        private readonly Dictionary<int, Device> _devices = new();
        private readonly Dictionary<int, HashSet<int>> _connections = new();

        public void AddDevice(Device device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            if (_devices.ContainsKey(device.Id))
                throw new InvalidOperationException("Device with same Id already exists.");

            _devices[device.Id] = device;
            _connections[device.Id] = new HashSet<int>();
        }

        public void ConnectDevices(int deviceId1, int deviceId2)
        {
            if (!_devices.ContainsKey(deviceId1) || !_devices.ContainsKey(deviceId2))
                throw new InvalidOperationException("One or both devices do not exist.");

            if (deviceId1 == deviceId2)
                throw new InvalidOperationException("A device cannot connect to itself.");

            _connections[deviceId1].Add(deviceId2);
            _connections[deviceId2].Add(deviceId1);
        }

        public List<Device> GetNeighbors(int deviceId)
        {
            if (!_devices.ContainsKey(deviceId))
                throw new InvalidOperationException("Device does not exist.");

            return _connections[deviceId]
                .Select(neighborId => _devices[neighborId])
                .ToList();
        }

        public bool CanReach(int sourceDeviceId, int targetDeviceId)
        {
            if (!_devices.ContainsKey(sourceDeviceId) || !_devices.ContainsKey(targetDeviceId))
                throw new InvalidOperationException("One or both devices do not exist.");

            var visited = new HashSet<int>();
            return DfsReachability(sourceDeviceId, targetDeviceId, visited);
        }

        private bool DfsReachability(int currentDeviceId, int targetDeviceId, HashSet<int> visited)
        {
            if (currentDeviceId == targetDeviceId)
                return true;

            visited.Add(currentDeviceId);

            foreach (var neighborId in _connections[currentDeviceId])
            {
                if (!visited.Contains(neighborId))
                {
                    if (DfsReachability(neighborId, targetDeviceId, visited))
                        return true;
                }
            }

            return false;
        }

        public List<Device> GetShortestPath(int sourceDeviceId, int targetDeviceId)
        {
            if (!_devices.ContainsKey(sourceDeviceId) || !_devices.ContainsKey(targetDeviceId))
                throw new InvalidOperationException("One or both devices do not exist.");

            var queue = new Queue<int>();
            var visited = new HashSet<int>();
            var previous = new Dictionary<int, int?>();

            queue.Enqueue(sourceDeviceId);
            visited.Add(sourceDeviceId);
            previous[sourceDeviceId] = null;

            while (queue.Count > 0)
            {
                int currentDeviceId = queue.Dequeue();

                if (currentDeviceId == targetDeviceId)
                    break;

                foreach (var neighborId in _connections[currentDeviceId])
                {
                    if (!visited.Contains(neighborId))
                    {
                        visited.Add(neighborId);
                        previous[neighborId] = currentDeviceId;
                        queue.Enqueue(neighborId);
                    }
                }
            }

            if (!visited.Contains(targetDeviceId))
                return new List<Device>();

            var path = new List<Device>();
            int? current = targetDeviceId;

            while (current is int deviceId)
            {
                path.Add(_devices[deviceId]);
                current = previous[deviceId];
            }

            path.Reverse();
            return path;
        }

        public int CountDisconnectedGroups()
        {
            var visited = new HashSet<int>();
            int groupCount = 0;

            foreach (var deviceId in _devices.Keys)
            {
                if (!visited.Contains(deviceId))
                {
                    groupCount++;
                    DfsMarkComponent(deviceId, visited);
                }
            }

            return groupCount;
        }

        private void DfsMarkComponent(int deviceId, HashSet<int> visited)
        {
            visited.Add(deviceId);

            foreach (var neighborId in _connections[deviceId])
            {
                if (!visited.Contains(neighborId))
                {
                    DfsMarkComponent(neighborId, visited);
                }
            }
        }
    }

    public class Program5
    {
        public static void Test()
        {
            var network = new Network();

            network.AddDevice(new Device(1, "Router-A", "Router"));
            network.AddDevice(new Device(2, "PC-1", "Computer"));
            network.AddDevice(new Device(3, "PC-2", "Computer"));
            network.AddDevice(new Device(4, "Server-1", "Server"));
            network.AddDevice(new Device(5, "Laptop-1", "Laptop"));
            network.AddDevice(new Device(6, "Printer-1", "Printer"));

            network.ConnectDevices(1, 2);
            network.ConnectDevices(2, 3);
            network.ConnectDevices(3, 4);
            network.ConnectDevices(5, 6);

            Console.WriteLine("Neighbors of PC-1:");
            foreach (var neighbor in network.GetNeighbors(2))
            {
                Console.WriteLine(neighbor.Name);
            }

            Console.WriteLine();
            Console.WriteLine($"Can Router-A reach Server-1? {network.CanReach(1, 4)}");
            Console.WriteLine($"Can Router-A reach Printer-1? {network.CanReach(1, 6)}");

            Console.WriteLine();
            Console.WriteLine("Shortest path from Router-A to Server-1:");
            var path = network.GetShortestPath(1, 4);
            Console.WriteLine(string.Join(" -> ", path.Select(d => d.Name)));

            Console.WriteLine();
            Console.WriteLine($"Disconnected groups: {network.CountDisconnectedGroups()}");
        }
    }
}