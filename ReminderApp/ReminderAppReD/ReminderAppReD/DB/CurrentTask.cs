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

        public long Id { get; set; }
        public byte[] Task { get; set; }
        public byte[] DateTime { get; set; }

        public virtual ICollection<CompletedTask> CompletedTasks { get; set; }
    }
}
