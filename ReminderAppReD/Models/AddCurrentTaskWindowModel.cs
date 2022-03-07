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
            new CurrentTasksTabModel().AddCurrentTask();
        }
	}
}
