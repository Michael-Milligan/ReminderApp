using Prism.Commands;
using ReminderAppReD.Models;
using ReminderAppReD.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ReminderAppReD.VMs
{
    class CurrentTasksTabVM
    {
        public static DelegateCommand<object> RemoveCommand { get; set; } = new DelegateCommand<object>((object parameter) =>
        {
            int i = (int)parameter;

            Grid TabGrid = (Application.Current.Windows[0] as MainWindow).CurrentTasksTab.Content as Grid;

            CurrentTasksTabModel.RemoveTask(Convert.ToString((TabGrid.Children[3 * i + 3] as Label).Content));
        });
    }
}
