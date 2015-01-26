using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Queue.Tests
{
    [TestClass]
    public class RandomizedQueueTests
    {
        [TestMethod]
        public void IsEmptyTest()
        {
            var queue = new RandomizedQueue<int>();
            Assert.IsTrue(queue.IsEmpty());

            queue.Enqueue(1);
            Assert.IsFalse(queue.IsEmpty());

            queue.Dequeue();
            Assert.IsTrue(queue.IsEmpty());
        }

        [TestMethod]
        public void SizeTest()
        {
            var queue = new RandomizedQueue<int>();
            Assert.AreEqual(0, queue.Size());

            queue.Enqueue(1);
            Assert.AreEqual(1, queue.Size());

            queue.Enqueue(2);
            Assert.AreEqual(2, queue.Size());

            queue.Dequeue();
            Assert.AreEqual(1, queue.Size());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DequeueEmptyTest()
        {
            var queue = new RandomizedQueue<int>();
            queue.Dequeue();
        }

        [TestMethod]
        public void EnqueueTest()
        {
            var queue = new RandomizedQueue<int>();
            queue.Enqueue(1);
            Assert.AreEqual(1, queue.Dequeue());

            queue.Enqueue(1);
            Assert.AreEqual(1, queue.Dequeue());

            queue.Enqueue(1);
            queue.Enqueue(1);
            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(1, queue.Dequeue());
        }

        [TestMethod]
        public void SampleTest()
        {
            var queue = new RandomizedQueue<int>();
            queue.Enqueue(1);
            Assert.AreEqual(1, queue.Sample());
        }

        [TestMethod]
        public void GetEnumeratorTest()
        {
            var queue = new RandomizedQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);

            var result = queue.ToArray();

            Assert.IsTrue(queue.Contains(1));
            Assert.IsTrue(queue.Contains(2));
        }
    }
}
