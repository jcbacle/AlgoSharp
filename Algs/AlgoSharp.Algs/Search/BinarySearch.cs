namespace AlgoSharp.Algs.Search
{
    public static class BinarySearch
    {
        public static int Rank(int key, int[] a)
        {
            var lowIndex = 0;
            var highIndex = a.Length - 1;

            while (lowIndex <= highIndex)
            {
                var midIndex = lowIndex + (highIndex - lowIndex)/2;
                var mid = a[midIndex];
                if (key < mid)
                {
                    // Left
                    highIndex = midIndex - 1;
                }
                else if (key > mid)
                {
                    // Right
                    lowIndex = midIndex + 1;
                }
                else
                {
                    // Key = mid
                    return midIndex;
                }
            }
            return -1;
        }
    }
}
