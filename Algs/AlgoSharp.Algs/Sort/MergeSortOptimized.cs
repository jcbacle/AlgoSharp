using System;

namespace AlgoSharp.Algs.Sort
{
    public static class MergeSortOptimized
    {
        private const int Cutoff = 7;

        public static void Sort<T>(T[] array) where T : IComparable<T>
        {
            var aux = (T[])array.Clone();   // Eliminate the copy to the auxiliary array for each merge
            Sort(aux, array, 0, array.Length-1);
        }

        private static void Sort<T>(T[] src, T[] dst, int lo, int hi) where T : IComparable<T>
        {
            if (hi <= lo + Cutoff - 1) {
                InsertionSort(dst, lo, hi);   // use insertion sort for small arrays to avoid too much overhead
                return;
            }

            if (lo >= hi) return;
            var mid = lo + (hi - lo) / 2;
            Sort(dst, src, lo, mid);
            Sort(dst, src, mid + 1, hi);
            if (src[mid].CompareTo(src[mid + 1]) < 0) Array.Copy(src, mid + 1, dst, mid+1, hi - mid);   // Recopy if already sorted
            Merge(src, dst, lo, mid, hi);
        }

        private static void InsertionSort<T>(T[] array, int lo, int hi) where T : IComparable<T>
        {
            for (int i = lo; i <= hi; i++)
            {
                for (int j = i; j > lo; j--)
                {
                    if (Less(array[j], array[j - 1])) Swap(array, j, j-1);
                }
            }
        }

        private static void Swap<T>(T[] array, int i, int j)
        {
            var swap = array[j];
            array[j] = array[i];
            array[i] = swap;
        }

        private static void Merge<T>(T[] src, T[] dst, int lo, int mid, int hi) where T : IComparable<T>
        {
            var i = lo;
            var j = mid + 1;
            for (int k = lo; k <= hi; k++)
            {
                if (i > mid) dst[k] = src[j++];
                else if (j > hi) dst[k] = src[i++];
                else if (Less(src[i], src[j])) dst[k] = src[i++];
                else dst[k] = src[j++];
            }
        }

        private static bool Less<T>(T x, T y) where T : IComparable<T>
        {
            return x.CompareTo(y) < 0;
        }
    }
}
