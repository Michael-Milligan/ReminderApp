using System;
using System.Collections.Generic;

#nullable disable

namespace ReminderAppReD
{
    public partial class CompletedTask
    {
        public long? TaskId { get; set; }
        public byte[] CompletionDateTime { get; set; }
        public long Id { get; set; }

        public virtual CurrentTask Task { get; set; }
    }
}
