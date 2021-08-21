using ReminderAppReD.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace ReminderAppReD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        DispatcherTimer timer;

        public App()
        {
            MainWindowModel model= new MainWindowModel();
            model.languages.Clear();
            model.languages.Add(new CultureInfo("en-US"));
            model.languages.Add(new CultureInfo("ru-RU"));

            language = ReminderAppReD.Properties.Settings.Default.defaultLanguage;

            timer = new DispatcherTimer(new TimeSpan(0, 5, 0), DispatcherPriority.Background,
                (sender, args) =>
                {
                    Thread thread = new(new MainWindowModel().CheckForTasksTime);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.IsBackground = true;
                    thread.Start();
                }, Current.Dispatcher);
            timer.Start();
        }

        public static CultureInfo language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                _ = value ?? throw new ArgumentNullException(nameof(value));
                //if (value.Name == language.Name) return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dictionary = new ResourceDictionary();
                dictionary.Source = new Uri($@"Resources\lang.{value.Name}.xaml", UriKind.Relative);
                

                ResourceDictionary oldDictionary = Application.Current.Resources.MergedDictionaries.FirstOrDefault();
                if (oldDictionary != null)
                {
                    Application.Current.Resources.MergedDictionaries.Remove(oldDictionary);
                }
                Application.Current.Resources.MergedDictionaries.Add(dictionary);
                ReminderAppReD.Properties.Settings.Default.defaultLanguage = language;
                ReminderAppReD.Properties.Settings.Default.Save();

                //if (Application.Current.Windows.Count != 0)
                //{
                //    var CurrentTasksTab = (Application.Current.Windows[0] as MainWindow).CurrentTasksTab;
                //    if (CurrentTasksTab.isInitialised != false) Methods.RefreshCurrentTasksGrid();
                //}
            }
        }
    }
}
