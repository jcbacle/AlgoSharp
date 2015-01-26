namespace AlgoSharp.Algs.UnionFind
{
    public class WeightedQuickUnion
    {
        private int _count;
        private readonly int[] _id;
        private readonly int[] _sz;

        public WeightedQuickUnion(int n)
        {
            _count = n;
            _id = new int[n];
            _sz = new int[n];
            for (int i = 0; i < n; i++)
            {
                _id[i] = i;
                _sz[i] = 1;
            }
        }

        public int Count
        {
            get { return _count; }
        }

        public bool IsConnected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        public int Find(int p)
        {
            while (_id[p] != p) p = _id[p];
            return p;
        }

        public void Union(int p, int q)
        {
            var pRoot = Find(p);
            var qRoot = Find(q);

            if (pRoot == qRoot) return;

            var pSize = _sz[pRoot];
            var qSize = _sz[qRoot];

            if (pSize > qSize)
            {
                _id[qRoot] = _id[pRoot];
                _sz[pRoot] += qSize;
            }
            else
            {
                _id[pRoot] = _id[qRoot];
                _sz[qRoot] += pSize;
            }

            _count = Count - 1;
        }

        public static void Main(string[] args)
        {
            var fileName = args[0];
            WeightedQuickUnion wqu = null;
            UnionFindInputReader.Foo(fileName, n => wqu = new WeightedQuickUnion(n), (p, q) => wqu.Union(p, q), () => wqu.Count);
        }
    }
}
