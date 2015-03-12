using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoSharp.Puzzle.Tests
{
    [TestClass]
    public class SolverTests
    {
        [TestMethod]
        public void MovesTest()
        {
            int[][] blocks =
            {
                new[] {0, 1, 3},
                new[] {4, 2, 5},
                new[] {7, 8, 6}
            };
            
            var solver = new Solver(new Board(blocks));
            var res = solver.Moves();

            Assert.AreEqual(4, res);
        }

        [TestMethod]
        public void SolutionTest()
        {
            int[][] blocks =
            {
                new[] {0, 1, 3},
                new[] {4, 2, 5},
                new[] {7, 8, 6}
            };

            var solver = new Solver(new Board(blocks));
            var res = solver.Solution().ToArray();

            var expected = new[]
                           {
                               new Board(new[]
                               {
                                   new[] {0, 1, 3},
                                   new[] {4, 2, 5},
                                   new[] {7, 8, 6}
                               }),
                               new Board(new[]
                               {
                                   new[] {1, 0, 3},
                                   new[] {4, 2, 5},
                                   new[] {7, 8, 6}
                               }),
                               new Board(new[]
                               {
                                   new[] {1, 2, 3},
                                   new[] {4, 0, 5},
                                   new[] {7, 8, 6}
                               }),
                               new Board(new[]
                               {
                                   new[] {1, 2, 3},
                                   new[] {4, 5, 0},
                                   new[] {7, 8, 6}
                               }),
                               new Board(new[]
                               {
                                   new[] {1, 2, 3},
                                   new[] {4, 5, 6},
                                   new[] {7, 8, 0}
                               })
                           };
            CollectionAssert.AreEqual(expected, res);
        }

        [TestMethod]
        public void IsSolvableFalse()
        {
            int[][] blocks =
            {
                new[] {1, 2, 3},
                new[] {4, 5, 6},
                new[] {8, 7, 0}
            };

            var solver = new Solver(new Board(blocks));
            var res = solver.IsSolvable();

            Assert.IsFalse(res);
        }

        [TestMethod]
        public void IsSolvableFalse2()
        {
            int[][] blocks =
            {
                new[] {1, 0},
                new[] {2, 3}
            };

            var solver = new Solver(new Board(blocks));
            var res = solver.IsSolvable();

            Assert.IsFalse(res);
        }
    }
}
