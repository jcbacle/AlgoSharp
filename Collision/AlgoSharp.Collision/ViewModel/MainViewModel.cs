using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using AlgoSharp.Collision.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;

namespace AlgoSharp.Collision.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private CancellationTokenSource _cancelToken;
        private Stopwatch _stopwatch;
        private double _previousElapsed;

        public MainViewModel()
        {
            Status = "Ready";
            Message = "Generate or Load File";
            DrawFrequency = 1;
            ParticuleCount = 100;
        }

        private async void Start()
        {
            try
            {
                IsRunning = true;
                _cancelToken = new CancellationTokenSource();
                var engine = new CollisionSystem(new List<Particule>(Particules), DrawFrequency);
                engine.DrawEvent += OnRaiseDrawEvent;
                _stopwatch = Stopwatch.StartNew();
                await Task.Run(() => engine.Simulate(_cancelToken.Token));
            }
            catch (Exception ex)
            {
                Status = "Error when running";
                Message = ex.Message;
                Debug.WriteLine(ex);
            }
            finally
            {
                IsRunning = false;
            }
        }

        private void OnRaiseDrawEvent(object sender, DrawEventArgs e)
        {
            Task.Delay(20).Wait();
            Particules = new ObservableCollection<Particule>(e.Particules);
            var currentElapsed = _stopwatch.ElapsedMilliseconds;
            Fps = 1000 / (currentElapsed - _previousElapsed);
            _previousElapsed = currentElapsed;
        }

        private void Stop()
        {
            _cancelToken.Cancel();
        }

        private void LoadFile()
        {
            var openFileDialog = new OpenFileDialog {Filter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*"};
            if (openFileDialog.ShowDialog() != true) return;

            FileName = Path.GetFileName(openFileDialog.FileName);
                
            List<Particule> particules = null;
            foreach (var line in File.ReadLines(openFileDialog.FileName))
            {
                if (particules == null)
                {
                    particules = new List<Particule>(int.Parse(line));
                    continue;
                }

                var args = line.Split(' ');
                var particule = new Particule(double.Parse(args[1]), double.Parse(args[2]), double.Parse(args[3]),
                    double.Parse(args[4]), double.Parse(args[5]), double.Parse(args[6]),
                    Color.FromRgb(byte.Parse(args[7]), byte.Parse(args[8]), byte.Parse(args[9])));
                particules.Add(particule);
            }

            Particules = new ObservableCollection<Particule>(particules);
        }

        private void Generate()
        {
            var particules = new List<Particule>(ParticuleCount);
            for (int i = 0; i < ParticuleCount; i++)
                particules.Add(new Particule());
            Particules = new ObservableCollection<Particule>(particules);
        }

        #region Properties

        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                Set(() => Status, ref _status, value);
            }
        }

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                Set(() => Message, ref _message, value);
            }
        }

        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                Set(() => FileName, ref _fileName, value);
            }
        }

        private int _particuleCount;
        public int ParticuleCount
        {
            get
            {
                return _particuleCount;
            }
            set
            {
                Set(() => ParticuleCount, ref _particuleCount, value);
            }
        }

        private double _drawFrequency;
        public double DrawFrequency
        {
            get
            {
                return _drawFrequency;
            }
            set
            {
                Set(() => DrawFrequency, ref _drawFrequency, value);
            }
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                Set(() => IsRunning, ref _isRunning, value);
                StartCommand.RaiseCanExecuteChanged();
                StopCommand.RaiseCanExecuteChanged();
                GenerateCommand.RaiseCanExecuteChanged();
                LoadFileCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<Particule> _particules;
        public ObservableCollection<Particule> Particules
        {
            get
            {
                return _particules;
            }
            set
            {
                Set(() => Particules, ref _particules, value);
                StartCommand.RaiseCanExecuteChanged();
            }
        }

        private double _fps;
        public double Fps
        {
            get
            {
                return _fps;
            }
            set
            {
                Set(() => Fps, ref _fps, value);
            }
        }

        #endregion

        #region Commands

        private RelayCommand _startCommand;
        public RelayCommand StartCommand
        {
            get
            {
                return _startCommand
                    ?? (_startCommand = new RelayCommand(
                    () =>
                    {
                        if (!StartCommand.CanExecute(null)) return;
                        Start();
                    },
                    () => !IsRunning && Particules != null));
            }
        }


        private RelayCommand _stopCommand;
        public RelayCommand StopCommand
        {
            get
            {
                return _stopCommand
                    ?? (_stopCommand = new RelayCommand(
                    () =>
                    {
                        if (!StopCommand.CanExecute(null)) return;
                        Stop();
                    },
                    () => IsRunning));
            }
        }

        private RelayCommand _generateCommand;
        public RelayCommand GenerateCommand
        {
            get
            {
                return _generateCommand
                    ?? (_generateCommand = new RelayCommand(
                    () =>
                    {
                        if (!GenerateCommand.CanExecute(null)) return;
                        Generate();
                    },
                    () => !IsRunning));
            }
        }

        private RelayCommand _loadFileCommand;
        public RelayCommand LoadFileCommand
        {
            get
            {
                return _loadFileCommand
                    ?? (_loadFileCommand = new RelayCommand(
                    () =>
                    {
                        if (!LoadFileCommand.CanExecute(null)) return;
                        LoadFile();
                    },
                    () => !IsRunning));
            }
        }

        #endregion
    }
}