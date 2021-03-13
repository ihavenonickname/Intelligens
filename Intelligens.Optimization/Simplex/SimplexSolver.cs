using System.Collections.Generic;

namespace Intelligens.Optimization.Simplex
{
    public class SimplexSolver
    {
        public IList<IList<double>> BuildTableau(LinearProblemModel model)
        {
            var tableau = new double[model.Constraints.Count + 1][];

            var totalColumns = model.Constraints.Count * 2 + 2;

            for (var i = 0; i < model.Constraints.Count; i += 1)
            {
                var constr = model.Constraints[i];

                tableau[i] = new double[totalColumns];

                for (var j = 0; j < model.CountVars; j += 1)
                {
                    tableau[i][j] = constr.Multipliers[j];
                }

                for (var j = model.CountVars; j < totalColumns; j += 1)
                {
                    tableau[i][j] = 0;
                }

                tableau[i][model.CountVars + i] = 1;
                tableau[i][totalColumns - 1] = constr.RightHandSide;
            }

            var lastRow = tableau.Length - 1;

            tableau[lastRow] = new double[totalColumns];

            for (var i = 0; i < model.CountVars; i += 1)
            {
                tableau[lastRow][i] = model.ObjectiveFunc[i] * -1;
            }

            for (var i = model.CountVars; i < totalColumns; i += 1)
            {
                tableau[lastRow][i] = 0;
            }

            tableau[lastRow][totalColumns - 2] = 1;

            return tableau;
        }

        public int? FindPivotColumn(IList<IList<double>> tableau)
        {
            var pivotColIdx = 0;
            var lastRowIdx = tableau.Count - 1;
            var lastColIdx = tableau[lastRowIdx].Count - 1;

            for (var i = 1; i < lastColIdx; i += 1)
            {
                if (tableau[lastRowIdx][i] < tableau[lastRowIdx][pivotColIdx])
                {
                    pivotColIdx = i;
                }
            }

            if (tableau[lastRowIdx][pivotColIdx] >= 0)
            {
                return null;
            }

            return pivotColIdx;
        }

        public int? FindPivotRow(IList<IList<double>> tableau, int pivotColIdx)
        {
            var lastColIdx = tableau[0].Count - 1;
            double? lesserRatio = null;
            int? lesserRatioIdx = null;

            for (var i = 0; i < tableau.Count; i += 1)
            {
                if (tableau[i][pivotColIdx] > 0)
                {
                    var x = tableau[i][lastColIdx] / tableau[i][pivotColIdx];

                    if (lesserRatio == null || x < lesserRatio)
                    {
                        lesserRatio = x;
                        lesserRatioIdx = i;
                    }
                }
            }

            return lesserRatioIdx;
        }

        public void RowOperations(IList<IList<double>> tableau, int pivotRowIdx, int pivotColIdx)
        {
            var countCols = tableau[0].Count;
            var pivot = tableau[pivotRowIdx][pivotColIdx];

            for (var i = 0; i < countCols; i += 1)
            {
                tableau[pivotRowIdx][i] /= pivot;
            }

            for (var i = 0; i < tableau.Count; i += 1)
            {
                if (i != pivotRowIdx)
                {
                    var multiplier = tableau[i][pivotColIdx] * -1;

                    for (var j = 0; j < countCols; j += 1)
                    {
                        tableau[i][j] += tableau[pivotRowIdx][j] * multiplier;
                    }
                }
            }
        }

        public LinearProblemSolution ExtractSolution(IList<IList<double>> tableau, int countVars)
        {
            var lastRowIdx = tableau.Count - 1;
            var lastColIdx = tableau[0].Count - 1;

            var variables = new double[countVars];

            for (var colIdx = 0; colIdx < countVars; colIdx += 1)
            {
                for (var rowIdx = 0; rowIdx <= lastRowIdx; rowIdx += 1)
                {
                    if (tableau[rowIdx][colIdx] != 0)
                    {
                        variables[colIdx] = tableau[rowIdx][lastColIdx];

                        break;
                    }
                }
            }

            var optimum = tableau[lastRowIdx][lastColIdx];

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
