using System.Windows.Data;
using AlgoSharp.CollinearVisualizer.Model;
using AlgoSharp.CollinearVisualizer.ViewModel;
using GalaSoft.MvvmLight.CommandWpf;

namespace AlgoSharp.CollinearVisualizer.Design
{
    public class DesignMainViewModel : IMainViewModel
    {
        public DesignMainViewModel()
        {
            Status = "Ready";
            FileName = @"c:\path\of\current\file.txt";
            XScale = 1000;
            YScale = 1000;
            ShapeThickness = 30;
            Shapes = new CompositeCollection {new LineItem(250, 750, 750, 250), new PointItem(250 - ShapeThickness / 2, 750 - ShapeThickness / 2)};
        }

        public string Status { get; private set; }
        public string FileName { get; private set; }
        public int XScale { get; private set; }
        public int YScale { get; private set; }
        public int ShapeThickness { get; set; }
        public CompositeCollection Shapes { get; private set; }
        public RelayCommand LoadFileCommand { get; private set; }
    }
}
