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

        //public static string[][] GetTasks()
        //{

        //}
    }
}
