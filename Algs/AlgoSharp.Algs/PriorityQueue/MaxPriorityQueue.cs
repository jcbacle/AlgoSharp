using System.Collections;
using System.Collections.Generic;

namespace AlgoSharp.Algs.PriorityQueue
{
    public class MaxPriorityQueue<T> : IEnumerable<T>
    {
        private readonly List<T> _array; // resizing array (constant amortized time)
        private readonly Comparer<T> _comparer;

        /// <summary>
        /// Create an empty priority queue
        /// </summary>
        public MaxPriorityQueue()
        {
            _array = new List<T> {default(T)};
            _comparer = Comparer<T>.Default;
        }

        /// <summary>
        /// Create a priority queue with given items
        /// </summary>
        /// <param name="items"></param>
        public MaxPriorityQueue(IEnumerable<T> items) : this()
        {
            foreach (var item in items)
                Insert(item);
        }

        /// <summary>
        /// Insert an item into the priority queue
        /// </summary>
        /// <param name="item"></param>
        public void Insert(T item)
        {
            _array.Add(item);
            Swim(Size());
        }

        /// <summary>
        /// Return and remove the largest key
        /// </summary>
        /// <returns></returns>
        public T DelMax()
        {
            var max = _array[1];
            Swap(1, Size());
            _array.RemoveAt(Size());
            Sink(1);
            return max;
        }

        /// <summary>
        /// Is the priority queue empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return Size() == 0;
        }

        /// <summary>
        /// Return the largest key
        /// </summary>
        /// <returns></returns>
        public T Max()
        {
            return _array[1];
        }

        /// <summary>
        /// Number of entries in the priority queue
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return _array.Count - 1;    // 1-based array
        }

        #region Array helper functions

        private void Swap(int i, int j)
        {
            var swap = _array[j];
            _array[j] = _array[i];
            _array[i] = swap;
        }

        private bool Less(T x, T y)
        {
            return _comparer.Compare(x, y) < 0;
        }

        #endregion

        #region Binary heap helper functions

        private void Swim(int k)
        {
            while (k > 1 && Less(_array[k / 2], _array[k]))
            {
                Swap(k/2, k);
                k /= 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= Size())
            {
                var j = 2*k;
                if (j < Size() && Less(_array[j], _array[j + 1])) j++;
                if (!Less(_array[k], _array[j])) break;
                Swap(k, j);
                k = j;
            }
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            return _array.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
