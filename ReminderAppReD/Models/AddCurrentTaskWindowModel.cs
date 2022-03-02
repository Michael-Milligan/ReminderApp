using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using ReminderAppReD.DB;
using ReminderAppReD.Views;
using ReminderAppReD.VMs;

namespace ReminderAppReD.Models
{
    class AddCurrentTaskWindowModel
    {
        public void AddTaskToList()
        {
            (((((Application.Current.Windows[0].Content as DockPanel).Children.Cast<UIElement>().ElementAt(1) as TabControl)
                    .Items[0] as TabItem).Content as CurrentTasksTab).Resources["vm"] as CurrentTasksTabVM).model.AddCurrentTask();
        }
	}
}
