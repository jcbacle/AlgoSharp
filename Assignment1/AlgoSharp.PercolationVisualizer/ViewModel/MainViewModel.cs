using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AlgoSharp.PercolationVisualizer.Helpers;
using AlgoSharp.PercolationVisualizer.Model;
using AlgoSharp.PercolationVisualizer.Services;

namespace AlgoSharp.PercolationVisualizer.ViewModel
{
    public interface IMainViewModel
    {
        string InputFile { get; set; }
        int CurrentLine { get; set; }
        int TotalLine { get; set; }
        double Delay { get; set; }
        string Status { get; set; }
        int GridSize { get; set; }
        PercolationModel PercolationModel { get; set; }
        RelayCommand LoadCommand { get; }
        RelayCommand StartCommand { get; }
    }

    public class MainViewModel : INotifyPropertyChanged, IMainViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly IPercolationService _percolationService;
        private const string Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        private const int DefaultDelay = 250;

        public MainViewModel(IDialogService dialogService, IPercolationService percolationService)
        {
            _dialogService = dialogService;
            _percolationService = percolationService;
            Delay = DefaultDelay;
            LoadCommand = new RelayCommand(Load);
            StartCommand = new RelayCommand(Start, false);
        }

        private void Load()
        {
            InputFile = _dialogService.GetFileName(Filter);
            if (InputFile == null) return;

            GridSize = int.Parse(File.ReadLines(InputFile).First());
            TotalLine = File.ReadLines(InputFile).Count() - 1;
            CurrentLine = 0;
            PercolationModel = new PercolationModel();

            StartCommand.SetCanExecute(true);
            Status = "Input file loaded";
        }

        private async void Start()
        {
            try
            {
                Status = "Running";
                CurrentLine = 0;
                StartCommand.SetCanExecute(false);
                _percolationService.Init(GridSize);
                await Read();
            }
            catch (Exception ex)
            {
                _dialogService.ShowException(ex);
            }
            finally
            {
                StartCommand.SetCanExecute(true);
                Status = "Finished";
            }
        }

        private async Task Read()
        {
            foreach (
                var union in
                    File.ReadLines(InputFile)
                        .Skip(1)
                        .Select(l => new {i = int.Parse(l.Substring(0, 2)), j = int.Parse(l.Substring(3, 2))})
                )
            {
                PercolationModel = _percolationService.Open(union.i, union.j);
                CurrentLine++;
                await Task.Delay((int) Delay);
            }
        }

        #region Commands

        public RelayCommand StartCommand { get; private set; }
        public RelayCommand LoadCommand { get; private set; }

        #endregion

        #region Properties


        private string _inputFile;
        public string InputFile
        {
            get { return _inputFile; }
            set
            {
                _inputFile = value;
                OnPropertyChanged();
            }
        }

        private int _currentLine;
        public int CurrentLine
        {
            get { return _currentLine; }
            set
            {
                _currentLine = value;
                OnPropertyChanged();
            }
        }

        private int _totalLine;
        public int TotalLine
        {
            get { return _totalLine; }
            set
            {
                _totalLine = value;
                OnPropertyChanged();
            }
        }

        private double _delay;
        public double Delay
        {
            get { return _delay; }
            set
            {
                _delay = value;
                OnPropertyChanged();
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        private int _gridSize;
        public int GridSize
        {
            get { return _gridSize; }
            set
            {
                _gridSize = value;
                OnPropertyChanged();
            }
        }

        private PercolationModel _percolationModel;
        public PercolationModel PercolationModel
        {
            get { return _percolationModel; }
            set
            {
                _percolationModel = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
