using System;
using System.Linq;
using AlgoSharp.Algs.PriorityQueue;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Algs.Tests.PriorityQueue
{
    [TestClass]
    public class MaxPriorityQueueTests
    {
        [TestMethod]
        public void InsertInOrderedHeap()
        {
            var input = new[] {"T", "P", "R", "N", "H", "O", "A", "E", "I", "G"};

            var pq = new MaxPriorityQueue<string>(input);
            Console.WriteLine("New: {0}", string.Join(",", pq));

            pq.Insert("S");
            Console.WriteLine("Insert 'S': {0}" , string.Join(",", pq));

            Assert.AreEqual("T", pq.DelMax());
            Console.WriteLine("DelMax: {0}", string.Join(",", pq));

            Assert.AreEqual("S", pq.DelMax());
            Console.WriteLine("DelMax: {0}", string.Join(",", pq));

            Assert.AreEqual("R", pq.DelMax());
            Console.WriteLine("DelMax: {0}", string.Join(",", pq));

            pq.Insert("S");
            Console.WriteLine("Insert 'S': {0}", string.Join(",", pq));

            Assert.AreEqual("S", pq.Max());
            Console.WriteLine("Max: {0}", string.Join(",", pq));
        }
    }
}
