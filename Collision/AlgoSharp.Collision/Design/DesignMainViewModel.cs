using System.Collections.ObjectModel;
using System.Windows.Media;
using AlgoSharp.Collision.Service;
using AlgoSharp.Collision.ViewModel;
using GalaSoft.MvvmLight.CommandWpf;

namespace AlgoSharp.Collision.Design
{
    public class DesignMainViewModel : IMainViewModel
    {
        public DesignMainViewModel()
        {
            IsRunning = false;
            Status = "Status";
            Message = "Message";
            FileName = "Brownian.txt";
            ParticuleCount = 1;
            DrawFrequency = 0.5;
            Particules = new ObservableCollection<Particule>
                         {
                             new Particule(0.9, 0.1, 0, 0, 0.1, 0, Colors.Green),
                             new Particule(0.2, 0.8, 0, 0, 0.2, 0, Colors.Red)
                         };
            Fps = 60.012345;
        }

        public bool IsRunning { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string FileName { get; set; }
        public int ParticuleCount { get; set; }
        public double DrawFrequency { get; set; }
        public ObservableCollection<Particule> Particules { get; set; }
        public RelayCommand StartCommand { get; private set; }
        public RelayCommand StopCommand { get; private set; }
        public RelayCommand GenerateCommand { get; private set; }
        public RelayCommand LoadFileCommand { get; private set; }
        public double Fps { get; private set; }
    }
}