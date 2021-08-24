using Prism.Commands;
using Prism.Mvvm;
using ReminderAppReD.Models;
using System;

namespace ReminderAppReD.VMs
{
    class AlertWindowVM : BindableBase
    {
        private AlertWindowModel model = new();
        public int taskId => model.taskId;
        public string taskDateTime { get; set; }
        public string postponingTime => model.postponingTime;

        public AlertWindowVM()
        {
            model.PropertyChanged += (s, e) => { RaisePropertyChanged(nameof(postponingTime)); };
            PostponeCommand = new(() =>
            {
                AlertWindowModel.PostponeAlert(Convert.ToInt32(postponingTime));
            });
            DoneCommand = new(() => { AlertWindowModel.Done(model.taskId); });
            taskDateTime = model.taskDateTime;
        }

        public DelegateCommand PostponeCommand { get; set; }
        public DelegateCommand DoneCommand { get; set; }
    }
}
