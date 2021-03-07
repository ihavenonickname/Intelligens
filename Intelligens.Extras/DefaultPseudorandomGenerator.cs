using System;

namespace Intelligens.Extras
{
    public class DefaultPseudorandomGenerator : IRandomGenerator
    {
        private readonly Random _random = new Random();

        public int Next(int minimum, int maximum)
        {
            return _random.Next(minimum, maximum);
        }
        public int Next(int maximum)
        {
            return _random.Next(maximum);
        }

        public double Next()
        {
            return _random.NextDouble();
        }
    }
}
