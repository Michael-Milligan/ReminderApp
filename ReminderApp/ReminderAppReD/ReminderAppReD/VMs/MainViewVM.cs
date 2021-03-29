using Prism.Commands;
using ReminderAppReD.Models;
using System.Windows;
using System.Windows.Controls;

namespace ReminderAppReD.VMs
{
    class MainViewVM
    {
        public DelegateCommand ExitCommand { get; set; } = new DelegateCommand(() => { MainViewModel.Exit(); });
        public DelegateCommand<string> SwitchToLanguageCommand { get; set; } = new DelegateCommand<string>((name) => 
        { MainViewModel.SwitchTo(name); });
    }
}
