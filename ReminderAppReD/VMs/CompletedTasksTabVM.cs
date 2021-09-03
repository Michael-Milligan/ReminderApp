using ReminderAppReD.Models;
using ReminderAppReD.DB;
using System.Collections.ObjectModel;

namespace ReminderAppReD.VMs
{
	class CompletedTasksTabVM
	{
		public ObservableCollection<CompletedTask> completedTasks { get; set; }

		public CompletedTasksTabVM()
		{
			completedTasks = CompletedTasksTabModel.completedTasks;
		}
	}
}
