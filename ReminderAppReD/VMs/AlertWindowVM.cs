using Prism.Commands;
using ReminderAppReD.Models;
using ReminderAppReD.Views;
using System;
using System.Linq;
using System.Windows;

namespace ReminderAppReD.VMs
{
    class AlertWindowVM
    {
        static AlertWindow window = Application.Current.Windows.Cast<AlertWindow>().First(item => item.Title == "AlertWindow");
        public static DelegateCommand PostponeCommand { get; set; } = new(() =>
        {
            AlertWindowModel.PostponeAlert(Convert.ToInt32(window.postponeTimeProperty));
        });
        public static DelegateCommand DoneCommand { get; set; } = new(() => { AlertWindowModel.Done(window.taskId); });
    }
}
