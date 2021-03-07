using System;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public interface IGeneCreationStrategy<T>
    {
        T CreateGene();
    }
}
