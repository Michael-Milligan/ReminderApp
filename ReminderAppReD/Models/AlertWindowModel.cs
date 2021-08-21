using Prism.Mvvm;

namespace ReminderAppReD.Models
{
    class AlertWindowModel : BindableBase
    {
        public int taskId { get; }
        public string taskDateTime { get; }

        private string _postponingTime;
        public string postponingTime { get { return _postponingTime; } set { _postponingTime = value;
                RaisePropertyChanged(nameof(_postponingTime));}}

        public static void PostponeAlert(int minutes)
        {

        }

        public static void Done(int taskId)
        {
            //TasksContext context = new();
            //CurrentTask task = context.CurrentTasks.Where(item => item.Id == taskId).First();
            //context.CompletedTasks.Add(new() { CompletionDateTime = DateTime.Now, TaskId = task.Id });
            //context.CurrentTasks.Remove(task);
            //context.SaveChanges();
            //TODO: Remake this
        }
    }
}
