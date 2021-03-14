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
    }
}
