using System.Linq;
using AlgoSharp.Algs.Sort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Algs.Tests.Sort
{
    [TestClass]
    public class SelectionSortTests
    {
        [TestMethod]
        public void SortEmptyArray()
        {
            var array = new int[0];
            
            SelectionSort.Sort(array, (x, y) => x.CompareTo(y));

            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void SortArray()
        {
            var array = new[] {3, 2, 1};

            SelectionSort.Sort(array, (x, y) => x.CompareTo(y));

            Assert.IsTrue(array.SequenceEqual(new []{1, 2, 3}));
        }
    }
}
