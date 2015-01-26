using System;
using AlgoSharp.Collinear;

namespace AlgoSharp.Console.Collinear
{
    public class DrawPointEventArgs : EventArgs
    {
        public Point P { get; set; }

        public DrawPointEventArgs(Point p)
        {
            P = p;
        }
    }
}