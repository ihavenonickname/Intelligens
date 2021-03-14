using System;
using System.Collections.Generic;

namespace Intelligens.Clustering.DBSCAN
{
    public class Dbscan
    {
        private const int NOISE = -1;
        private const int NOT_VISITED = 0;

        private readonly IDistance _distance;
        private readonly int _dataCount;

        public Dbscan(int dataCount, IDistance distance)
        {
            _dataCount = dataCount;
            _distance = distance;
        }

        // TODO Allow this to be injected
        private List<int> RangeQuery(int i, double eps)
        {
            var neighbors = new List<int>();

            for (var j = 0; j < _dataCount; j += 1)
            {
                if (i != j && _distance.Distance(i, j) <= eps)
                {
                    neighbors.Add(j);
                }
            }

            return neighbors;
        }

        public IList<int> FindClusters(double eps, int minPts)
        {
            var currentCluster = 1;
            var labels = new int[_dataCount];

            for (var i = 0; i < _dataCount; i += 1)
            {
                if (labels[i] != NOT_VISITED)
                {
                    continue;
                }

                var neighborsSet = new HashSet<int>(RangeQuery(i, eps));

                if (neighborsSet.Count < minPts)
                {
                    labels[i] = NOISE;

                    continue;
                }

                var neighborsQueue = new Queue<int>(neighborsSet);

                labels[i] = currentCluster;

                while (neighborsQueue.Count > 0)
                {
                    var j = neighborsQueue.Dequeue();

                    if (labels[j] != NOT_VISITED)
                    {
                        continue;
                    }

                    labels[j] = currentCluster;

                    var N = RangeQuery(j, eps);

                    if (N.Count >= minPts)
                    {
                        foreach (var neighbor in N)
                        {
                            if (!neighborsSet.Contains(neighbor))
                            {
                                neighborsSet.Add(neighbor);
                                neighborsQueue.Enqueue(neighbor);
                            }
                        }
                    }
                }

                currentCluster += 1;
            }

            return labels;
        }
    }
}
