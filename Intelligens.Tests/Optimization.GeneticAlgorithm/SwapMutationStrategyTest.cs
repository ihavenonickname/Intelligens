using Intelligens.Extras;
using Intelligens.Optimization.GeneticAlgorithm;
using Xunit;

namespace Intelligens.Tests.Optimization.GeneticAlgorithm
{
    public class SwapMutationStrategyTest
    {
        [Fact]
        public void Mutate_WithDifferentIndexes_ShouldCallRandomTwice()
        {
            // Arrange
            var stubPrng = new FakeRandomListGenerator(new [] {0, 2});
            var mutationStrategy= new SwapMutationStrategy<int>(stubPrng, 0.05);
            var chromosome = new [] {0, 1, 2};

            // Act
            mutationStrategy.Mutate(chromosome);

            // Assert
            Assert.Equal(new [] {2, 1, 0}, chromosome);
        }

        [Fact]
        public void Mutate_WithRepeatingIndexes_ShouldCallRandomUntilDifferent()
        {
            // Arrange
            var stubPrng = new FakeRandomListGenerator(new [] {0, 0, 0, 0, 0, 2});
            var mutationStrategy= new SwapMutationStrategy<int>(stubPrng, 0.05);
            var chromosome = new [] {0, 1, 2};

            // Act
            mutationStrategy.Mutate(chromosome);

            // Assert
            Assert.Equal(new [] {2, 1, 0}, chromosome);
        }
    }
}
