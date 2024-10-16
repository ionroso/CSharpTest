using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCycleDetection
{
    internal class GraphUndirectedDFS
    {
        // Mental model: visited non-parent = cycle
        public class Solution
        {
            public bool HasCycleUndirected(int n, int[][] edges)
            {
                List<int>[] graph = new List<int>[n];

                for (int i = 0; i < n; i++)
                {
                    graph[i] = new List<int>();
                }

                foreach (int[] edge in edges)
                {
                    int u = edge[0];
                    int v = edge[1];

                    graph[u].Add(v);
                    graph[v].Add(u);
                }

                bool[] visited = new bool[n];

                for (int node = 0; node < n; node++)
                {
                    if (!visited[node])
                    {
                        if (DfsHasCycle(graph, node, parent: -1, visited))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            private bool DfsHasCycle(
                List<int>[] graph,
                int node,
                int parent,
                bool[] visited)
            {
                visited[node] = true;

                foreach (int neighbor in graph[node])
                {
                    if (!visited[neighbor])
                    {
                        if (DfsHasCycle(graph, neighbor, node, visited))
                        {
                            return true;
                        }
                    }
                    else if (neighbor != parent)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
