using System;
using AlgoSharp.Algs.PriorityQueue;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Algs.Tests.PriorityQueue
{
    [TestClass]
    public class MinPriotityQueueTests
    {
        [TestMethod]
        public void InsertInOrderedHeap()
        {
            var input = new[] {"D", "A", "C", "E"};
            
            var pq = new MinPriorityQueue<string>(input);
            Console.WriteLine("Initial: {0}", string.Join(",", pq));

            pq.Insert("B");
            Console.WriteLine("Insert 'B': {0}", string.Join(",", pq));
            
            Assert.AreEqual("A", pq.DelMin());
            Console.WriteLine("DelMin: {0}", string.Join(",", pq));

            Assert.AreEqual("B", pq.DelMin());
            Console.WriteLine("DelMin: {0}", string.Join(",", pq));

            Assert.AreEqual("C", pq.DelMin());
            Console.WriteLine("DelMin: {0}", string.Join(",", pq));

            pq.Insert("B");
            Assert.AreEqual("B", pq.Min());
            Console.WriteLine("Min: {0}", string.Join(",", pq));
        }
    }
}
