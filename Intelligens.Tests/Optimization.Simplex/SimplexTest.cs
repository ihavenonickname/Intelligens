using Xunit;
using Intelligens.Optimization.Simplex;
using System;

namespace Intelligens.Tests.Optimization.Simplex
{
    public class SimplexSolverTest
    {
        [Fact]
        public void BuildTableau_ShouldWork_WithValidModel()
        {
            // Arrange
            var simplex = new SimplexSolver();

            var model = new LinearProblemModel(new double[] { 2, -3, 4 }, new[]
            {
                new Constraint(new double[] {4, -3,  1},  3),
                new Constraint(new double[] {1,  1,  1}, 10),
                new Constraint(new double[] {2,  1, -1}, 10),
            });

            var expected = new double[,]
            {
                { 4, -3,  1, 1, 0, 0, 0,  3},
                { 1,  1,  1, 0, 1, 0, 0, 10},
                { 2,  1, -1, 0, 0, 1, 0, 10},
                {-2,  3, -4, 0, 0, 0, 1,  0},
            };

            // Act
            var actual = simplex.BuildTableau(model);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FindPivotColumn_ShouldReturnIndex_WithNonOptimalSolution()
        {
            // Arrange
            var simplex = new SimplexSolver();

            var tableau = new double[,]
            {
                { 4, -3,  1, 1, 0, 0, 0,  3},
                { 1,  1,  1, 0, 1, 0, 0, 10},
                { 2,  1, -1, 0, 0, 1, 0, 10},
                {-2,  3, -4, 0, 0, 0, 1,  0},
            };

            var expected = 2;

            // Act
            var actual = simplex.FindPivotColumn(tableau);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FindPivotColumn_ShouldReturnNegative_WithOptimalSolution()
        {
            // Arrange
            var simplex = new SimplexSolver();

            var tableau = new double[,]
            {
                {4, -3,  1, 1, 0, 0, 0,  3},
                {1,  1,  1, 0, 1, 0, 0, 10},
                {2,  1, -1, 0, 0, 1, 0, 10},
                {0,  3,  4, 0, 0, 0, 1,  0},
            };

            int? expected = null;

            // Act
            var actual = simplex.FindPivotColumn(tableau);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FindPivotRow_ShouldWork_WithValidTableau()
        {
            // Arrange
            var simplex = new SimplexSolver();

            var tableau = new double[,]
            {
                {0, -3, 1,   1,  3, 0, 0,  3},
                {4,  1, 0,   0,  1, 0, 0, 10},
                {0,  1, 0, -10,  0, 2, 0, 10},
                {0,  3, 0,   0, -4, 0, 5, 15}
            };

            var pivotColIdx = (int)simplex.FindPivotColumn(tableau);

            var expected = 0;

            // Act
            var actual = simplex.FindPivotRow(tableau, pivotColIdx);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FindPivotRow_ShouldReturnNull_WithUnboundSolution()
        {
            // Arrange
            var simplex = new SimplexSolver();

            var tableau = new double[,]
            {
                {  0,  0, 5,  2,  0, 0,   4},
                { -8, 20, 0,  6,  5, 0,  72},
                {-20,  0, 0, 26, 15, 4, 232},
            };

            var pivotColIdx = (int)simplex.FindPivotColumn(tableau);

            int? expected = null;

            // Act
            var actual = simplex.FindPivotRow(tableau, pivotColIdx);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RowOperations_ShouldCleanPivot_WithValidTableau()
        {
            // Arrange
            var simplex = new SimplexSolver();

            var tableau = new double[,]
            {
                {1, -6   , 0, 0,   4   , 1080},
                {0,  0.66, 0, 1,  -0.34,   10},
                {0,  0.34, 1, 0,   0.34,   90},
            };

            var pivotRowIdx = 1;
            var pivotColIdx = 1;

            var expected = new double[,]
            {
                {1, 0, 0,  9.12,  0.88, 1170.9 },
                {0, 1, 0,  1.52, -0.52,   15.15},
                {0, 0, 1, -0.52,  0.52,   84.85},
            };

            // Act
            simplex.RowOperations(tableau, pivotRowIdx, pivotColIdx);

            // Assert
            Assert.Equal(expected[0, 5], tableau[0, 5], 1);
            Assert.Equal(expected[1, 5], tableau[1, 5], 1);
            Assert.Equal(expected[2, 5], tableau[2, 5], 1);
        }

        [Fact]
        public void Solve_ShouldReturnValidSolution_WithValidProblem()
        {
            // Arrange
            var simplex = new SimplexSolver();

            var model = new LinearProblemModel(new double[] { 10, 12 }, new[]
            {
                new Constraint(new double[] {1, 1}, 100),
                new Constraint(new double[] {1, 3}, 270),
            });

            // var expected = new[]
            // {
            //     new double[] {1, 0,  1.5, -0.5, 0,   15},
            //     new double[] {0, 1, -0.5,  0.5, 0,   85},
            //     new double[] {0, 0,  9  ,  1  , 1, 1170},
            // };

            var expectedOptimum = 1170;

            var expectedVariables = new[] { 15, 85 };

            // Act
            var actual = simplex.Solve(model);

            // Assert
            Assert.True(actual.IsValid);
            Assert.Equal(expectedOptimum, actual.Optimum, 1);
            Assert.Equal(expectedVariables.Length, actual.Variables.Count);
            Assert.Equal(expectedVariables[0], actual.Variables[0], 1);
            Assert.Equal(expectedVariables[1], actual.Variables[1], 1);
        }

        [Fact]
        public void Solve_ShouldReturnValidSolution_WithValidProblem2()
        {
            // Arrange
            var simplex = new SimplexSolver();

            var model = new LinearProblemModel(new double[] { 10, 7 }, new[]
            {
                new Constraint(new double[] {2, 3}, 3600),
                new Constraint(new double[] {5, 3}, 4500),
                new Constraint(new double[] {1, 0},  600),
            });

            var expectedOptimum = 10_000;

            var expectedVariables = new[] { 300, 1000 };

            // Act
            var actual = simplex.Solve(model);

            // Assert
            Assert.True(actual.IsValid);
            Assert.Equal(expectedOptimum, actual.Optimum, 1);
            Assert.Equal(expectedVariables.Length, actual.Variables.Count);
            Assert.Equal(expectedVariables[0], actual.Variables[0], 1);
            Assert.Equal(expectedVariables[1], actual.Variables[1], 1);
        }

        [Fact]
        public void Solve_ShouldReturnInvalidSolution_WithUnboundProblem()
        {
            // Arrange
            var simplex = new SimplexSolver();

            var model = new LinearProblemModel(new double[] { 1, 1 }, new[]
            {
                new Constraint(new double[] {1, 0},  0),
            });

            // Act
            var actual = simplex.Solve(model);

            // Assert
            Assert.False(actual.IsValid);
        }
    }
}
