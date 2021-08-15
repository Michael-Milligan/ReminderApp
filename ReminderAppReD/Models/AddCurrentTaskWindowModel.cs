using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ReminderAppReD.DB;
using ReminderAppReD.Views;
using ReminderAppReD.VMs;

namespace ReminderAppReD.Models
{
    class AddCurrentTaskWindowModel
    {
        public static void AddNewTask(string TaskText, string TaskTime)
        {
            TasksContext Context = new TasksContext();
            CurrentTask newTask = new CurrentTask();
            newTask.Task = TaskText;
            newTask.DateTime = TaskTime;
            Context.CurrentTasks.Add(newTask);
            Context.SaveChanges();

            Application.Current.Windows.Cast<Window>().Where(item => item.Title == "AddCurrentTaskView").First().Close();

            Methods.RefreshCurrentTasksGrid();
        }
    }
}
