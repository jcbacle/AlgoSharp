using System.Threading.Tasks;
using System.Windows.Data;
using AlgoSharp.Collinear;
using AlgoSharp.CollinearVisualizer.Model;
using AlgoSharp.Console.Collinear;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Win32;

namespace AlgoSharp.CollinearVisualizer.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        public MainViewModel()
        {
            CanExecuteLoadFileCommand = true;
            Status = "Ready";
            FileName = string.Empty;
            Shapes = new CompositeCollection();
            XScale = 32768;
            YScale = 32768;
            ShapeThickness = 300;
        }

        private async void ExecuteLoadFileCommand()
        {            
            if (!LoadFileCommand.CanExecute(null)) return;
            CanExecuteLoadFileCommand = false;

            var openFileDialog = new OpenFileDialog { Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*" };
            if (openFileDialog.ShowDialog() == false) return;
            FileName = openFileDialog.FileName;

            //Messenger.Default.Send(new ClearMessage());
            Shapes.Clear();

            Status = "Drawing";
            Fast.RaiseDrawPoint += OnRaiseDrawPoint;
            Fast.RaiseDrawLine += OnRaiseDrawLine;
            await Task.Run(() => Fast.Main(new[] { FileName }));
            
            Fast.RaiseDrawPoint -= OnRaiseDrawPoint;
            Fast.RaiseDrawLine -= OnRaiseDrawLine;
            CanExecuteLoadFileCommand = true;
            Status = "Ready";
        }

        void OnRaiseDrawPoint(object sender, DrawPointEventArgs e)
        {
            //Messenger.Default.Send(new DrawPointMessage(e.P));
            DispatcherHelper.CheckBeginInvokeOnUI(() => Shapes.Add(new PointItem(e.P.X, YScale - e.P.Y)));
        }

        void OnRaiseDrawLine(object sender, DrawLineEventArgs e)
        {
            //Messenger.Default.Send(new DrawLineMessage(e.P, e.Q));
            DispatcherHelper.CheckBeginInvokeOnUI(() => Shapes.Add(new LineItem(e.P.X, YScale - e.P.Y, e.Q.X, YScale - e.Q.Y)));
        }

        #region Properties

        private int _shapeThickness;
        public int ShapeThickness
        {
            get { return _shapeThickness; }
            set { Set(() => ShapeThickness, ref _shapeThickness, value); }
        }

        private CompositeCollection _shapes;
        public CompositeCollection Shapes
        {
            get { return _shapes; }
            set { Set(() => Shapes, ref _shapes, value); }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set { Set(() => Status, ref _status, value); }
        }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { Set(() => FileName, ref _fileName, value); }
        }

        private int _xScale;
        public int XScale
        {
            get { return _xScale; }
            set { Set(() => XScale, ref _xScale, value); }
        }

        private int _yScale;
        public int YScale
        {
            get { return _yScale; }
            set { Set(() => YScale, ref _yScale, value); }
        }

        #endregion

        #region Commands

        private RelayCommand _loadFileCommand;
        public RelayCommand LoadFileCommand
        {
            get
            {
                return _loadFileCommand ?? (_loadFileCommand = new RelayCommand(
                    ExecuteLoadFileCommand,
                    () => CanExecuteLoadFileCommand));
            }
        }

        private bool _canExecuteLoadFileCommand;
        private bool CanExecuteLoadFileCommand
        {
            get { return _canExecuteLoadFileCommand; }
            set
            {
                _canExecuteLoadFileCommand = value;
                LoadFileCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

    }
}