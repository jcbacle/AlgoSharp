using System.Collections;
using System.Collections.Generic;

namespace AlgoSharp.Algs.PriorityQueue
{
    public class MinPriorityQueue<T> : IEnumerable<T>
    {
        private readonly List<T> _array;
        private readonly Comparer<T> _comparer;

        public MinPriorityQueue()
        {
            _array = new List<T> {default(T)};
            _comparer = Comparer<T>.Default;
        }

        public MinPriorityQueue(T[] array) : this()
        {
            foreach (var item in array)
                Insert(item);
        }

        public int Size()
        {
            return _array.Count - 1;
        }

        public bool IsEmpty()
        {
            return Size() == 0;
        }

        public T Min()
        {
            return _array[1];
        }

        public T DelMin()
        {
            var min = _array[1];
            Swap(1, Size());
            _array.RemoveAt(Size());
            Sink(1);
            return min;
        }

        public void Insert(T item)
        {
            _array.Add(item);
            Swim(Size());
        }

        #region Array helper functions

        private void Swap(int i, int j)
        {
            var swap = _array[i];
            _array[i] = _array[j];
            _array[j] = swap;
        }

        private bool Less(T x, T y)
        {
            return _comparer.Compare(x, y) < 0;
        }

        #endregion

        #region Binary heap helper functions

        private void Sink(int k)
        {
            while (2 * k <= Size())
            {
                var j = 2*k;
                if (j < Size() && Less(_array[j + 1], _array[j])) j++;
                if (Less(_array[k], _array[j])) break;
                Swap(k, j);
                k = j;
            }
        }

        private void Swim(int k)
        {
            while (k > 1 && Less(_array[k], _array[k / 2]))
            {
                Swap(k / 2, k);
                k /= 2;
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
