using ReminderAppReD.DB;
using System.Linq;
using System.Windows.Controls;

namespace ReminderAppReD.Views
{
    /// <summary>
    /// Interaction logic for CurrentTasksTab.xaml
    /// </summary>
    public partial class CurrentTasksTab : UserControl
    {
        public CurrentTasksTab()
        {
            InitializeComponent();

            TasksContext Context = new TasksContext();
            CurrentTask[] Tasks = Context.CurrentTasks.ToArray();
            for (int i = 0; i < Tasks.Length; ++i)
            {
                TabGrid.RowDefinitions.Add(new RowDefinition());

                Label TaskIdLabel = new Label();
                TaskIdLabel.Content = Tasks[i].Id;
                Grid.SetColumn(TaskIdLabel, 0);
                Grid.SetRow(TaskIdLabel, i + 1);

                Label TaskNameLabel = new Label();
                TaskNameLabel.Content = Tasks[i].Task;
                Grid.SetColumn(TaskNameLabel, 1);
                Grid.SetRow(TaskNameLabel, i + 1);

                Label TaskDateTimeLabel = new Label();
                TaskDateTimeLabel.Content = Tasks[i].DateTime.ToString();
                Grid.SetColumn(TaskDateTimeLabel, 2);
                Grid.SetRow(TaskDateTimeLabel, i + 1);
            }
        }
    }
}
