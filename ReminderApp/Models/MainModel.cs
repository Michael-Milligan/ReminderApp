using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReminderApp.Models
{
    class MainModel: BindableBase
    {
        public static void Fn()
        {
            new MainWindow().Show();
        }

        public static void CheckIfItsTime()
        {
            try
            {
                TasksContext Context = new TasksContext();
                CurrentTask[] CurrentTasks = Context.CurrentTasks.ToArray();
                IEnumerable<DateTime> Times = CurrentTasks.Select(item => item.Date_Time);

                while (Times.Any() == true)
                {
                    DateTime Now = DateTime.Now;
                    foreach (DateTime time in Times)
                    {
                        if (CompareTimes(Now, time))
                        {
                            CurrentTask temp = CurrentTasks.
                                Where(item => CompareTimes(time, item.Date_Time)).First();
                            Application.Current.Dispatcher.Invoke(() => Alert(temp));
                            throw new FormatException();
                        }
                    }
                }
            }
            catch (FormatException e) { }
        }


        public static void Alert(CurrentTask Task)
        {
            TimeAlert Alert = new TimeAlert(Task);
            Application.Current.Windows[0].Content = Alert.Content;
            Alert.MakeAlert();
        }

        private static bool CompareTimes(DateTime First, DateTime Second)
        {
            return First.Year == Second.Year &&
                First.Month == Second.Month &&
                First.Day == Second.Day &&
                First.Hour == Second.Hour &&
                First.Minute == Second.Minute;
        }

        public static void ListOfCurrentTasks_Click()
        {
            Application.Current.Windows[0].Content = new ListOfCurrentTasks().Content;
        }

        public static void CompletedTasks_Click()
        {
            Application.Current.Windows[0].Content = new CompletedTasksWindow().Content;
        }

        public static void Invisible_ModeOn()
        {
            Application.Current.Windows[0].Height = 0;
            Application.Current.Windows[0].Width = 0;
            Application.Current.Windows[0].WindowStyle = WindowStyle.None;
            Application.Current.Windows[0].ShowInTaskbar = false;
            Application.Current.Windows[0].ShowActivated = false;
            Application.Current.Windows[0].WindowState = WindowState.Minimized;
        }
    }
}
