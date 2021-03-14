using Intelligens.Classification.KNearestNeighbors;
using Xunit;

namespace Intelligens.Test.KNearestNeighbors
{
    public class KnnAlgorithmTest
    {
        [Fact]
        public void f()
        {
            // Arrange
            var knn = new KnnAlgorithm();
            var dataset = new[]
            {
                new KnnDatasetItem<string>(new [] {0.0, 0.0}, "a"),
                new KnnDatasetItem<string>(new [] {1.0, 0.0}, "a"),
                new KnnDatasetItem<string>(new [] {0.0, 1.0}, "a"),
                new KnnDatasetItem<string>(new [] {1.0, 1.0}, "a"),
                new KnnDatasetItem<string>(new [] {8.0, 8.0}, "b"),
                new KnnDatasetItem<string>(new [] {9.0, 8.0}, "b"),
                new KnnDatasetItem<string>(new [] {8.0, 9.0}, "b"),
                new KnnDatasetItem<string>(new [] {9.0, 9.0}, "b"),
            };
            var coords = new[] { 0.5, 0.5 };
            var expected = "a";

            // Act
            var actual = knn.Classify(dataset, 4, coords);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void g()
        {
            // Arrange
            var knn = new KnnAlgorithm();
            var dataset = new[]
            {
                new KnnDatasetItem<string>(new [] {0.0, 0.0}, "a"),
                new KnnDatasetItem<string>(new [] {1.0, 0.0}, "a"),
                new KnnDatasetItem<string>(new [] {0.0, 1.0}, "a"),
                new KnnDatasetItem<string>(new [] {1.0, 1.0}, "a"),
                new KnnDatasetItem<string>(new [] {8.0, 8.0}, "b"),
                new KnnDatasetItem<string>(new [] {9.0, 8.0}, "b"),
                new KnnDatasetItem<string>(new [] {8.0, 9.0}, "b"),
                new KnnDatasetItem<string>(new [] {9.0, 9.0}, "b"),
            };
            var coords = new[] { 8.5, 8.5 };
            var expected = "b";

            // Act
            var actual = knn.Classify(dataset, 4, coords);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
