using System;
using System.Collections.Generic;
using System.Linq;
using Intelligens.Extras;

namespace Intelligens.Regression
{
    public class LeastSquares
    {
        public IList<double> LinearFit(IList<double> x, IList<double> y)
        {
            if (x.Count != y.Count)
            {
                throw new ArgumentException("x and y must have same length", nameof(x));
            }

            if (x.Count < 2)
            {
                throw new ArgumentException("x and y must have same length", nameof(x));
            }

            var n = x.Count;
            var xSum = x.Sum();
            var ySum = y.Sum();
            var xySum = x.Zip(y, (a, b) => a * b).Sum();
            var xSqSum = x.Select(a => a * a).Sum();

            var A = new[,]
            {
                { n,    xSum   },
                { xSum, xSqSum },
            };

            var B = new[]
            {
                ySum,
                xySum,
            };

            // var answer = MoreMath.Multiply(MoreMath.Inverse(A), B);

            // return new [] { answer[1, 0], answer[1, 1] };

            MoreMath.GaussJordanMethod(A, B);

            return B;
        }
    }
}
