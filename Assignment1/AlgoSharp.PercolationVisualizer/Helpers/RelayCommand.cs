using System;
using System.Windows.Input;

namespace AlgoSharp.PercolationVisualizer.Helpers
{
    // See MVVM Light Toolkit https://mvvmlight.codeplex.com/
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private bool _canExecute;

        public RelayCommand(Action execute,  bool canExecute = true)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public void SetCanExecute(bool canExecute)
        {
            _canExecute = canExecute;
            OnCanExecuteChanged();
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged;

        private void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
