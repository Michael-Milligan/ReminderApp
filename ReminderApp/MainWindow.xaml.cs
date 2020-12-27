using System;
using System.Collections.Generic;
using System.Linq;
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

            Thread AThread = new Thread(Fn)
            {
                IsBackground = true
            };
            AThread.SetApartmentState(ApartmentState.STA);
            AThread.Start();
        }

        public static void Fn()
        {
            new Window().Show();
        }

        public static void CheckIfItsTime()
        {
            try
            {
                DateTime Now = DateTime.Now;
                TasksContext Context = new TasksContext();
                CurrentTask[] CurrentTasks = Context.CurrentTasks.ToArray();
                IEnumerable<DateTime> Times = CurrentTasks.Select(item => item.Date_Time);

                while (true)
                {
                    foreach (DateTime time in Times)
                    {
                        if (CompareTimes(Now, time))
                        {
                            CurrentTask temp = CurrentTasks.
                                Where(item => CompareTimes(time, Now)).First();
                            Alert(temp);
                            throw new FormatException();
                        }
                    }
                }
            }
            catch (FormatException e) { }
        }

        private static void Alert(CurrentTask Task)
        {
            TimeAlert Alert = new TimeAlert(Task);
            Alert.Show();
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
