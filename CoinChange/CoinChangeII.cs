using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinChange
{
    internal class CoinChangeII
    {
        public void Test()
        {

        }

        class Solution
        {
            public int Change(int sum, int[] coins)
            {
                int n = coins.Length;

                // 2d dp array where n is the number of coin
                // denominations and sum is the target sum
                int[,] dp = new int[n + 1, sum + 1];

                // Represents the base case where the target sum is 0,
                // and there is only one way to make change: by not
                // selecting any coin
                dp[0, 0] = 1;
                for (int i = 1; i <= n; i++)
                {
                    for (int j = 0; j <= sum; j++)
                    {

                        // Add the number of ways to make change without
                        // using the current coin
                        dp[i, j] += dp[i - 1, j];

                        if ((j - coins[i - 1]) >= 0)
                        {

                            // Add the number of ways to make change
                            // using the current coin
                            dp[i, j] += dp[i, j - coins[i - 1]];
                        }
                    }
                }
                return dp[n, sum];
            }
        }
    }
}
