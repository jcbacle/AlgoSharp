using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoSharp.Percolation
{
    public static class StatsExtensions
    {
        public static double Mean(this List<double> values)
        {
            return values.Sum() / values.Count();
        }

        public static double SampleVariance(this List<double> values)
        {
            var mean = values.Mean();
            return values.Sum(v => Math.Pow(v - mean, 2)) / (values.Count - 1);
        }

        public static double SampleStdDev(this List<double> values)
        {
            return Math.Sqrt(values.SampleVariance());
        }

        public static Tuple<double, double> Confidence(this List<double> values, double confidenceCoefficient)
        {
            var count = values.Count;
            var mean = values.Mean();
            var stdDev = values.SampleStdDev();
            var confidenceLow = mean - confidenceCoefficient * stdDev / Math.Sqrt(count);
            var confidenceHigh = mean + confidenceCoefficient * stdDev / Math.Sqrt(count);
            return new Tuple<double, double>(confidenceLow, confidenceHigh);
        }
    }
}
