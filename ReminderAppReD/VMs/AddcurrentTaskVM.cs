using Prism.Commands;
using ReminderAppReD.Views;
using ReminderAppReD.Models;
using System;
using System.Windows;

namespace ReminderAppReD.VMs
{
    class AddCurrentTaskVM
    {
        public DelegateCommand SendCommand { get; set; } = new DelegateCommand(() => 
        { 
            string TaskName = (Application.Current.Windows[2] as AddCurrentTaskView).NameTextBox.Text;
            string TaskDate = (Application.Current.Windows[2] as AddCurrentTaskView).DateTextBox.Text;
            try
            {
                DateTime time = DateTime.Parse(TaskDate);
                AddCurrentTaskWindowModel.AddNewTask(TaskName, time);
                Application.Current.Windows[2].Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please, enter a valid date and time in valid format", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        });
    }
}
