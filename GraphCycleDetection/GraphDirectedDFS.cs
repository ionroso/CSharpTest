using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCycleDetection
{
    internal class GraphDirectedDFS
    {
        //Mental model: back-edge into recursion stack
        public class Solution
        {
            public bool HasCycleDirected(int n, int[][] edges)
            {
                List<int>[] graph = new List<int>[n];

                for (int i = 0; i < n; i++)
                {
                    graph[i] = new List<int>();
                }

                foreach (var edge in edges)
                {
                    int from = edge[0];
                    int to = edge[1];

                    graph[from].Add(to);
                }

                int[] state = new int[n];
                // 0 = unvisited
                // 1 = visiting
                // 2 = visited

                for (int node = 0; node < n; node++)
                {
                    if (state[node] == 0)
                    {
                        if (DfsDetectCycle(graph, node, state))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            private bool DfsDetectCycle(List<int>[] graph, int node, int[] state)
            {
                if (state[node] == 1)
                {
                    return true; // found back-edge → cycle
                }

                if (state[node] == 2)
                {
                    return false; // already fully processed
                }

                state[node] = 1; // visiting

                foreach (int neighbor in graph[node])
                {
                    if (DfsDetectCycle(graph, neighbor, state))
                    {
                        return true;
                    }
                }

                state[node] = 2; // fully visited
                return false;
            }
        }
    }
}
