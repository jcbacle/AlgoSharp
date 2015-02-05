using System;
using System.Collections.Generic;

namespace AlgoSharp.Algs.Search
{
    public static class QuickSelect<T>
    {
        #region QuickSelect API

        public static T Select(T[] array, int k)
        {
            return Select(array, k, Comparer<T>.Default);
        }

        public static T Select(T[] array, int k, IComparer<T> comparer)
        {
            if (k < 1 || k > array.Length) throw new ArgumentOutOfRangeException(string.Format("k must be between 1 and {0}", array.Length));

            Select(array, k - 1, 0, array.Length - 1, comparer);

            return array[k - 1];
        }

        #endregion

        #region QuickSelect helper functions

        private static void Select(T[] array, int k, int lo, int hi, IComparer<T> comparer)
        {
            if (lo == hi) return;

            var pivotIndex = PivotIndex(array, lo, hi, comparer);
            var pivot = array[pivotIndex];
            Swap(array, lo, pivotIndex);
            
            var lt = lo;
            var gt = hi;
            var i = lo;

            while (i <= gt)
            {
                var current = array[i];
                var cmp = comparer.Compare(current, pivot);
                if (cmp < 0)
                {
                    Swap(array, i++, lt++);
                }
                else if (cmp > 0)
                {
                    Swap(array, i, gt--);
                }
                else
                {
                    i++;
                }
            }

            if (k < lt) Select(array, k, lo, lt - 1, comparer);
            else if (k > gt) Select(array, k, gt + 1, hi, comparer);
        }

        private static int PivotIndex(T[] array, int lo, int hi, IComparer<T> comparer)
        {
            var n = array.Length;
            if (n < 16)
            {
                return Median(array, lo, lo + n/2, hi, comparer);
            }

            var range = n/8;
            var m1 = Median(array, lo, lo + range, lo + 2*range, comparer);
            var m2 = Median(array, lo + 3*range, lo + 4*range, lo + 5*range, comparer);
            var m3 = Median(array, lo + 6*range, lo + 7*range, hi, comparer);
            return Median(array, m1, m2, m3, comparer);
        }

        private static int Median(T[] array, int i, int j, int k, IComparer<T> comparer)
        {
            return Less(array[i], array[j], comparer)
                ? (Less(array[i], array[k], comparer) ? (Less(array[j], array[k], comparer) ? j : k) : i)
                : (Less(array[k], array[j], comparer) ? (Less(array[j], array[k], comparer) ? k : j) : i);
        }

        #endregion

        #region Array helper functions

        private static bool Less(T x, T y, IComparer<T> comparer)
        {
            return comparer.Compare(x, y) < 0;
        }

        private static void Swap(T[] array, int i, int j)
        {
            var swap = array[j];
            array[j] = array[i];
            array[i] = swap;
        }

        #endregion
    }
}
