using Prism.Commands;
using Prism.Mvvm;
using ReminderAppReD.DB;
using ReminderAppReD.Models;
using ReminderAppReD.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ReminderAppReD.VMs
{
    class CurrentTasksTabVM : BindableBase
    {
        readonly CurrentTasksTabModel model = new();
        public static DelegateCommand<object> RemoveCommand;
        public ReadOnlyObservableCollection<CurrentTask> values => model.CurrentTasks;

        public CurrentTasksTabVM()
        {
            RemoveCommand = new DelegateCommand<object>((object parameter) =>
            {
                int i = (int)parameter;

                Grid TabGrid = (Application.Current.Windows[0] as MainWindow).CurrentTasksTab.Content
                as Grid;

                model.RemoveTask(Convert.ToString((TabGrid.Children[3 * i + 2] as Label).
                    Content));
            });

            model.PropertyChanged += new((s, e) => { RaisePropertyChanged(e.PropertyName); });
        }
    }
}
