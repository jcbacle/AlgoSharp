using System;
using AlgoSharp.Collinear;

namespace AlgoSharp.Console.Collinear
{
    public class DrawLineEventArgs : EventArgs
    {
        public Point P { get; set; }
        public Point Q { get; set; }

        public DrawLineEventArgs(Point p, Point q)
        {
            P = p;
            Q = q;
        }
    }
}