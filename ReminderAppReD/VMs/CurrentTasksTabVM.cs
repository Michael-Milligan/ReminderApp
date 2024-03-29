﻿using Prism.Commands;
using Prism.Mvvm;
using ReminderAppReD.DB;
using ReminderAppReD.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ReminderAppReD.VMs
{
    class CurrentTasksTabVM : BindableBase
    {
        public readonly CurrentTasksTabModel model = new();
        public DelegateCommand<object> RemoveCommand { get; set; }
        public ObservableCollection<CurrentTask> CurrentTasks {get; set; }

        public CurrentTasksTabVM()
        {
            RemoveCommand = new DelegateCommand<object>((object parameter) =>
            {
                model.RemoveTask(parameter.ToString());
            });

            model.PropertyChanged += OnPropertyChanged;
            CurrentTasks = CurrentTasksTabModel.currentTasks;
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "CurrentTasks":
                    RaisePropertyChanged(nameof(CurrentTasks));
                    break;
            }
        }
    }
}
