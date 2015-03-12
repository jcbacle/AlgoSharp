using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoSharp.Puzzle
{
    public class Board
    {
        private readonly int[][] _blocks;

        // construct a board from an N-by-N array of blocks
        // (where blocks[i][j] = block in row i, column j)
        public Board(int[][] blocks)
        {
            _blocks = blocks.Select(a => a.ToArray()).ToArray();
        }

        // board dimension N
        public int Dimension()
        {
            return _blocks.Length;
        }

        // number of blocks out of place
        public int Hamming()
        {
            var hamming = 0;
            var num = 0;
            for (var i = 0; i < _blocks.Length; i++)
            {
                for (var j = 0; j < _blocks.Length; j++)
                {
                    num++;
                    var block = _blocks[i][j];
                    if (block != 0 && block != num) hamming++;
                }                
            }
                    
            return hamming;
        }

        // sum of Manhattan distances between blocks and goal
        public int Manhattan()
        {
            var manhattan = 0;

            for (var i = 0; i < _blocks.Length; i++)
            {
                for (var j = 0; j < _blocks.Length; j++)
                {
                    var block = _blocks[i][j];
                    if (block == 0) continue;
                    var rowGoal = (block - 1) / _blocks.Length;
                    var colGoal = (block - 1) % _blocks.Length;
                    manhattan += Math.Abs(rowGoal - i) + Math.Abs(colGoal - j);
                }
            }

            return manhattan;
        }

        // is this board the goal board?
        public bool IsGoal()
        {
            var num = 0;
            for (var i = 0; i < _blocks.Length; i++)
            {
                for (var j = 0; j < _blocks.Length; j++)
                {
                    if (i == _blocks.Length-1 && j == _blocks.Length-1 && _blocks[i][j] == 0) continue;
                    if (_blocks[i][j] != ++num) return false;
                }
            }
            return true;
        }

        // a board that is obtained by exchanging two adjacent blocks in the same row
        public Board Twin()
        {
            if (_blocks[0][0] != 0 && _blocks[0][1] != 0) 
                return new Board(_blocks).Swap(0, 0, 0, 1);
            return new Board(_blocks).Swap(1, 0, 1, 1);
        }

        private Board Swap(int i1, int j1, int i2, int j2)
        {
            var temp = _blocks[i1][j1];
            _blocks[i1][j1] = _blocks[i2][j2];
            _blocks[i2][j2] = temp;
            return this;
        }

        // all neighboring boards
        public IEnumerable<Board> Neighbors()
        {
            var neighbors = new List<Board>();

            for (var i = 0; i < _blocks.Length; i++)
                for (var j = 0; j < _blocks.Length; j++)
                {
                    if (_blocks[i][j] != 0) continue;
                    if (i > 0) neighbors.Add(new Board(_blocks).Swap(i, j, i-1, j));
                    if (i < _blocks.Length - 1) neighbors.Add(new Board(_blocks).Swap(i, j, i+1, j));
                    if (j > 0) neighbors.Add(new Board(_blocks).Swap(i, j, i, j-1));
                    if (j < _blocks.Length - 1) neighbors.Add(new Board(_blocks).Swap(i, j, i, j+1));
                }

            return neighbors;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Board) obj;
            for (var i = 0; i < _blocks.Length; i++)
                for (var j = 0; j < _blocks.Length; j++)
                    if (_blocks[i][j] != other._blocks[i][j]) 
                        return false;
            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                if (_blocks == null) return 0;
                var hash = 17;
                for (var i = 0; i < _blocks.Length; i++)
                    for (var j = 0; j < _blocks.Length; j++)
                        hash = hash*31 + _blocks[i][j].GetHashCode();
                return hash;
            }            
        }

        // string representation of this board (in the output format specified below)
        public override String ToString()
        {
            var sb = new StringBuilder(_blocks.Length).AppendLine();
            foreach (int[] b in _blocks)
                sb.AppendLine(string.Join(" ", b));
            return sb.ToString();
        }
    }
}