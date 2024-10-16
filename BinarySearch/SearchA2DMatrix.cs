using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearch
{
    internal class SearchA2DMatrix
    {
        public void Test()
        {
            Console.WriteLine(SearchMatrix([[1]], target: 0));
        }

        public bool SearchMatrix(int[][] matrix, int target)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;

            (int index, bool found) rez = BinarySearchColumn(matrix, 0, target);

            if (rez.found) {
                return true;
            }

            if (rez.index == 0)
            {
                return false;
            }

            return BinarySearchRow(matrix, rez.index-1, target);
        }


        public bool BinarySearchRow(int[][] matrix, int rowIdx, int target)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;

            int l = 0, r = n - 1;

            while (l <= r)
            {
                var mid = l + (r - l) / 2;
                if (matrix[rowIdx][mid] == target)
                {
                    return true;
                }
                else if (matrix[rowIdx][mid] > target)
                {
                    r = mid - 1;
                }
                else
                {
                    l = mid + 1;
                }
            }

            return false;
        }

        public (int index, bool found) BinarySearchColumn(int[][] matrix, int columnIdx, int target)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;

            int l = 0, r = m - 1;

            while (l <= r)
            {
                var mid = l + (r - l) / 2;
                if (matrix[mid][columnIdx] == target)
                {
                    return (mid, true);
                }
                else if (matrix[mid][columnIdx] > target)
                {
                    r = mid - 1;
                }
                else
                {
                    l = mid + 1;
                }
            }

            return (l, false);
        }
    }
}
