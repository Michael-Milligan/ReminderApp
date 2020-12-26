﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using NAudio;
using NAudio.Wave;

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

            Thread AlertThread = new Thread(CheckIfItsTime);
            AlertThread.Start();
        }

        public static void CheckIfItsTime()
        {
            try
            {
                TasksContext Context = new TasksContext();
                CurrentTask[] CurrentTasks = Context.CurrentTasks.ToArray();
                IEnumerable<DateTime> Times = CurrentTasks.Select(item => item.Date_Time);

                while (true)
                {
                    foreach (DateTime time in Times)
                    {
                        if (CompareTimes(DateTime.Now, time))
                        {
                            CurrentTask temp = CurrentTasks.
                                Where(item => CompareTimes(time, DateTime.Now)).First();
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
