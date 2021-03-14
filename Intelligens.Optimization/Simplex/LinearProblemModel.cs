using System;
using System.Collections.Generic;

namespace Intelligens.Optimization.Simplex
{
    public class LinearProblemModel
    {
        public IReadOnlyList<double> ObjectiveFunc { get; private set; }
        public IReadOnlyList<Constraint> Constraints { get; private set; }

        public int CountVars
        {
            get
            {
                return ObjectiveFunc.Count;
            }
        }

        public LinearProblemModel(IReadOnlyList<double> multipliers, IReadOnlyList<Constraint> constraints)
        {
            foreach (var constraint in constraints)
            {
                if (constraint.Multipliers.Count != multipliers.Count)
                {
                    throw new ArgumentException(nameof(constraints));
                }
            }

            ObjectiveFunc = multipliers;
            Constraints = constraints;
        }
    }

}
