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
        public DelegateCommand<object> RemoveCommand { get; set; }
        public ReadOnlyObservableCollection<CurrentTask> values {get; set;}

        public CurrentTasksTabVM()
        {
            RemoveCommand = new DelegateCommand<object>((object parameter) =>
            {
                model.RemoveTask(parameter as string);
            });

            model.PropertyChanged += new((s, e) => { RaisePropertyChanged(e.PropertyName); });
            values = model.CurrentTasks;
        }
    }
}
