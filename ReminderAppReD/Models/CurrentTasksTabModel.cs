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
        public ObservableCollection<CurrentTask> currentTasks;
        AddCurrentTaskWindow window = new AddCurrentTaskWindow();

        public CurrentTasksTabModel()
        {
            TasksContext Context = new TasksContext();
            currentTasks = new(Context.CurrentTasks);
        }

        public void RemoveTask(string _Id)
        {
            int Id = Convert.ToInt32(_Id);
            TasksContext Context = new TasksContext();
            Context.CurrentTasks.Remove(Context.CurrentTasks.Where(item => item.Id == Id).First());
            Context.SaveChanges();

            currentTasks.Remove(currentTasks.First(item => item.Id == Id));
            RaisePropertyChanged(nameof(currentTasks));
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

            currentTasks.Add(newTask);
            RaisePropertyChanged(nameof(currentTasks));

            window.Close();
        }

        public void MoveCurrentToCompleted(int Id)
        {
            TasksContext context = new();
            CurrentTask task = context.CurrentTasks.First(item => item.Id == Id);
            //CompletedTasksTabModel.AddNew(new() { CompletionDateTime = DateTime.Now, TaskId = task.Id });
            context.CurrentTasks.Remove(task);
            currentTasks.Remove(task);
            context.SaveChanges();
        }

        public void PostponeTask(int Id, int minutes)
        {
            TasksContext context = new();
            CurrentTask task = context.CurrentTasks.First(item => item.Id == Id);
            CurrentTask newTask= new CurrentTask() { Task = task.Task, DateTime = task.DateTime + new DateTime(0, 0, 0, 0, minutes, 0) };
            context.CurrentTasks.Add(newTask);
            currentTasks.Add(newTask);
            context.SaveChanges();
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
