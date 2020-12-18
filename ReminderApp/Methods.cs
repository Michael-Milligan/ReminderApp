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

        //public static void BuildTasks(ref Grid CurrentGrid)
        //{

        //}

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
                Results[i, 2] = TasksArray[i].Time.ToString();
                Results[i, 3] = TasksArray[i].Date.ToString();
            }
            return Results;
        }
    }
}
