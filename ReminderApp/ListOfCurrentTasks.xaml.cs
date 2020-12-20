using System.Linq;
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
                    Content = Rows[i, 0],
                    VerticalContentAlignment = VerticalAlignment.Center
                };

                Label Task = new Label
                {
                    Content = Rows[i, 1],
                    VerticalContentAlignment = VerticalAlignment.Center
                };

                Label Date_Time = new Label
                {
                    Content = Rows[i, 3],
                    VerticalContentAlignment = VerticalAlignment.Center
                };

                Button RemoveButton = new Button()
                {
                    Content = "Remove"
                };
                RemoveButton.Click += Remove_OnClick;


                grid.Children.Add(Id);
                Grid.SetRow(Id, i + 1);
                Grid.SetColumn(Id, 0);

                grid.Children.Add(Task);
                Grid.SetRow(Task, i + 1);
                Grid.SetColumn(Task, 1);

                grid.Children.Add(Date_Time);
                Grid.SetRow(Date_Time, i + 1);
                Grid.SetColumn(Date_Time, 2);

                grid.Children.Add(RemoveButton);
                Grid.SetRow(RemoveButton, i + 1);
                Grid.SetColumn(RemoveButton, 3);

            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddCurrentTaskWindow AddWindow = new AddCurrentTaskWindow();
            AddWindow.ShowDialog();
        }

        private void Remove_OnClick(object sender, RoutedEventArgs e)
        {
            var grid = ((Content as DockPanel).Children[1] as Grid);
            int Index = grid.Children.IndexOf(sender as Button);
            int RowIndex = (Index - 3) / 4;

            var Context = new TasksContext();
            try
            {
                CurrentTask ToRemove = Context.CurrentTasks.ToList()[RowIndex - 1];
                Context.CurrentTasks.Remove(ToRemove);
            }
            catch (System.Exception)
            {
                MessageBox.Show("Something gone wrong while deleting from DB", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            Context.SaveChanges();

            //TODO: Add ToRemove to CompletedTasks table

            Application.Current.Windows[0].Content = new ListOfCurrentTasks().Content;
            
        }
    }
}
