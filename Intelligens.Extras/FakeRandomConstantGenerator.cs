using System;
using System.Collections.Generic;

namespace Intelligens.Extras
{
    public class FakeRandomConstantGenerator : IRandomGenerator
    {
        private readonly int _integer;
        private readonly double _double;

        public FakeRandomConstantGenerator(int c1, double c2)
        {
            _integer = c1;
            _double = c2;
        }

        public int Next(int min, int max)
        {
            return _integer;
        }

        public int Next(int max)
        {
            return Next(0, 0);
        }

        public double Next()
        {
            return _double;
        }
    }
}
