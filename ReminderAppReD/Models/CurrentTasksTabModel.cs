using ReminderAppReD.DB;
using ReminderAppReD.Views;
using ReminderAppReD.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ReminderAppReD.Models
{
    class CurrentTasksTabModel
    {
        public static void RemoveTask(string nameToDelete)
        {
            TasksContext Context = new TasksContext();
            Context.CurrentTasks.Remove(Context.CurrentTasks.Where(item => item.Task == nameToDelete).First());
            Context.SaveChanges();

            Methods.RefreshCurrentTasksGrid();
        }

        public static void OnMouseEnter(object sender, RoutedEventArgs args)
        {
            (sender as ScrollViewer).Opacity = 1;
        }

        public static void OnMouseLeave(object sender, RoutedEventArgs args)
        {
            (sender as ScrollViewer).Opacity = 0;
        }
    }
}
