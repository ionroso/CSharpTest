namespace TwoPointers
{
    internal class CompareVersionNumbers
    {
        public static void Test()
        {
            new Solution().CompareVersion("1.2", "1.10");
        }


        public class Solution1
        {
            public int CompareVersion(string version1, string version2)
            {
                string[] nums1 = version1.Split(".");
                string[] nums2 = version2.Split(".");
                int n1 = nums1.Length, n2 = nums2.Length;


                // compare versions
                int i1, i2;
                for (int i = 0; i < Math.Max(n1, n2); ++i)
                {
                    i1 = i < n1 ? int.Parse(nums1[i]) : 0;
                    i2 = i < n2 ? int.Parse(nums2[i]) : 0;
                    if (i1 != i2)
                    {
                        return i1 > i2 ? 1 : -1;
                    }
                }
                //The versions are equal
                return 0;
            }
        }


        public class Solution
        {
            private (int, int) getNextChunk(string version, int n, int p)
            {
                if (p > n - 1)
                {
                    return (0, p);
                }

                int pEnd = p;
                while (pEnd < n && version[pEnd] != '.')
                {
                    ++pEnd;
                }

                int i = Int32.Parse(version.Substring(p, pEnd != n - 1 ? pEnd - p : n - p));
                p = pEnd + 1;
                return (i, p);
            }

            public int CompareVersion(string version1, string version2)
            {
                int p1 = 0, p2 = 0;
                int n1 = version1.Length, n2 = version2.Length;

                while (p1 < n1 || p2 < n2)
                {
                    (int val1, int pos1) = getNextChunk(version1, n1, p1);
                    p1 = pos1;
                    (int val2, int pos2) = getNextChunk(version2, n2, p2);
                    p2 = pos2;
                    if (val1 != val2)
                    {
                        return val1 > val2 ? 1 : -1;
                    }
                }

                return 0;
            }
        }

    }
}
