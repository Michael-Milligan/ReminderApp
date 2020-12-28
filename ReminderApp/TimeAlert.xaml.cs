using NAudio.Wave;
using System;
using System.Linq;
using System.Threading;
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

        #region Loop
            /// <summary>
            /// Stream for looping playback
            /// </summary>
            public class LoopStream : WaveStream
            {
                WaveStream sourceStream;

                /// <summary>
                /// Creates a new Loop stream
                /// </summary>
                /// <param name="sourceStream">The stream to read from. Note: the Read method of this stream should return 0 when it reaches the end
                /// or else we will not loop to the start again.</param>
                public LoopStream(WaveStream sourceStream)
                {
                    this.sourceStream = sourceStream;
                    this.EnableLooping = true;
                }

                /// <summary>
                /// Use this to turn looping on or off
                /// </summary>
                public bool EnableLooping { get; set; }

                /// <summary>
                /// Return source stream's wave format
                /// </summary>
                public override WaveFormat WaveFormat
                {
                    get { return sourceStream.WaveFormat; }
                }

                /// <summary>
                /// LoopStream simply returns
                /// </summary>
                public override long Length
                {
                    get { return sourceStream.Length; }
                }

                /// <summary>
                /// LoopStream simply passes on positioning to source stream
                /// </summary>
                public override long Position
                {
                    get { return sourceStream.Position; }
                    set { sourceStream.Position = value; }
                }

                public override int Read(byte[] buffer, int offset, int count)
                {
                    int totalBytesRead = 0;

                    while (totalBytesRead < count)
                    {
                        int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                        if (bytesRead == 0)
                        {
                            if (sourceStream.Position == 0 || !EnableLooping)
                            {
                                // something wrong with the source stream
                                break;
                            }
                            // loop
                            sourceStream.Position = 0;
                        }
                        totalBytesRead += bytesRead;
                    }
                    return totalBytesRead;
                }
            }

            private WaveOut waveOut;
            LoopStream loop;
            public void MakeAlert()
            {
                if (waveOut == null)
                {
                    Mp3FileReader reader = new Mp3FileReader(@"beep.mp3");
                    loop = new LoopStream(reader);
                    waveOut = new WaveOut();
                    waveOut.Init(loop);
                    waveOut.Play();
                }
                else
                {
                    waveOut.Stop();
                    waveOut.Dispose();
                    waveOut = null;
                }
            }

        #endregion
       
        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            TasksContext Context = new TasksContext();
            Context.CurrentTasks.Remove(ThisTask);
            Context.CompletedTasks.Add(new CompletedTask()
            {
                TaskId = ThisTask.Id,
                CompletionDateTime = DateTime.Now
            });
            Context.SaveChanges();
            Application.Current.Windows[0].Content = new MainWindow().Content;
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
