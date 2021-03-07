using System.Collections.Generic;
using Intelligens.Extras;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public class RandomResetMutationStrategy<T> : IMutationStrategy<T>
    {
        private readonly IRandomGenerator _prng;
        private readonly IGeneCreationStrategy<T> _geneCreationStrategy;
        private readonly double _rate;

        public RandomResetMutationStrategy(IGeneCreationStrategy<T> geneCreationStrategy, IRandomGenerator prng, double rate)
        {
            _geneCreationStrategy = geneCreationStrategy;
            _prng = prng;
            _rate = rate;
        }

        public bool ShouldMutate()
        {
            return _prng.Next() < _rate;
        }

        public void Mutate(IList<T> chromosome)
        {
            chromosome[_prng.Next(chromosome.Count)] = _geneCreationStrategy.CreateGene();
        }
    }
}
