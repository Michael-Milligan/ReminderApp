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
        public readonly AddCurrentTaskWindowModel model = new();
        public DelegateCommand SendCommand { get; set; }

        public AddCurrentTaskVM()
        {
            SendCommand = new DelegateCommand(() =>
            {
                try
                {
                    model.AddTaskToList();
                }
                catch (FormatException)
                {
                    if (MessageBox.Show("Please, enter a valid date and time in valid format", "Error",
                        MessageBoxButton.OKCancel, MessageBoxImage.Error) == MessageBoxResult.Cancel)
                    {
                        
                        AddCurrentTaskWindow window = Application.Current.Windows.Cast<Window>().First(item => item.Title == "AddCurrentTaskView") as AddCurrentTaskWindow;
                        window.Close(); 
                    }
                }
            });
        }
    }
}
