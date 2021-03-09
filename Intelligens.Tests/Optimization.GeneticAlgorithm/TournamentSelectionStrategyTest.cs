using Intelligens.Extras;
using Intelligens.Optimization.GeneticAlgorithm;
using Xunit;

namespace Intelligens.Tests.Optimization.GeneticAlgorithm
{
    public class TournamentSelectionStrategyTest
    {
        [Fact]
        public void Selection_ShouldPreferFitterOverTime()
        {
            // Arrange
            var prng = new DefaultRandomGenerator();
            var selectionStrategy = new TournamentSelectionStrategy<int>(5, prng);
            var population = new []
            {
                new [] {9, 9, 9},
                new [] {8, 8, 8},
                new [] {7, 7, 7},
                new [] {6, 6, 6},
                new [] {5, 5, 5},
                new [] {4, 4, 4},
                new [] {3, 3, 3},
                new [] {2, 2, 2},
                new [] {1, 1, 1},
                new [] {0, 0, 0},
            };

            var count = 10000;
            var sum = 0.0;

            // Act
            for (var i = 0; i < count; i += 1)
            {
                var pair = selectionStrategy.Select(population, null);
                sum += (pair.Item1[0] + pair.Item2[0]) / 2.0;
            }

            var average = sum / count;

            // Assert
            Assert.True(average > 5);
        }

        [Fact]
        public void Tournament_ShouldSelectTheBest()
        {
            // Arrange
            var stubPrng = new FakeRandomListGenerator(new [] { 1, 3, 5 });
            var tournamentSelectionStrategy = new TournamentSelectionStrategy<int>(3, stubPrng);
            var population = new []
            {
                new [] {0, 0, 0},
                new [] {1, 1, 1},
                new [] {2, 2, 2},
                new [] {3, 3, 3},
                new [] {4, 4, 4},
                new [] {5, 5, 5},
                new [] {6, 6, 6},
            };

            // Act
            var parents = tournamentSelectionStrategy.Select(population, null);

            // Assert
            Assert.Equal(parents.Item1, population[1]);
            Assert.Equal(parents.Item2, population[3]);
        }
    }
}
