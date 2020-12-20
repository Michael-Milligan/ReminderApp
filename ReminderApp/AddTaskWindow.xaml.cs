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
    /// Interaction logic for AddCurrentTaskWindow.xaml
    /// </summary>
    public partial class AddCurrentTaskWindow : Window
    {

        public AddCurrentTaskWindow()
        {
            InitializeComponent();
        }

        public string TaskProp { get; set; }
        public DateTime Date_TimeProp { get; set; }

        public void Finish_OnClick(object Sender, RoutedEventArgs Args)
        {
            TaskProp = TaskBox.Text;
            try
            {
                Date_TimeProp = Convert.ToDateTime(Date_TimeBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please, enter valid Date_Time", "Format Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            var Context = new TasksContext();
            Context.CurrentTasks.Add(new CurrentTask()
            {
                Task = TaskProp,
                Date_Time = Date_TimeProp
            });
            Context.SaveChanges();
            Application.Current.Windows[0].Content = new ListOfCurrentTasks().Content;
            Close();
        }
    }
}
