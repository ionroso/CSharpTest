namespace JumpProblems;

internal class JumpIII
{
    public void Test()
    {
        Console.WriteLine(new Solution().CanReach([4, 2, 3, 0, 3, 1, 2], 5));
        //Console.WriteLine(canJump([3, 2, 1, 0, 4]));
    }

    class Solution
    {
        public bool CanReach(int[] arr, int start)
        {
            int n = arr.Length;

            Queue<int> q = new Queue<int>();
            q.Enqueue(start);

            while (q.Any())
            {
                int node = q.Dequeue();

                // check if reach zero
                if (arr[node] == 0) { return true; }
                if (arr[node] < 0) { continue; }

                // check available next steps
                if (node + arr[node] < n)
                {
                    q.Enqueue(node + arr[node]);
                }

                if (node - arr[node] >= 0)
                {
                    q.Enqueue(node - arr[node]);
                }

                // mark as visited
                arr[node] = -arr[node];
            }
            return false;
        }
    }
}
