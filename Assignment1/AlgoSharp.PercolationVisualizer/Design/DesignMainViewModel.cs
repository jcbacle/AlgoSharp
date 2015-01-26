using System.Collections.Generic;
using AlgoSharp.PercolationVisualizer.Helpers;
using AlgoSharp.PercolationVisualizer.Model;
using AlgoSharp.PercolationVisualizer.ViewModel;

namespace AlgoSharp.PercolationVisualizer.Design
{
    public class DesignMainViewModel : IMainViewModel
    {
        public DesignMainViewModel()
        {
            InputFile = @"c:\path\of\current\input\file.txt";
            CurrentLine = 25;
            TotalLine = 100;
            Delay = 1000;
            Status = "Running";
            GridSize = 2;
            PercolationModel = new PercolationModel
                               {
                                   Sites = new List<SiteModel>
                                           {
                                               new SiteModel {Col = 0, Row = 0, IsFull = true, IsOpen = true},
                                               new SiteModel {Col = 1, Row = 0, IsFull = false, IsOpen = false},
                                               new SiteModel {Col = 0, Row = 1, IsFull = false, IsOpen = false},
                                               new SiteModel {Col = 1, Row = 1, IsFull = false, IsOpen = true}
                                           },
                                   IsPercolated = false,
                                   OpenSites = 2
                               };
        }

        public string InputFile { get; set; }
        public int CurrentLine { get; set; }
        public int TotalLine { get; set; }
        public double Delay { get; set; }
        public string Status { get; set; }
        public int GridSize { get; set; }
        public PercolationModel PercolationModel { get; set; }
        public RelayCommand StartCommand { get; private set; }
        public RelayCommand LoadCommand { get; private set; }
    }
}
