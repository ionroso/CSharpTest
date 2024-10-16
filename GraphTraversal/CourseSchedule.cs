using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    internal class CourseSchedule
    {
        private class Solution
        {
            public bool CanFinish(int numCourses, int[][] prerequisites)
            {
                // graph[i] contains all courses that depend on course i.
                // Example: if we have prerequisite [1, 0],
                // it means to take course 1, we must first take course 0.
                // So the edge is: 0 -> 1.
                List<int>[] graph = new List<int>[numCourses];

                // indegree[i] = how many prerequisites course i still has.
                int[] indegree = new int[numCourses];

                // Initialize adjacency list.
                for (int i = 0; i < numCourses; i++)
                {
                    graph[i] = new List<int>();
                }

                // Build graph and indegree array.
                foreach (int[] pair in prerequisites)
                {
                    int course = pair[0];
                    int prereq = pair[1];

                    // prereq must come before course.
                    graph[prereq].Add(course);

                    // course has one more prerequisite.
                    indegree[course]++;
                }

                Queue<int> queue = new();

                // Courses with indegree 0 have no prerequisites.
                // They can be taken immediately.
                for (int course = 0; course < numCourses; course++)
                {
                    if (indegree[course] == 0)
                    {
                        queue.Enqueue(course);
                    }
                }

                int completed = 0;

                // Kahn's algorithm:
                // Process all currently available courses.
                while (queue.Count > 0)
                {
                    int course = queue.Dequeue();

                    // We are able to take this course.
                    completed++;

                    // Taking this course unlocks dependent courses.
                    foreach (int nextCourse in graph[course])
                    {
                        // One prerequisite for nextCourse is now satisfied.
                        indegree[nextCourse]--;

                        // If no prerequisites remain, this course is ready.
                        if (indegree[nextCourse] == 0)
                        {
                            queue.Enqueue(nextCourse);
                        }
                    }
                }

                // If we completed all courses, there is no cycle.
                // If not, some courses are stuck in a cycle.
                return completed == numCourses;
            }
        }
    }
}
