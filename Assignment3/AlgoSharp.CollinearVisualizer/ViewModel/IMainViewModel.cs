using System.Windows.Data;
using GalaSoft.MvvmLight.CommandWpf;

namespace AlgoSharp.CollinearVisualizer.ViewModel
{
    public interface IMainViewModel
    {
        /// <summary>
        /// Sets and gets the Status property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        string Status { get; }

        /// <summary>
        /// Sets and gets the FileName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Set the x-scale
        /// </summary>
        int XScale { get; }

        /// <summary>
        /// Set the y-scale
        /// </summary>
        int YScale { get; }

        /// <summary>
        /// Sets and gets the thickness property
        /// </summary>
        int ShapeThickness { get; set; }

        /// <summary>
        /// Sets and gets the shapes property
        /// </summary>
        CompositeCollection Shapes { get; }

        /// <summary>
        /// Gets the LoadFileCommand.
        /// </summary>
        RelayCommand LoadFileCommand { get; }
    }
}