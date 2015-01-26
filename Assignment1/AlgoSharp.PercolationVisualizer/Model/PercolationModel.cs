using System.Collections.Generic;

namespace AlgoSharp.PercolationVisualizer.Model
{
    public class PercolationModel
    {
        public List<SiteModel> Sites { get; set; }
        public bool IsPercolated { get; set; }
        public int OpenSites { get; set; }

        public PercolationModel()
        {
            Sites = new List<SiteModel>();
            OpenSites = 0;
            IsPercolated = false;
        }
    }
}