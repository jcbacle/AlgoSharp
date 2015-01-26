using AlgoSharp.Algs.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Algs.Tests.Search
{
    [TestClass]
    public class BinarySearchTests
    {
        [TestMethod]
        public void RankWithOneElement()
        {
            Assert.AreEqual(0, BinarySearch.Rank(1, new[] { 1 }));
            Assert.AreEqual(-1, BinarySearch.Rank(2, new[] { 1 }));
        }

        [TestMethod]
        public void RankWithTwoElements()
        {
            Assert.AreEqual(0, BinarySearch.Rank(1, new[] { 1 , 3}));
            Assert.AreEqual(1, BinarySearch.Rank(3, new[] { 1, 3 }));
            Assert.AreEqual(-1, BinarySearch.Rank(2, new[] { 1, 3 }));            
        }
    }
}
