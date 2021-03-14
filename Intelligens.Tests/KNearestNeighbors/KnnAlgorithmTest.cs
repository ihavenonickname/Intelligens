using System;
using Intelligens.Classification.KNearestNeighbors;
using Xunit;

namespace Intelligens.Test.KNearestNeighbors
{
    public class KnnAlgorithmTest
    {
        [Fact]
        public void Classify_ShouldWork_WithValidDataset()
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
        public void Classify_ShouldWork_WithValidDataset2()
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

        [Fact]
        public void Classify_ShouldFail_WhenKIsTooLarge()
        {
            // Arrange
            var knn = new KnnAlgorithm();
            var dataset = new KnnDatasetItem<string>[] { };
            var coords = new double[] { };
            var k = 1;

            // Act
            Action f = () => knn.Classify(dataset, k, coords);

            // Assert
            Assert.Throws<ArgumentException>(f);
        }

        [Fact]
        public void Classify_ShouldFail_WhenKIsTooSmall()
        {
            // Arrange
            var knn = new KnnAlgorithm();
            var dataset = new KnnDatasetItem<string>[] { null, null, null };
            var coords = new double[] { };
            var k = 0;

            // Act
            Action f = () => knn.Classify(dataset, k, coords);

            // Assert
            Assert.Throws<ArgumentException>(f);
        }
    }
}
