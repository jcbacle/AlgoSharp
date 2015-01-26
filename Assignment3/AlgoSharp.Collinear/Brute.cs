using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AlgoSharp.Collinear
{
    // O(n^4) execution time
    // O(n) space
    public static class Brute
    {
        public static void Main(string[] args)
        {
            var fileName = args[0];
            var points = File.ReadLines(fileName).Skip(1).Select(ParseLine).ToArray();

            Array.Sort(points);

            for (int i = 0; i < points.Length; i++)
            {
                var p = points[i];

                for (int j = i + 1; j < points.Length; j++)
                {
                    var q1 = points[j];
                    var q1Slope = p.SlopeTo(q1);

                    for (int k = j + 1; k < points.Length; k++)
                    {
                        var q2 = points[k];
                        var q2Slope = p.SlopeTo(q2);

                        if (q1Slope != q2Slope) continue;

                        for (int l = k + 1; l < points.Length; l++)
                        {
                            var q3 = points[l];
                            var q3Slope = p.SlopeTo(q3);

                            if (q3Slope != q2Slope) continue;

                            System.Console.WriteLine("{0} -> {1} -> {2} -> {3}", p, q1, q2, q3);
                        }
                    }
                }
            }
        }

        private static Point ParseLine(string line)
        {
            var ints = Regex.Split(line, @"\b\s+\b").Select(int.Parse).ToArray();
            return new Point(ints[0], ints[1]);
        }
    }
}
