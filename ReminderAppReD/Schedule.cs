using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

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
            Regex date = new(@"^(.+)\.(.+)\.(.+)\s.*\s");
            Regex time = new(@"\s.*\s(.+):(.+):(.+)\.(.+)$");
            Regex weekDay = new(@"\s(.*)\s");
            MatchCollection dateMatches = date.Matches(scheduleString);
            MatchCollection timeMatches = time.Matches(scheduleString);
            MatchCollection weekDayMatches = weekDay.Matches(scheduleString);

            FillList(dateMatches[0].Groups[1].Value, ref years, 
                DateTime.Now.Year, 10);
            FillList(dateMatches[0].Groups[2].Value, ref months, 1, 12);
            FillList(dateMatches[0].Groups[3].Value, ref days, 1, 31);

            FillList(weekDayMatches[0].Groups[1].Value, ref weekDays, 1, 7);

            FillList(timeMatches[0].Groups[1].Value, ref hours, 0, 24);
            FillList(timeMatches[0].Groups[2].Value, ref minutes, 0, 60);
            FillList(timeMatches[0].Groups[3].Value, ref seconds, 0, 60);
            FillList(timeMatches[0].Groups[4].Value, ref milliseconds, 0, 999);

        }

        private void FillList(string data, ref List<int> listToFill, int beginNumber, int defaultCapacity)
        {
            if (data.Contains(','))
            {
                foreach(string part in  data.Split())
                {
                    FillList(part, ref listToFill, beginNumber, defaultCapacity);
                }
            }
            else if (data == "*")
            { 
                if(beginNumber == 0) listToFill.AddRange(Enumerable.Range(beginNumber, defaultCapacity + 1).ToList());
                else listToFill.AddRange(Enumerable.Range(beginNumber, defaultCapacity).ToList());
            }
            else if (data.Contains('*') && data.Contains('/'))
            {
                int increase = Convert.ToInt32(new Regex(@"\*/(\d+)").Match(data).Groups[1].Value);
                for (int i = beginNumber; i < defaultCapacity; i += increase)
                    listToFill.Add(i);
            }
            else if (data.Contains('-'))
            {
                int[] boundaries = data.Split('-').Select(item => Convert.ToInt32(item)).ToArray();
                listToFill.AddRange(Enumerable.Range(boundaries[0], boundaries[1] - boundaries[0] + 1));
            }

            else if (data.Contains('-') && data.Contains('/'))
            {
                Regex regex = new(@"(\d+)-(\d+)/(/d+)");
                MatchCollection matches = regex.Matches(data);
                int[] boundaries = new int[3] { Convert.ToInt32(matches[0].Groups[1].Value), 
                    Convert.ToInt32(matches[0].Groups[2].Value),
                    Convert.ToInt32(matches[0].Groups[3].Value) };

                for (int i = boundaries[0]; i < boundaries[1]; i += boundaries[2])
                    listToFill.Add(i);
            }
            else listToFill.Add(Convert.ToInt32(data));
            listToFill.Sort();
        }

        #region Nearest

        /// <summary>
        /// Возвращает следующий ближайший к заданному времени момент в расписании или
        /// само заданное время, если оно есть в расписании.
        /// </summary>
        /// <param name="t1">Заданное время</param>
        /// <returns>Ближайший момент времени в расписании</returns>
        public DateTime NearestEvent(DateTime t1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает предыдущий ближайший к заданному времени момент в расписании или
        /// само заданное время, если оно есть в расписании.
        /// </summary>
        /// <param name="t1">Заданное время</param>
        /// <returns>Ближайший момент времени в расписании</returns>
        public DateTime NearestPrevEvent(DateTime t1)
        {
            throw new NotImplementedException();
        }
        #endregion

        /// <summary>
        /// Возвращает следующий момент времени в расписании.
        /// </summary>
        /// <param name="t1">Время, от которого нужно отступить</param>
        /// <returns>Следующий момент времени в расписании</returns>
        public DateTime NextEvent(DateTime t1)
        {
            int millisecond = Next(t1.Millisecond, in milliseconds),
                second = Next(t1.Second, in seconds),
                minute = Next(t1.Minute, in minutes),
                hour = Next(t1.Hour, in hours),

                day = Next(t1.Day, in days),
                month = Next(t1.Month, in months),
                year = Next(t1.Year, in years),
                weekDay = (int)(new DateTime(year, month, day).DayOfWeek);

            //Look, I know the complexity here is enormous: O(n^7), but to be honest, most of the events would be in 1 or 2 cycles of the year cycle:
            //we look for the year and other parameters of the DateTime after our, so even if in the whole our year there wouldn't be any events, there will be one in the next
            //one with, I presume, more than 80% probability. Also, there would be small amount of cycling, because, usually users don't want to be notified every millisecond
            //or second, so the complexity is reduced to O(n^5), while presuming there would be at least one event by one-two years the complexity falls to acceptable O(n^4).
            //Finally, let's remember that the possible size of each parameter is limited to double-digit number, which gives us really small amount of cycles and time:
            //60 mins * 24 hours * 31 days(somewhere even less) * 12 months * 10 years ~ 5,356,800 which is not that much;

            for (int _year = 0; _year < years.Last(); _year = Next(_year, in years))
            {
                for (int _month = 0; _month < months.Last(); _month = Next(_month, in months))
                {
                    for (int _day = 0; _day < days.Last(); _day = Next(_day, in days))
                    {
                        if (_day > DateTime.DaysInMonth(_year, _month)) continue;
                        for (int _hour = 0; _hour < hours.Last(); _hour = Next(_hour, in hours))
                        {
                            for (int _minute = 0; _minute < minutes.Last(); _minute = Next(_minute, in minutes))
                            {
                                for (int _second = 0; _second < seconds.Last(); _second = Next(_second, in seconds))
                                {
                                    for (int _millisecond = 0; _millisecond < milliseconds.Last(); _millisecond = Next(_millisecond, in milliseconds))
                                    {
                                        DateTime time = new(_year, _month, _day, _hour, _minute, _second, _millisecond);
                                        if (IsValid(time))
                                        {
                                            return time;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            throw new Exception("Next event couldn't be found");
        }

        private int Next(int data, in List<int> listToSearch)
        {
            int i = 0;
            while (listToSearch[i++] < data) { }
            return listToSearch[i];
        }

        private int Prev(int data, in List<int> listToSearch)
        {
            int i = 0;
            while (listToSearch[i++] < data) { }
            return listToSearch[i - 1];
        }

        private bool IsValid(DateTime time) {
                return milliseconds.Contains(time.Millisecond) && seconds.Contains(time.Second) &&
            minutes.Contains(time.Minute) && hours.Contains(time.Hour) && days.Contains(time.Day) && months.Contains(time.Month) &&
            years.Contains(time.Year) && weekDays.Contains((int)time.DayOfWeek);
        }

        /// <summary>
        /// Возвращает предыдущий момент времени в расписании.
        /// </summary>
        /// <param name="t1">Время, от которого нужно отступить</param>
        /// <returns>Предыдущий момент времени в расписании</returns>
        public DateTime PrevEvent(DateTime t1)
        {
            throw new NotImplementedException();
        }
    }
}
