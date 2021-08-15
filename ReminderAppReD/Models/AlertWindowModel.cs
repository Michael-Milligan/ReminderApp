using ReminderAppReD.DB;
using System;
using System.Linq;

namespace ReminderAppReD.Models
{
    class AlertWindowModel
    {
        public static void PostponeAlert(int minutes)
        {

        }

        public static void Done(int taskId)
        {
            TasksContext context = new();
            CurrentTask task = context.CurrentTasks.Where(item => item.Id == taskId).First();
            context.CompletedTasks.Add(new() { CompletionDateTime = DateTime.Now, TaskId = task.Id });
            context.CurrentTasks.Remove(task);
            context.SaveChanges();
        }
    }
}
