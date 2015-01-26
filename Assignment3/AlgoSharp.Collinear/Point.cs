using System;
using System.Collections.Generic;

namespace AlgoSharp.Collinear
{
    public class Point : IComparable<Point>
    {
        // compare points by slope
        //public readonly Comparator<Point> SLOPE_ORDER ; // YOUR DEFINITION HERE

        public int X { get; private set; }
        public int Y { get; private set; }

        // create the point (x, y)
        public Point(int x, int y)
        {
            /* DO NOT MODIFY */
            X = x;
            Y = y;
        }

        // plot this point to standard drawing
        public void Draw()
        {
            /* DO NOT MODIFY */
            //StdDraw.point(x, y);
        }

        // draw line between this point and that point to standard drawing
        public void DrawTo(Point that)
        {
            /* DO NOT MODIFY */
            //StdDraw.line(this.x, this.y, that.x, that.y);
        }

        // slope between this point and that point
        public double SlopeTo(Point that)
        {
            if (that.X == X) return that.Y == Y ? double.NegativeInfinity : double.PositiveInfinity;
            return (double)(that.Y - Y) / (that.X - X);
        }

        // is this point lexicographically smaller than that one?
        // comparing y-coordinates and breaking ties by x-coordinates
        public int CompareTo(Point other)
        {
            if (Y < other.Y) return -1;
            if (Y > other.Y) return 1;
            if (X < other.X) return -1;
            if (X > other.X) return 1;
            return 0;
        }

        public override String ToString()
        {
            /* DO NOT MODIFY */
            return "(" + X + ", " + Y + ")";
        }
    }

    public class SlopeComparer : Comparer<Point>
    {
        private readonly Point _p;

        public SlopeComparer(Point p)
        {
            _p = p;
        }

        public override int Compare(Point q, Point r)
        {
            var qSlope = _p.SlopeTo(q);
            var rSlope = _p.SlopeTo(r);
            return qSlope.CompareTo(rSlope);
        }
    }
}
