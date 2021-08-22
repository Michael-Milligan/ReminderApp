﻿using Prism.Mvvm;
using Prism.Common;
using ReminderAppReD.DB;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ReminderAppReD.Models
{
    class CurrentTasksTabModel : BindableBase
    {
        private ObservableCollection<CurrentTask> _CurrentTasks { get
            {
                TasksContext Context = new TasksContext();
                return new(Context.CurrentTasks);
            }}
        public ReadOnlyObservableCollection<CurrentTask> CurrentTasks;

        public CurrentTasksTabModel()
        {
            CurrentTasks = new(_CurrentTasks);
        }
        public void RemoveTask(string nameToDelete)
        {
            TasksContext Context = new TasksContext(); 
            Context.CurrentTasks.Remove(Context.CurrentTasks.Where(item => item.Task == nameToDelete).First());
            Context.SaveChanges();
            RaisePropertyChanged(nameof(_CurrentTasks));
        }

        public void OnMouseEnter(object sender, RoutedEventArgs args)
        {
            (sender as ScrollViewer).Opacity = 1;
        }

        public void OnMouseLeave(object sender, RoutedEventArgs args)
        {
            (sender as ScrollViewer).Opacity = 0;
        }

        public void PropertyChangedPublic(string propertyName) => RaisePropertyChanged(propertyName);
    }
}
