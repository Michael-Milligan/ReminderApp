using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Threading;
using ReminderAppReD.Models;

namespace ReminderAppReD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 5, 0), DispatcherPriority.Background,new MainWindowModel().CheckForTasksTime, Application.Current.Dispatcher);
            timer.Start();
        }
    }
}
