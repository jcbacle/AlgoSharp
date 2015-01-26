namespace AlgoSharp.PercolationVisualizer.Model
{
    public class SiteModel
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsOpen { get; set; }
        public bool IsFull { get; set; }
    }
}