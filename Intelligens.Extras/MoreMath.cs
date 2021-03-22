using System;
using System.Collections.Generic;

namespace Intelligens.Extras
{
    public static class MoreMath
    {
        public static double EuclideanDistance(IReadOnlyList<double> coords1, IReadOnlyList<double> coords2)
        {
            var acc = 0.0;

            for (var i = 0; i < coords1.Count; i += 1)
            {
                acc += Math.Pow(coords1[i] - coords2[i], 2);
            }

            return Math.Sqrt(acc);
        }

        public static double[,] Multiply(double[,] a, double[,] b)
        {
            var rowCount = a.GetLength(0);
            var colCount = b.GetLength(1);
            var m = a.GetLength(1);

            var r = new double[rowCount, colCount];

            for (var row = 0; row < rowCount; row += 1)
            {
                for (var col = 0; col < colCount; col += 1)
                {
                    var sum = 0.0;

                    for (var k = 0; k < m; k += 1)
                    {
                        sum += a[row, k] * b[k, col];
                    }

                    r[row, col] = sum;
                }
            }

            return r;
        }

        public static void GaussJordanMethod(double[,] A, double[] B)
        {
            if (A.GetLength(0) != A.GetLength(1))
            {
                throw new ArgumentException("Matrix A is not a square", nameof(A));
            }

            if (A.GetLength(0) != B.Length)
            {
                throw new ArgumentException("A and B must have the same number of rows", nameof(B));
            }

            var order = A.GetLength(0);

            for (var pivotRow = 0; pivotRow < order; pivotRow += 1)
            {
                var pivotCol = pivotRow;
                var pivot = A[pivotRow, pivotCol];

                for (var col = 0; col < order; col += 1)
                {
                    A[pivotRow, col] /= pivot;
                }

                B[pivotRow] /= pivot;

                for (var row = 0; row < order; row += 1)
                {
                    if (row == pivotRow || A[row, pivotCol] == 0)
                    {
                        continue;
                    }

                    var factor = A[row, pivotCol];

                    for (var col = pivotCol; col < order; col += 1)
                    {
                        A[row, col] = A[row, col] - factor * A[pivotRow, col];
                    }

                    B[row] = B[row] - factor * B[pivotRow];
                }
            }
        }
    }
}
