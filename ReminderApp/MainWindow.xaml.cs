using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Menu ThisMenu = (Content as DockPanel).Children[0] as Menu;
            Methods.MakeMenu(ref ThisMenu);

            Thread AlertThread = new Thread(CheckIfItsTime)
            {
                IsBackground = true
            };
            AlertThread.SetApartmentState(ApartmentState.STA);
            AlertThread.Start();

        }

        public static void Fn()
        {
            new Window().Show();
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
            Content = new ListOfCurrentTasks().Content;
        }

        private void CompletedTasks_Click(object sender, RoutedEventArgs e)
        {
            Content = new CompletedTasksWindow().Content;
        }

        private void Invisible_ModeOn(object sender, RoutedEventArgs e)
        {
            Height = 0;
            Width = 0;
            WindowStyle = WindowStyle.None;
            ShowInTaskbar = false;
            ShowActivated = false;
            WindowState = WindowState.Minimized;
        }
    }
}
