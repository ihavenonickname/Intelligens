using Intelligens.Extras;
using Xunit;

namespace Intelligens.Tests.Extras
{
    public class MoreMath_MultiplyTest
    {
        [Fact]
        public void Multiply_ShouldWork_WithSquareMatrices1()
        {
            // Arrange
            var a = new double[,]
            {
                {0, 1},
                {0, 0}
            };

            var b = new double[,]
            {
                {0, 0},
                {1, 0},
            };

            var expected = new double[,]
            {
                {1, 0},
                {0, 0}
            };

            // Act
            var actual = MoreMath.Multiply(a, b);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Multiply_ShouldWork_WithSquareMatrices2()
        {
            // Arrange
            var a = new double[,]
            {
                {0, 0},
                {1, 0}
            };

            var b = new double[,]
            {
                {0, 1},
                {0, 0},
            };

            var expected = new double[,]
            {
                {0, 0},
                {0, 1}
            };

            // Act
            var actual = MoreMath.Multiply(a, b);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Multiply_ShouldWork_WithDifferentMatrices()
        {
            // Arrange
            var a = new double[,]
            {
                {3, 4, 2},
            };

            var b = new double[,]
            {
                {13, 9, 7, 15},
                {8, 7, 4, 6},
                {6, 4, 0, 3}
            };

            var expected = new double[,]
            {
                {83, 63, 37, 75},
            };

            // Act
            var actual = MoreMath.Multiply(a, b);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
