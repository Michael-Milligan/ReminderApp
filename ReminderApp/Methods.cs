using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ReminderApp
{
    public static class Methods
    {
        public static void MakeMenu(ref Menu CurrentMenu)
        {
            MenuItem Exit = new MenuItem() { Header = "Exit" };
            Exit.Click += Exit_Click;
            _ = CurrentMenu.Items.Add(Exit);
        }

        private static void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to quit?",
                "Question", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        public static string[,] GetCurrentTasks()
        {
            TasksContext Context = new TasksContext();
            DbSet<CurrentTask> CurrentTasks = Context.CurrentTasks;
            CurrentTask[] TasksArray = CurrentTasks.ToArray();

            string[,] Results = new string[TasksArray.Length, 4];
            for (int i = 0; i < Results.GetLength(0); ++i)
            {
                Results[i, 0] = TasksArray[i].Id.ToString();
                Results[i, 1] = TasksArray[i].Task.ToString();
                Results[i, 3] = TasksArray[i].Date_Time.ToString();
            }
            return Results;
        }

        public static string[,] GetCompletedTasks()
        {
            TasksContext Context = new TasksContext();
            DbSet<CompletedTask> CompletedTasks = Context.CompletedTasks;
            CompletedTask[] TasksArray = CompletedTasks.ToArray();

            string[,] Results = new string[TasksArray.Length, 2];
            for (int i = 0; i < Results.GetLength(0); ++i)
            {
                Results[i, 0] = TasksArray[i].TaskId.ToString();
                Results[i, 1] = TasksArray[i].CompletionDateTime.ToString();
            }
            return Results;
        }
    }
}
