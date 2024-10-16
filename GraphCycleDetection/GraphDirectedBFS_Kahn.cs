using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCycleDetection
{
    internal class GraphDirectedBFS_Kahn
    {
        //Mental Models: some tasks stay permanently locked
        public class Solution
        {
            public bool HasCycleDirectedKahn(int n, int[][] edges)
            {
                List<int>[] graph = new List<int>[n];
                int[] indegree = new int[n];

                for (int i = 0; i < n; i++)
                {
                    graph[i] = new List<int>();
                }

                foreach (var edge in edges)
                {
                    int from = edge[0];
                    int to = edge[1];

                    graph[from].Add(to);
                    indegree[to]++;
                }

                Queue<int> queue = new();

                for (int node = 0; node < n; node++)
                {
                    if (indegree[node] == 0)
                    {
                        queue.Enqueue(node);
                    }
                }

                int processed = 0;

                while (queue.Count > 0)
                {
                    int node = queue.Dequeue();
                    processed++;

                    foreach (int neighbor in graph[node])
                    {
                        indegree[neighbor]--;

                        if (indegree[neighbor] == 0)
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }

                return processed != n;
            }
        }
    }
}
