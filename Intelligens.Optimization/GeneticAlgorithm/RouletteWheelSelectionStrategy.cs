using System;
using System.Collections.Generic;
using System.Linq;
using Intelligens.Extras;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public class RouletteWheelSelectionStrategy<T> : ISelectionStrategy<T>
    {
        private readonly IRandomGenerator _prng;

        public RouletteWheelSelectionStrategy(IRandomGenerator prng)
        {
            _prng = prng;
        }

        public Tuple<IList<T>, IList<T>> Select(IList<IList<T>> population, IList<double> fitness)
        {
            var parent1Idx = RunTheWheel(fitness);
            var fitnessWithoutPreviousParent = fitness.Where((_, i) => i != parent1Idx).ToArray();
            var populationWithoutPreviousParent = population.Where((_, i) => i != parent1Idx).ToArray();
            var parent2Idx = RunTheWheel(fitnessWithoutPreviousParent);

            return new Tuple<IList<T>, IList<T>>(population[parent1Idx], populationWithoutPreviousParent[parent2Idx]);
        }

        private int RunTheWheel(IList<double> fitness)
        {
            var sumFitness = fitness.Sum();
            var relativeFitness = fitness.Select(x => x / sumFitness).ToArray();
            var rnd = _prng.Next();
            var i = 0;
            var partialSum = 0.0;

            while (true)
            {
                partialSum += relativeFitness[i];

                if (partialSum >= rnd)
                {
                    return i;
                }

                i += 1;
            }
        }
    }
}
