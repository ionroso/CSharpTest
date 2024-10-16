using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCycleDetection
{
    internal class GraphUndirectedBFS
    {
        // Mental model: visited non-parent = cycle
        public class Solution
        {
            public bool HasCycleUndirectedBfs(int n, int[][] edges)
            {
                List<int>[] graph = new List<int>[n];

                for (int i = 0; i < n; i++)
                {
                    graph[i] = new List<int>();
                }

                foreach (var edge in edges)
                {
                    int u = edge[0];
                    int v = edge[1];

                    graph[u].Add(v);
                    graph[v].Add(u);
                }

                bool[] visited = new bool[n];

                for (int start = 0; start < n; start++)
                {
                    if (visited[start])
                    {
                        continue;
                    }

                    Queue<(int node, int parent)> queue = new();

                    queue.Enqueue((start, -1));
                    visited[start] = true;

                    while (queue.Count > 0)
                    {
                        var (node, parent) = queue.Dequeue();

                        foreach (int neighbor in graph[node])
                        {
                            if (!visited[neighbor])
                            {
                                visited[neighbor] = true;
                                queue.Enqueue((neighbor, node));
                            }
                            else if (neighbor != parent)
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
        }
    }
}
