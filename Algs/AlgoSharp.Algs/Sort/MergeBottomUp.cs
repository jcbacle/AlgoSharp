using System;

namespace AlgoSharp.Algs.Sort
{
    public static class MergeBottomUp
    {
        public static void Sort<T>(T[] array) where T : IComparable<T>
        {
            var n = array.Length;
            var aux = new T[n];

            // Interval size: 2, 4, 8, ..., 2^n
            for (int s = 2; s <= n; s = 2 * s)
            {
                for (int i = 0; i < n; i = i + s)
                {
                    var lo = i;
                    var hi = i + s - 1;
                    var mid = lo + (hi - lo)/2;
                    Merge(array, aux, lo, mid, hi);
                }
            }
        }

        private static void Merge<T>(T[] array, T[] aux, int lo, int mid, int hi) where T : IComparable<T>
        {   
            Console.WriteLine("Merge(lo: {0}, hi: {1}, mid: {2})", lo, hi, mid);

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
