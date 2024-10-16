using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class AllPathsFromSourceToTarget
    {
        public void Test()
        {

        }


        public class Solution
        {
            public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
            {
                var output = new List<IList<int>>();
                Dfs(graph, 0, new List<int>(), output, new HashSet<int>());
                return output;

            }

            private void Dfs(int[][] graph, int v, List<int> path, List<IList<int>> output, HashSet<int> visited)
            {
                if (visited.Contains(v)) return;

                if (v == graph.Length - 1)
                {
                    var newPath = new List<int>(path);
                    newPath.Add(v);
                    output.Add(newPath);
                    return;
                }

                visited.Add(v);
                path.Add(v);

                foreach (var neighbor in graph[v])
                {
                    Dfs(graph, neighbor, path, output, visited);
                }

                path.RemoveAt(path.Count - 1);
                visited.Remove(v);
            }
        }

    }
}
