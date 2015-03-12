using System;
using System.Collections.Generic;
using AlgoSharp.Algs.PriorityQueue;

namespace AlgoSharp.Puzzle
{
    public class Solver
    {
        private readonly SearchNode _node;

        // find a solution to the initial board (using the A* algorithm)
        public Solver(Board initial)
        {
            var pq = new MinPriorityQueue<SearchNode>();
            _node = new SearchNode(initial, 0, null);
            pq.Insert(_node);

            var pqTwin = new MinPriorityQueue<SearchNode>();            
            var nodeTwin = new SearchNode(initial.Twin(), 0, null);            
            pqTwin.Insert(nodeTwin);

            while (!_node.Board.IsGoal() && !nodeTwin.Board.IsGoal())
            {
                Explore(pq, ref _node);
                Explore(pqTwin, ref nodeTwin);
            }
        }

        private static void Explore(MinPriorityQueue<SearchNode> pq, ref SearchNode node)
        {
            foreach (var neighbor in node.Board.Neighbors())
            {
                if (node.Previous != null && neighbor.Equals(node.Previous.Board)) continue;
                pq.Insert(new SearchNode(neighbor, node.PastMove + 1, node));
            }
            node = pq.DelMin();
        }

        // is the initial board solvable?
        public bool IsSolvable()
        {
            return _node.Board.IsGoal();
        }

        // min number of moves to solve initial board; -1 if unsolvable
        public int Moves()
        {
            if (!IsSolvable()) return -1;
            return _node.PastMove;
        }

        // sequence of boards in a shortest solution; null if unsolvable
        public IEnumerable<Board> Solution()
        {
            if (!IsSolvable()) return null;
            
            var boards = new Stack<Board>();
            var temp = _node;
            while (temp != null)
            {
                boards.Push(temp.Board);
                temp = temp.Previous;
            }

            return boards;
        }
    }

    class SearchNode : IComparable<SearchNode>
    {
        public SearchNode(Board board, int pastMove, SearchNode previous)
        {
            Board = board;
            PastMove = pastMove;
            Previous = previous;
        }

        public Board Board { get; private set; }
        public int PastMove { get; private set; }
        public SearchNode Previous { get; private set; }
        
        public int CompareTo(SearchNode other)
        {
            //return Board.Manhattan().CompareTo(other.Board.Manhattan());
            //return Board.Hamming().CompareTo(other.Board.Hamming());
            return Board.Manhattan() + PastMove - other.Board.Manhattan() - other.PastMove;
        }
    }
}