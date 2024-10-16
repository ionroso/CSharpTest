using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphFindUnion
{
    internal class UnionFind_or_DisjointSetUnion
    {
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
                    parent[i] = i; // each node starts as its own parent
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

            public bool Union(int a, int b)
            {
                int rootA = Find(a);
                int rootB = Find(b);

                if (rootA == rootB)
                {
                    return false; // already connected
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

                return true; // merged successfully
            }

            public bool Connected(int a, int b)
            {
                return Find(a) == Find(b);
            }
        }
    }
}
