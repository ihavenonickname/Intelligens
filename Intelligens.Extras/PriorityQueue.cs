using System;
using System.Collections.Generic;

namespace Intelligens.Extras
{
    public class PriorityQueue<T, U> where U : IComparable
    {
        private readonly int _capacity;
        private readonly List<T> _items;
        private readonly List<U> _priorities;

        public IReadOnlyList<T> Items
        {
            get
            {
                return _items;
            }
        }

        public PriorityQueue(int capacity)
        {
            _capacity = capacity;
            _items = new List<T>(_capacity);
            _priorities = new List<U>(_capacity);
        }

        public void Add(T item, U priority)
        {
            int i;

            for (i = 0; i < _priorities.Count; i += 1)
            {
                if (priority.CompareTo(_priorities[i]) > 0)
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
    }
}
