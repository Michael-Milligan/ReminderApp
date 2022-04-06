using ReminderAppReD.DB;
using ReminderAppReD.Models;
using ReminderAppReD.VMs;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ReminderAppReD
{
    class Methods
    {
        public static void AddScrollViewer(ref Grid grid, int column, int row, int columnSpan = 1, int rowSpan = 1)
        {
            CurrentTasksTabModel model = new();
            _ = grid ?? throw new ArgumentNullException(nameof(grid));

            if (grid.ColumnDefinitions.Count < 4) grid.ColumnDefinitions.Add(new() { Width = new GridLength(15, GridUnitType.Pixel) });

            ScrollViewer scroll = new();
            scroll.MouseEnter += CurrentTasksTabModel.OnMouseEnter;
            scroll.MouseLeave += CurrentTasksTabModel.OnMouseLeave;

            grid.Children.Add(scroll);
            Grid.SetColumn(scroll, column + 1);
            Grid.SetColumnSpan(scroll, columnSpan);
            Grid.SetRow(scroll, row);
            Grid.SetRowSpan(scroll, rowSpan);
        }
    }
}
