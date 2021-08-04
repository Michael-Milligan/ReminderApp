 using ReminderAppReD.DB;
using ReminderAppReD.Views;
using ReminderAppReD.VMs;
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
            for (int i = 3; i < TabGrid.Children.Count; ++i)
            {
                TabGrid.Children.RemoveAt(i);
            }
            TabGrid.RowDefinitions.Clear();
            TabGrid.RowDefinitions.Add(new RowDefinition());

            TasksContext Context = new TasksContext();
            CurrentTask[] Tasks = Context.CurrentTasks.ToArray();
            for (int i = 0; i < Tasks.Length; ++i)
            {
                TabGrid.RowDefinitions.Add(new RowDefinition());

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
        }
    }
}
