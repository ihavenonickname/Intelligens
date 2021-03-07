using System;
using System.Collections.Generic;
using System.Linq;
using Intelligens.Extras;
using Intelligens.Optimization.GeneticAlgorithm;
using Xunit;

namespace Intelligens.Tests.Optimization.GeneticAlgorithm
{
    public class GeneticAlgorithmTest
    {
        public class MockGeneCreationStrategy : IGeneCreationStrategy<double>
        {
            private readonly Random _random = new Random();

            public double CreateGene()
            {
                return _random.Next(-1_000, 999) + _random.NextDouble();
            }
        }

        public class MockFitnessEvaluationStrategy : IFitnessEvaluationStrategy<double>
        {
            public double EvaluateFitness(IList<double> chromossome)
            {
                var result = SumDiff(chromossome);

                return result == 0 ? double.MaxValue : (1 / result);
            }

            public bool IsFitnessEnough(double fitness)
            {
                return fitness >= 10;
            }
        }

        public static double SumDiff(IEnumerable<double> numbers)
        {
            return Math.Abs(10 - numbers.Sum());
        }

        [Fact]
        public void Test1()
        {
            // Arrange
            const int POPULATION_SIZE = 200;
            const int MAX_GENERATION = 10_000;
            const int CHROMOSOME_SIZE = 8;
            const double MUTATION_RATE = 0.05;

            var defaultRandom = new DefaultPseudorandomGenerator();
            var geneCreationStrategy = new MockGeneCreationStrategy();
            var fitnessEvaluationStrategy = new MockFitnessEvaluationStrategy();
            var selectionStrategy = new TournamentSelectionStrategy<double>(50, defaultRandom);
            var crossoverStrategy = new UniformCrossoverStrategy<double>();
            var mutationStrategy = new RandomResetMutationStrategy<double>(geneCreationStrategy, defaultRandom, MUTATION_RATE);
            var ga = new GeneticAlgorithm<double>(selectionStrategy, crossoverStrategy, mutationStrategy, geneCreationStrategy, fitnessEvaluationStrategy);

            // Act
            var simulationResult = ga.Evolve(POPULATION_SIZE, MAX_GENERATION, CHROMOSOME_SIZE);

            // Assert
            Assert.True(simulationResult.Generation <= MAX_GENERATION, "GA did not converge");
            Assert.True(SumDiff(simulationResult.Best) < 1.0, "Wrong chromossome");
        }
    }
}
