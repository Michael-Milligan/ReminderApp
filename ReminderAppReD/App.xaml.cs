using ReminderAppReD.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ReminderAppReD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MainWindowModel model= new MainWindowModel();
            model.languages.Clear();
            model.languages.Add(new CultureInfo("en-US"));
            model.languages.Add(new CultureInfo("ru-RU"));

            language = ReminderAppReD.Properties.Settings.Default.defaultLanguage;
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

                if (Application.Current.Windows.Count != 0)
                {
                    var CurrentTasksTab = (Application.Current.Windows[0] as MainWindow).CurrentTasksTab;
                    if (CurrentTasksTab.isInitialised != false) Methods.RefreshCurrentTasksGrid();
                }
            }
        }
    }
}
