using System;
using AlgoSharp.Algs.UnionFind;

namespace AlgoSharp.Percolation
{
    public class Percolation
    {
        private readonly bool[,] _open;
        private readonly int _n;
        private readonly WeightedQuickUnion _ufPercolate;
        private readonly WeightedQuickUnion _ufFull;
        private readonly int _top;
        private readonly int _bottom;

        /// <summary>
        /// Create N-by-N grid, with all sites blocked
        /// </summary>
        /// <param name="n"></param>
        public Percolation(int n)
        {
            if (n <= 0) throw new ArgumentException("n must be greather than 0");
            _n = n;
            _open = new bool[_n, _n];
            var size = _n*_n;
            _ufPercolate = new WeightedQuickUnion(size + 2);
            _ufFull = new WeightedQuickUnion(size + 1);
            _top = size;
            _bottom = size + 1;
        }

        /// <summary>
        /// open site if it is not open already
        /// </summary>
        /// <param name="row">row</param>        
        /// <param name="col">column</param>
        public void Open(int row, int col)
        {
            CheckRange(row, col);

            if (IsOpen(row, col)) return;

            _open[row, col] = true;

            var index = GetIndex(row, col);

            Union(index, row - 1, col);     // Union with Up neighbor
            Union(index, row + 1, col);     // Union with Down neighbor
            Union(index, row, col - 1);     // Union with Left neighbor
            Union(index, row, col + 1);     // Union with Right neighbor
        }

        private void Union(int index, int row, int col)
        {
            if (col < 0 || col > _n - 1) return;
            
            if (row < 0)
            {
                _ufPercolate.Union(index, _top);
                _ufFull.Union(index, _top);
                return;
            }

            if (row > _n - 1)
            {
                _ufPercolate.Union(index,_bottom);
                return;
            }

            if (IsOpen(row, col))
            {
                _ufPercolate.Union(index, GetIndex(row, col));
                _ufFull.Union(index, GetIndex(row, col));
            }            
        }

        /// <summary>
        /// Is site open?
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="col">column</param>
        /// <returns></returns>
        public bool IsOpen(int row, int col)
        {
            CheckRange(row, col);
            return _open[row, col];
        }

        private int GetIndex(int row, int col)
        {
            return col + _n * row;
        }

        /// <summary>
        /// Is site full?
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="col">column</param>
        /// <returns></returns>
        public bool IsFull(int row, int col)
        {
            CheckRange(row, col);
            return _ufFull.IsConnected(GetIndex(row, col), _top);
        }

        private void CheckRange(int row, int col)
        {
            if (row < 0 || row > _n || col < 0 || col > _n) throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// Does the system percolate ?
        /// </summary>
        /// <returns>true if the system percolate, else false</returns>
        public bool Percolates()
        {
            return _ufPercolate.IsConnected(_top, _bottom);
        }
    }
}
