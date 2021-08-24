using System;
using System.Windows;

namespace ReminderAppReD.Views
{
    /// <summary>
    /// Interaction logic for AlertWindow.xaml
    /// </summary>
    public partial class AlertWindow : Window
    {
        public string postponeTimeProperty { get { return postponeTime.Text; }}

        public AlertWindow()
        {
            InitializeComponent();
        }
    }
}
