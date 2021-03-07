using System;
using System.Collections.Generic;
using Intelligens.Extras;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public class GeneticAlgorithm<T>
    {
        private readonly ISelectionStrategy<T> _selectionStrategy;
        private readonly ICrossoverStrategy<T> _crossoverStrategy;
        private readonly IMutationStrategy<T> _mutationStrategy;
        private readonly IGeneCreationStrategy<T> _geneCreationStrategy;
        private readonly IFitnessEvaluationStrategy<T> _fitnessEvaluationStrategy;

        public GeneticAlgorithm(
            ISelectionStrategy<T> selectionStrategy,
            ICrossoverStrategy<T> crossoverStrategy,
            IMutationStrategy<T> mutationStrategy,
            IGeneCreationStrategy<T> geneCreationStrategy,
            IFitnessEvaluationStrategy<T> fitnessEvaluationStrategy)
        {
            _selectionStrategy = selectionStrategy;
            _crossoverStrategy = crossoverStrategy;
            _mutationStrategy = mutationStrategy;
            _geneCreationStrategy = geneCreationStrategy;
            _fitnessEvaluationStrategy = fitnessEvaluationStrategy;
        }

        public EvolutionResult<T> Evolve(int populationSize, int maxGenerations, int chromossomeSize)
        {
            IList<IList<T>> population = new T[populationSize][];
            double[] fitting = new double[populationSize];
            int generation = 1;

            for (int i = 0; i < populationSize; i += 1)
            {
                population[i] = new T[chromossomeSize];

                for (var j = 0; j < chromossomeSize; j += 1)
                {
                    population[i][j] = _geneCreationStrategy.CreateGene();
                }
            }

            for (; generation <= maxGenerations; generation += 1)
            {
                for (int i = 0; i < populationSize; i += 1)
                {
                    fitting[i] = _fitnessEvaluationStrategy.EvaluateFitness(population[i]);
                }

                Array.Sort(fitting, (T[][])population);
                Array.Reverse((T[][])population);
                Array.Reverse(fitting);

                if (_fitnessEvaluationStrategy.IsFitnessEnough(fitting[0]))
                {
                    break;
                }

                IList<IList<T>> nextPopulation = new T[populationSize][];

                for (var i = 0; i < populationSize; i += 1)
                {
                    var selectionResult = _selectionStrategy.Select(population);
                    var offspring = _crossoverStrategy.Crossover(selectionResult.Item1, selectionResult.Item2);

                    if (_mutationStrategy.ShouldMutate())
                    {
                        _mutationStrategy.Mutate(offspring);
                    }

                    nextPopulation[i] = offspring;
                }

                population = nextPopulation;
            }

            return new EvolutionResult<T>(population[0], fitting[0], generation);
        }
    }
}
