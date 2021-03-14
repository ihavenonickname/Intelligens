using System;
using System.Collections.Generic;
using System.Linq;
using Intelligens.Clustering.DBSCAN;
using Intelligens.Extras;
using Xunit;
using Xunit.Abstractions;

namespace Intelligens.Tests.Clustering.DBSCAN
{
    public class DbscanTest
    {
        public class EucledianDistance : IDistance
        {
            private double[][] dataset;

            public EucledianDistance(double[][] dataset)
            {
                this.dataset = dataset;
            }

            public double Distance(int i, int j)
            {
                return MoreMath.EuclideanDistance(dataset[i], dataset[j]);
            }
        }

        private readonly ITestOutputHelper _testOutputHelper;

        public DbscanTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void FindClusters_ShouldWork_WithNoisyDataset()
        {
            // Arrange
            var dataset = new double[][]
            {
                new double[] {1, 1},
                new double[] {1, 2},
                new double[] {2, 2},
                new double[] {2, 1},

                new double[] {5, 5},

                new double[] {8, 8},
                new double[] {8, 9},
                new double[] {9, 9},
                new double[] {9, 8},
            };

            var expected = new int[]
            {
                1, 1, 1, 1, -1, 2, 2, 2, 2
            };

            var distance = new EucledianDistance(dataset);

            var dbscan = new Dbscan(dataset.Length, distance);

            // Act
            var actual = dbscan.FindClusters(2, 3);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FindCluster_ShouldWork_WithUnorderedDataset()
        {
            // Arrange
            var rnd = new Random();

            var dataset = new double[90][];

            for (var i = 0; i < 90; i += 3)
            {
                dataset[i + 0] = new double[] { rnd.Next(1, 10), rnd.Next(1, 10) };
                dataset[i + 1] = new double[] { rnd.Next(50, 60), rnd.Next(50, 60) };
                dataset[i + 2] = new double[] { rnd.Next(90, 100), rnd.Next(90, 100) };
            }

            var clutersSize = 30;

            var distance = new EucledianDistance(dataset);

            var dbscan = new Dbscan(dataset.Length, distance);

            // Act
            var labels = dbscan.FindClusters(8, 3);

            // Assert
            Assert.Equal(clutersSize, labels.Where(x => x == 1).Count());
            Assert.Equal(clutersSize, labels.Where(x => x == 2).Count());
            Assert.Equal(clutersSize, labels.Where(x => x == 3).Count());
        }
    }
}
