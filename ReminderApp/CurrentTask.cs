using System;
using System.Collections.Generic;

namespace ReminderApp
{
    public partial class CurrentTask
    {
        public CurrentTask()
        {
            CompletedTasks = new HashSet<CompletedTask>();
        }

        public int Id { get; set; }
        public string Task { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<CompletedTask> CompletedTasks { get; set; }
    }
}
