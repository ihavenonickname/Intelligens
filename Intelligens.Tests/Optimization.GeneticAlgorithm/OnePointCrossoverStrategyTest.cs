using System.Collections.Generic;
using Intelligens.Extras;
using Intelligens.Optimization.GeneticAlgorithm;
using Xunit;

namespace Intelligens.Tests.Optimization.GeneticAlgorithm
{
    public class OnePointCrossoverStrategyTest
    {
        [Fact]
        public void Crossover_AtMiddle_ShouldGenerateBalancedOffspring()
        {
            // Arrange
            var sutbPrng = new FakeRandomListGenerator(new [] {1});
            var crossoverStrategy = new OnePointCrossoverStrategy<int>(sutbPrng);
            var parent1 = new [] {0, 0, 0, 0};
            var parent2 = new [] {1, 1, 1, 1};

            // Act
            var offspring = crossoverStrategy.Crossover(parent1, parent2);

            // Assert
            Assert.Equal(new [] {0, 0, 1, 1}, offspring);
        }

        [Fact]
        public void Crossover_AtExtremeLeft_ShouldGenerateAtLeastOneParent1Gene()
        {
            // Arrange
            var sutbPrng = new FakeRandomListGenerator(new [] {0});
            var crossoverStrategy = new OnePointCrossoverStrategy<int>(sutbPrng);
            var parent1 = new [] {0, 0, 0, 0};
            var parent2 = new [] {1, 1, 1, 1};

            // Act
            var offspring = crossoverStrategy.Crossover(parent1, parent2);

            // Assert
            Assert.Equal(new [] {0, 1, 1, 1}, offspring);
        }

        [Fact]
        public void Crossover_AtExtremeRight_ShouldGenerateAtLeastOneParent2Gene()
        {
            // Arrange
            var sutbPrng = new FakeRandomListGenerator(new [] {2});
            var crossoverStrategy = new OnePointCrossoverStrategy<int>(sutbPrng);
            var parent1 = new [] {0, 0, 0, 0};
            var parent2 = new [] {1, 1, 1, 1};

            // Act
            var offspring = crossoverStrategy.Crossover(parent1, parent2);

            // Assert
            Assert.Equal(new [] {0, 0, 0, 1}, offspring);
        }
    }
}
