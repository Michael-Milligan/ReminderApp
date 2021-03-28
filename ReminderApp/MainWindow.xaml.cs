using ReminderApp.Models;
using ReminderApp.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

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
            var model = new MainModel();
            Menu ThisMenu = (Content as DockPanel).Children[0] as Menu;
            Methods.MakeMenu(ref ThisMenu);

            Thread AlertThread = new Thread(MainModel.CheckIfItsTime)
            {
                IsBackground = true
            };
            AlertThread.SetApartmentState(ApartmentState.STA);
            AlertThread.Start();

        }

        
    }
}
