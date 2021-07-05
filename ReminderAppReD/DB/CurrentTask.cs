using System;
using System.Collections.Generic;

#nullable disable

namespace ReminderAppReD.DB
{
    public partial class CurrentTask
    {
        public CurrentTask()
        {
            CompletedTasks = new HashSet<CompletedTask>();
        }

        public int Id { get; set; }
        public string Task { get; set; }
        public DateTime DateTime { get; set; }

        public virtual ICollection<CompletedTask> CompletedTasks { get; set; }
    }
}
