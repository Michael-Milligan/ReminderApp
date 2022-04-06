using Prism.Mvvm;
using ReminderAppReD.DB;
using ReminderAppReD.Views;
using ReminderAppReD.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ReminderAppReD.Models
{
    class AlertWindowModel : BindableBase
    {
        /// <summary>
        /// The value will be overwritten by next task, but we use it only on creation of windows, so wouldn't hurt
        /// </summary>
        public static CurrentTaskWithSchedule alertTask {  get; set; }

        private string _postponingTime;
        public string postponingTime 
        {
	        get => _postponingTime;
	        set 
	        { 
		        _postponingTime = value;
                RaisePropertyChanged(nameof(_postponingTime));
	        }
        }

        public static void PostponeAlert(int Id, int minutes, string windowId)
        {
            CurrentTasksTabModel.PostponeTask(Id, minutes);
            Ok(windowId);
        }

        public static void Done(int Id, string windowId)
        {
            CurrentTasksTabModel.MoveCurrentToCompleted(Id);
            Ok(windowId);
        }

        public static void Ok(string windowId)
        {
	        Application.Current.Windows.Cast<Window>().OfType<AlertWindow>().First(item => item.Id == windowId).Close();
        }
    }
}
