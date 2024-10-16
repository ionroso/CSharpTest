namespace JumpProblems
{
    internal class JumpVI
    {
        public void Test()
        {
            Console.WriteLine(new Solution().MaxResult([4, 2, 3, 0, 3, 1, 2], 5));
            //Console.WriteLine(canJump([3, 2, 1, 0, 4]));
        }

        class Solution
        {
            public int MaxResult(int[] nums, int k)
            {
                int n = nums.Length;
                int[] score = new int[n];
                score[0] = nums[0];
                PriorityQueue<int[],int> priorityQueue = new(Comparer<int>.Create((a, b)=>b - a));
                priorityQueue.Enqueue(new int[] { nums[0], 0 }, nums[0]);
                for (int i = 1; i < n; i++)
                {
                    // pop the old index
                    while (priorityQueue.Peek()[1] < i - k)
                    {
                        priorityQueue.Dequeue();
                    }
                    score[i] = nums[i] + score[priorityQueue.Peek()[1]];
                    priorityQueue.Enqueue(new int[] { score[i], i }, i);
                }
                return score[n - 1];
            }
        }

        class SolutionLinedList {
            private class Score
            {
                public int index, score;
                public Score(int index, int score)
                {
                    this.index = index;
                    this.score = score;
                }
            }

            public int MaxResult(int[] nums, int k) {
                int n = nums.Length;
                int score = nums[0];
                LinkedList<Score> dq = new();
                dq.AddLast(new Score(0, score));
                for (int i = 1; i < n; i++) {
                    // pop the old index
                    while (dq.First() != null && dq.First().index < i - k) {
                        dq.RemoveFirst();
                    }

                    score = dq.First().score + nums[i];
                    // pop the smaller value
                    while (dq.Last() != null && score >= dq.Last().score) {
                        dq.Last();
                    }
                    dq.AddLast(new Score(i, score));
                }
                return score;
            }
        }
    }
}
