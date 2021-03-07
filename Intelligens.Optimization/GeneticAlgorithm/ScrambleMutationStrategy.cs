using System.Collections.Generic;
using Intelligens.Extras;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public class ScrambleMutationStrategy<T> : IMutationStrategy<T>
    {
        private readonly IRandomGenerator _prng;
        private readonly double _rate;

        public ScrambleMutationStrategy(IRandomGenerator prng, double rate)
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
            var size = _prng.Next(2, chromosome.Count);
            var start = _prng.Next(0, chromosome.Count - size);

            for (var i = start + size - 1; i >= start; i -= 1)
            {
                var j = _prng.Next(start, i);
                var temp = chromosome[i];
                chromosome[i] = chromosome[j];
                chromosome[j] = temp;
            }
        }
    }
}
