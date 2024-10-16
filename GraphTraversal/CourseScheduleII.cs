using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    internal class CourseScheduleII
    {
        private class Solution
        {
            public int[] FindOrder(int numCourses, int[][] prerequisites)
            {
                List<int>[] graph = new List<int>[numCourses];
                int[] indegree = new int[numCourses];

                for (int i = 0; i < numCourses; i++)
                {
                    graph[i] = new List<int>();
                }

                // Build graph:
                // prereq -> course
                foreach (int[] pair in prerequisites)
                {
                    int course = pair[0];
                    int prereq = pair[1];

                    graph[prereq].Add(course);
                    indegree[course]++;
                }

                Queue<int> queue = new();

                // Courses with no prerequisites
                for (int course = 0; course < numCourses; course++)
                {
                    if (indegree[course] == 0)
                    {
                        queue.Enqueue(course);
                    }
                }

                List<int> order = new();

                while (queue.Count > 0)
                {
                    int course = queue.Dequeue();

                    // Add to topological order
                    order.Add(course);

                    // Unlock dependent courses
                    foreach (int nextCourse in graph[course])
                    {
                        indegree[nextCourse]--;

                        if (indegree[nextCourse] == 0)
                        {
                            queue.Enqueue(nextCourse);
                        }
                    }
                }

                // Cycle exists
                if (order.Count != numCourses)
                {
                    return Array.Empty<int>();
                }

                return order.ToArray();
            }
        }
    }
}
