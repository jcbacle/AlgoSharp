using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Queue.Tests
{
    [TestClass]
    public class DequeTests
    {
        [TestMethod]
        public void IsEmptyTest()
        {
            var deque = new Deque<int>();
            Assert.IsTrue(deque.IsEmpty());
            deque.AddFirst(1);
            Assert.IsFalse(deque.IsEmpty());
        }

        [TestMethod]
        public void SizeTest()
        {
            var deque = new Deque<int>();
            Assert.AreEqual(0, deque.Size());
            deque.AddFirst(1);
            Assert.AreEqual(1, deque.Size());
            deque.AddFirst(1);
            Assert.AreEqual(2, deque.Size());
        }

        [TestMethod]
        public void AddRemoveFirstTest()
        {
            var deque = new Deque<int>();
            deque.AddFirst(1);
            deque.AddFirst(2);
            Assert.AreEqual(2, deque.RemoveFirst());
            Assert.AreEqual(1, deque.RemoveFirst());
        }

        [TestMethod]
        public void AddRemoveLastTest()
        {
            var deque = new Deque<int>();
            deque.AddLast(1);
            deque.AddLast(2);
            Assert.AreEqual(2, deque.RemoveLast());
            Assert.AreEqual(1, deque.RemoveLast());
        }

        [TestMethod]
        public void IntermixedTest()
        {
            // build 2 - 1 - 0 sequence
            var deque = new Deque<int>();
            deque.AddFirst(1);
            deque.AddFirst(2);
            deque.AddLast(0);

            Assert.AreEqual(3, deque.Size());
            Assert.AreEqual(2, deque.RemoveFirst());

            Assert.AreEqual(2, deque.Size());
            Assert.AreEqual(0, deque.RemoveLast());

            Assert.AreEqual(1, deque.Size());
            Assert.AreEqual(1, deque.RemoveLast());

            Assert.AreEqual(0, deque.Size());
        }

        [TestMethod]
        public void EnumeratorTest()
        {
            var deque = new Deque<int>();
            deque.AddFirst(1);
            deque.AddFirst(2);

            Assert.AreEqual(2, deque.First());
            Assert.AreEqual(1, deque.Last());
        }
    }
}