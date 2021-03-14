using System.Collections.Generic;

namespace Intelligens.Classification.KNearestNeighbors
{
    public class KnnDatasetItem<T>
    {
        public IReadOnlyList<double> Coordinates { get; private set; }
        public T Class { get; private set; }

        public KnnDatasetItem(IReadOnlyList<double> coordinates, T @class)
        {
            Coordinates = coordinates;
            Class = @class;
        }
    }
}
