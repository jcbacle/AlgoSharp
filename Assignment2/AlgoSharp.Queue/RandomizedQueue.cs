using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoSharp.Queue
{
    public class RandomizedQueue<T> : IEnumerable<T>
    {
        private static Random _random = new Random();
        private T[] _queue;
        private int _count;
        private int _head;
        private int _tail;

        // construct an empty randomized queue
        public RandomizedQueue()
        {   
            _queue = new T[1];
            _count = 0;
            _head = 0;
            _tail = 0;
        }
        
        // is the queue empty?
        public bool IsEmpty()
        {
            return _count == 0;
        }                

        // return the number of items on the queue
        public int Size()
        {
            return _count;
        }

        // add the item
        public void Enqueue(T item)
        {
            if (_count == _queue.Length) Resize(_queue.Length * 2);
            _tail = ++_tail % _queue.Length;
            _queue[_tail] = item;            
            _count++;
        }

        private void Resize(int newSize)
        {
            var newQueue = new T[newSize];
            for (int i = 0; i < _count; i++)
            {
                newQueue[i] = _queue[(i + _head) % _queue.Length];
            }
            _queue = newQueue;
            _head = 0;
            _tail = _count - 1;
        }
            
        // delete and return a random item
        public T Dequeue()
        {
            if (IsEmpty()) throw new InvalidOperationException();
            if (_count == _queue.Length / 4) Resize(_queue.Length / 2);
            var randomIndex = GetRandomIndex();
            var randomItem = _queue[randomIndex];
            _queue[randomIndex] = _queue[_head];
            _queue[_head] = default(T); // avoid loitering
            _head = ++_head % _queue.Length;
            _count--;
            return randomItem;
        }    

        // return (but do not delete) a random item
        public T Sample()
        {
            return _queue[GetRandomIndex()];
        }

        private int GetRandomIndex()
        {
            var randomIndex = (_random.Next(0, _count) + _head) % _queue.Length;
            return randomIndex;
        }

        // return an independent iterator over items in random order
        public IEnumerator<T> GetEnumerator()
        {
            // copy
            var queue = CloneQueue();

            // knuth shuffling
            for (int i = 0; i < _count; i++)
            {
                var randomIndex = _random.Next(i, _count);
                var randomItem = queue[randomIndex];
                queue[randomIndex] = queue[i];    // swap item
                yield return randomItem;
            }
        }

        private T[] CloneQueue()
        {
            var queue = new T[_count];
            for (int i = 0; i < _count; i++)
            {
                queue[i] = _queue[(i + _head) % _queue.Length];
            }
            return queue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
