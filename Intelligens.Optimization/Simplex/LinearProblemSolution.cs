using System.Collections.Generic;

namespace Intelligens.Optimization.Simplex
{
    public class LinearProblemSolution
    {
        public bool IsValid { get; private set; }
        public double Optimum { get; private set; }
        public IReadOnlyList<double> Variables { get; private set; }

        public LinearProblemSolution(IReadOnlyList<double> variables, double optimum)
        {
            Variables = variables;
            Optimum = optimum;
            IsValid = true;
        }

        private LinearProblemSolution()
        {
            IsValid = false;
        }

        public static LinearProblemSolution CreateInvalid()
        {
            return new LinearProblemSolution();
        }
    }
}
