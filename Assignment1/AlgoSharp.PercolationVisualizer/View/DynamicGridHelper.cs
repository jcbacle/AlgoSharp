using System.Windows;
using System.Windows.Controls;

namespace AlgoSharp.PercolationVisualizer.View
{
    // See. http://rachel53461.wordpress.com/2011/09/17/wpf-grids-rowcolumn-count-properties/
    public class DynamicGridHelper
    {
        public static readonly DependencyProperty GridSizeProperty =
            DependencyProperty.RegisterAttached("GridSize", typeof (int), typeof (DynamicGridHelper),
                new PropertyMetadata(default(int), OnGridSizeChanged));

        public static int GetGridSize(DependencyObject d)
        {
            return (int) d.GetValue(GridSizeProperty);
        }

        public static void SetGridSize(DependencyObject d, int gridSize)
        {
            d.SetValue(GridSizeProperty, gridSize);
        }

        private static void OnGridSizeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var gridSize = (int)dependencyPropertyChangedEventArgs.NewValue;
            var grid = dependencyObject as Grid;

            if (grid == null || gridSize < default(int)) return;

            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();

            for (int i = 0; i < gridSize; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)});
                grid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(1, GridUnitType.Star)});
            }
        }
    }
}
