using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCycleDetection
{
    internal class GrapthUnionFind
    {
    public class Solution
        {
            public bool HasCycle(int n, int[][] edges)
            {
                UnionFind uf = new UnionFind(n);

                foreach (var edge in edges)
                {
                    int u = edge[0];
                    int v = edge[1];

                    // already connected -> cycle
                    if (uf.Find(u) == uf.Find(v))
                    {
                        return true;
                    }

                    uf.Union(u, v);
                }

                return false;
            }
        }

        public class UnionFind
        {
            private int[] parent;
            private int[] rank;

            public UnionFind(int n)
            {
                parent = new int[n];
                rank = new int[n];

                for (int i = 0; i < n; i++)
                {
                    parent[i] = i;
                    rank[i] = 1;
                }
            }

            public int Find(int x)
            {
                if (parent[x] != x)
                {
                    parent[x] = Find(parent[x]); // path compression
                }

                return parent[x];
            }

            public void Union(int a, int b)
            {
                int rootA = Find(a);
                int rootB = Find(b);

                if (rootA == rootB)
                {
                    return;
                }

                if (rank[rootA] < rank[rootB])
                {
                    parent[rootA] = rootB;
                }
                else if (rank[rootA] > rank[rootB])
                {
                    parent[rootB] = rootA;
                }
                else
                {
                    parent[rootB] = rootA;
                    rank[rootA]++;
                }
            }
        }

        class UnionFindSimplified
        {
            private int[] parent;

            public UnionFindSimplified(int n)
            {
                parent = new int[n];

                for (int i = 0; i < n; i++)
                {
                    parent[i] = i;
                }
            }

            public int Find(int x)
            {
                while (parent[x] != x)
                {
                    x = parent[x];
                }

                return x;
            }

            public void Union(int a, int b)
            {
                int rootA = Find(a);
                int rootB = Find(b);

                if (rootA == rootB)
                {
                    return;
                }

                parent[rootB] = rootA;
            }

            public bool Connected(int a, int b)
            {
                return Find(a) == Find(b);
            }
        }
    }
}
