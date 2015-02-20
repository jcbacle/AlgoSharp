using System.Collections.ObjectModel;
using AlgoSharp.Collision.Service;
using GalaSoft.MvvmLight.CommandWpf;

namespace AlgoSharp.Collision.ViewModel
{
    public interface IMainViewModel
    {
        bool IsRunning { get; set; }
        string Status { get; set; }
        string Message { get; set; }
        string FileName { get; set; }
        int ParticuleCount { get; set; }
        double DrawFrequency { get; set; }
        ObservableCollection<Particule> Particules{ get; set; }
        RelayCommand StartCommand { get; }
        RelayCommand StopCommand { get; }
        RelayCommand GenerateCommand { get; }
        RelayCommand LoadFileCommand { get; }
        double Fps { get; }
    }
}