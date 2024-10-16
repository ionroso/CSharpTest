namespace JumpProblems;

internal class JumpI
{
    public void Test()
    {
        Console.WriteLine(canJump([1, 3, 1]));
        //Console.WriteLine(canJump([3, 2, 1, 0, 4]));
    }

    public bool canJump(int[] nums)
    {
        int n = nums.Length;
        bool[] reached = new bool[n];
        reached[0] = true;

        int reachedIdx = 0;

        for (int i = 0; i < n; i++)
        {
            if (!reached[i])
            {
                return false;
            }

            if (i == n - 1 || i + nums[i] >= n - 1)
            {
                return true;
            }

            while (reachedIdx <= i + nums[i])
            {
                reached[reachedIdx] = true;
                reachedIdx++;
            }
            reachedIdx--;
        }

        return false;
    }
}
