﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReminderApp
{
    public partial class CurrentTask
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Task { get; set; }
        public DateTime Date_Time { get; set; }

        public virtual ICollection<CompletedTask> CompletedTasks { get; set; }
    }
}
