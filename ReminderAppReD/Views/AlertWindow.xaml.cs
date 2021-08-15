using System;
using System.Windows;

namespace ReminderAppReD.Views
{
    /// <summary>
    /// Interaction logic for AlertWindow.xaml
    /// </summary>
    public partial class AlertWindow : Window
    {
        public int taskId;
        public DateTime taskDate;

        public AlertWindow(int taskId, DateTime taskTime)
        {
            InitializeComponent();
            this.taskId = taskId;
            taskDate = taskTime;
        }
    }
}
