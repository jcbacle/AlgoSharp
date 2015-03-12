using System;
using System.IO;
using System.Linq;

namespace AlgoSharp.Puzzle
{
    static class Program
    {
        static void Main(string[] args)
        {
            // create initial board from file
            var filename = args[0];
            Board initial;
            using (var file = File.OpenText(filename))
            {
                var n = Convert.ToInt32(file.ReadLine());
                var blocks = new int[n][];
                for (var i = 0; i < n; i++)
                {
                    blocks[i] = file.ReadLine().Split(' ').Where(s => !string.IsNullOrEmpty(s)).Select(s => Convert.ToInt32(s)).ToArray();
                }
                initial = new Board(blocks);
            }

            // solve the puzzle
            var solver = new Solver(initial);

            // print solution to standard output
            if (!solver.IsSolvable())
                Console.WriteLine("No solution possible");
            else
            {
                Console.WriteLine("Minimum number of moves = " + solver.Moves());
                foreach (var board in solver.Solution())
                    Console.WriteLine(board);
            }
        }
    }
}
