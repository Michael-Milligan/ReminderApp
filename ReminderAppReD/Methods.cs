 using ReminderAppReD.DB;
using ReminderAppReD.Views;
using ReminderAppReD.VMs;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ReminderAppReD
{
    class Methods
    {
        public static void RefreshCurrentTasksGrid()
        {
            Grid TabGrid = (Application.Current.Windows[0] as MainWindow).CurrentTasksTab.Content as Grid;
            for (int j = 3; j < TabGrid.Children.Count; ++j)
            {
                TabGrid.Children.RemoveAt(j);
            }
            TabGrid.RowDefinitions.Clear();
            TabGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30, GridUnitType.Pixel) });

            TasksContext Context = new TasksContext();
            CurrentTask[] Tasks = Context.CurrentTasks.ToArray();
            int i = 0;
            for (; i < Tasks.Length; ++i)
            {
                TabGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                Label TaskNameLabel = new Label();
                TaskNameLabel.Content = Tasks[i].Task;
                TabGrid.Children.Add(TaskNameLabel);
                Grid.SetColumn(TaskNameLabel, 0);
                Grid.SetRow(TaskNameLabel, i + 1);

                Label TaskDateTimeLabel = new Label();
                TaskDateTimeLabel.Content = Tasks[i].DateTime.ToString();
                TabGrid.Children.Add(TaskDateTimeLabel);
                Grid.SetColumn(TaskDateTimeLabel, 1);
                Grid.SetRow(TaskDateTimeLabel, i + 1);

                Button RemoveButton = new Button();
                RemoveButton.Content = "Remove";
                RemoveButton.Command = CurrentTasksTabVM.RemoveCommand;
                RemoveButton.CommandParameter = i;
                TabGrid.Children.Add(RemoveButton);
                Grid.SetColumn(RemoveButton, 2);
                Grid.SetRow(RemoveButton, i + 1);
            }

            AddScrollViewer(ref TabGrid, 2, 1, 1, i);
        }

        public static void AddScrollViewer(ref Grid grid, int column, int row, int columnSpan = 1, int rowSpan = 1)
        {
            _ = grid ?? throw new ArgumentNullException(nameof(grid));
            
            ScrollViewer scroll = new();
            scroll.GotFocus += OnFocusGot;
            scroll.LostFocus += OnFocusLost;

            grid.Children.Add(scroll);
            Grid.SetColumn(scroll, column);
            Grid.SetColumnSpan(scroll, columnSpan);
            Grid.SetRow(scroll, row);
            Grid.SetRowSpan(scroll, rowSpan);
        }

        public static void OnFocusGot(object sender, RoutedEventArgs args)
        {
            (sender as ScrollViewer).Visibility = Visibility.Visible; 
        }

        public static void OnFocusLost(object sender, RoutedEventArgs args)
        {
            (sender as ScrollViewer).Visibility = Visibility.Hidden;
        }
    }
}
