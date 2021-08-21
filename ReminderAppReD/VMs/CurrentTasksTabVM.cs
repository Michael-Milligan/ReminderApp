using Prism.Commands;
using Prism.Mvvm;
using ReminderAppReD.DB;
using ReminderAppReD.Models;
using System.Collections.ObjectModel;

namespace ReminderAppReD.VMs
{
    class CurrentTasksTabVM : BindableBase
    {
        readonly CurrentTasksTabModel model = new();
        public DelegateCommand<object> RemoveCommand;
        public ReadOnlyObservableCollection<CurrentTask> values => model.CurrentTasks;

        public CurrentTasksTabVM()
        {
            RemoveCommand = new DelegateCommand<object>((object parameter) =>
            {
                model.RemoveTask(parameter as string);
            });

            model.PropertyChanged += new((s, e) => { RaisePropertyChanged(e.PropertyName); });
        }
    }
}
