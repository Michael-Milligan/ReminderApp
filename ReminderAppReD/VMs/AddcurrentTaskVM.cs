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
            AddCurrentTaskView window = Application.Current.Windows.Cast<Window>().First(item => item.Title == "AddCurrentTaskView") as AddCurrentTaskView;
            string TaskName = window.NameTextBox.Text;
            string TaskDate = window.DateTextBox.Text;
            try
            {
                AddCurrentTaskWindowModel.AddNewTask(TaskName, TaskDate);
            }
            catch (FormatException)
            {
                if (MessageBox.Show("Please, enter a valid date and time in valid format", "Error",
                    MessageBoxButton.OKCancel, MessageBoxImage.Error) == MessageBoxResult.Cancel)
                    window.Close();
            }
        });
    }
}
