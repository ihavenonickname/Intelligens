using System;
using System.Collections.Generic;
using Intelligens.Extras;

namespace Intelligens.Classification.KNearestNeighbors
{
    public class KnnAlgorithm
    {
        public T Classify<T>(ICollection<KnnDatasetItem<T>> dataset, int k, IReadOnlyList<double> coords)
        {
            if (k > dataset.Count)
            {
                throw new ArgumentException(nameof(k));
            }

            if (k < 1)
            {
                throw new ArgumentException(nameof(k));
            }

            var queue = new PriorityQueue<T, double>(k);

            foreach (var item in dataset)
            {
                var d = MoreMath.EuclideanDistance(item.Coordinates, coords);

                queue.Add(item.Class, d * -1);
            }

            return queue.Items[0];
        }
    }
}
