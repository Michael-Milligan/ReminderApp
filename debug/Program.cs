using System;
using ReminderAppReD;

namespace debug
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Schedule("*.9.1,2,3-5,10-20/4 1-5 10:00:00.000").NextEvent(DateTime.Now.AddDays(25)));
        }
    }
}
