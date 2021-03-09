using Intelligens.Extras;
using Intelligens.Optimization.GeneticAlgorithm;
using Xunit;

namespace Intelligens.Tests.Optimization.GeneticAlgorithm
{
    public class RouletteWheelSelectionStrategyTest
    {
        [Fact]
        public void Selection_ShouldPreferFitterOverTime()
        {
            // Arrange
            var prng = new DefaultRandomGenerator();
            var selectionStrategy = new RouletteWheelSelectionStrategy<int>(prng);
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
            var fitness = new []
            {
                9.0, 8.0, 7.0, 6.0, 5.0, 4.0, 3.0, 2.0, 1.0, 0.1
            };

            var count = 10000;
            var sum = 0.0;

            // Act
            for (var i = 0; i < count; i += 1)
            {
                var pair = selectionStrategy.Select(population, fitness);
                sum += (pair.Item1[0] + pair.Item2[0]) / 2.0;
            }

            var average = sum / count;

            // Assert
            Assert.True(average > 5);
        }

        [Fact]
        public void Selection_ShouldNotSelectSameParents()
        {
            // Arrange
            var prng = new FakeRandomConstantGenerator(0, 0.5);
            var selectionStrategy = new RouletteWheelSelectionStrategy<int>(prng);
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
            var fitness = new []
            {
                9.0, 8.0, 7.0, 6.0, 5.0, 4.0, 3.0, 2.0, 1.0, 0.1
            };

            // Act
            var pair = selectionStrategy.Select(population, fitness);

            // Assert
            Assert.NotEqual(pair.Item1, pair.Item2);
        }
    }
}
