using System;
using System.Windows;
using Microsoft.Win32;

namespace AlgoSharp.PercolationVisualizer.Services
{
    public interface IDialogService
    {
        void ShowException(Exception exception);
        string GetFileName(string filter);
    }

    public class DialogService : IDialogService
    {
        public void ShowException(Exception exception)
        {
            MessageBox.Show(exception.ToString(), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public string GetFileName(string filter)
        {
            var openFileDialog = new OpenFileDialog {Filter = filter};
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }
    }
}