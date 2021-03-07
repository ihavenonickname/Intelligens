using System.Collections.Generic;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public interface ICrossoverStrategy<T>
    {
        IList<T> Crossover(IList<T> parent1, IList<T> parent2);
    }
}
