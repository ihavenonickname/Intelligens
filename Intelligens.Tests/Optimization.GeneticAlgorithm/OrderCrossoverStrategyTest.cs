using Intelligens.Extras;
using Intelligens.Optimization.GeneticAlgorithm;
using Xunit;

namespace Intelligens.Tests.Optimization.GeneticAlgorithm
{
    public class OrderCrossoverStrategyTest
    {
        [Fact]
        public void Crossover_WithIndexesAtMiddle_ShouldWork()
        {
            var stubPrng = new FakeRandomListGenerator(new [] {3, 2});
            var crossoverStrategy = new OrderCrossoverStrategy<int>(stubPrng);
            var parent1 = new [] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var parent2 = new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};

            var chromosome = crossoverStrategy.Crossover(parent1, parent2);

            Assert.Equal(new []  {9, 0, 2, 3, 4, 1, 5, 6, 7, 8}, chromosome);
        }

        [Fact]
        public void Crossover_WithIndexesAtLeft_ShouldWork()
        {
            var stubPrng = new FakeRandomListGenerator(new [] {3, 0});
            var crossoverStrategy = new OrderCrossoverStrategy<int>(stubPrng);
            var parent1 = new [] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var parent2 = new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};

            var chromosome = crossoverStrategy.Crossover(parent1, parent2);

            Assert.Equal(new []  {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}, chromosome);
        }

        [Fact]
        public void Crossover_WithIndexesAtRight_ShouldWork()
        {
            var stubPrng = new FakeRandomListGenerator(new [] {3, 7});
            var crossoverStrategy = new OrderCrossoverStrategy<int>(stubPrng);
            var parent1 = new [] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var parent2 = new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};

            var chromosome = crossoverStrategy.Crossover(parent1, parent2);

            Assert.Equal(new []  {1, 2, 3, 4, 5, 6, 0, 7, 8, 9}, chromosome);
        }
    }
}
