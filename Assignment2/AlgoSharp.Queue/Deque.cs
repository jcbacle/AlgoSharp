using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoSharp.Queue
{
    public class Deque<T> : IEnumerable<T>
    {
        private Node _head;
        private int _count;

        private class Node
        {
            public T Item { get; set; }
            public Node Prev { get; set; }
            public Node Next { get; set; }
        }

        // construct an empty deque
        public Deque()
        {
            _count = 0;
        }              
        
        // is the deque empty?
        public bool IsEmpty()
        {
            return _count == 0;
        }

        // return the number of items on the deque
        public int Size()
        {
            return _count;
        }

        // insert the item at the front
        public void AddFirst(T item)
        {
            if (_head == null)
            {
                _head = new Node {Item = item};
                _head.Next = _head;
                _head.Prev = _head;
            }
            else
            {
                var oldHead = _head;
                _head = new Node { Item = item, Next = oldHead, Prev = oldHead.Prev };
                oldHead.Prev = _head;                
            }

            _count++;
        }
        
        // insert the item at the end
        public void AddLast(T item)
        {
            if (_head == null)
            {
                _head = new Node { Item = item };
                _head.Next = _head;
                _head.Prev = _head;
            }
            else
            {
                var oldLast = _head.Prev;
                _head.Prev = new Node { Item = item, Prev = oldLast, Next = _head };
                oldLast.Next = _head.Prev;
            }
            _count++;
        }
        
        // delete and return the item at the front
        public T RemoveFirst()
        {
            if (IsEmpty()) throw new InvalidOperationException();
            var oldHead = _head;
            _head = oldHead.Next;
            _head.Prev = oldHead.Prev;
            _count--;
            return oldHead.Item;
        }

        // delete and return the item at the end
        public T RemoveLast()
        {
            if (IsEmpty()) throw new InvalidOperationException();
            var oldLast = _head.Prev;
            _head.Prev = oldLast.Prev;
            _head.Prev.Next = _head;
            _count--;
            return oldLast.Item;
        }

        // return an iterator over items in order from front to end
        public IEnumerator<T> GetEnumerator()
        {
            var current = _head;
            for (int i = 1; i <= _count; i++)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
