using System.Collections.Generic;

namespace AlgoSharp.Algs.Sort
{
    public static class HeapSort<T>
    {
        public static void Sort(T[] array)
        {
            Sort(array, Comparer<T>.Default);
        }

        public static void Sort(T[] array, IComparer<T> comparer)
        {
            for(int i = array.Length/2; i < array.Length; i++)
            {
                Swim(array, i, comparer);
            }

            for (int i = array.Length - 1; i > 0; i--)
            {
                Swap(array, i, 0);
                Sink(array, 0, i, comparer);
            }
        }

        #region HeapSort helper functions

        private static void Swim(T[] array, int k, IComparer<T> comparer)
        {
            while (k > 0 && Less(array[(k - 1)/2], array[k], comparer))
            {
                Swap(array, (k - 1)/2, k);
                k = (k - 1) / 2;
            }
        }

        private static void Sink(T[] array, int k, int n, IComparer<T> comparer)
        {
            while (2 * k + 1 < n)
            {
                var j = 2 * k + 1;
                if (j < n - 1 && Less(array[j], array[j + 1], comparer)) j++;
                if (Less(array[j], array[k], comparer)) break;
                Swap(array, k, j);
                k = j;
            }
        }

        #endregion

        #region Array helper functions

        private static void Swap(T[] array, int i, int j)
        {
            var swap = array[j];
            array[j] = array[i];
            array[i] = swap;
        }

        private static bool Less(T x, T y, IComparer<T> comparer)
        {
            return comparer.Compare(x, y) < 0;
        }

        #endregion
    }
}
