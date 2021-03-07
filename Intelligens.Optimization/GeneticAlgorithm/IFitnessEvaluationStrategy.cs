using System.Collections.Generic;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public interface IFitnessEvaluationStrategy<T>
    {
        bool IsFitnessEnough(double fitness);
        double EvaluateFitness(IList<T> chromossome);
    }
}
