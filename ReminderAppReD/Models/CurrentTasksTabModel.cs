using Prism.Mvvm;
using Prism.Common;
using ReminderAppReD.DB;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ReminderAppReD.Views;

namespace ReminderAppReD.Models
{
    class CurrentTasksTabModel : BindableBase
    {
        public ObservableCollection<CurrentTask> CurrentTasks;

        public CurrentTasksTabModel()
        {
            TasksContext Context = new TasksContext();
            CurrentTasks = new(Context.CurrentTasks);
        }

        public void RemoveTask(string nameToDelete)
        {
            TasksContext Context = new TasksContext();
            Context.CurrentTasks.Remove(Context.CurrentTasks.Where(item => item.Task == nameToDelete).First());
            Context.SaveChanges();

            CurrentTasks.Remove(CurrentTasks.First(item => item.Task == nameToDelete));
            RaisePropertyChanged(nameof(CurrentTasks));
        }

        public static void AddCurrentTask()
        {
            AddCurrentTaskWindow window = new AddCurrentTaskWindow();
            window.Show();

            TasksContext Context = new TasksContext();
            CurrentTask newTask = new CurrentTask();
            newTask.Task = TaskText;
            newTask.DateTime = TaskTime;
            Context.CurrentTasks.Add(newTask);
            Context.SaveChanges();

            window.Close();
        }

        public void OnMouseEnter(object sender, RoutedEventArgs args)
        {
            (sender as ScrollViewer).Opacity = 1;
        }

        public void OnMouseLeave(object sender, RoutedEventArgs args)
        {
            (sender as ScrollViewer).Opacity = 0;
        }
    }
}
