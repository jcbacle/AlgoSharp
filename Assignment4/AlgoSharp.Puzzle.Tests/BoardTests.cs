using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Puzzle.Tests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void DimensionTest()
        {
            int[][] blocks =
            {
                new[] {0, 1, 3},
                new[] {4, 2, 5},
                new[] {7, 8, 6}
            };

            var board = new Board(blocks);

            Assert.AreEqual(3, board.Dimension());
        }

        [TestMethod]
        public void HammingTest()
        {
            int[][] blocks =
            {
                new[] {8, 1, 3},
                new[] {4, 0, 2},
                new[] {7, 6, 5}
            };

            var board = new Board(blocks);

            Assert.AreEqual(5, board.Hamming());
        }

        [TestMethod]
        public void ManhattanTest()
        {
            int[][] blocks =
            {
                new[] {8, 1, 3},
                new[] {4, 0, 2},
                new[] {7, 6, 5}
            };

            var board = new Board(blocks);

            Assert.AreEqual(10, board.Manhattan());
        }

        [TestMethod]
        public void IsGoalTestFalse()
        {
            int[][] blocks =
            {
                new[] {8, 1, 3},
                new[] {4, 0, 2},
                new[] {7, 6, 5}
            };
            var board = new Board(blocks);
            Assert.IsFalse(board.IsGoal());
        }

        [TestMethod]
        public void IsGoalTestTrue()
        {
            int[][] blocks =
            {
                new[] {1, 2, 3},
                new[] {4, 5, 6},
                new[] {7, 8, 0}
            };
            var board = new Board(blocks);
            Assert.IsTrue(board.IsGoal());
        }

        [TestMethod]
        public void EqualsTestTrue()
        {
            int[][] blocks =
            {
                new[] {1, 2, 3},
                new[] {4, 5, 6},
                new[] {7, 8, 0}
            };
            var board = new Board(blocks);
            Assert.IsTrue(board.Equals(new Board(blocks)));            
        }

        [TestMethod]
        public void EqualsTestFalse()
        {
            int[][] blocks =
            {
                new[] {1, 2, 3},
                new[] {4, 5, 6},
                new[] {7, 8, 0}
            };
            int[][] otherBlocks =
            {
                new[] {8, 1, 3},
                new[] {4, 0, 2},
                new[] {7, 6, 5}
            };

            var board = new Board(blocks);
            var otherBoard = new Board(otherBlocks);

            Assert.IsFalse(board.Equals(otherBoard));
        }

        [TestMethod]
        public void TwinTest()
        {
            int[][] blocks =
            {
                new[] {8, 1, 3},
                new[] {4, 0, 2},
                new[] {7, 6, 5}
            };
            int[][] twinBlocks =
            {
                new[] {1, 8, 3},
                new[] {4, 0, 2},
                new[] {7, 6, 5}
            };
            var board = new Board(blocks);
            var twinBoard = board.Twin();
            var expectedBoard = new Board(twinBlocks);
            Assert.IsTrue(twinBoard.Equals(expectedBoard));
        }

        [TestMethod]
        public void NeighborsTest()
        {
            int[][] blocks =
            {
                new[] {1, 2, 3},
                new[] {4, 0, 6},
                new[] {7, 8, 5}
            };
            var board = new Board(blocks);

            var expectedNeigbors = new List<Board>
                                   {
                                       new Board(new[]
                                                 {
                                                     new[] {1, 2, 3},
                                                     new[] {0, 4, 6},
                                                     new[] {7, 8, 5}
                                                 }),
                                       new Board(new[]
                                                 {
                                                     new[] {1, 2, 3},
                                                     new[] {4, 6, 0},
                                                     new[] {7, 8, 5}
                                                 }),
                                       new Board(new[]
                                                 {
                                                     new[] {1, 0, 3},
                                                     new[] {4, 2, 6},
                                                     new[] {7, 8, 5}
                                                 }),
                                       new Board(new[]
                                                 {
                                                     new[] {1, 2, 3},
                                                     new[] {4, 8, 6},
                                                     new[] {7, 0, 5}
                                                 })
                                   };
            var res = board.Neighbors().ToList();

            CollectionAssert.AreEquivalent(expectedNeigbors, res);
        }
    }
}
