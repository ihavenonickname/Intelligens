using System;
using System.Collections.Generic;
using System.Linq;
using Intelligens.Extras;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public class TournamentSelectionStrategy<T> : ISelectionStrategy<T>
    {
        private readonly IRandomGenerator _prng;
        private readonly int _k;

        public TournamentSelectionStrategy(int k, IRandomGenerator prng)
        {
            _k = k;
            _prng = prng;
        }

        public Tuple<IList<T>, IList<T>> Select(IList<IList<T>> population)
        {
            var pool = new HashSet<int>();

            while (pool.Count < _k)
            {
                pool.Add(_prng.Next(population.Count));
            }

            var parent1index = pool.Min();
            pool.Remove(parent1index);
            var parent2index = pool.Min();

            return new Tuple<IList<T>, IList<T>>(population[parent1index], population[parent2index]);
        }
    }
}
