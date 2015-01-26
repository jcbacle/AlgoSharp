using AlgoSharp.Algs.UnionFind;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Algs.Tests.UnionFind
{
    [TestClass]
    public class WeightedQuickUnionTests
    {
        [TestMethod]
        public void UnionTests()
        {
            var wqu = new WeightedQuickUnion(10);

            wqu.Union(4, 3);
            wqu.Union(3, 8);
            wqu.Union(6, 5);
            wqu.Union(9, 4);
            wqu.Union(2, 1);
            wqu.Union(8, 9);
            wqu.Union(5, 0);
            wqu.Union(7, 2);
            wqu.Union(6, 1);
            wqu.Union(1, 0);
            wqu.Union(6, 7);

            Assert.IsTrue(wqu.IsConnected(9, 4));
            Assert.IsTrue(wqu.IsConnected(9, 3));
            Assert.IsTrue(wqu.IsConnected(9, 8));

            Assert.IsFalse(wqu.IsConnected(9, 1));

            Assert.IsTrue(wqu.IsConnected(1, 2));
            Assert.IsTrue(wqu.IsConnected(1, 7));
            Assert.IsTrue(wqu.IsConnected(1, 6));
            Assert.IsTrue(wqu.IsConnected(1, 5));
            Assert.IsTrue(wqu.IsConnected(1, 0));
        }
    }
}
