using System;
using System.Collections.Generic;

namespace Intelligens.Extras
{
    public class FakeRandomListGenerator : IRandomGenerator
    {
        private readonly IList<int> _integers;
        private readonly IList<double> _doubles;
        private int _idxIntegers;
        private int _idxDoubles;

        public FakeRandomListGenerator(IList<int> integers) : this(integers, null)
        { }

        public FakeRandomListGenerator(IList<double> doubles) : this(null, doubles)
        { }

        public FakeRandomListGenerator(IList<int> integers, IList<double> doubles)
        {
            _integers = integers;
            _doubles = doubles;

            _idxIntegers = 0;
            _idxDoubles = 0;
        }

        public int Next(int min, int max)
        {
            if (_integers == null)
            {
                throw new Exception("Integers not provided");
            }

            if (_idxIntegers == _integers.Count)
            {
                throw new Exception("Integers exhausted");
            }

            var n = _integers[_idxIntegers];

            _idxIntegers += 1;

            return n;
        }

        public int Next(int max)
        {
            return Next(0, 0);
        }

        public double Next()
        {
            if (_doubles == null)
            {
                throw new Exception("Doubles not provided");
            }

            if (_idxDoubles == _doubles.Count)
            {
                throw new Exception("Doubles exhausted");
            }

            var n = _doubles[_idxDoubles];

            _idxDoubles += 1;

            return n;
        }
    }
}
