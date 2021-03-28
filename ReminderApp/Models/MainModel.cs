using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReminderApp.Models
{
    class MainModel
    {
        MainWindow window;
        public MainModel(MainWindow window)
        {
            this.window = window;
        }

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

        private void ListOfCurrentTasks_Click(object sender, RoutedEventArgs e)
        {
            window.Content = new ListOfCurrentTasks().Content;
        }

        private void CompletedTasks_Click(object sender, RoutedEventArgs e)
        {
            window.Content = new CompletedTasksWindow().Content;
        }

        private void Invisible_ModeOn(object sender, RoutedEventArgs e)
        {
            window.Height = 0;
            window.Width = 0;
            window.WindowStyle = WindowStyle.None;
            window.ShowInTaskbar = false;
            window.ShowActivated = false;
            window.WindowState = WindowState.Minimized;
        }
    }
}
