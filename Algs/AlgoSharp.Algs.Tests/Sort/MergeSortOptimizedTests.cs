using System.Linq;
using AlgoSharp.Algs.Sort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Algs.Tests.Sort
{
    [TestClass]
    public class MergeSortOptimizedTests
    {
        [TestMethod]
        public void Sort2Elements()
        {
            var array = new[] { 5, 3 };
            MergeSortOptimized.Sort(array);
            Assert.IsTrue(array.SequenceEqual(new[] { 3, 5 }));
        }

        [TestMethod]
        public void Sort4Elements()
        {
            var array = new[] { 3, 2, 1, 4 };
            MergeSortOptimized.Sort(array);
            Assert.IsTrue(array.SequenceEqual(new[] { 1, 2, 3, 4 }));
        }

        [TestMethod]
        public void Sort8Elements()
        {
            var array = new[] { 5, 3, 1, 7, 4, 2, 6, 8 };
            MergeSortOptimized.Sort(array);
            Assert.IsTrue(array.SequenceEqual(new[] { 1, 2, 3, 4, 5, 6, 7, 8 }));
        }
    }
}
