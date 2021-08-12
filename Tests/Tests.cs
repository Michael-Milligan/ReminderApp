using NUnit.Framework;
using ReminderAppReD;
using System;
using System.Collections.Generic;

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
            Schedule schedule = new("*.9.1,2,3-5,10-20/3 1-5 10:00:00.000");

            Assert.AreEqual(0, schedule.milliseconds[0]);
            Assert.AreEqual(0, schedule.seconds[0]);
            Assert.AreEqual(0, schedule.minutes[0]);
            Assert.AreEqual(10, schedule.hours[0]);

            Assert.AreEqual(5, schedule.weekDays.Count);
            Assert.AreEqual(new List<int>(){ 1, 2, 3, 4, 5, 10, 13, 16, 19 }, schedule.days);
            Assert.AreEqual(9, schedule.months[0]);
            Assert.AreEqual(100, schedule.years.Count);
        }

        [Test]
        public void ScheduleNextEventTest()
        {
            Schedule schedule = new("*.9.1,2,3-5,10-20/4 1-5 10:00:00.000");

            Assert.AreEqual(Convert.ToDateTime("10:00:00.0 AM 14.08.2021"), schedule.NextEvent(DateTime.Now));
        }

        [Test]
        public void SchedulePrevEventTest()
        {

        }
    }
}