using System.Windows;
using System.Windows.Controls;

namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for ListOfCurrentTasks.xaml
    /// </summary>
    public partial class ListOfCurrentTasks : Window
    {
        public ListOfCurrentTasks()
        {
            InitializeComponent();
            Menu ThisMenu = (Content as DockPanel).Children[0] as Menu;
            Methods.MakeMenu(ref ThisMenu);

            Grid grid = ((Content as DockPanel).Children[1] as Grid);
            string[,] Rows = Methods.GetCurrentTasks();

            for (int i = 0; i < Rows.GetLength(0); ++i)
            {
                grid.RowDefinitions.Add(new RowDefinition());

                Label Id = new Label
                {
                    Content = Rows[i, 0]
                };

                Label Task = new Label
                {
                    Content = Rows[i, 1]
                };

                Label Time = new Label
                {
                    Content = Rows[i, 2]
                };

                Label Date = new Label
                {
                    Content = Rows[i, 3]
                };

                grid.Children.Add(Id);
                Grid.SetRow(Id, i + 1);
                Grid.SetColumn(Id, 0);

                grid.Children.Add(Task);
                Grid.SetRow(Task, i + 1);
                Grid.SetColumn(Task, 1);

                grid.Children.Add(Time);
                Grid.SetRow(Time, i + 1);
                Grid.SetColumn(Time, 2);

                grid.Children.Add(Date);
                Grid.SetRow(Date, i + 1);
                Grid.SetColumn(Date, 3);

            }
        }

        
    }
}
