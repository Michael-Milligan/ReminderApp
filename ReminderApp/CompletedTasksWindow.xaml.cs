using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for CompletedTasksWindow.xaml
    /// </summary>
    public partial class CompletedTasksWindow : Window
    {
        public CompletedTasksWindow()
        {
            try
            {
                InitializeComponent();

                Menu ThisMenu = (Content as DockPanel).Children[0] as Menu;
                Methods.MakeMenu(ref ThisMenu);

                Grid grid = ((Content as DockPanel).Children[1] as Grid);
                string[,] Rows = Methods.GetCompletedTasks();

                for (int i = 0; i < Rows.GetLength(0); ++i)
                {
                    grid.RowDefinitions.Add(new RowDefinition());

                    Label TaskId = new Label
                    {
                        Content = Rows[i, 0],
                        VerticalContentAlignment = VerticalAlignment.Center
                    };

                    Label CompletionDate_Time = new Label
                    {
                        Content = Rows[i, 1],
                        VerticalContentAlignment = VerticalAlignment.Center
                    };


                    grid.Children.Add(TaskId);
                    Grid.SetRow(TaskId, i + 1);
                    Grid.SetColumn(TaskId, 0);

                    grid.Children.Add(CompletionDate_Time);
                    Grid.SetRow(CompletionDate_Time, i + 1);
                    Grid.SetColumn(CompletionDate_Time, 1);

                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace, 
                    "Error", MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }
    }
}
