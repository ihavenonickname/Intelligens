using System;
using System.Collections.Generic;

namespace Intelligens.Extras
{
    public class PriorityQueue<T, U> where U : IComparable
    {
        private readonly bool _greaterFirst;
        private readonly int _capacity;
        private readonly List<T> _items;
        private readonly List<U> _priorities;

        public IList<T> Items
        {
            get
            {
                return _items;
            }
        }

        public PriorityQueue(int capacity, bool greaterFirst)
        {
            _capacity = capacity;
            _greaterFirst = greaterFirst;
            _items = new List<T>(_capacity);
            _priorities = new List<U>(_capacity);
        }

        public PriorityQueue(int capacity) : this(capacity, true)
        { }

        public void Add(T item, U priority)
        {
            int i;

            for (i = 0; i < _priorities.Count; i += 1)
            {
                if (FoundPosition(priority, _priorities[i]))
                {
                    break;
                }
            }

            if (i == _priorities.Count)
            {
                if (_priorities.Count < _capacity)
                {
                    _priorities.Add(priority);
                    _items.Add(item);
                }
            }
            else
            {
                _priorities.Insert(i, priority);
                _items.Insert(i, item);

                if (_priorities.Count > _capacity)
                {
                    _priorities.RemoveAt(_capacity);
                    _items.RemoveAt(_capacity);
                }
            }
        }

        private bool FoundPosition(U item1, U item2)
        {
            return _greaterFirst ? item1.CompareTo(item2) > 0 : item1.CompareTo(item2) < 0;
        }
    }
}
