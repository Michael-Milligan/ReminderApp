using ReminderAppReD.DB;
using ReminderAppReD.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            new AddCurrentTaskView().Show();
        }

        public CurrentTaskWithSchedule alertTask { get; private set; }
        public void CheckForTasksTime()
        {
            TasksContext context = new TasksContext();
            CurrentTaskWithSchedule[] tasks = context.CurrentTasks.Select(item => new CurrentTaskWithSchedule(item, item.DateTime)).ToArray();

            try
            {
                alertTask = tasks.Where(item => CompareDates(item.schedule.NextEvent(DateTime.Now), DateTime.Now)).First();
                new AlertWindow(alertTask.task.Id, alertTask.schedule.NextEvent(DateTime.Now)).Show();
            }
            catch (Exception e)
            {
                File.AppendAllText("D:\\1.txt", e.Message + "\n" + e.StackTrace);
            }
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
            return time1.Year == time2.Year &&
                time1.Month == time2.Month &&
                time1.Day == time2.Day &&
                time1.Hour == time2.Hour &&
                Math.Abs(time1.Minute - time2.Minute) < 10;

        }
    }
}
