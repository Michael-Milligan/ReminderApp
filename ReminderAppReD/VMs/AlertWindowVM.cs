﻿using Prism.Commands;
using Prism.Mvvm;
using ReminderAppReD.DB;
using ReminderAppReD.Models;
using ReminderAppReD.Views;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace ReminderAppReD.VMs
{
    class AlertWindowVM : BindableBase
    {
        private AlertWindowModel model = new();
        public CurrentTaskWithSchedule alertTask { get; set; }
        public DateTime taskNextTime { get; set; }
        public string postponingTime { get; set; }

        public AlertWindowVM()
        {
            model.PropertyChanged += OnPropertyChanged;
            PostponeCommand = new(() =>
            {
                AlertWindowModel.PostponeAlert(alertTask.task.id, Convert.ToInt32(postponingTime));
            });
            DoneCommand = new(() => { AlertWindowModel.Done(alertTask.task.id); });
            OkCommand = new(() => { AlertWindowModel.Ok(); });
            alertTask = AlertWindowModel.alertTask;
            taskNextTime = alertTask.schedule.NextEvent(DateTime.Now);
        }

        public DelegateCommand PostponeCommand { get; set; }
        public DelegateCommand DoneCommand { get; set; }
        public DelegateCommand OkCommand { get; set; }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch(args.PropertyName)
            {
                case "postponingTime":
                    RaisePropertyChanged(nameof(postponingTime));
                    break;
            }
        }
    }
}
