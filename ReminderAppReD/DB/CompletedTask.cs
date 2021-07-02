using System;
using System.Collections.Generic;

#nullable disable

namespace ReminderAppReD.DB
{
    public partial class CompletedTask
    {
        public int TaskId { get; set; }
        public DateTime CompletionDateTime { get; set; }
        public int Id { get; set; }

        public virtual CurrentTask Task { get; set; }
    }
}
