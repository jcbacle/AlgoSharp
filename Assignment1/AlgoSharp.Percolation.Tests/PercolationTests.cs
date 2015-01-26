using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Percolation.Tests
{
    [TestClass]
    public class PercolationTests
    {
        private Percolation _percolation;

        [TestInitialize]
        public void SetUp()
        {
            _percolation = new Percolation(2);
        }

        [TestMethod]
        public void InitTest()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Assert.IsFalse(_percolation.IsOpen(i, j));
                    Assert.IsFalse(_percolation.IsFull(i, j));
                }
            }
            Assert.IsFalse(_percolation.Percolates());            
        }

        [TestMethod]
        public void IsOpenTest()
        {
            _percolation.Open(1, 0);            
            Assert.IsTrue(_percolation.IsOpen(1, 0));
            Assert.IsFalse(_percolation.Percolates());
        }

        [TestMethod]
        public void IsFullTest()
        {
            _percolation.Open(0, 1);
            Assert.IsTrue(_percolation.IsFull(0, 1));
            Assert.IsFalse(_percolation.Percolates());
        }

        [TestMethod]
        public void PercolatesTest()
        {
            _percolation.Open(1, 0);            
            Assert.IsFalse(_percolation.IsFull(1,0));

            _percolation.Open(0,0);

            Assert.IsTrue(_percolation.IsFull(0,0));
            Assert.IsTrue(_percolation.IsFull(1, 0));
            Assert.IsTrue(_percolation.Percolates());
        }
    }
}
