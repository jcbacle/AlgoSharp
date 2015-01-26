namespace AlgoSharp.CollinearVisualizer.Model
{
    public class PointItem
    {
        public PointItem(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
    }
}
