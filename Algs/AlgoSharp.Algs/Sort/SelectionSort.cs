using System;

namespace AlgoSharp.Algs.Sort
{
    // o(n²) comparisons and O(n) swaps
    // see. http://www.sorting-algorithms.com/selection-sort
    public static class SelectionSort
    {
        public static void Sort<T>(T[] array, Func<T, T, int> comparison)
        {
            if (array.Length == 0) return;

            for (int i = 0; i < array.Length; i++)
            {
                var minIndex = i;
                var min = array[minIndex];

                for (int j = i; j < array.Length; j++)
                {
                    var current = array[j];
                    if (Less(current, min, comparison))
                    {
                        min = current;
                        minIndex = j;
                    }
                }

                array[minIndex] = array[i];
                array[i] = min;
            }
        }

        private static bool Less<T>(T x, T y, Func<T, T, int> comparison)
        {
            return comparison(x, y) < 0;
        }
    }
}
