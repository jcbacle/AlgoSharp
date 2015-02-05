using System.Linq;
using AlgoSharp.Algs.Sort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Algs.Tests.Sort
{
    [TestClass]
    public class HeapSortTests
    {
        [TestMethod]
        public void SortTest()
        {
            var input = new[] {1, 2, 3, 4, 5, 6};
            HeapSort<int>.Sort(input);
            Assert.IsTrue(input.SequenceEqual(new [] {1, 2, 3, 4, 5, 6}));
        }
    }
}
