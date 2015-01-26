using System;
using System.IO;
using System.Linq;

namespace AlgoSharp.Algs.UnionFind
{
    public static class UnionFindInputReader
    {
        public static void Foo(string fileName, Action<int> instance, Action<int ,int> union, Func<int> count)
        {
            using (var reader = File.OpenText(fileName))
            {
                var n = Int32.Parse(reader.ReadLine());
                instance(n);

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int[] nodes = line.Split(' ').Select(Int32.Parse).ToArray();
                    union(nodes[0], nodes[1]);
                    Console.WriteLine("{0} {1}", nodes[0], nodes[1]);
                }

                Console.WriteLine("{0} components", count());
            }
        }
    }
}
