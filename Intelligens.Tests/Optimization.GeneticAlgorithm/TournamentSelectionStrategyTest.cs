using Intelligens.Extras;
using Intelligens.Optimization.GeneticAlgorithm;
using Xunit;

namespace Intelligens.Tests.Optimization.GeneticAlgorithm
{
    public class TournamentSelectionStrategyTest
    {
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
