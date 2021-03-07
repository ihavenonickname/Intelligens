using System.Collections.Generic;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public class EvolutionResult<T>
    {
        public IList<T> Best { get; private set; }
        public double Fitness { get; private set; }
        public int Generation { get; private set; }

        public EvolutionResult(IList<T> best, double fitness, int generation)
        {
            Best = best;
            Fitness = fitness;
            Generation = generation;
        }
    }
}
