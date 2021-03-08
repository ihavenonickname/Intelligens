using Intelligens.Extras;
using Intelligens.Optimization.GeneticAlgorithm;
using Xunit;

namespace Intelligens.Tests.Optimization.GeneticAlgorithm
{
    public class BitFlipMutationStrategyTest
    {
        [Fact]
        public void Mutate_InRightExtreme_ShouldWork()
        {
            // Arrange
            var stubPrng = new FakeRandomListGenerator(new [] {0});
            var scrambleStrategy = new BitFlipMutationStrategy(stubPrng, 0.05);
            var chromossome = new [] {true, true, true};

            // Act
            scrambleStrategy.Mutate(chromossome);

            // Assert
            Assert.Equal(chromossome, new [] {false, true, true});
        }

        [Fact]
        public void Mutate_InLeftExtreme_ShouldWork()
        {
            // Arrange
            var stubPrng = new FakeRandomListGenerator(new [] {2});
            var scrambleStrategy = new BitFlipMutationStrategy(stubPrng, 0.05);
            var chromossome = new [] {true, true, true};

            // Act
            scrambleStrategy.Mutate(chromossome);

            // Assert
            Assert.Equal(chromossome, new [] {true, true, false});
        }
    }
}
