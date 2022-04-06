using System;
using System.Collections.Generic;

namespace ReminderAppReD.DB
{
    public partial class CurrentTask
    {
        public CurrentTask()
        {
            CompletedTasks = new HashSet<CompletedTask>();
        }

        public long id { get; set; }
        public string task { get; set; }
        public string dateTime { get; set; }

        public virtual ICollection<CompletedTask> CompletedTasks { get; set; }
    }
}
