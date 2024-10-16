using System;
using System.Collections.Generic;
using System.Linq;

namespace Design
{
    public record User(int Id, string Name);

    public class SocialNetwork
    {
        private readonly Dictionary<int, User> _users = new();
        private readonly Dictionary<int, HashSet<int>> _friendships = new();

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (_users.ContainsKey(user.Id))
                throw new InvalidOperationException("User with same Id already exists.");

            _users[user.Id] = user;
            _friendships[user.Id] = new HashSet<int>();
        }

        public void AddFriendship(int userId1, int userId2)
        {
            if (!_users.ContainsKey(userId1) || !_users.ContainsKey(userId2))
                throw new InvalidOperationException("One or both users do not exist.");

            if (userId1 == userId2)
                throw new InvalidOperationException("A user cannot friend themselves.");

            _friendships[userId1].Add(userId2);
            _friendships[userId2].Add(userId1);
        }

        public List<User> GetDirectFriends(int userId)
        {
            if (!_users.ContainsKey(userId))
                throw new InvalidOperationException("User does not exist.");

            return _friendships[userId]
                .Select(friendId => _users[friendId])
                .ToList();
        }

        public bool AreConnected(int sourceUserId, int targetUserId)
        {
            if (!_users.ContainsKey(sourceUserId) || !_users.ContainsKey(targetUserId))
                throw new InvalidOperationException("One or both users do not exist.");

            var visited = new HashSet<int>();
            return Dfs(sourceUserId, targetUserId, visited);
        }

        private bool Dfs(int currentUserId, int targetUserId, HashSet<int> visited)
        {
            if (currentUserId == targetUserId)
                return true;

            visited.Add(currentUserId);

            foreach (var friendId in _friendships[currentUserId])
            {
                if (!visited.Contains(friendId))
                {
                    if (Dfs(friendId, targetUserId, visited))
                        return true;
                }
            }

            return false;
        }

        public List<User> GetShortestFriendshipPath(int sourceUserId, int targetUserId)
        {
            if (!_users.ContainsKey(sourceUserId) || !_users.ContainsKey(targetUserId))
                throw new InvalidOperationException("One or both users do not exist.");

            var queue = new Queue<int>();
            var visited = new HashSet<int>();
            var previous = new Dictionary<int, int?>();

            queue.Enqueue(sourceUserId);
            visited.Add(sourceUserId);
            previous[sourceUserId] = null;

            while (queue.Count > 0)
            {
                int currentUserId = queue.Dequeue();

                if (currentUserId == targetUserId)
                    break;

                foreach (var friendId in _friendships[currentUserId])
                {
                    if (!visited.Contains(friendId))
                    {
                        visited.Add(friendId);
                        previous[friendId] = currentUserId;
                        queue.Enqueue(friendId);
                    }
                }
            }

            if (!visited.Contains(targetUserId))
                return new List<User>();

            var path = new List<User>();
            int? current = targetUserId;

            while (current != null)
            {
                path.Add(_users[current.Value]);
                current = previous[current.Value];
            }

            path.Reverse();
            return path;
        }
    }
    /*
     * while (current.HasValue)
{
    path.Add(_users[current.Value]);
    current = previous[current.Value];
}

    while (current.HasValue)
{
    path.Add(_users[current.Value]);
    current = previous[current.Value];
}
     */

    public class Program3
    {
        public static void Test()
        {
            var network = new SocialNetwork();

            network.AddUser(new User(1, "Alice"));
            network.AddUser(new User(2, "Bob"));
            network.AddUser(new User(3, "Charlie"));
            network.AddUser(new User(4, "Diana"));
            network.AddUser(new User(5, "Eve"));

            network.AddFriendship(1, 2);
            network.AddFriendship(2, 3);
            network.AddFriendship(3, 4);

            Console.WriteLine("Direct friends of Bob:");
            foreach (var friend in network.GetDirectFriends(2))
            {
                Console.WriteLine(friend.Name);
            }

            Console.WriteLine();
            Console.WriteLine($"Are Alice and Diana connected? {network.AreConnected(1, 4)}");
            Console.WriteLine($"Are Alice and Eve connected? {network.AreConnected(1, 5)}");

            Console.WriteLine();
            Console.WriteLine("Shortest path from Alice to Diana:");
            var path = network.GetShortestFriendshipPath(1, 4);

            if (path.Count == 0)
            {
                Console.WriteLine("No path found.");
            }
            else
            {
                Console.WriteLine(string.Join(" -> ", path.Select(u => u.Name)));
            }
        }
    }
}