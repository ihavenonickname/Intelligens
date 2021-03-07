using System;
using System.Collections.Generic;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public class UniformCrossoverStrategy<T> : ICrossoverStrategy<T>
    {
        public IList<T> Crossover(IList<T> parent1, IList<T> parent2)
        {
            IList<T> offspring = new T[parent1.Count];

            for (var i = 0; i < parent1.Count; i += 1)
            {
                offspring[i] = (i % 2 == 0 ? parent1 : parent2)[i];
            }

            return offspring;
        }
    }
}
