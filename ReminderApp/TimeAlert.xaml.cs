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
using NAudio.Wave;

namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for TimeAlert.xaml
    /// </summary>
    public partial class TimeAlert : Window
    {
        protected bool IsClosed { get; set; }
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
    }
}
