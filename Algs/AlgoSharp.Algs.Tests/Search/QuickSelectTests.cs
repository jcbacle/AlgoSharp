using System;
using AlgoSharp.Algs.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Algs.Tests.Search
{
    [TestClass]
    public class QuickSelectTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OutOfLowerBoundTest()
        {
            var input = new[] {1};
            QuickSelect<int>.Select(input, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OutOfUpperBoundTest()
        {
            var input = new [] {1};
            QuickSelect<int>.Select(input, 2);
        }
        
        [TestMethod]
        public void OneElementTest()
        {
            var input = new[] {1};
            var actual = QuickSelect<int>.Select(input, 1);
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void FiveElementTest()
        {
            var input = new[] {5, 3, 2, 4, 1, 5};
            Assert.AreEqual(1, QuickSelect<int>.Select((int[])input.Clone(), 1));
            Assert.AreEqual(2, QuickSelect<int>.Select((int[])input.Clone(), 2));
            Assert.AreEqual(3, QuickSelect<int>.Select((int[])input.Clone(), 3));
            Assert.AreEqual(4, QuickSelect<int>.Select((int[])input.Clone(), 4));
            Assert.AreEqual(5, QuickSelect<int>.Select((int[])input.Clone(), 5));
            Assert.AreEqual(5, QuickSelect<int>.Select((int[])input.Clone(), 6));
        }

    }
}
