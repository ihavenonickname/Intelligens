using System.Collections.Generic;

namespace Intelligens.Classification.KNearestNeighbors
{
    public class KnnDatasetItem<T>
    {
        public IList<double> Coordinates { get; private set; }
        public T Class { get; private set; }

        public KnnDatasetItem(IList<double> coordinates, T @class)
        {
            Coordinates = coordinates;
            Class = @class;
        }
    }
}
