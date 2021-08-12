using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            years = Enumerable.Range(DateTime.Now.Year, 100).ToList();
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
                DateTime.Now.Year, 100);
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
        }

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

        /// <summary>
        /// Возвращает следующий момент времени в расписании.
        /// </summary>
        /// <param name="t1">Время, от которого нужно отступить</param>
        /// <returns>Следующий момент времени в расписании</returns>
        public DateTime NextEvent(DateTime t1)
        {
            throw new NotImplementedException();
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
