using AlgoSharp.PercolationVisualizer.Model;

namespace AlgoSharp.PercolationVisualizer.Services
{
    public interface IPercolationService
    {
        void Init(int gridSize);
        PercolationModel Open(int i, int j);
    }

    public class PercolationService : IPercolationService
    {
        private Percolation.Percolation _percolationEngine;
        private int _gridSize;

        public void Init(int gridSize)
        {
            _gridSize = gridSize;
            _percolationEngine = new Percolation.Percolation(_gridSize);
        }

        public PercolationModel Open(int i, int j)
        {
            _percolationEngine.Open(i, j);

            var percolationData = new PercolationModel {IsPercolated = _percolationEngine.Percolates()};

            for (int row = 0; row < _gridSize; row++)
            {
                for (int col = 0; col < _gridSize; col++)
                {
                    if (_percolationEngine.IsFull(row, col))
                    {
                        percolationData.Sites.Add(new SiteModel { Row = row, Col = col, IsFull = true });
                        percolationData.OpenSites++;
                    }
                    else if (_percolationEngine.IsOpen(row, col))
                    {
                        percolationData.Sites.Add(new SiteModel { Row = row, Col = col, IsOpen = true });
                        percolationData.OpenSites++;
                    }
                    else
                    {
                        percolationData.Sites.Add(new SiteModel { Row = row, Col = col });
                    }
                }
            }

            return percolationData;
        }
    }
}