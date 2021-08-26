using Prism.Mvvm;
using ReminderAppReD.DB;
using ReminderAppReD.Views;
using ReminderAppReD.VMs;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ReminderAppReD.Models
{
    class AlertWindowModel : BindableBase
    {
        public static CurrentTaskWithSchedule alertTask {  get; set; }

        private string _postponingTime;
        public string postponingTime { get { return _postponingTime; } set { _postponingTime = value;
                RaisePropertyChanged(nameof(_postponingTime));}}

        public static void PostponeAlert(int Id, int minutes)
        {
            CurrentTasksTabModel.PostponeTask(Id, minutes);
            Ok();
        }

        public static void Done(int Id)
        {
            CurrentTasksTabModel.MoveCurrentToCompleted(Id);
            Ok();
        }

        public static void Ok()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => (Application.Current.Windows.Cast<Window>().First(item => item.Title == "AlertWindow") as AlertWindow).Close()));
        }
    }
}
