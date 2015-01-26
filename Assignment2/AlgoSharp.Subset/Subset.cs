using System;
using System.Linq;

namespace AlgoSharp.Subset
{
    static class Subset
    {
        static void Main(string[] args)
        {
            var k = int.Parse(args[0]);
            var strings = args.Skip(1).ToArray();

            // knuth shuffling
            var random = new Random();
            for (int i = 0; i < k; i++)
            {
                var randomIndex = random.Next(i, strings.Length);
                Console.WriteLine(strings[randomIndex]);
                strings[randomIndex] = strings[i];
            }
        }
    }
}
