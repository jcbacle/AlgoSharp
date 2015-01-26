using System;
using System.Windows.Input;

namespace AlgoSharp.PercolationVisualizer.Helpers
{
    // See MVVM Light Toolkit https://mvvmlight.codeplex.com/
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private bool _canExecute;

        public RelayCommand(Action<T> execute, bool canExecute)
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
            _execute((T) parameter);
        }

        public event EventHandler CanExecuteChanged;

        private void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
