using System;
using System.Collections.Generic;

#nullable disable

namespace ReminderAppReD.DB
{
    public partial class CurrentTask
    {
        public CurrentTask()
        {
            completedTasks = new HashSet<CompletedTask>();
        }

        public int id { get; set; }
        public string task { get; set; }
        public string dateTime { get; set; }

        public virtual ICollection<CompletedTask> completedTasks { get; set; }
    }
}
