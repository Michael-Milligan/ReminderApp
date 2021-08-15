using Prism.Commands;
using ReminderAppReD.Views;
using ReminderAppReD.Models;
using System;
using System.Linq;
using System.Windows;

namespace ReminderAppReD.VMs
{
    class AddCurrentTaskVM
    {
        public DelegateCommand SendCommand { get; set; } = new DelegateCommand(() => 
        { 
            string TaskName = (Application.Current.Windows[1] as AddCurrentTaskView).NameTextBox.Text;
            string TaskDate = (Application.Current.Windows[1] as AddCurrentTaskView).DateTextBox.Text;
            try
            {
                AddCurrentTaskWindowModel.AddNewTask(TaskName, TaskDate);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please, enter a valid date and time in valid format", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        });
    }
}
