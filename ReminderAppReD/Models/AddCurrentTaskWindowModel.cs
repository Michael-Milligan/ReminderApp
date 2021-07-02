using System;
using ReminderAppReD.DB;

namespace ReminderAppReD.Models
{
    class AddCurrentTaskWindowModel
    {
        public static void AddNewTask(string TaskText, DateTime TaskTime)
        {
            TasksContext context = new TasksContext();
            context.CurrentTasks.Add(new CurrentTask(TaskText, TaskTime));
            context.SaveChanges();
        }
    }
}
