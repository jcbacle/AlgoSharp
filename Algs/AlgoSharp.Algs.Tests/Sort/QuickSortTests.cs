using System;
using System.Linq;
using AlgoSharp.Algs.Sort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Algs.Tests.Sort
{
    [TestClass]
    public class QuickSortTests
    {
        [TestMethod]
        public void SortEmptyArray()
        {
            var array = new int[0];
            QuickSort.Sort(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void SortOneElementArray()
        {
            var array = new[] {1};
            QuickSort.Sort(array);
            Assert.AreEqual(1, array.Length);            
        }

        [TestMethod]
        public void SortWithInsertionSort()
        {
            var array = new[] { 4, 2, 3, 1 };
            var expected = (int[])array.Clone();
            Array.Sort(expected);
            QuickSort.Sort(array, 4);
            Assert.IsTrue(array.SequenceEqual(expected));
        }

        [TestMethod]
        public void SortWithQuickSort()
        {
            var array = new[] { 4, 2, 3, 1 };
            var expected = (int[])array.Clone();
            Array.Sort(expected);
            QuickSort.Sort(array, 0);
            Assert.IsTrue(array.SequenceEqual(expected));
        }

        [TestMethod]
        public void SortWithNintherMedian()
        {
            var array = new[] { 4, 2, 3, 1, 4, 7, 9, 6, 8, 5, 15, 12, 4, 4, 7, 0 };
            var expected = (int[])array.Clone();
            Array.Sort(expected);
            QuickSort.Sort(array, 0);
            Assert.IsTrue(array.SequenceEqual(expected));
        }
    }
}
