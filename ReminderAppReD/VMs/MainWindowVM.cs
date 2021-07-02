using Prism.Commands;
using ReminderAppReD.Models;
using System.Windows;
using System.Windows.Controls;

namespace ReminderAppReD.VMs
{
    class MainWindowVM
    {
        public DelegateCommand ExitCommand { get; set; } = new DelegateCommand(() => { MainWindowModel.Exit(); });
        public DelegateCommand<string> SwitchToLanguageCommand { get; set; } = new DelegateCommand<string>((name) => 
        { MainWindowModel.SwitchTo(name); });

        public DelegateCommand AddCurrentTaskCommand { get; set; } = new DelegateCommand(() => { MainWindowModel.AddCurrentTask(); });
    }
}
