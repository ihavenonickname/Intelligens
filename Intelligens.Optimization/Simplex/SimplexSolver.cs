using System.Collections.Generic;

namespace Intelligens.Optimization.Simplex
{
    public class SimplexSolver
    {
        public double[,] BuildTableau(LinearProblemModel model)
        {
            var countRows = model.Constraints.Count + 1;
            var countCols = model.Constraints.Count * 2 + 2;
            var tableau = new double[countRows, countCols];

            for (var i = 0; i < model.Constraints.Count; i += 1)
            {
                var constr = model.Constraints[i];

                for (var j = 0; j < model.CountVars; j += 1)
                {
                    tableau[i, j] = constr.Multipliers[j];
                }

                for (var j = model.CountVars; j < countCols; j += 1)
                {
                    tableau[i, j] = 0;
                }

                tableau[i, model.CountVars + i] = 1;
                tableau[i, countCols - 1] = constr.RightHandSide;
            }

            var lastRow = countRows - 1;

            for (var i = 0; i < model.CountVars; i += 1)
            {
                tableau[lastRow, i] = model.ObjectiveFunc[i] * -1;
            }

            for (var i = model.CountVars; i < countCols; i += 1)
            {
                tableau[lastRow, i] = 0;
            }

            tableau[lastRow, countCols - 2] = 1;

            return tableau;
        }

        public int? FindPivotColumn(double[,] tableau)
        {
            var pivotColIdx = 0;
            var lastRowIdx = tableau.GetLength(0) - 1;
            var lastColIdx = tableau.GetLength(1) - 1;

            for (var i = 1; i < lastColIdx; i += 1)
            {
                if (tableau[lastRowIdx, i] < tableau[lastRowIdx, pivotColIdx])
                {
                    pivotColIdx = i;
                }
            }

            if (tableau[lastRowIdx, pivotColIdx] >= 0)
            {
                return null;
            }

            return pivotColIdx;
        }

        public int? FindPivotRow(double[,] tableau, int pivotColIdx)
        {
            var lastColIdx = tableau.GetLength(1) - 1;
            double? lesserRatio = null;
            int? lesserRatioIdx = null;

            for (var i = 0; i < tableau.GetLength(0); i += 1)
            {
                if (tableau[i, pivotColIdx] > 0)
                {
                    var x = tableau[i, lastColIdx] / tableau[i, pivotColIdx];

                    if (lesserRatio == null || x < lesserRatio)
                    {
                        lesserRatio = x;
                        lesserRatioIdx = i;
                    }
                }
            }

            return lesserRatioIdx;
        }

        public void RowOperations(double[,] tableau, int pivotRowIdx, int pivotColIdx)
        {
            var countCols = tableau.GetLength(1);
            var pivot = tableau[pivotRowIdx, pivotColIdx];

            for (var i = 0; i < countCols; i += 1)
            {
                tableau[pivotRowIdx, i] /= pivot;
            }

            for (var i = 0; i < tableau.GetLength(0); i += 1)
            {
                if (i != pivotRowIdx)
                {
                    var multiplier = tableau[i, pivotColIdx] * -1;

                    for (var j = 0; j < countCols; j += 1)
                    {
                        tableau[i, j] += tableau[pivotRowIdx, j] * multiplier;
                    }
                }
            }
        }

        public LinearProblemSolution ExtractSolution(double[,] tableau, int countVars)
        {
            var lastRowIdx = tableau.GetLength(0) - 1;
            var lastColIdx = tableau.GetLength(1) - 1;

            var variables = new double[countVars];

            for (var colIdx = 0; colIdx < countVars; colIdx += 1)
            {
                for (var rowIdx = 0; rowIdx <= lastRowIdx; rowIdx += 1)
                {
                    if (tableau[rowIdx, colIdx] != 0)
                    {
                        variables[colIdx] = tableau[rowIdx, lastColIdx];

                        break;
                    }
                }
            }

            var optimum = tableau[lastRowIdx, lastColIdx];

            return new LinearProblemSolution(variables, optimum);
        }

        public LinearProblemSolution Solve(LinearProblemModel model)
        {
            var tableau = BuildTableau(model);

            while (true)
            {
                var pivotColIdx = FindPivotColumn(tableau);

                if (pivotColIdx == null)
                {
                    return ExtractSolution(tableau, model.CountVars);
                }

                var pivotRowIdx = FindPivotRow(tableau, pivotColIdx.Value);

                if (pivotRowIdx == null)
                {
                    return LinearProblemSolution.CreateInvalid();
                }

                RowOperations(tableau, pivotRowIdx.Value, pivotColIdx.Value);
            }
        }
    }
}
