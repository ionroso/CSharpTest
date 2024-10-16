using System;
using System.Collections.Generic;

namespace GraphTopologicalSorting
{
    internal class TopologicalSorting
    {
        public void Test()
        {
            var solution = new SolutionDFS();

            List<int>[] graph = new List<int>[4];
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            // 0 -> 1 -> 3
            // 0 -> 2 -> 3
            graph[0].Add(1);
            graph[0].Add(2);
            graph[1].Add(3);
            graph[2].Add(3);

            List<int> order = solution.TopologicalSort(graph);

            Console.WriteLine(string.Join(", ", order));
            // Possible output: 0, 2, 1, 3
        }
        class SolutionDFS
        {
            public List<int> TopologicalSort(List<int>[] graph)
            {
                int n = graph.Length;

                int[] state = new int[n];
                List<int> order = new();

                for (int node = 0; node < n; node++)
                {
                    if (state[node] == 0)
                    {
                        if (DfsDetectCycleAndBuildOrder(graph, node, state, order))
                        {
                            return new List<int>();
                        }
                    }
                }

                order.Reverse();
                return order;
            }

            private bool DfsDetectCycleAndBuildOrder(
                List<int>[] graph,
                int node,
                int[] state,
                List<int> order)
            {
                if (state[node] == 1)
                {
                    return true;
                }

                if (state[node] == 2)
                {
                    return false;
                }

                state[node] = 1;

                foreach (int neighbor in graph[node])
                {
                    if (DfsDetectCycleAndBuildOrder(graph, neighbor, state, order))
                    {
                        return true;
                    }
                }

                state[node] = 2;
                order.Add(node);

                return false;
            }
        }

        class SolutionBFS
        {
            public List<int> TopologicalSortBfs(List<int>[] graph)
            {
                int n = graph.Length;

                int[] indegree = new int[n];

                for (int node = 0; node < n; node++)
                {
                    foreach (int neighbor in graph[node])
                    {
                        indegree[neighbor]++;
                    }
                }

                Queue<int> queue = new();

                for (int node = 0; node < n; node++)
                {
                    if (indegree[node] == 0)
                    {
                        queue.Enqueue(node);
                    }
                }

                List<int> order = new();

                while (queue.Count > 0)
                {
                    int node = queue.Dequeue();

                    order.Add(node);

                    foreach (int neighbor in graph[node])
                    {
                        indegree[neighbor]--;

                        if (indegree[neighbor] == 0)
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }

                // cycle detection
                if (order.Count != n)
                {
                    return new List<int>();
                }

                return order;
            }
        }
    }
}