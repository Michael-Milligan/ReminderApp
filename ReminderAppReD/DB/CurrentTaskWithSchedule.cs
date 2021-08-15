namespace ReminderAppReD.DB
{
    class CurrentTaskWithSchedule
    {
        public readonly CurrentTask task;
        public readonly Schedule schedule;

        public CurrentTaskWithSchedule(CurrentTask task, string scheduleString)
        {
            this.task = task;
            schedule = new Schedule(scheduleString);
        }
    }
}
