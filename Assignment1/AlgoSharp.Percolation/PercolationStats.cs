using System;
using System.Collections.Generic;

namespace AlgoSharp.Percolation
{
    public class PercolationStats
    {
        private readonly double _mean;
        private readonly double _stdDev;
        private readonly Tuple<double, double> _confidence;

        /// <summary>
        /// Perform t independent experiments on an n-by-n grid
        /// </summary>
        /// <param name="n">Size of grid n-by-n</param>
        /// <param name="t">Number of independent experiments</param>
        public PercolationStats(int n, int t)
        {
            if (n <= 0) throw new ArgumentException("n must be greather than 0");
            if (t <= 0) throw new ArgumentException("t must be greather than 0");

            var measures = new List<double>(t);

            var random = new Random();

            // Repeat experiment T times
            for (int x = 0; x < t; x++)
            {
                // Initialize all sites to be blocked.
                var percolation = new Percolation(n);

                // Repeat the following until the system percolates:
                var open = 0;
                while (!percolation.Percolates())
                {
                    // Choose a site (row i, column j) uniformly at random among all blocked sites.
                    int row, col;
                    do
                    {
                        row = random.Next(0, n);
                        col = random.Next(0, n);
                    } while (percolation.IsOpen(row, col));

                    // Open the site (row i, column j).
                    percolation.Open(row, col);
                    open++;
                }

                // The fraction of sites that are opened when the system percolates provides an estimate of the percolation threshold.
                var measure = (double) open / (n * n);
                measures.Add(measure);
            }

            // Compute Statistics
            _mean = measures.Mean();
            _stdDev = measures.SampleStdDev();
            _confidence = measures.Confidence(1.96);
        }
        
        /// <summary>
        /// Sample mean of percolation threshold
        /// </summary>
        /// <returns></returns>
        public double Mean()
        {
            return _mean;
        }

        /// <summary>
        /// Sample standard deviation of percolation threshold
        /// </summary>
        /// <returns></returns>
        public double StdDev()
        {
            return _stdDev;
        }

        /// <summary>
        /// Low  endpoint of 95% confidence interval
        /// </summary>
        /// <returns></returns>
        public double ConfidenceLow()
        {
            return _confidence.Item1;
        }

        /// <summary>
        /// High endpoint of 95% confidence interval
        /// </summary>
        /// <returns></returns>
        public double ConfidenceHigh()
        {
            return _confidence.Item2;
        }

        static void Main(string[] args)
        {
            var n = int.Parse(args[0]);
            var t = int.Parse(args[1]);

            var stats = new PercolationStats(n, t);

            System.Console.WriteLine("PercolationsStats({0}, {1})", n, t);
            System.Console.WriteLine("mean() = {0}", stats.Mean());
            System.Console.WriteLine("stddev() = {0}", stats.StdDev());
            System.Console.WriteLine("95% confidenceLow() = {0}", stats.ConfidenceLow());
            System.Console.WriteLine("95% confidenceHigh() = {0}", stats.ConfidenceHigh());
        }
    }
}
