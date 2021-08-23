using ReminderAppReD.DB;
using ReminderAppReD.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ReminderAppReD.Models
{
    class MainWindowModel
    {
        public readonly List<CultureInfo> languages = new List<CultureInfo>();
        public static void Exit()
        {
            Application.Current.Shutdown();
        }
        public static void SwitchTo(string name)
        {
            App.language = name switch
            {
                "ru" => new CultureInfo("ru-RU"),
                "en" => new CultureInfo("en-US"),
                _ => throw new Exception("Wrong language")
            };

            var items = ((Application.Current.Windows[0].Content as DockPanel).Children[0] as Menu).Items;
            foreach (var item in items)
            {
                (item as MenuItem).IsChecked = (item as MenuItem).Name == name;
            }
        }
        public static void AddCurrentTask()
        {
            CurrentTasksTabModel.AddCurrentTask();
        }

        public CurrentTaskWithSchedule alertTask { get; private set; }
        public void CheckForTasksTime()
        {
            TasksContext context = new TasksContext();
            CurrentTaskWithSchedule[] tasks = context.CurrentTasks.Select(item => new CurrentTaskWithSchedule(item, item.DateTime)).ToArray();

            try
            {
                alertTask = tasks.First(item => CompareDates(item.schedule.NextEvent(DateTime.Now), DateTime.Now));
                AlertWindow alertWindow = new AlertWindow();
                alertWindow.Fill(alertTask.task.Id, alertTask.schedule.NextEvent(DateTime.Now));
                alertWindow.Show();
                System.Windows.Threading.Dispatcher.Run();
            }
            catch (Exception) { }
            
        }

        /// <summary>
        /// Checks whether the given times are equal in terms of year, month, 
        /// day and hour parameters while giving 10 minutes of space for minutes 
        /// parameter
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        private bool CompareDates(DateTime time1, DateTime time2)
        {
            TimeSpan difference = time1.Subtract(time2);
            return difference.Days == 0 && difference.Hours == 0 && difference.Minutes < 10;

        }
    }
}
