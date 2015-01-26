using AlgoSharp.Algs.UnionFind;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Algs.Tests.UnionFind
{
    [TestClass]
    public class QuickFindTests
    {
        [TestMethod]
        public void UnionTests()
        {
            var qf = new QuickFind(10);
            
            qf.Union(4, 3);
            qf.Union(3, 8);
            qf.Union(6, 5);
            qf.Union(9, 4);
            qf.Union(2, 1);
            qf.Union(8, 9);
            qf.Union(5, 0);
            qf.Union(7, 2);
            qf.Union(6, 1);
            qf.Union(1, 0);
            qf.Union(6, 7);

            Assert.IsTrue(qf.IsConnected(9,4));
            Assert.IsTrue(qf.IsConnected(9, 3));
            Assert.IsTrue(qf.IsConnected(9, 8));

            Assert.IsFalse(qf.IsConnected(9,1));

            Assert.IsTrue(qf.IsConnected(1, 2));
            Assert.IsTrue(qf.IsConnected(1, 7));
            Assert.IsTrue(qf.IsConnected(1, 6));
            Assert.IsTrue(qf.IsConnected(1, 5));
            Assert.IsTrue(qf.IsConnected(1, 0));
        }
    }
}
