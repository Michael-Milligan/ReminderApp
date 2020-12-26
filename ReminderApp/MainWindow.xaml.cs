using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using NAudio;
using NAudio.Wave;

namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            Menu ThisMenu = (Content as DockPanel).Children[0] as Menu;
            Methods.MakeMenu(ref ThisMenu);

            Timer CheckingTimer = new Timer(CheckIfItsTime, null, 0, 1000*5);
            
        }

        public static void CheckIfItsTime(object Sender)
        {
            TasksContext Context = new TasksContext();
            CurrentTask[] CurrentTasks = Context.CurrentTasks.ToArray();
            IEnumerable<DateTime> Times = CurrentTasks.Select(item => item.Date_Time);

            if (Times.Contains(DateTime.Now))
            {
                TimeAlert Alert = new TimeAlert(CurrentTasks.
                    Where(item => item.Date_Time == DateTime.Now).First());
                Alert.Show();
            }
        }

        private void ListOfCurrentTasks_Click(object sender, RoutedEventArgs e)
        {
            Content = new ListOfCurrentTasks().Content;
        }

        private void CompletedTasks_Click(object sender, RoutedEventArgs e)
        {
            Content = new CompletedTasksWindow().Content;
        }

        private void Invisible_ModeOn(object sender, RoutedEventArgs e)
        {
            Height = 0;
            Width = 0;
            WindowStyle = WindowStyle.None;
            ShowInTaskbar = false;
            ShowActivated = false;
            WindowState = WindowState.Minimized;
        }
    }
}
