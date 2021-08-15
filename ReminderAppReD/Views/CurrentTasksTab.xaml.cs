using ReminderAppReD.DB;
using ReminderAppReD.Models;
using ReminderAppReD.VMs;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace ReminderAppReD.Views
{
    /// <summary>
    /// Interaction logic for CurrentTasksTab.xaml
    /// </summary>
    public partial class CurrentTasksTab : UserControl
    {
        public bool isInitialised = false;
        public CurrentTasksTab()
        {
            InitializeComponent();

            TasksContext Context = new TasksContext();
            CurrentTask[] Tasks = Context.CurrentTasks.ToArray();
            int i = 0;
            for (; i < Tasks.Length; ++i)
            {
                TabGrid.RowDefinitions.Add(new() { Height = System.Windows.GridLength.Auto });

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
                RemoveButton.Content = Application.Current.Resources.MergedDictionaries[0]["CurrentTaskTabRemoveButtonContent"];
                //RemoveButton.Content = "Remove";
                RemoveButton.Command = CurrentTasksTabVM.RemoveCommand;
                RemoveButton.CommandParameter = i;
                TabGrid.Children.Add(RemoveButton);
                Grid.SetColumn(RemoveButton, 2);
                Grid.SetRow(RemoveButton, i + 1);
            }

            if(i != 0) Methods.AddScrollViewer(ref TabGrid, 2, 1, 1, i);
            isInitialised = true;
        }
    }
}
