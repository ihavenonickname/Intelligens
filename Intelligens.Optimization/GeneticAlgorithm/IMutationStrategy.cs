using System.Collections.Generic;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public interface IMutationStrategy<T>
    {
        bool ShouldMutate();
        void Mutate(IList<T> chromosome);
    }
}
