using Prism.Commands;
using ReminderAppReD.Views;
using ReminderAppReD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReminderAppReD.VMs
{
    class AddcurrentTaskVM
    {
        public DelegateCommand SendCommand { get; set; } = new DelegateCommand(() => 
        { 
            string TaskName = Application.Current.Windows[1].NameTextBox.Content;
            string TaskDate = Application.Current.Windows[1].DateTextBox.Content;
            try
            {
                DateTime time = DateTime.Parse(TaskDate);
                AddCurrentTaskWindowModel.AddNewTask(TaskName, time);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please, enter a valid date and time in valid format", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        });
    }
}
