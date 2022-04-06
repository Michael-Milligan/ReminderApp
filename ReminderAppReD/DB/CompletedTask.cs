using System;
using System.Collections.Generic;

namespace ReminderAppReD.DB
{
    public partial class CompletedTask
    {
        public long? taskId { get; set; }
        public string completionDateTime { get; set; }
        public long id { get; set; }

        public virtual CurrentTask Task { get; set; }
    }
}
