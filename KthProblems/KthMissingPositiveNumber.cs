namespace KthProblems
{
    internal class KthMissingPositiveNumber
    {
        public void Test()
        {
            //Console.WriteLine(FindKthPositive([5, 6, 7, 11], k: 4));
            //Console.WriteLine(FindKthPositive([1, 9], k: 5));
            //Console.WriteLine(FindKthPositive([1, 3, 9], k: 5));

            //Console.WriteLine(FindKthPositive([1, 3, 9], k: 7));
            //Console.WriteLine(FindKthPositive([1, 2 ], k: 1));
            //Console.WriteLine(FindKthPositive([1, 3 ], k: 1));
            Console.WriteLine(new Solution().FindKthPositive([1], k: 1));

        }

        // case#1 k < arr[0] 
        // case#2 missed in the array < arr[0] 
        // case#3 countLeft < arr[0] 

        class Solution
        {
            public int FindKthPositive(int[] arr, int k)
            {
                int left = 0, right = arr.Length - 1;
                while (left <= right)
                {
                    int pivot = left + (right - left) / 2;
                    // If number of positive integers
                    // which are missing before arr[pivot]
                    // is less than k -->
                    // continue to search on the right.
                    if (arr[pivot] - pivot - 1 < k)
                    {
                        left = pivot + 1;
                        // Otherwise, go left.
                    }
                    else
                    {
                        right = pivot - 1;
                    }
                }

                // At the end of the loop, left = right + 1,
                // and the kth missing is in-between arr[right] and arr[left].
                // The number of integers missing before arr[right] is
                // arr[right] - right - 1 -->
                // the number to return is
                // arr[right] + k - (arr[right] - right - 1) = k + left
                return left + k;
            }
        }
    }
}
