using System;
using System.IO;
using System.Linq;

namespace AlgoSharp.Algs.UnionFind
{
    public class QuickFind
    {
        private int _count;
        private readonly int[] _id;

        public QuickFind(int n)
        {
            _count = n;
            _id = new int[n];
            for (int i = 0; i < n; i++)
            {
                _id[i] = i;
            }
        }

        public int Count
        {
            get { return _count; }
        }

        public bool IsConnected(int p, int q)
        {
            return _id[p] == _id[q];
        }

        public void Union(int p, int q)
        {
            if (IsConnected(p, q)) return;

            var idp = _id[p];
            var idq = _id[q];
            for (int i = 0; i < _id.Length; i++)
            {
                if (_id[i] == idp) _id[i] = idq;
            }

            _count = Count - 1;
        }

        public static void Main(string[] args)
        {
            var fileName = args[0];
            QuickFind quickFind = null;
            UnionFindInputReader.Foo(fileName, n => quickFind = new QuickFind(n), (p, q) => quickFind.Union(p, q), () => quickFind.Count);
        }
    }
}
