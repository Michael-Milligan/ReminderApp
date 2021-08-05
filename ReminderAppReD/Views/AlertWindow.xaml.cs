using System;
using System.Windows;

namespace ReminderAppReD.Views
{
    /// <summary>
    /// Interaction logic for AlertWindow.xaml
    /// </summary>
    public partial class AlertWindow : Window
    {
        public string taskName;
        public DateTime taskDate;
        public AlertWindow(string taskName, DateTime taskTime)
        {
            InitializeComponent();
            this.taskName = taskName;
            taskDate = taskTime;
        }
    }
}
