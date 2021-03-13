using Xunit;
using Intelligens.Optimization.Simplex;
using System;

namespace Intelligens.Tests.Optimization.Simplex
{
    public class LinearProblemModelTest
    {
        [Fact]
        public void LinearProblemModel_ShouldFail_WithWeirdVariableCount()
        {
            // Arrange
            var objective = new double[] { 1, 1, 1 };
            var constraints = new[]
            {
                new Constraint(new double[] {1},  3),
                new Constraint(new double[] {1,  1}, 10),
                new Constraint(new double[] {1,  1, -1}, 10),
            };

            // Act
            Action f = () => new LinearProblemModel(objective, constraints);

            // Assert
            Assert.Throws<ArgumentException>(f);
        }
    }
}
