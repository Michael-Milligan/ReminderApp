using NAudio.Wave;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for TimeAlert.xaml
    /// </summary>
    public partial class TimeAlert : Window
    {
        protected bool IsClosed { get; set; }
        public CurrentTask ThisTask { get; init; }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }

        public TimeAlert(CurrentTask TaskToDo)
        {
            InitializeComponent();

            Menu ThisMenu = (Content as DockPanel).Children[0] as Menu;
            Methods.MakeMenu(ref ThisMenu);

            ThisTask = TaskToDo;
            TaskDescription.Content = "The time is up: \n" + ThisTask.Task;
        }

        public void MakeAlert()
        {
            Mp3FileReader reader = new Mp3FileReader(@"beep.mp3");
            WaveOut waveOut = new WaveOut(); // or WaveOutEvent()
            waveOut.Init(reader);
            while (!IsClosed)
            {
                waveOut.Play();
            }
            if (IsClosed)
            {

            }
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            TasksContext Context = new TasksContext();
            Context.CurrentTasks.Remove(ThisTask);
            Context.CompletedTasks.Add(new CompletedTask()
            {
                TaskId = ThisTask.Id,
                CompletionDateTime = DateTime.Now
            });
        }

        private void LateButton_Click(object sender, RoutedEventArgs e)
        {
            TasksContext Context = new TasksContext();
            DateTime Time = DateTime.Now;
            Time.AddMinutes(10);
            Context.CurrentTasks.Find(ThisTask).Date_Time = Time;
            Context.SaveChanges();
        }
    }
}
