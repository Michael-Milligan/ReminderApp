using System;
using ReminderAppReD.DB;

namespace ReminderAppReD.Models
{
    class AddCurrentTaskWindowModel
    {
        public static void AddNewTask(string TaskText, DateTime TaskTime)
        {
            TasksContext context = new TasksContext();
            CurrentTask newTask = new CurrentTask();
            newTask.Task = TaskText;
            newTask.DateTime = TaskTime;
            context.CurrentTasks.Add(newTask);
            context.SaveChanges();
        }
    }
}
