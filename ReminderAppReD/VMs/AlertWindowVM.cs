using Prism.Commands;
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
            PostponeCommand = new((string windowId) =>
            {
                AlertWindowModel.PostponeAlert(alertTask.task.id, Convert.ToInt32(postponingTime), windowId);
            });
            DoneCommand = new((string windowId) => { AlertWindowModel.Done(alertTask.task.id, windowId); });
            OkCommand = new((string windowId) => { AlertWindowModel.Ok(windowId); });
            alertTask = AlertWindowModel.alertTask;
            taskNextTime = alertTask.schedule.NextEvent(DateTime.Now);
        }

        public DelegateCommand<string> PostponeCommand { get; set; }
        public DelegateCommand<string> DoneCommand { get; set; }
        public DelegateCommand<string> OkCommand { get; set; }

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
