using System.Collections.Generic;
using Intelligens.Extras;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public class BitFlipMutationStrategy : IMutationStrategy<bool>
    {
        private readonly IRandomGenerator _prng;
        private readonly double _rate;

        public BitFlipMutationStrategy(IRandomGenerator prng, double rate)
        {
            _prng = prng;
            _rate = rate;
        }

        public bool ShouldMutate()
        {
            return _prng.Next() < _rate;
        }

        public void Mutate(IList<bool> chromosome)
        {
            var i = _prng.Next(chromosome.Count);

            chromosome[i] = !chromosome[i];
        }
    }
}
