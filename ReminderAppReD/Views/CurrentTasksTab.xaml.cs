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

                Label TaskName = new Label();
                TaskName.Content = Tasks[i].Task;

            }
        }
    }
}
