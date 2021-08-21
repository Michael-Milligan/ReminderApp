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
        //TODO: move logic to models with MVVM stuff
        //public static void RefreshCurrentTasksGrid()
        //{
        //    Grid TabGrid = (Application.Current.Windows[0] as MainWindow).CurrentTasksTab.Content as Grid;
        //    int count = TabGrid.Children.Count;
        //    for (int j = 2; j < count; ++j)
        //    {
        //        //we will delete item after item after headers until there are only headers left 
        //        TabGrid.Children.RemoveAt(2);
        //    }
        //    TabGrid.RowDefinitions.Clear();
        //    TabGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30, GridUnitType.Pixel) });

        //    TasksContext Context = new TasksContext();
        //    CurrentTask[] Tasks = Context.CurrentTasks.ToArray();
        //    int i = 0;
        //    for (; i < Tasks.Length; ++i)
        //    {
        //        TabGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

        //        Label TaskNameLabel = new Label();
        //        TaskNameLabel.Content = Tasks[i].Task;
        //        TabGrid.Children.Add(TaskNameLabel);
        //        Grid.SetColumn(TaskNameLabel, 0);
        //        Grid.SetRow(TaskNameLabel, i + 1);

        //        Label TaskDateTimeLabel = new Label();
        //        TaskDateTimeLabel.Content = Tasks[i].DateTime.ToString();
        //        TabGrid.Children.Add(TaskDateTimeLabel);
        //        Grid.SetColumn(TaskDateTimeLabel, 1);
        //        Grid.SetRow(TaskDateTimeLabel, i + 1);

        //        Button RemoveButton = new Button();
        //        RemoveButton.Content = Application.Current.Resources.MergedDictionaries[0]["CurrentTaskTabRemoveButtonContent"];
        //        //RemoveButton.Content = "Remove";
        //        RemoveButton.Command = CurrentTasksTabVM.RemoveCommand;
        //        RemoveButton.CommandParameter = i;
        //        TabGrid.Children.Add(RemoveButton);
        //        Grid.SetColumn(RemoveButton, 2);
        //        Grid.SetRow(RemoveButton, i + 1);
        //    }

        //    if (i != 0) AddScrollViewer(ref TabGrid, 2, 1, 1, i);
        //}

        public static void AddScrollViewer(ref Grid grid, int column, int row, int columnSpan = 1, int rowSpan = 1)
        {
            CurrentTasksTabModel model = new();
            _ = grid ?? throw new ArgumentNullException(nameof(grid));

            if (grid.ColumnDefinitions.Count < 4) grid.ColumnDefinitions.Add(new() { Width = new GridLength(15, GridUnitType.Pixel) });

            ScrollViewer scroll = new();
            scroll.MouseEnter += model.OnMouseEnter;
            scroll.MouseLeave += model.OnMouseLeave;

            grid.Children.Add(scroll);
            Grid.SetColumn(scroll, column + 1);
            Grid.SetColumnSpan(scroll, columnSpan);
            Grid.SetRow(scroll, row);
            Grid.SetRowSpan(scroll, rowSpan);
        }
    }
}
