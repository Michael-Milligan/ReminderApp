using Prism.Mvvm;
using ReminderAppReD.DB;
using ReminderAppReD.Views;
using ReminderAppReD.VMs;
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
            (((((Application.Current.Windows[0].Content as DockPanel).Children.Cast<UIElement>().ElementAt(1) as TabControl)
                    .Items[0] as TabItem).Content as CurrentTasksTab).Resources["vm"] as CurrentTasksTabVM).model.PostponeTask(Id, minutes);
            Ok();
        }

        public static void Done(int Id)
        {
            (((((Application.Current.Windows[0].Content as DockPanel).Children.Cast<UIElement>().ElementAt(1) as TabControl)
                    .Items[0] as TabItem).Content as CurrentTasksTab).Resources["vm"] as CurrentTasksTabVM).model.MoveCurrentToCompleted(Id);
            Ok();
        }

        public static void Ok()
        {
            (Application.Current.Windows.Cast<Window>().First(item => item.Title == "AlertWindow") as AlertWindow).Close();
        }
    }
}
