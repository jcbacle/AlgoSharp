using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AlgoSharp.Console.Collinear;

namespace AlgoSharp.Collinear
{
    // Todo: test with Dictionnary => O(n²) instead of O(n².lg(n))
    public static class Fast
    {
        public static event EventHandler<DrawPointEventArgs> RaiseDrawPoint;
        public static event EventHandler<DrawLineEventArgs> RaiseDrawLine;

        public static void Main(string[] args)
        {
            var fileName = args[0];
            var points = File.ReadLines(fileName).Skip(1).Select(ParseLine).ToArray();

            foreach (var point in points)
            {
                OnRaiseDrawPointEvent(point);
            }
            
            var lines = new HashSet<Tuple<double, Point>>();

            for (int i = 1; i < points.Length; i++)
            {
                Array.Sort(points, i, points.Length - i, new SlopeComparer(points[i - 1]));

                var prevSlope = double.NegativeInfinity;
                var collinears = new List<Point>();

                for (int j = i; j < points.Length; j++)
                {                    
                    var curSlope = points[i-1].SlopeTo(points[j]);
                    
                    // do not print subsegments
                    if (lines.Contains(new Tuple<double, Point>(curSlope, points[j])))
                    {
                        continue;
                    }
                    
                    if (curSlope == prevSlope)
                    {
                        collinears.Add(points[j]);
                        if (j < points.Length - 1) continue;
                    }

                    if (collinears.Count() >= 4)
                    {
                        collinears.Sort();
                        var sb = new StringBuilder();
                        var first = true;
                        foreach (var point in collinears)
                        {
                            if (first)
                            {
                                first = false;
                                sb.Append(point);
                            }
                            else
                            {
                                sb.Append(string.Format(" -> {0}", point));
                            }

                            lines.Add(new Tuple<double, Point>(prevSlope, point));
                        }

                        System.Console.WriteLine(sb);
                        OnRaiseDrawLineEvent(collinears.First(), collinears.Last());
                    }
                    prevSlope = curSlope;
                    collinears = new List<Point> {points[i - 1], points[j]};
                }
            }
        }

        private static void OnRaiseDrawLineEvent(Point p, Point q)
        {
            var handler = RaiseDrawLine;
            if (handler == null) return;
            handler(null, new DrawLineEventArgs(p, q));
        }

        private static void OnRaiseDrawPointEvent(Point p)
        {
            var handler = RaiseDrawPoint;
            if (handler == null) return;
            handler(null, new DrawPointEventArgs(p));
        }

        private static Point ParseLine(string line)
        {
            var ints = Regex.Split(line, @"\b\s+\b").Select(int.Parse).ToArray();
            return new Point(ints[0], ints[1]);
        }
    }
}
