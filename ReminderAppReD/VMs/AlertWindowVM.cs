using Prism.Commands;
using ReminderAppReD.Models;
using System;
using System.Linq;

namespace ReminderAppReD.VMs
{
    class AlertWindowVM
    {
        public static DelegateCommand<string> PostponeCommand { get; set; } = new((text) =>
        {
            AlertWindowModel.PostponeAlert(ChecknConvert(text));
        });
        public static DelegateCommand<string> DoneCommand { get; set; } = new((taskName) => { AlertWindowModel.Done(taskName); });

        public static int ChecknConvert(string text)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException("text");
            if (!text.All(char.IsDigit)) throw new ArgumentException("There must be an integer number of minutes");
            return Convert.ToInt32(text);
        }
    }
}
