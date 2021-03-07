using Intelligens.Extras;
using Intelligens.Optimization.GeneticAlgorithm;
using Xunit;

namespace Intelligens.Tests.Optimization.GeneticAlgorithm
{
    public class ScrambleMutationStrategyTest
    {
        [Fact]
        public void Mutate_InRightExtreme_ShouldWork()
        {
            // Arrange
            var stubPrng = new FakeRandomListGenerator(new [] {3, 7, 7, 7, 7});
            var scrambleStrategy = new ScrambleMutationStrategy<int>(stubPrng, 0.05);
            var chromossome = new [] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            // Act
            scrambleStrategy.Mutate(chromossome);

            // Assert
            Assert.Equal(chromossome, new [] {0, 1, 2, 3, 4, 5, 6, 8, 9, 7});
        }

        [Fact]
        public void Mutate_InLeftExtreme_ShouldWork()
        {
            // Arrange
            var stubPrng = new FakeRandomListGenerator(new [] {3, 0, 0, 0, 0});
            var scrambleStrategy = new ScrambleMutationStrategy<int>(stubPrng, 0.05);
            var chromossome = new [] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            // Act
            scrambleStrategy.Mutate(chromossome);

            // Assert
            Assert.Equal(chromossome, new [] {1, 2, 0, 3, 4, 5, 6, 7, 8, 9});
        }
    }
}
