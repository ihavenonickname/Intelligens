using Intelligens.Optimization.GeneticAlgorithm;
using Xunit;

namespace Intelligens.Tests.Optimization.GeneticAlgorithm
{
    public class UniformCrossoverStrategyTest
    {
        [Fact]
        public void Crossover_WithOddChromosome_ShouldWork()
        {
            // Arrange
            var crossoverStrategy = new UniformCrossoverStrategy<double>();
            var stubParent1 = new [] {1.0, 1.0, 1.0};
            var stubParent2 = new [] {2.0, 2.0, 2.0};
            var expected = new [] {1.0, 2.0, 1.0};

            // Act
            var offspring = crossoverStrategy.Crossover(stubParent1, stubParent2);

            // Assert
            Assert.Equal(offspring, expected);
        }

        [Fact]
        public void Crossover_WithEvenChromosome_ShouldWork()
        {
            // Arrange
            var crossoverStrategy = new UniformCrossoverStrategy<double>();
            var stubParent1 = new [] {1.0, 1.0, 1.0, 1.0};
            var stubParent2 = new [] {2.0, 2.0, 2.0, 2.0};
            var expected = new [] {1.0, 2.0, 1.0, 2.0};

            // Act
            var offspring = crossoverStrategy.Crossover(stubParent1, stubParent2);

            // Assert
            Assert.Equal(offspring, expected);
        }
    }
}
