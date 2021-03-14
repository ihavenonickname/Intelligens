using System;
using System.Collections.Generic;
using Intelligens.Extras;

namespace Intelligens.Classification.KNearestNeighbors
{
    public class KnnAlgorithm
    {
        private double Distance(IList<double> coords1, IList<double> coords2)
        {
            var acc = 0.0;

            for (var i = 0; i < coords1.Count; i += 1)
            {
                acc += Math.Pow(coords1[i] - coords2[i], 2);
            }

            return Math.Sqrt(acc);
        }

        public T Classify<T>(ICollection<KnnDatasetItem<T>> dataset, int k, IList<double> coords)
        {
            if (dataset.Count < k)
            {
                throw new ArgumentException(nameof(k));
            }

            var queue = new PriorityQueue<T, double>(k);

            foreach (var item in dataset)
            {
                var d = Distance(item.Coordinates, coords);
                queue.Add(item.Class, d * -1);
            }

            return queue.Items[0];
        }
    }
}
