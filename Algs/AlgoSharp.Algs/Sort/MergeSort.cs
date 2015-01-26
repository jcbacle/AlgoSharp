using System;

namespace AlgoSharp.Algs.Sort
{
    // O(n.lg(n)) time
    // See. http://www.sorting-algorithms.com/merge-sort
    public static class MergeSort
    {
        public static void Sort<T>(T[] array) where T : IComparable<T>
        {
            var aux = new T[array.Length];
            Sort(array, aux, 0, array.Length - 1);
        }

        private static void Sort<T>(T[] array, T[] aux, int lo, int hi) where T : IComparable<T>
        {
            if (lo >= hi) return;
            var mid = lo + (hi - lo) / 2;
            Sort(array, aux, lo, mid);
            Sort(array, aux, mid + 1, hi);
            if (Less(array[mid], array[mid + 1])) return;   // Stop if already sorted
            Merge(array, aux, lo, mid, hi);
        }

        private static void Merge<T>(T[] array, T[] aux, int lo, int mid, int hi) where T : IComparable<T>
        {
            // Copy to auxiliary array
            for (int k = lo; k <= hi; k++)
            {
                aux[k] = array[k];
            }

            var i = lo;
            var j = mid + 1;
            for (int k = lo; k <= hi; k++)
            {
                if (i > mid) array[k] = aux[j++];
                else if (j > hi) array[k] = aux[i++];
                else if (Less(aux[i], aux[j])) array[k] = aux[i++];
                else array[k] = aux[j++];
            }
        }

        private static bool Less<T>(IComparable<T> x, T y)
        {
            return x.CompareTo(y) < 0;
        }
    }
}
