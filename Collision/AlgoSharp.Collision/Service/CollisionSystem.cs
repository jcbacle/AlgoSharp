using System;
using System.Collections.Generic;
using System.Threading;
using AlgoSharp.Algs.PriorityQueue;

namespace AlgoSharp.Collision.Service
{
    public class CollisionSystem
    {
        private readonly List<Particule> _particules;
        private readonly double _drawFrequency;
        private readonly MinPriorityQueue<CollisionEvent> _pq;
        private double _time;

        public event EventHandler<DrawEventArgs> DrawEvent;

        public CollisionSystem(List<Particule> particules, double drawFrequency)
        {
            _particules = particules;
            _drawFrequency = drawFrequency;
            _pq = new MinPriorityQueue<CollisionEvent>();
            _time = 0;
        }

        public void Simulate(CancellationToken token)
        {
            // Predict collision for each a
            foreach (Particule particule in _particules) PredictCollision(particule);

            // First event to draw all particules
            _pq.Insert(new CollisionEvent(0));

            // Event loop
            while (!_pq.IsEmpty() && !token.IsCancellationRequested)
            {
                var collisionEvent = _pq.DelMin();
                if (!collisionEvent.IsValid()) continue;

                // Move all particules to the next event
                foreach (Particule p in _particules)
                    p.Move(collisionEvent.EventTime - _time);

                _time = collisionEvent.EventTime;

                if (collisionEvent.A != null && collisionEvent.B != null)
                    collisionEvent.A.BounceOff(collisionEvent.B);
                else if (collisionEvent.A != null)
                    collisionEvent.A.BounceOffVerticalWall();
                else if (collisionEvent.B != null)
                    collisionEvent.B.BounceOffHorizontalWall();
                else RaiseDrawEvent();

                PredictCollision(collisionEvent.A);
                PredictCollision(collisionEvent.B);
            }
        }

        private void RaiseDrawEvent()
        {
            var handler = DrawEvent;
            if (handler != null) handler(this, new DrawEventArgs(_particules));

            _pq.Insert(new CollisionEvent(_time + 1 / _drawFrequency));
        }

        private void PredictCollision(Particule particule)
        {
            if (particule == null) return;

            double collisionTime;

            // compute a collision time
            foreach (var p in _particules)
            {
                collisionTime = particule.TimeToHit(p);
                _pq.Insert(new CollisionEvent(_time + collisionTime, particule, p));
            }

            // compute wall collision time
            collisionTime = particule.TimeToHitVerticalWall();
            _pq.Insert(new CollisionEvent(_time + collisionTime, particule));
            collisionTime = particule.TimeToHitHorizontalWall();
            _pq.Insert(new CollisionEvent(_time + collisionTime, null, particule));
        }
    }

    public class DrawEventArgs : EventArgs
    {
        public List<Particule> Particules { get; private set; }

        public DrawEventArgs(List<Particule> particules)
        {
            Particules = particules;
        }
    }

    public class CollisionEvent : IComparable<CollisionEvent>
    {
        public double EventTime { get; private set; }
        public Particule A { get; private set; }
        public Particule B { get; private set; }
        private readonly int _countA;
        private readonly int _countB;

        public CollisionEvent(double eventTime, Particule a = null, Particule b = null)
        {
            EventTime = eventTime;
            A = a;
            B = b;
            _countA = A != null ? A.Count : -1;
            _countB = B != null ? B.Count : -1;
        }

        public int CompareTo(CollisionEvent other)
        {
            return EventTime.CompareTo(other.EventTime);
        }

        public bool IsValid()
        {
            if (A != null && A.Count != _countA) return false;
            if (B != null && B.Count != _countB) return false;
            return true;
        }
    }
}
