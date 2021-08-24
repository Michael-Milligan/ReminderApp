using Prism.Mvvm;
using Prism.Common;
using ReminderAppReD.DB;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ReminderAppReD.Views;
using ReminderAppReD.VMs;
using System;

namespace ReminderAppReD.Models
{
    class CurrentTasksTabModel : BindableBase
    {
        public ObservableCollection<CurrentTask> CurrentTasks;
        AddCurrentTaskWindow window = new AddCurrentTaskWindow();

        public CurrentTasksTabModel()
        {
            TasksContext Context = new TasksContext();
            CurrentTasks = new(Context.CurrentTasks);
        }

        public void RemoveTask(string _Id)
        {
            int Id = Convert.ToInt32(_Id);
            TasksContext Context = new TasksContext();
            Context.CurrentTasks.Remove(Context.CurrentTasks.Where(item => item.Id == Id).First());
            Context.SaveChanges();

            CurrentTasks.Remove(CurrentTasks.First(item => item.Id == Id));
            RaisePropertyChanged(nameof(CurrentTasks));
        }

        public void ShowAddCurrentTaskWindow()
        {
            window.Show();
        }

        public void AddCurrentTask()
        {
            TasksContext Context = new TasksContext();
            CurrentTask newTask = new CurrentTask();
            newTask.Task = window.NameTextBox.Text;
            newTask.DateTime = window.DateTextBox.Text;
            Context.CurrentTasks.Add(newTask);
            Context.SaveChanges();

            CurrentTasks.Add(newTask);
            RaisePropertyChanged(nameof(CurrentTasks));

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
