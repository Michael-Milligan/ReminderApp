using System;
using System.Collections.Generic;

#nullable disable

namespace ReminderAppReD
{
    public partial class CompletedTask
    {
        public int taskId { get; set; }
        public DateTime completionDateTime { get; set; }
        public int id { get; set; }

        public virtual CurrentTask Task { get; set; }
    }
}
