using ReminderAppReD.DB;
using ReminderAppReD.Views;
using ReminderAppReD.VMs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Diagnostics;

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
            CurrentTasksTabModel.ShowAddCurrentTaskWindow();
        }
        public static CurrentTaskWithSchedule alertTask { get; private set; }

		/// <summary>
		/// Gets every task's nextEvent and compares it's to current time
		/// </summary>
		public static void CheckForTasksTime()
		{
			try
			{
				alertTask = CurrentTasksTabModel.currentTasks.Select(item => new CurrentTaskWithSchedule(item, item.dateTime)) .First(item =>
					CompareDates(item.schedule.NextEvent(DateTime.Now), DateTime.Now));
				AlertWindowModel.alertTask = alertTask;
				AlertWindow alertWindow = new AlertWindow
				{
					Topmost = true
				};
				alertWindow.Show();
				alertWindow.Focus();
				System.Windows.Threading.Dispatcher.Run();
			}
			catch (InvalidOperationException)
			{
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

        /// <summary>
        /// Checks whether the given times are equal in terms of year, month, 
        /// day and hour parameters while giving 1 minute of space for minutes 
        /// parameter
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        private static bool CompareDates(DateTime time1, DateTime time2)
        {
            TimeSpan difference = time1.Subtract(time2);
            return difference.Days == 0 && difference.Hours == 0 && difference.Minutes < 1;

        }
    }
}
