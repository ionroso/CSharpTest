namespace Arrays
{
    public class Rotate
    {
        public void Rot()
        {
            int[][] array = new int[][]
            {
                new [] { 1, 2, 3, 4},
                new [] { 5, 6, 7, 8},
                new [] { 9, 10, 11, 12 },
                new [] { 13, 14, 15, 16 },
            };

            int n = array.Length;

            for (int i = 1; i < array.Length; i++)
            {
                for(int j = 0; j < n-i; j++)
                {
                    var temp = array[i+j][j];
                    array[i+j][j] = array[j][i + j];
                    array[j][i + j] = temp;
                }
            }

            for (int i = 0; i < n/2; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    var temp = array[j][i];
                    array[j][i] = array[j][n-1-i];
                    array[j][n-1-i] = temp;
                }
            }



            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(array[i][j] + "  ");
                }
                Console.WriteLine();
            }
        }
    }
}
