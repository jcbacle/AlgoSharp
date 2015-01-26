using System.Linq;
using AlgoSharp.Algs.Sort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Algs.Tests.Sort
{
    [TestClass]
    public class InsertionSortTests
    {
        [TestMethod]
        public void SortEmptyArrayTest()
        {
            var array = new int[0];
            InsertionSort.Sort(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void SortArrayTest()
        {
            var array = new[] {3, 2, 1, 4, 5};
            InsertionSort.Sort(array);
            Assert.IsTrue(array.SequenceEqual(new []{1, 2, 3, 4, 5}));
        }
    }
}
