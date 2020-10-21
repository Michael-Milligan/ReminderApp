using System;
using System.Collections.Generic;

namespace ReminderApp
{
    public partial class CompletedTask
    {
        public DateTime CompletionDateTime { get; set; }
        public int TaskId { get; set; }

        public virtual CurrentTask Task { get; set; }
    }
}
