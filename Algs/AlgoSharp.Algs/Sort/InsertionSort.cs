using System;

namespace AlgoSharp.Algs.Sort
{
    // O(n²) comparisons and swaps
    // see. http://www.sorting-algorithms.com/insertion-sort
    public static class InsertionSort
    {
        public static void Sort<T>(T[] array) where T : IComparable<T>
        {
            var n = array.Length;

            for (int i = 0; i < n; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (Less(array[j], array[j - 1]))
                        Exchange(array, j, j - 1);
                    else
                        break;
                }
            }
        }

        private static void Exchange<T>(T[] array, int i, int j)
        {
            var item = array[j];
            array[j] = array[i];
            array[i] = item;
        }

        private static bool Less<T>(T x, T y) where T : IComparable<T>
        {
            return x.CompareTo(y) < 0;
        }
    }
}
