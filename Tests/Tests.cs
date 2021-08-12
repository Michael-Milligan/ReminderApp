using NUnit.Framework;
using ReminderAppReD;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ScheduleEmptyConstructorTest()
        {
            Schedule emptySchedule= new();

            Assert.AreEqual(1000, emptySchedule.milliseconds.Count);
            Assert.AreEqual(60, emptySchedule.seconds.Count);
            Assert.AreEqual(60, emptySchedule.minutes.Count);
            Assert.AreEqual(24, emptySchedule.hours.Count);

            Assert.AreEqual(8, emptySchedule.weekDays.Count);
            Assert.AreEqual(31, emptySchedule.days.Count);
            Assert.AreEqual(12, emptySchedule.months.Count);
            Assert.AreEqual(100, emptySchedule.years.Count);
        }

        [Test]
        public void ScheduleConstructorTest()
        {
            Schedule schedule = new("*.9.*/2 1-5 10:00:00.000");

            Assert.AreEqual(0, schedule.milliseconds[0]);
            Assert.AreEqual(0, schedule.seconds[0]);
            Assert.AreEqual(0, schedule.minutes[0]);
            Assert.AreEqual(10, schedule.hours[0]);

            Assert.AreEqual(5, schedule.weekDays.Count);
            Assert.AreEqual(15, schedule.days.Count);
            Assert.AreEqual(9, schedule.months[0]);
            Assert.AreEqual(100, schedule.years.Count);
        }

        [Test]
        public void ScheduleNearestEventTest()
        {

        }

        [Test]
        public void ScheduleNearestPrevEventTest()
        {

        }

        [Test]
        public void ScheduleNextEventTest()
        {

        }

        public void SchedulePrevEventTest()
        {

        }
    }
}