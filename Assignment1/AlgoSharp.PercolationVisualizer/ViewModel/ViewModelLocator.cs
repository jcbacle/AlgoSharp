using System.ComponentModel;
using System.Windows;
using AlgoSharp.PercolationVisualizer.Design;
using AlgoSharp.PercolationVisualizer.Services;

namespace AlgoSharp.PercolationVisualizer.ViewModel
{
    // See MVVM Light Toolkit https://mvvmlight.codeplex.com/
    public class ViewModelLocator
    {
        public IMainViewModel MainViewModel { get; private set; }

        public ViewModelLocator()
        {
            if (IsInDesignMode())
            {
                MainViewModel = new DesignMainViewModel();
            }
            else
            {
                MainViewModel = new MainViewModel(new DialogService(), new PercolationService());
            }
        }

        private static bool IsInDesignMode()
        {
            return (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;
        }
    }
}
