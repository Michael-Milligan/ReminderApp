using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReminderAppReD
{
    /// <summary>
    /// Класс для задания и расчета времени по расписанию.
    /// </summary>
    public class Schedule
    {
        public readonly List<int> milliseconds = new();
        public readonly List<int> seconds = new();
        public readonly List<int> minutes = new();
        public readonly List<int> hours = new();

        public readonly List<int> weekDays = new();
        /// <summary>
        /// Days maybe overflown for this month, so correspond it with current month
        /// </summary>
        public readonly List<int> days = new();
        public readonly List<int> months = new();
        public readonly List<int> years = new();
        /// <summary>
        /// Создает пустой экземпляр, который будет соответствовать
        /// расписанию типа "*.*.* * *:*:*.*" (раз в 1 мс).
        /// </summary>
        public Schedule()
        {
            milliseconds = Enumerable.Range(0, 1000).ToList();
            seconds = Enumerable.Range(0, 60).ToList();
            minutes = Enumerable.Range(0, 60).ToList();
            hours = Enumerable.Range(0, 24).ToList();

            days = Enumerable.Range(1, 31).ToList();
            weekDays = Enumerable.Range(0, 8).ToList();
            months = Enumerable.Range(1, 12).ToList();
            years = Enumerable.Range(DateTime.Now.Year, 10).ToList();
        }

        /// <summary>
        /// Создает экземпляр из строки с представлением расписания.
        /// </summary>
        /// <param name="scheduleString">Строка расписания.
        /// Формат строки:
        ///     yyyy.MM.dd w HH:mm:ss.fff
        ///     yyyy.MM.dd HH:mm:ss.fff
        ///     HH:mm:ss.fff
        ///     yyyy.MM.dd w HH:mm:ss
        ///     yyyy.MM.dd HH:mm:ss
        ///     HH:mm:ss
        /// Где yyyy - год (2000-2100)
        ///     MM - месяц (1-12)
        ///     dd - число месяца (1-31 или 32). 32 означает последнее число месяца
        ///     w - день недели (0-6). 0 - воскресенье, 6 - суббота
        ///     HH - часы (0-23)
        ///     mm - минуты (0-59)
        ///     ss - секунды (0-59)
        ///     fff - миллисекунды (0-999). Если не указаны, то 0
        /// Каждую часть даты/времени можно задавать в виде списков и диапазонов.
        /// Например:
        ///     1,2,3-5,10-20/3
        ///     означает список 1,2,3,4,5,10,13,16,19
        /// Дробью задается шаг в списке.
        /// Звездочка означает любое возможное значение.
        /// Например (для часов):
        ///     */4
        ///     означает 0,4,8,12,16,20
        /// Вместо списка чисел месяца можно указать 32. Это означает последнее
        /// число любого месяца.
        /// Пример:
        ///     *.9.*/2 1-5 10:00:00.000
        ///     означает 10:00 во все дни с пн. по пт. по нечетным числам в сентябре
        ///     *:00:00
        ///     означает начало любого часа
        ///     *.*.01 01:30:00
        ///     означает 01:30 по первым числам каждого месяца
        /// </param>
        public Schedule(string scheduleString)
        {
            Regex date = new(@"^(.+)\.(.+)\.([^\s]+)\s.*\s?");
            Regex time = new(@"\s.*\s?([^s]+):(.+):(.+)\.(.+)$");
            Regex weekDay = new(@"\s(.*)\s");
            MatchCollection dateMatches = date.Matches(scheduleString);
            MatchCollection timeMatches = time.Matches(scheduleString);
            MatchCollection weekDayMatches = weekDay.Matches(scheduleString);

            FillList(dateMatches[0].Groups[1].Value, ref years, 
                DateTime.Now.Year, 10);
            FillList(dateMatches[0].Groups[2].Value, ref months, 1, 12);
            FillList(dateMatches[0].Groups[3].Value, ref days, 1, 31);

            if(weekDayMatches.Count != 0) FillList(weekDayMatches[0].Groups[1].Value, ref weekDays, 1, 7);
            else FillList("*", ref weekDays, 1, 7);

            FillList(timeMatches[0].Groups[1].Value, ref hours, 0, 24);
            FillList(timeMatches[0].Groups[2].Value, ref minutes, 0, 60);
            FillList(timeMatches[0].Groups[3].Value, ref seconds, 0, 60);
            FillList(timeMatches[0].Groups[4].Value, ref milliseconds, 0, 999);

        }

        private void FillList(string data, ref List<int> listToFill, int beginNumber, int defaultCapacity)
        {
            object locker = 0;
            if(data == "" || data == null) FillList("*", ref listToFill, beginNumber, defaultCapacity);
            if (data.Contains(','))
            {
                foreach (string part in data.Split(','))
                {
                    FillList(part, ref listToFill, beginNumber, defaultCapacity);
                }
            }
            else if (data == "*")
            {
                if (beginNumber == 0) lock (locker)
                    { listToFill.AddRange(Enumerable.Range(beginNumber, defaultCapacity + 1).ToList()); }
                else lock (locker) { listToFill.AddRange(Enumerable.Range(beginNumber, defaultCapacity).ToList()); }
            }
            else if (data.Contains('*') && data.Contains('/'))
            {
                int increase = Convert.ToInt32(new Regex(@"\*/(\d+)").Match(data).Groups[1].Value);
                for (int i = beginNumber; i < defaultCapacity; i += increase)
                {
                    lock (locker) { listToFill.Add(i); }
                }
            }
            else if (data.Contains('-') && data.Contains('/'))
            {
                Regex regex = new(@"(\d+)-(\d+)/(\d+)");
                MatchCollection matches = regex.Matches(data);
                int[] boundaries = new int[3] { Convert.ToInt32(matches[0].Groups[1].Value),
                    Convert.ToInt32(matches[0].Groups[2].Value),
                    Convert.ToInt32(matches[0].Groups[3].Value) };

                for (int i = boundaries[0]; i < boundaries[1]; i += boundaries[2])
                    lock (locker) { listToFill.Add(i); }
            }
            else if (data.Contains('-'))
            {
                int[] boundaries = data.Trim().Split('-').Select(item => Convert.ToInt32(item)).ToArray();
                lock (locker) { listToFill.AddRange(Enumerable.Range(boundaries[0], boundaries[1] - boundaries[0] + 1)); }
            }
            else lock (locker) { listToFill.Add(Convert.ToInt32(data)); }
            lock (locker)
            {
                listToFill.Sort();
            }
        }

        private class Time
        {
            private Schedule schedule;
            public int millisecond { get; set; }
            int _second;
            int _minute;
            int _hour;

            int _day;
            int _month;
            int _year;

            public int second { get { return _second; } set { _second = value; millisecond = schedule.milliseconds[0]; } }
            public int minute { get { return _minute; } set { _minute = value; second = schedule.seconds[0]; } }
            public int hour { get { return _hour; } set { _hour = value;  minute = schedule.minutes[0]; } }
            public int day { get { return _day; } set { _day = value; hour = schedule.hours[0]; } }
            public int month { get { return _month; } set { _month = value; day = schedule.days[0]; } }
            public int year { get { return _year; } set { _year = value; month = schedule.months[0];} }

            public Time(Schedule schedule)
            {
                millisecond = 0;
                _second = 0;
                _minute = 0;
                _hour = 0;
                _day = 0;
                _month = 0;
                _year = 0;
                this.schedule = schedule;
            }

            public void IncreaseProperty(string propertyName)
            {
                bool overflowBit;
                switch(propertyName)
                {
                    case "millisecond":
                        (millisecond, overflowBit) = schedule.Next(millisecond, in schedule.milliseconds);
                        if (overflowBit) IncreaseProperty("second");
                        break;
                    case "second":
                        (second, overflowBit) = schedule.Next(second, in schedule.seconds);
                        if (overflowBit) IncreaseProperty("minute");
                        break;
                    case "minute":
                        (minute, overflowBit) = schedule.Next(minute, in schedule.minutes);
                        if (overflowBit) IncreaseProperty("hour");
                        break;
                    case "hour":
                        (hour, overflowBit) = schedule.Next(hour, in schedule.hours);
                        if (overflowBit) IncreaseProperty("day");
                        break;
                    case "day":
                        (day, overflowBit) = schedule.Next(day, in schedule.days);
                        if (overflowBit) IncreaseProperty("month");
                        break;
                    case "month":
                        (month, overflowBit) = schedule.Next(month, in schedule.months);
                        if (overflowBit) IncreaseProperty("year");
                        break;
                    case "year":
                        (year, overflowBit) = schedule.Next(year, in schedule.years);
                        if (overflowBit) throw new Exception("Next event couldn't be found");
                        break;
                }
            }
        }

        /// <summary>
        /// Возвращает следующий момент времени в расписании.
        /// </summary>
        /// <param name="t1">Время, от которого нужно отступить</param>
        /// <returns>Следующий момент времени в расписании</returns>
        public DateTime NextEvent(DateTime t1)
        {
            //Look, I know the complexity here is enormous: O(n^7), but:
            //1) for each parameter with count one we reduce the complexity by one degree of n, so complexity is equal to O(n^k), where k - number of parameters with non single parameter,
            //2) the bigger count of the parameters means the greatest possibility of meeting the event,
            //3) the smaller one means, the calculations would be easier by reducing cycle number(maybe even by degree of n)

            //Same logic would be for PrevEvent function

            Time time = new(this);

            time.year = t1.Year;
            time.month = t1.Month;
            time.day = t1.Day;
            time.hour = t1.Hour;
            time.minute = t1.Minute;
            time.second = t1.Second;
            time.millisecond = Next(t1.Millisecond, milliseconds).value;
            
            bool[] overflowBits = new bool[6]; overflowBits.Initialize();

            while (time.year <= years.Last())
            {
                while (time.month <= months.Last())
                {
                    while (time.day <= days.Last())
                    {
                        if (time.day > DateTime.DaysInMonth(time.year, time.month))
                            time.IncreaseProperty("day");
                        if (!weekDays.Contains((int)new DateTime(time.year, time.month, time.day).DayOfWeek))
                            time.IncreaseProperty("day");
                        while (time.hour <= hours.Last())
                        {
                            while (time.minute <= minutes.Last())
                            {
                                while (time.second <= seconds.Last())
                                {
                                    while (time.millisecond <= milliseconds.Last())
                                    {
                                        DateTime result = new(time.year, time.month, time.day, time.hour, time.minute, time.second, time.millisecond);
                                        if (IsValid(result))
                                        {
                                            return result;
                                        }
                                        time.IncreaseProperty("millisecond");
                                    }
                                    time.IncreaseProperty("second");
                                }
                                time.IncreaseProperty("minute");
                            }
                            time.IncreaseProperty("hour");
                        }
                        time.IncreaseProperty("day");
                    }
                    time.IncreaseProperty("month");
                }
                time.IncreaseProperty("year");
            }
            throw new Exception("Next event couldn't be found");
        }

        private (int value, bool hasOverflown) Next(int data, in List<int> listToSearch)
        {
            int i = 0;
            try
            {
                while (listToSearch[i] < data) { ++i; }
                return (listToSearch[i], false);

            }
            catch (ArgumentOutOfRangeException)
            {

                return (listToSearch[0], true);
            }
        }

        private bool IsValid(DateTime time) {
                return milliseconds.Contains(time.Millisecond) && seconds.Contains(time.Second) &&
            minutes.Contains(time.Minute) && hours.Contains(time.Hour) && days.Contains(time.Day) && months.Contains(time.Month) &&
            years.Contains(time.Year) && weekDays.Contains((int)time.DayOfWeek);
        }
    }
}
