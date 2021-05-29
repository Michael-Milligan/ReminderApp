using Prism.Commands;
using ReminderAppReD.Views;
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
        public DelegateCommand SendCommand { get; set; } = new DelegateCommand(() => {  });
        public bool CheckForDateErrors()
        {
            AddCurrentTaskView Window = Application.Current.Windows[1] as AddCurrentTaskView;
            try
            {
                DateTime.Parse(Window.DateTextBox.Text);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }
    }
}
