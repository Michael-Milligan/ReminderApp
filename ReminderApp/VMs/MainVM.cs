using Prism.Commands;
using ReminderApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.VMs
{
    class MainVM
    {
        public DelegateCommand SwitchToCurrentTasks { get; set; } = new DelegateCommand(() => { MainModel.ListOfCurrentTasks_Click(); });
        public DelegateCommand SwitchToCompletedTasks { get; set; } = new DelegateCommand(() => { MainModel.CompletedTasks_Click(); });
        public DelegateCommand SwitchToInvisibleMode { get; set; } = new DelegateCommand(() => { MainModel.Invisible_ModeOn(); });
    }
}
