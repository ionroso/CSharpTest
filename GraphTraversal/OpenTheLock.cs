using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class OpenTheLock
    {
        
         
        public void Test()
        {
            var solution = new Solution();
            string[] deadends = ["0201", "0101", "0102", "1212", "2002"];
            string target = "0202";
            var result = solution.OpenLock(deadends, target);
        }


        public class Solution
        {
            public int OpenLock(string[] deadends, string target)
            {
                return BFS(new HashSet<string>(deadends), target, new HashSet<string>());
            }

            private int BFS(HashSet<string> deadSet, string targetState, HashSet<string> visited)
            {
                var startState = "0000";

                if (deadSet.Contains(startState)) return -1;
                if (targetState == startState) return 0;

                Queue<string> queue = new();
                queue.Enqueue(startState);
                visited.Add(startState);

                int perLevelCount = 1;
                int steps = 0;
                while (queue.Count > 0)
                {
                    int newPerLevelCount = 0;
                    while (perLevelCount > 0)
                    {
                        var state = queue.Dequeue();
                        var nextStates = GetNextStates(state);
                        foreach (var nextState in nextStates)
                        {
                            if (deadSet.Contains(nextState) || visited.Contains(nextState)) continue;
                            if (nextState == targetState) return steps + 1;
                            queue.Enqueue(nextState);
                            visited.Add(nextState);
                            newPerLevelCount++;
                        }

                        perLevelCount--;
                    }

                    perLevelCount = newPerLevelCount;

                    steps++;
                }

                return -1;
            }

            private IEnumerable<string> GetNextStates(string state)
            {
                int[] digits = NewState(state);

                for (int i = 0; i < 4; i++)
                {
                    int original = digits[i];

                    digits[i] = (original + 1) % 10;
                    yield return $"{digits[0]}, {digits[1]}, {digits[2]}, {digits[3]}";

                    digits[i] = (original + 9) % 10;
                    yield return $"{digits[0]}, {digits[1]}, {digits[2]}, {digits[3]}";

                    digits[i] = original;
                }
            }
            private static int[] NewState(string s) => new int[] { s[0] - '0', s[1] - '0', s[2] - '0', s[3] - '0' };
        }


        public class Solution1
        {
            private record State(int W1, int W2, int W3, int W4);
            public int OpenLock(string[] deadends, string target)
            {
                return BFS(new HashSet<State>(deadends.Select(NewState)), NewState(target), new HashSet<State>());
            }

            private int BFS(HashSet<State> deadSet, State targetState, HashSet<State> visited)
            {
                State startState = NewState("0000");

                if (deadSet.Contains(startState)) return -1;
                if(targetState == startState) return 0;

                Queue<State> queue = new();
                queue.Enqueue(startState);
                visited.Add(startState);

                int perLevelCount = 1;
                int steps = 0;
                while (queue.Count > 0)
                {
                    int newPerLevelCount = 0;
                    while (perLevelCount > 0)
                    {
                        var state = queue.Dequeue();
                        var nextStates = GetNextStates(state);
                        foreach (var nextState in nextStates)
                        {
                            if (deadSet.Contains(nextState) || visited.Contains(nextState)) continue;
                            if (nextState == targetState) return steps+1;
                            queue.Enqueue(nextState);
                            visited.Add(nextState);
                            newPerLevelCount++;
                        }

                        perLevelCount--;
                    }

                    perLevelCount = newPerLevelCount;

                    steps++;
                }

                return -1;
            }

            private IEnumerable<State> GetNextStates(State state)
            {
                int[] digits = { state.W1, state.W2, state.W3, state.W4 };

                for (int i = 0; i < 4; i++)
                {
                    int original = digits[i];

                    digits[i] = (original + 1) % 10;
                    yield return new State(digits[0], digits[1], digits[2], digits[3]);

                    digits[i] = (original + 9) % 10;
                    yield return new State(digits[0], digits[1], digits[2], digits[3]);

                    digits[i] = original;
                }
            }

            private static State NewState(string s)
            {
                return new State(
                    s[0] - '0',
                    s[1] - '0',
                    s[2] - '0',
                    s[3] - '0'
                );
            }
        }
    }
}
