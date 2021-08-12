using ReminderAppReD.DB;
using ReminderAppReD.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public int alertTaskId { get; private set; }
        public void CheckForTasksTime(object sender, EventArgs args)
        {
            TasksContext context = new TasksContext();
            CurrentTask[] tasks = context.CurrentTasks.ToArray();

            try
            {
                alertTaskId = tasks.Where(item => item.DateTime < DateTime.Now).First().Id;
            }
            catch (Exception)
            {

            }
        }
    }
}
