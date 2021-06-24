using System;
using System.Collections.Generic;

#nullable disable

namespace ReminderAppReD
{
    public partial class CurrentTask
    {
        public CurrentTask(string TaskName, DateTime DateTime)
        {
            CompletedTasks = new HashSet<CompletedTask>();
        }

        public int Id { get; set; }
        public string Task { get; set; }
        public DateTime DateTime { get; set; }

        public virtual ICollection<CompletedTask> CompletedTasks { get; set; }
    }
}
