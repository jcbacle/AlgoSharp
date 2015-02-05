using System;

namespace AlgoSharp.Algs.Sort
{
    /// <summary>
    /// Cutoff to insertion sort for small subarrays
    /// Partition scheme: Bentley-Mcllroy 3-way partitioning
    /// Partition item: Tukey's ninther
    /// 
    /// O(n.ln(n))
    /// See. http://www.sorting-algorithms.com/quick-sort
    /// </summary>
    public static class QuickSort
    {
        private const int DefaultCutOff = 10;

        public static void Sort<T>(T[] array, int cutoff) where T : IComparable<T>
        {
            Sort(array, 0, array.Length - 1, cutoff);
        }

        public static void Sort<T>(T[] array) where T : IComparable<T>
        {
            Sort(array, 0, array.Length - 1, DefaultCutOff);
        }

        private static void Sort<T>(T[] array, int lo, int hi, int cutoff) where T : IComparable<T>
        {
            // Exit condition
            if (lo + cutoff >= hi)
            {
                InsertionSort(array, lo, hi);
                return;
            }

            var i = lo;
            var lt = lo;
            var gt = hi;

            // Before
            var pivotIndex = PivotIndex(array, lo, hi);
            Exchange(array, pivotIndex, lo);
            var pivot = array[pivotIndex];

            // During
            while (i <= gt)
            {
                var cmp = array[i].CompareTo(pivot);
                if (cmp < 0) Exchange(array, lt++, i++);
                else if (cmp > 0) Exchange(array, i, gt--);
                else i++;
            }

            // After
            Sort(array, lo, lt-1, cutoff);
            Sort(array, gt+1, hi, cutoff);
        }

        private static void InsertionSort<T>(T[] array, int lo, int hi) where T : IComparable<T>
        {
            for (int i = lo; i <= hi; i++)
                for (int j = i; j > lo; j--)
                    if (Less(array[j], array[j-1])) Exchange(array, j, j-1);
        }

        private static void Exchange<T>(T[] array, int i, int j) where T : IComparable<T>
        {
            var swap = array[j];
            array[j] = array[i];
            array[i] = swap;
        }

        private static int PivotIndex<T>(T[] array, int lo, int hi) where T : IComparable<T>
        {
            var n = hi - lo + 1;
            if (n < 16)
                return MedianIndex(array, lo, lo + n / 2, hi);

            var range = n / 8;
            var m1 = MedianIndex(array, lo, lo + range, lo + range * 2);
            var m2 = MedianIndex(array, lo + range * 3, lo + range * 4, lo + range * 5);
            var m3 = MedianIndex(array, lo + range * 6, lo + range * 7, hi);
            return MedianIndex(array, m1, m2, m3);
        }

        private static int MedianIndex<T>(T[] array, int i, int j, int k) where T : IComparable<T>
        {
            return Less(array[i], array[j])
                ? (Less(array[i], array[k]) ? (Less(array[j], array[k]) ? j : k) : i)
                : (Less(array[k], array[i]) ? (Less(array[j], array[k]) ? k : j) : i);
        }

        private static bool Less<T>(IComparable<T> p, IComparable<T> q)
        {
            return p.CompareTo((T)q) < 0;
        }
    }
}
