namespace ReminderAppReD.DB
{
    class CurrentTaskWithSchedule
    {
        public CurrentTask task { get; }
        public readonly Schedule schedule;

        public CurrentTaskWithSchedule(CurrentTask task, string scheduleString)
        {
            this.task = task;
            schedule = new Schedule(scheduleString);
        }
    }
}
