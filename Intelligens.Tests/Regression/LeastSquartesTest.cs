using System;
using Intelligens.Regression;
using Xunit;

namespace Intelligens.Tests.Regression
{
    public class LeastSquartesTest
    {
        [Fact]
        public void LinearFit_ShouldFail_WhenArraysHaveDifferentSize()
        {
            // Arrange
            var x = new double[] { 1, 2, 3 };
            var y = new double[] { 1, 2, 3, 4 };
            var ls = new LeastSquares();

            // Act
            Action f = () => ls.LinearFit(x, y);

            // Assert
            Assert.Throws<ArgumentException>(f);
        }

        [Fact]
        public void LinearFit_ShouldFail_WhenArraysTooSmall()
        {
            // Arrange
            var x = new double[] { 1 };
            var y = new double[] { 1 };
            var ls = new LeastSquares();

            // Act
            Action f = () => ls.LinearFit(x, y);

            // Assert
            Assert.Throws<ArgumentException>(f);
        }

        [Fact]
        public void f()
        {
            // Arrange
            var x = new double[] { 1, 2, 3, 4, 5, 6, 7 };
            var y = new double[] { 3, 4, 5, 6, 7, 8, 9 };
            var ls = new LeastSquares();
            var expected = new double[] { 2, 1 };

            // Act
            var actual = ls.LinearFit(x, y);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void g()
        {
            // Arrange
            var x = new double[] { 1, 2, 3, 4, 5, 6, 7 };
            var y = new double[] { 7, 10, 13, 16, 19, 22, 25 };
            var ls = new LeastSquares();
            var expected = new double[] { 4, 3 };

            // Act
            var actual = ls.LinearFit(x, y);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
