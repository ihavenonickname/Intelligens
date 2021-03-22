using System;
using Intelligens.Extras;
using Xunit;

namespace Intelligens.Tests.Extras
{
    public class MoreMath_GaussJordanMethodTest
    {
        [Fact]
        public void GaussJordanMethod_ShouldThrowException_WithNonsquaredMatrix()
        {
            // Arrange
            var A = new double[,]
            {
                {0, 0, 0},
                {0, 0, 0},
            };

            var B = new double[]
            {
                0,
                0,
            };

            // Act
            Action f = () => MoreMath.GaussJordanMethod(A, B);

            // Assert
            Assert.Throws<ArgumentException>(f);
        }

        [Fact]
        public void GaussJordanMethod_ShouldThrowException_WithDifferentRowNumbers()
        {
            // Arrange
            var A = new double[,]
            {
                {0, 0, 0},
                {0, 0, 0},
            };

            var B = new double[]
            {
                0,
                0,
                0
            };

            // Act
            Action f = () => MoreMath.GaussJordanMethod(A, B);

            // Assert
            Assert.Throws<ArgumentException>(f);
        }

        [Fact]
        public void GaussJordanMethod_ShouldWork_WithSquareMatrices1()
        {
            // Arrange
            var A = new double[,]
            {
                {2,  2,  3,  2},
                {0,  2,  0,  1},
                {4, -3,  0,  1},
                {6,  1, -6, -5}
            };

            var B = new double[]
            {
                -2,
                 0,
                -7,
                 6
            };

            var expectedB = new double[]
            {
                -0.5,
                1,
                1.0 / 3.0,
                -2
            };

            // Act
            MoreMath.GaussJordanMethod(A, B);

            // Assert
            Assert.Equal(expectedB[0], B[0], 1);
            Assert.Equal(expectedB[1], B[1], 1);
            Assert.Equal(expectedB[2], B[2], 1);
            Assert.Equal(expectedB[3], B[3], 1);
        }
    }
}
