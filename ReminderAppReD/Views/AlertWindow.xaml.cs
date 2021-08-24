using System;
using System.Windows;

namespace ReminderAppReD.Views
{
    /// <summary>
    /// Interaction logic for AlertWindow.xaml
    /// </summary>
    public partial class AlertWindow : Window
    {
        public int taskId { get; set; }
        public DateTime taskDate { get; set; }
        public string taskName { get; set; }
        public string postponeTimeProperty { get { return postponeTime.Text; }}

        public AlertWindow()
        {
            InitializeComponent();
        }

        public void Fill(int _taskId, DateTime _taskTime, string _taskName)
        {
            taskId = _taskId;
            taskDate = _taskTime;
            taskName = _taskName;
        }
    }
}
