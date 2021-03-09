using System;
using System.Collections.Generic;

namespace Intelligens.Optimization.GeneticAlgorithm
{
    public interface ISelectionStrategy<T>
    {
        Tuple<IList<T>, IList<T>> Select(IList<IList<T>> population, IList<double> fitness);
    }
}
