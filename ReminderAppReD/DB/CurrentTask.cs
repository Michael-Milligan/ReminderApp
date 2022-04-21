using System;
using System.Collections.Generic;

#nullable disable

namespace ReminderAppReD
{
    public partial class CurrentTask
    {
        public CurrentTask()
        {
            CompletedTasks = new HashSet<CompletedTask>();
        }

        public int id { get; set; }
        public string task { get; set; }
        public string dateTime { get; set; }

        public virtual ICollection<CompletedTask> CompletedTasks { get; set; }
    }
}
