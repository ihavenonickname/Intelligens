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
    }
}
