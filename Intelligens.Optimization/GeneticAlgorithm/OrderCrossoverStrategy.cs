using System.Collections.Generic;
using Intelligens.Extras;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public class OrderCrossoverStrategy<T> : ICrossoverStrategy<T>
    {
        private readonly IRandomGenerator _prng;

        public OrderCrossoverStrategy(IRandomGenerator prng)
        {
            _prng = prng;
        }

        public IList<T> Crossover(IList<T> parent1, IList<T> parent2)
        {
            var genesCount = parent1.Count;
            var size = _prng.Next(1, genesCount);
            var start = _prng.Next(0, genesCount - size + 1);
            var endIdx = size + start - 1;
            var offspring = new T[genesCount];
            var genesUsed = new HashSet<T>();
            var genesParent2Queued = new Queue<T>(parent2);

            for (var i = start; i <= endIdx; i += 1)
            {
                offspring[i] = parent1[i];
                genesUsed.Add(parent1[i]);
            }

            var j = endIdx + 1;

            while (j < genesCount)
            {
                var gene = genesParent2Queued.Dequeue();

                if (!genesUsed.Contains(gene))
                {
                    offspring[j] = gene;
                    j += 1;
                }
            }

            j = 0;

            while (j < start)
            {
                var gene = genesParent2Queued.Dequeue();

                if (!genesUsed.Contains(gene))
                {
                    offspring[j] = gene;
                    j += 1;
                }
            }

            return offspring;
        }
    }
}
