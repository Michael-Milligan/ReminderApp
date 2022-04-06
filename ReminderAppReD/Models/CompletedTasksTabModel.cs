using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReminderAppReD.DB;

namespace ReminderAppReD.Models
{
	class CompletedTasksTabModel
	{
		public static ObservableCollection<CompletedTask> completedTasks;
		static CompletedTasksTabModel()
		{
			TasksContext context = new();
			completedTasks = new(context.CompletedTasks);
		}
	}
}
