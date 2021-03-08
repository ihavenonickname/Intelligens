using System.Collections.Generic;
using Intelligens.Extras;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public class SwapMutationStrategy<T> : IMutationStrategy<T>
    {
        private readonly IRandomGenerator _prng;
        private readonly double _rate;

        public SwapMutationStrategy(IRandomGenerator prng, double rate)
        {
            _prng = prng;
            _rate = rate;
        }

        public bool ShouldMutate()
        {
            return _prng.Next() < _rate;
        }

        public void Mutate(IList<T> chromosome)
        {
            var i = _prng.Next(chromosome.Count);
            var j = _prng.Next(chromosome.Count);

            while (i == j)
            {
                j = _prng.Next(chromosome.Count);
            }

            var temp = chromosome[i];
            chromosome[i] = chromosome[j];
            chromosome[j] = temp;
        }
    }
}
