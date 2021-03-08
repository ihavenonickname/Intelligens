using System.Collections.Generic;
using Intelligens.Extras;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public class OnePointCrossoverStrategy<T> : ICrossoverStrategy<T>
    {
        private readonly IRandomGenerator _prng;

        public OnePointCrossoverStrategy(IRandomGenerator prng)
        {
            _prng = prng;
        }

        public IList<T> Crossover(IList<T> parent1, IList<T> parent2)
        {
            var offspring = new T[parent1.Count];

            var j = _prng.Next(parent1.Count - 1);

            for (var i = 0; i < parent1.Count; i += 1)
            {
                offspring[i] = (i <= j ? parent1 : parent2)[i];
            }

            return offspring;
        }
    }
}
