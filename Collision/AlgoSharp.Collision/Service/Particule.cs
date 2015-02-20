using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace AlgoSharp.Collision.Service
{
    public class Particule
    {
        private static readonly Random Random = new Random();

        public double Rx { get; private set; }
        public double Ry { get; private set; }
        private double Vx { get; set; }
        private double Vy { get; set; }
        public double Radius { get; private set; }
        private double Mass { get; set; }
        public Color Color { get; private set; }
        public int Count { get; private set; }
        private HashSet<int> _neighborCells;

        public Particule(double rx, double ry, double vx, double vy, double radius, double mass, Color color)
        {
            Rx = rx;
            Ry = ry;
            Vx = vx;
            Vy = vy;
            Radius = radius;
            Mass = mass;
            Color = color;
            _neighborCells = new HashSet<int>();
        }

        public Particule()
        {
            Radius = 0.01;
            Rx = Random.NextDouble() * (1 - 2 * Radius) + Radius;
            Ry = Random.NextDouble() * (1 - 2 * Radius) + Radius;
            Vx = 0.01 * (Random.NextDouble() - 0.5);
            Vy = 0.01 * (Random.NextDouble() - 0.5);
            Mass = 0.5;
            Color = Colors.Black;
        }

        public void Move(double t)
        {
            Rx += t*Vx;
            Ry += t*Vy;
            BuildNeighborList();
        }

        private void BuildNeighborList()
        {
            var index = Rx%0.01 + 100*Ry%0.01;

        }

        public void BounceOffVerticalWall()
        {
            Vx *= -1;
            Count++;
        }

        public void BounceOffHorizontalWall()
        {
            Vy *= -1;
            Count++;
        }

        public void BounceOff(Particule that)
        {
            double dx = that.Rx - Rx;
            double dy = that.Ry - Ry;
            double dvx = that.Vx - Vx;
            double dvy = that.Vy - Vy;
            double dvdr = dx * dvx + dy * dvy;             // dv dot dr
            double dist = Radius + that.Radius;   // distance between particle centers at collison

            // normal force F, and in x and y directions
            double f = 2 * Mass * that.Mass * dvdr / ((Mass + that.Mass) * dist);
            double fx = f * dx / dist;
            double fy = f * dy / dist;

            // update velocities according to normal force
            Vx += fx / Mass;
            Vy += fy / Mass;
            that.Vx -= fx / that.Mass;
            that.Vy -= fy / that.Mass;

            // update collision counts
            Count++;
            that.Count++;
        }

        public double TimeToHit(Particule that)
        {
            if (this == that) return double.PositiveInfinity;
            double dx = that.Rx - Rx;
            double dy = that.Ry - Ry;
            double dvx = that.Vx - Vx;
            double dvy = that.Vy - Vy;
            double dvdr = dx * dvx + dy * dvy;
            if (dvdr > 0) return double.PositiveInfinity;
            double dvdv = dvx * dvx + dvy * dvy;
            double drdr = dx * dx + dy * dy;
            double sigma = Radius + that.Radius;
            double d = (dvdr * dvdr) - dvdv * (drdr - sigma * sigma);
            if (drdr < sigma*sigma) return double.PositiveInfinity; // ignored overlapping particules (due to random generation)
            if (d < 0) return double.PositiveInfinity;
            return -(dvdr + Math.Sqrt(d)) / dvdv;
        }

        public double TimeToHitVerticalWall()
        {
            if (Vx > 0) return (1 - Rx - Radius) / Vx;
            if (Vx < 0) return (Radius - Rx) / Vx;
            return double.PositiveInfinity;
        }

        public double TimeToHitHorizontalWall()
        {
            if (Vy > 0) return (1 - Ry - Radius)/Vy;
            if (Vy < 0) return (Radius - Ry)/Vy;
            return double.PositiveInfinity;
        }
    }
}
