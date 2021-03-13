using System.Collections.Generic;

namespace Intelligens.Optimization.Simplex
{
    public class Constraint
    {
        public IList<double> Multipliers { get; private set; }
        public double RightHandSide { get; private set; }

        public Constraint(IList<double> multipliers, double rhs)
        {
            Multipliers = multipliers;
            RightHandSide = rhs;
        }
    }

}
