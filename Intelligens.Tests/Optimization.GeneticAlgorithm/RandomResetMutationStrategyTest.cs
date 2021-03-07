using Intelligens.Extras;
using Intelligens.Optimization.GeneticAlgorithm;
using Xunit;

namespace Intelligens.Tests.Optimization.GeneticAlgorithm
{
    public class RandomResetMutationStrategyTest
    {
        private class FakeGeneCreationStrategy : IGeneCreationStrategy<int>
        {
            public int CreateGene()
            {
                return 42;
            }
        }

        [Fact]
        public void Mutate_ShouldUpdateInplace()
        {
            // Arrange
            var stubPrng = new FakeRandomFixedListGenerator(new [] {1});
            var stubGeneCreation = new FakeGeneCreationStrategy();
            var mutationStrategy = new RandomResetMutationStrategy<int>(stubGeneCreation, stubPrng, 0);
            var chromossome = new [] { 0, 0, 0 };

            // Act
            mutationStrategy.Mutate(chromossome);

            // Assert
            Assert.Equal(42, chromossome[1]);
        }

        [Fact]
        public void ShouldMutate_WithZeroRate_ShouldAlwaysReturnFalse()
        {
            // Arrange
            var stubPrng = new FakeRandomConstantGenerator(0, 0.0);
            var stubGeneCreation = new FakeGeneCreationStrategy();
            var mutationStrategy = new RandomResetMutationStrategy<int>(stubGeneCreation, stubPrng, 0);
            var counter = 0;

            // Act
            for (var i = 0; i < 1_000; i += 1)
            {
                if (mutationStrategy.ShouldMutate())
                {
                    counter += 1;
                }
            }

            // Assert
            Assert.Equal(0, counter);
        }

        [Fact]
        public void ShouldMutate_WithMaxRate_ShouldAlwaysReturnTrue()
        {
            // Arrange
            var stubPrng = new FakeRandomConstantGenerator(0, 0.0);
            var stubGeneCreation = new FakeGeneCreationStrategy();
            var mutationStrategy = new RandomResetMutationStrategy<int>(stubGeneCreation, stubPrng, 1);
            var counter = 0;

            // Act
            for (var i = 0; i < 1_000; i += 1)
            {
                if (!mutationStrategy.ShouldMutate())
                {
                    counter += 1;
                }
            }

            // Assert
            Assert.Equal(0, counter);
        }
    }
}
