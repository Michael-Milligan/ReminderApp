using System;
using System.Windows;

namespace ReminderAppReD.Views
{
    /// <summary>
    /// Interaction logic for AlertWindow.xaml
    /// </summary>
    public partial class AlertWindow : Window
    {
        public AlertWindow()
        {
            InitializeComponent();
            Application.Current.Resources["alertWindow"] = this;
        }
    }
}
