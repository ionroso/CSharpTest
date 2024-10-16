using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public class NumberOfIslands
    {
        public void Test()
        {
            char[][] grid =
            {
                ['1','1','1','1','0'],
                ['1','1','0','1','0'],
                ['1','1','0','0','0'],
                ['0','0','0','0','0']
            };

            //char[][] grid =
            //{
            //  ['1','1','0','0','0'],
            //  ['1','1','0','0','0'],
            //  ['0','0','1','0','0'],
            //  ['0','0','0','1','1']
            //};

            System.Console.WriteLine(new SolutionUnionFind().NumIslands(grid));
        }

        public class SolutionUnionFind
        {
            private class UnionFind
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
                        parent[x] = Find(parent[x]);
                    }
                    return parent[x];
                }

                public bool Union(int a, int b)
                {
                    int rootA = Find(a);
                    int rootB = Find(b);

                    if (rootA == rootB) { return false; }

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
                    return true;
                }
            }

            public int NumIslands(char[][] grid)
            {
                if (grid == null || grid.Length == 0)
                {
                    return 0;
                }

                int rows = grid.Length;
                int cols = grid[0].Length;

                UnionFind uf = new UnionFind(rows * cols);

                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (grid[r][c] == '0') continue;

                        int currentIndex = r * cols + c;

                        if (IsLand(grid, r - 1, c))
                        {
                            int cellTop = (r - 1) * cols + c;
                            uf.Union(currentIndex, cellTop);
                        }

                        if (IsLand(grid, r , c - 1))
                        {
                            int cellLeft = r * cols + (c - 1);
                            uf.Union(currentIndex, cellLeft);
                        }
                    }
                }

                HashSet<int> islandIds = new HashSet<int>();
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (grid[r][c] == '1')
                        {
                            islandIds.Add(uf.Find(r * cols + c));
                        }
                    }
                }

                return islandIds.Count;
            }

            private bool IsLand(char[][] grid, int r, int c)
            {
                return r >= 0 &&
                       r < grid.Length &&
                       c >= 0 &&
                       c < grid[0].Length &&
                       grid[r][c] == '1';
            }
        }

        public class SolutionDFS
        {
            private static readonly int[][] Directions = { [1, 0], [-1, 0], [0, 1], [0, -1] };

            public int NumIslands(char[][] grid)
            {
                if (grid == null || grid.Length == 0)
                {
                    return 0;
                }

                int rows = grid.Length;
                int cols = grid[0].Length;
                int count = 0;

                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (grid[r][c] == '0') continue;

                        count++;

                        Dfs(grid, r, c);
                    }
                }

                return count;
            }

            private void Dfs(char[][] grid, int r, int c)
            {
                if (!IsLand(grid, r, c))
                {
                    return;
                }

                grid[r][c] = '0';

                foreach (var dir in Directions)
                {
                    int nr = r + dir[0];
                    int nc = c + dir[1];
                    Dfs(grid, nr, nc);
                }
            }
            private void DfsIterative(char[][] grid, int startRow, int startCol)
            {
                Stack<(int row, int col)> stack = new();

                stack.Push((startRow, startCol));
                grid[startRow][startCol] = '0'; // mark visited when pushing

                while (stack.Count > 0)
                {
                    var (row, col) = stack.Pop();

                    foreach (var dir in Directions)
                    {
                        int nr = row + dir[0];
                        int nc = col + dir[1];

                        if (!IsLand(grid, nr, nc))
                        {
                            continue;
                        }

                        grid[nr][nc] = '0'; // mark before pushing
                        stack.Push((nr, nc));
                    }
                }
            }

            private bool IsLand(char[][] grid, int r, int c)
            {
                return r >= 0 &&
                       r < grid.Length &&
                       c >= 0 &&
                       c < grid[0].Length &&
                       grid[r][c] == '1';
            }
        }

        class SolutionBFS
        {
            private static readonly int[][] Directions = { [1, 0], [-1, 0], [0, 1], [0, -1] };

            public int NumIslands(char[][] grid)
            {
                if (grid == null || grid.Length == 0)
                {
                    return 0;
                }

                int rows = grid.Length;
                int cols = grid[0].Length;
                int count = 0;

                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (grid[r][c] != '0') continue;

                        count++;

                        Bfs(grid, r, c);
                    }
                }

                return count;
            }

            private void Bfs(char[][] grid, int startRow, int startCol)
            {
                var queue = new Queue<(int row, int col)>();

                grid[startRow][startCol] = '0';
                queue.Enqueue((startRow, startCol));

                while (queue.Count > 0)
                {
                    var (row, col) = queue.Dequeue();

                    foreach (var dir in Directions)
                    {
                        int nr = row + dir[0];
                        int nc = col + dir[1];

                        if (!IsLand(grid, nr, nc))
                        {
                            continue;
                        }

                        grid[nr][nc] = '0';
                        queue.Enqueue((nr, nc));
                    }
                }
            }

            private bool IsLand(char[][] grid, int r, int c)
            {
                return r >= 0 &&
                       r < grid.Length &&
                       c >= 0 &&
                       c < grid[0].Length &&
                       grid[r][c] == '1';
            }
        }
    }
}
